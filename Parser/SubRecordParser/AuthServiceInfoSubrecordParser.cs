using System.Collections.Generic;
using Transport.EGTS.Enum;
using Transport.EGTS.Enum.Subrecord;
using Transport.EGTS.Exceptions;
using br = Transport.EGTS.Helpers.ByteCombineHelper;
using Transport.EGTS.Model.Subrecord;

namespace Transport.EGTS.Parser.SubRecordParser
{
	internal class AuthServiceInfoSubrecordParser:ISubrecordParser
	{
		#region Implementation of ISubrecordParser

		/// <summary>
		/// Gets or sets the SRT.
		/// </summary>
		/// <value>
		/// The SRT.
		/// </value>
		public EgtsSubrecordType SRT
		{
			get { return EgtsSubrecordType.ServiceInfo; }
		}

		/// <summary>
		/// Поскольку в документации всё делится на конкретно описанные сервиса
		/// (и в передаче данных тоже есть инфа именно по описанным сервисам, а не отдельным парсером как тут)
		/// надо пометить парсер типом (неизвестно, пригодится ли, но...)
		/// </summary>
		/// <value>
		/// The type of the service.
		/// </value>
		public EgtsServiceType ServiceType
		{
			get { return EgtsServiceType.Authentication; }
		}

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
			var ret = new EgtsAuthServiceInfo();

			byte byt = 0;
			if (!br.ReadNextByteBytes(ref byt, dataArray, ref remainsize, ref offset))
				return null;
			ret.ST = (EgtsServiceType) byt;
			if (!br.ReadNextByteBytes(ref byt, dataArray, ref remainsize, ref offset))
				return null;
			ret.SST = (EgtsServiceState) byt;

			if (!br.ReadNextByteBytes(ref byt, dataArray, ref remainsize, ref offset))
				return null;
			parseBitfields(ret, byt);

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
			var obj = subrecord as EgtsAuthServiceInfo;
			if (obj == null)
				throw new EgtsProceedException(string.Format("Wrong data passed in parser. Expected type {0}, passed type {1}", SRT,
					subrecord.SubrecordType));
			var ret = new List<byte>();

			ret.Add((byte)obj.ST);
			ret.Add((byte)obj.SST);
			ret.Add(byteBitfields(obj));

			return ret.ToArray();
		}

		#endregion

		#region private methods

		private void parseBitfields(EgtsAuthServiceInfo si,byte b)
		{
			si.SRVRP = (EgtsProcessingPriority) (b & 0x03);
			b >>= 7;
			si.SRVA = (b & 0x01) == 1;
		}

		private byte byteBitfields(EgtsAuthServiceInfo si)
		{
			byte ret = 0;
			ret |= (byte) (((byte) si.SRVRP) & 0x03);
			ret |= (byte) ((si.SRVA ? 1 : 0) << 7);
			return ret;
		}

		#endregion private methods
	}
}
