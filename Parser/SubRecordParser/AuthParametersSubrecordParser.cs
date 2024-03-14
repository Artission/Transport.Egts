using System.Collections.Generic;
using System.Text;
using Transport.EGTS.Enum;
using Transport.EGTS.Enum.Subrecord;
using Transport.EGTS.Exceptions;
using br = Transport.EGTS.Helpers.ByteCombineHelper;
using Transport.EGTS.Model;
using Transport.EGTS.Model.Subrecord;

namespace Transport.EGTS.Parser.SubRecordParser
{
	internal class AuthParametersSubrecordParser : ISubrecordParser
	{
		#region Implementation of ISubrecordParser

		/// <summary>
		/// Gets or sets the SubrecordType.
		/// </summary>
		/// <value>
		/// The SubrecordType.
		/// </value>
		public EgtsSubrecordType SRT { get { return EgtsSubrecordType.AuthParams; } }

		/// <summary>
		/// Поскольку в документации всё делится на конкретно описанные сервиса
		/// (и в передаче данных тоже есть инфа именно по описанным сервисам, а не отдельным парсером как тут)
		/// надо пометить парсер типом (неизвестно, пригодится ли, но...)
		/// </summary>
		/// <value>
		/// The type of the service.
		/// </value>
		public EgtsServiceType ServiceType { get { return EgtsServiceType.Authentication; } }

		/// <summary>
		/// Reads the sub record from bytes.
		/// </summary>
		/// <param name="srl">The subrecord data length.</param>
		/// <param name="dataArray">The data array.</param>
		/// <param name="remainsize">The remainsize.</param>
		/// <param name="offset">The offset.</param>
		/// <returns></returns>
		public ISubrecordData ReadSubRecordFromBytes(ushort srl, byte[] dataArray, ref ushort remainsize, ref ushort offset)
		{
			var ret = new EgtsAuthParameters();

			ushort ush = 0;
			byte byt = 0;
			if (!br.ReadNextByteBytes(ref byt, dataArray, ref remainsize, ref offset))
				return null;
			parseHeaderBitFields(ret, byt);

			if (ret.PKE)
			{
				if (!br.ReadNextByteBytes(ref byt, dataArray, ref remainsize, ref offset))
					return null;
				ret.PKL = byt;
				if (ret.PKL==0 || ret.PKL>Constants.MaxPublicKeyLength)
					return null;
				ret.PublicKeyBytes.Clear();
				var pbk = br.ReadRawByteArray(ret.PKL, dataArray, ref remainsize, ref offset);
				if (pbk==null)
					return null;
				ret.PublicKeyBytes.AddRange(pbk);
			}
			// TODO должно проверяться ENA, и как-то дешифровываться, но неясно как. в сырцах на С вообще на это поле забили, хотя если оно 0 то дальше и парсит нечего(по документации)
			/*
			 * if (ret.ENA==0)
			 *	return null;
			 */
			if (ret.ISLE)
			{
				if (!br.ReadNextUshortBytes(ref ush, dataArray, ref remainsize, ref offset))
					return null;
				ret.ISL = ush;
			}

			if (ret.MSE)
			{
				if (!br.ReadNextUshortBytes(ref ush, dataArray, ref remainsize, ref offset))
					return null;
				ret.MSZ = ush;
			}

			if (ret.SSE)
			{
				var str = br.ReadStringToDelimiter(Constants.MaxServerSequenceLength, dataArray, ref remainsize, ref offset);
				if (str==null)
					return null;
				ret.SS = str;
			}

			if (ret.EXE)
			{
				var str = br.ReadStringToDelimiter(Constants.MaxExpressionLength, dataArray, ref remainsize, ref offset);
				if (str == null)
					return null;
				ret.EXP = str;
			}

			return ret;
		}

		/// <summary>
		/// Writes the subrecord to bytes.
		/// SRL заполняется тут же
		/// </summary>
		/// <param name="subrecord">The subrecord.</param>
		/// <returns></returns>
		/// <exception cref="System.NotImplementedException"></exception>
		public byte[] WriteSubrecordToBytes(ISubrecordData subrecord)
		{
			var obj = subrecord as EgtsAuthParameters;
			if (obj == null)
				throw new EgtsProceedException(string.Format("Wrong data passed in parser. Expected type {0}, passed type {1}", SRT,
					subrecord.SubrecordType));
			var ret = new List<byte>();
			var ushSize = sizeof (ushort);

			byte byt = byteHeaderBitFields(obj);
			ret.Add(byt);
			
			if (obj.PKE)
			{
				if (obj.PublicKeyBytes.Count==0)
					throw new EgtsProceedException("AuthParameters. PKE is set to true, but no public key bytes set.");
				obj.PKL = (ushort) obj.PublicKeyBytes.Count;
				ret.AddRange(br.WriteValueToBuffer(obj.PKL, ushSize));
				ret.AddRange(obj.PublicKeyBytes);
			}
			// TODO нужно подумать о ENA И как-то зашифровывать, пока нема
			/*
			 * if (obj.ENA==0)
			 *	return null;
			 */
			if (obj.ISLE)
				ret.AddRange(br.WriteValueToBuffer(obj.ISL, ushSize));

			if (obj.MSE)
				ret.AddRange(br.WriteValueToBuffer(obj.MSZ, ushSize));

			if (obj.SSE)
			{
				if (string.IsNullOrWhiteSpace(obj.SS))
					throw new EgtsProceedException("EgtsParameters. SSE is set to true, but no SS written");
				ret.AddRange(Encoding.ASCII.GetBytes(obj.SS));
				ret.Add(0);
			}

			if (obj.EXE)
			{
				if (string.IsNullOrWhiteSpace(obj.EXP))
					throw new EgtsProceedException("EgtsParameters. EXE is set to true, but no EXP written");
				ret.AddRange(Encoding.ASCII.GetBytes(obj.EXP));
				ret.Add(0);
			}

			return ret.ToArray();
		}

		#endregion

		#region private methods

		private void parseHeaderBitFields(EgtsAuthParameters parm, byte b)
		{
			// TODO ВАЖНО убедиться что ENA именно двухбитовое(как по документации), в сырцах оно 1-битовое
			parm.ENA = (byte)(b & 0x03);
			b >>= 2;
			parm.PKE = (b & 0x01) == 1;
			b >>= 1;
			parm.ISLE = (b & 0x01) == 1;
			b >>= 1;
			parm.MSE = (b & 0x01) == 1;
			b >>= 1;
			parm.SSE = (b & 0x01) == 1;
			b >>= 1;
			parm.EXE = (b & 0x01) == 1;
		}

		private byte byteHeaderBitFields(EgtsAuthParameters parm)
		{
			byte ret = 0;
			
			// TODO ВАЖНО убедиться что ENA именно двухбитовое(как по документации), в сырцах оно 1-битовое
			var offset = 0;
			ret |= (byte)(parm.ENA & 0x03);
			offset += 2;
			ret |= (byte) ((parm.PKE ? 1 : 0) << offset);
			offset++;
			ret |= (byte)((parm.ISLE ? 1 : 0) << offset);
			offset++;
			ret |= (byte)((parm.MSE ? 1 : 0) << offset);
			offset++;
			ret |= (byte)((parm.SSE ? 1 : 0) << offset);
			offset++;
			ret |= (byte)((parm.EXE ? 1 : 0) << offset);

			return ret;
		}

		#endregion private methods
	}
}
