using System.Collections.Generic;
using Transport.EGTS.Enum;
using Transport.EGTS.Enum.Subrecord;
using Transport.EGTS.Exceptions;
using byteReader = Transport.EGTS.Helpers.ByteCombineHelper;
using Transport.EGTS.Model.Subrecord;

namespace Transport.EGTS.Parser.SubRecordParser
{
	internal class CommandDataSubrecordParser:ISubrecordParser
	{
		#region Implementation of ISubrecordParser

		/// <summary>
		/// Gets or sets the SRT.
		/// </summary>
		/// <value>
		/// The SRT.
		/// </value>
		public EgtsSubrecordType SRT { get{return EgtsSubrecordType.CommandData;} }

		/// <summary>
		/// Поскольку в документации всё делится на конкретно описанные сервиса
		/// (и в передаче данных тоже есть инфа именно по описанным сервисам, а не отдельным парсером как тут)
		/// надо пометить парсер типом (неизвестно, пригодится ли, но...)
		/// </summary>
		/// <value>
		/// The type of the service.
		/// </value>
		public EgtsServiceType ServiceType { get { return EgtsServiceType.Commands; } }

		/// <summary>
		/// Reads the sub record from bytes.
		/// </summary>
		/// <param name="srl">The SRL.</param>
		/// <param name="dataArray">The data array.</param>
		/// <param name="remainsize">The remainsize.</param>
		/// <param name="offset">The offset.</param>
		/// <returns></returns>
		public ISubrecordData ReadSubRecordFromBytes(ushort srl, byte[] dataArray, ref ushort remainsize, ref ushort offset)
		{
			var ret = new EgtsCommandData();
			var beginSize = remainsize;

			byte byt = 0;
			uint uin = 0;
			ushort ush = 0;
			if (byteReader.ReadNextByteBytes(ref byt, dataArray, ref remainsize, ref offset))
				parseCommandDataTypesBits(ret, byt);
			else 
				return null;

			
			if (byteReader.ReadNextUintBytes(ref uin, dataArray, ref remainsize, ref offset))
				ret.CID = uin;
			else
				return null;

			if (byteReader.ReadNextUintBytes(ref uin, dataArray, ref remainsize, ref offset))
				ret.SID = uin;
			else
				return null;

			if (byteReader.ReadNextByteBytes(ref byt, dataArray, ref remainsize, ref offset))
				parseCommandDataFlagsBits(ret, byt);
			else
				return null;

			if (ret.CHSFE)
			{
				if (byteReader.ReadNextByteBytes(ref byt, dataArray, ref remainsize, ref offset))
					ret.CHS = (EgtsSymbolEncoding)byt;
				else
					return null;
			}
			if (ret.ACFE)
			{
				if (byteReader.ReadNextByteBytes(ref byt, dataArray, ref remainsize, ref offset))
					ret.ACL = byt;
				else
					return null;
				if (ret.ACL > 0)
				{
					ret.AuthorizationCodeData.Clear();			// вот тут возможно я не так понял
					for (int i = 0; i < ret.ACL; i++)
					{
						if (byteReader.ReadNextByteBytes(ref byt, dataArray, ref remainsize, ref offset))
							ret.AuthorizationCodeData.Add(byt);
						else
							return null;
					}
				}
			}

			switch (ret.CT)
			{		// остальных типов в сырцах тоже не предусмотрено, отчего-то
				case EgtsCommandType.Command:
					var command = new EgtsCommand();
					if (byteReader.ReadNextUshortBytes(ref ush, dataArray, ref remainsize, ref offset))
						command.ADR = ush;
					else
						break;
					if (byteReader.ReadNextByteBytes(ref byt, dataArray, ref remainsize, ref offset))
						parseCommandBits(command, byt);
					else
						break;
					if (byteReader.ReadNextUshortBytes(ref ush, dataArray, ref remainsize, ref offset))
						command.CCD = (EgtsCommandCode)ush;
					else
						break;
					ret.Command = command;
					break;
				case EgtsCommandType.CommandConfirmation:
					var conf = new EgtsCommand();
					if (byteReader.ReadNextUshortBytes(ref ush, dataArray, ref remainsize, ref offset))
						conf.ADR = ush;
					else
						break;
					if (byteReader.ReadNextUshortBytes(ref ush, dataArray, ref remainsize, ref offset))
						conf.CCD = (EgtsCommandCode)ush;
					else
						break;
					ret.Command = conf;
					break;
			}

			var totalRed = beginSize - remainsize;
			var dtSize = srl - totalRed;
			ret.Command.AdditionalData.Clear();			// вот тут возможно я не так понял 2
			for (int i = 0; i < dtSize; i++)
			{
				if (byteReader.ReadNextByteBytes(ref byt, dataArray, ref remainsize, ref offset))
					ret.Command.AdditionalData.Add(byt);
				else
					return null;
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
			var obj = subrecord as EgtsCommandData;
			if (obj == null)
				throw new EgtsProceedException(string.Format("Wrong data passed in parser. Expected type {0}, passed type {1}", SRT,
					subrecord.SubrecordType));
			var ret = new List<byte>();
			var byt = byteCommandDataTypesBits(obj);
			var bytSize = sizeof (byte);
			var ushSize = sizeof (ushort);
			var uinSize = sizeof (uint);
			ret.AddRange(byteReader.WriteValueToBuffer(byt, bytSize));
			ret.AddRange(byteReader.WriteValueToBuffer(obj.CID, uinSize));
			ret.AddRange(byteReader.WriteValueToBuffer(obj.SID, uinSize));
			byt = byteCommandDataFlagsBits(obj);
			ret.AddRange(byteReader.WriteValueToBuffer(byt, bytSize));
			
			if (obj.CHSFE)
			{
				if (obj.CHS == null)
					throw new EgtsProceedException("EgtsCommand. CHSFE set to true, but no CHS set");
				ret.AddRange(byteReader.WriteValueToBuffer((byte)obj.CHS, bytSize));
			}
			if (obj.ACFE)
			{
				if (obj.AuthorizationCodeData.Count==0)
					throw new EgtsProceedException("EgtsCommand. ACFE set to true, but no AuthorizationCodeData set");
				ret.AddRange(byteReader.WriteValueToBuffer((byte) obj.AuthorizationCodeData.Count, bytSize));
				ret.AddRange(obj.AuthorizationCodeData);
			}

			if (obj.Command == null)
				throw new EgtsProceedException("EgtsCommand. No command set in commandData.");

			switch (obj.CT)
			{		// остальных типов в сырцах тоже не предусмотрено, отчего-то
				case EgtsCommandType.Command:
					
					ret.AddRange(byteReader.WriteValueToBuffer(obj.Command.ADR, ushSize));
					ret.AddRange(byteReader.WriteValueToBuffer(byteCommandBits(obj.Command), bytSize));
					ret.AddRange(byteReader.WriteValueToBuffer((byte)obj.Command.CCD, ushSize));
					break;
				case EgtsCommandType.CommandConfirmation:
					ret.AddRange(byteReader.WriteValueToBuffer(obj.Command.ADR, ushSize));
					ret.AddRange(byteReader.WriteValueToBuffer((byte)obj.Command.CCD, ushSize));
					break;
			}
			if (obj.Command.AdditionalData.Count > 0)
			{
				ret.AddRange(obj.Command.AdditionalData);
			}

			return ret.ToArray();
		}

		#endregion

		#region private methods

		private void parseCommandDataTypesBits(EgtsCommandData cd, byte b)
		{
			cd.CCT = (EgtsCommandConfirmationType)(b & 0x0F);
			b >>= 4;
			cd.CT = (EgtsCommandType)(b & 0x0F); 
		}

		private byte byteCommandDataTypesBits(EgtsCommandData cd)
		{
			byte ret = 0;
			ret |= ((byte) cd.CCT);
			ret |= (byte) (((byte) cd.CT) << 4);
			return ret;
		}

		private void parseCommandDataFlagsBits(EgtsCommandData cd, byte b)
		{
			cd.CHSFE = (b & 0x01) == 1;
			b >>= 1;
			cd.ACFE = (b & 0x01) == 1;
		}

		private byte byteCommandDataFlagsBits(EgtsCommandData cd)
		{
			byte ret = 0;
			ret |= (byte)(cd.CHSFE ? 1 : 0);
			ret |= (byte)((cd.ACFE ? 1 : 0) << 1);
			return ret;
		}

		private void parseCommandBits(EgtsCommand cd, byte b)
		{
			cd.SZ = (byte) (b & 0x0F);
			b >>= 4;
			cd.ACT = (EgtsCommandAction)(b & 0x0F);
		}

		private byte byteCommandBits(EgtsCommand cd)
		{
			byte ret = 0;
			ret |= cd.SZ;
			ret |= (byte) ((byte) (cd.ACT ?? 0) << 4);
			return ret;
		}

		#endregion private methods
	}
}
