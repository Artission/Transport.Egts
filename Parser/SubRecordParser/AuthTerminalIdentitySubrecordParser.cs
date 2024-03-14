using System;
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
	internal class AuthTerminalIdentitySubrecordParser:ISubrecordParser
	{
		#region Implementation of ISubrecordParser

		/// <summary>
		/// Gets or sets the SubrecordType.
		/// </summary>
		/// <value>
		/// The SubrecordType.
		/// </value>
		public EgtsSubrecordType SRT { get { return EgtsSubrecordType.TerminalIdentity; } }

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
			var ret = new EgtsAuthTerminalIdentityData();

			byte byt = 0;
			ushort ush = 0;
			uint uin = 0;

			if (!br.ReadNextUintBytes(ref uin, dataArray, ref remainsize, ref offset))
				return null;
			ret.TID = uin;
			if (!br.ReadNextByteBytes(ref byt, dataArray, ref remainsize, ref offset))
				return null;
			parseTermIdentityBitfields(ret, byt);

			if (ret.HDIDE)
			{
				if (!br.ReadNextUshortBytes(ref ush, dataArray, ref remainsize, ref offset))
					return null;
				ret.HDID = ush;
			}
			if (ret.IMEIE)
			{
				var str = br.ReadFixedSizeString(Constants.IMEILength, dataArray, ref remainsize, ref offset);
				if (str==null)
					return null;
				ret.IMEI = str;
			}
			if (ret.IMSIE)
			{
				var str = br.ReadFixedSizeString(Constants.IMSILength, dataArray, ref remainsize, ref offset);
				if (str == null)
					return null;
				ret.IMSI = str;
			}
			if (ret.LNGCE)
			{
				var str = br.ReadFixedSizeString(Constants.LanguageCodeLength, dataArray, ref remainsize, ref offset);
				if (str == null)
					return null;
				ret.LNGC = str;
			}
			if (ret.NIDE)
			{
				var arr = br.ReadRawByteArray(Constants.NetworkIDLength, dataArray, ref remainsize, ref offset);
				if (arr==null)
					return null;
				arr.CopyTo(ret.NID, 0);
			}
			if (ret.BSE)
			{
				if (!br.ReadNextUshortBytes(ref ush, dataArray, ref remainsize, ref offset))
					return null;
				ret.BS = ush;
			}
			if (ret.MNE)
			{
				var str = br.ReadFixedSizeString(Constants.MSISDNLength, dataArray, ref remainsize, ref offset);
				if (str == null)
					return null;
				ret.MSISDN = str;
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
			var obj = subrecord as EgtsAuthTerminalIdentityData;
			if (obj == null)
				throw new EgtsProceedException(string.Format("Wrong data passed in parser. Expected type {0}, passed type {1}", SRT,
					subrecord.SubrecordType));
			var ret = new List<byte>();
			
			var ushSize = sizeof(ushort);
			var uinSize = sizeof(uint);
			ret.AddRange(br.WriteValueToBuffer(obj.TID, uinSize));
			ret.Add(byteTermIdentityBitfields(obj));
			
			if (obj.HDIDE)
			{
				ret.AddRange(br.WriteValueToBuffer(obj.HDID, uinSize));
			}
			if (obj.IMEIE)
			{
				if (string.IsNullOrWhiteSpace(obj.IMEI))
					throw new EgtsProceedException("TerminalIdentity. IMEIE is set to true, but no IMEI found", SRT);
				ret.AddRange(Encoding.ASCII.GetBytes(obj.IMEI.PadRight((int)Constants.IMEILength,Char.MinValue)));
			}
			if (obj.IMSIE)
			{
				if (string.IsNullOrWhiteSpace(obj.IMSI))
					throw new EgtsProceedException("TerminalIdentity. IMSIE is set to true, but no IMSI found", SRT);
				ret.AddRange(Encoding.ASCII.GetBytes(obj.IMSI.PadRight((int)Constants.IMSILength, Char.MinValue)));
			}
			if (obj.LNGCE)
			{
				if (string.IsNullOrWhiteSpace(obj.LNGC))
					throw new EgtsProceedException("TerminalIdentity. LNGCE is set to true, but no LNGC found", SRT);
				ret.AddRange(Encoding.ASCII.GetBytes(obj.LNGC.PadRight((int)Constants.LanguageCodeLength, Char.MinValue)));
			}
			if (obj.NIDE)
			{
				if (obj.NID.Length==0)
					throw new EgtsProceedException("TerminalIdentity. NIDE is set to true, but no NID found", SRT);
				ret.AddRange(obj.NID);
			}
			if (obj.BSE)
			{
				ret.AddRange(br.WriteValueToBuffer(obj.BS, ushSize));
			}
			if (obj.MNE)
			{
				if (string.IsNullOrWhiteSpace(obj.MSISDN))
					throw new EgtsProceedException("TerminalIdentity. MNE is set to true, but no MSISDN found", SRT);
				ret.AddRange(Encoding.ASCII.GetBytes(obj.MSISDN.PadRight((int)Constants.MSISDNLength, Char.MinValue)));
			}

			return ret.ToArray();
		}

		#endregion

		#region private methods

		private void parseTermIdentityBitfields(EgtsAuthTerminalIdentityData ident, byte b)
		{
			ident.HDIDE = (b & 0x01) == 1;
			b >>= 1;
			ident.IMEIE = (b & 0x01) == 1;
			b >>= 1;
			ident.IMSIE = (b & 0x01) == 1;
			b >>= 1;
			ident.LNGCE = (b & 0x01) == 1;
			b >>= 1;
			ident.SSRA = (b & 0x01) == 1;
			b >>= 1;
			ident.NIDE = (b & 0x01) == 1;
			b >>= 1;
			ident.BSE = (b & 0x01) == 1;
			b >>= 1;
			ident.MNE = (b & 0x01) == 1;   
		}

		private byte byteTermIdentityBitfields(EgtsAuthTerminalIdentityData ident)
		{
			byte ret = 0;
			var offset = 0;
			ret |= (byte)((ident.HDIDE ? 1 : 0) << offset);
			offset++;
			ret |= (byte)((ident.IMEIE ? 1 : 0) << offset);
			offset++;
			ret |= (byte)((ident.IMSIE ? 1 : 0) << offset);
			offset++;
			ret |= (byte)((ident.LNGCE ? 1 : 0) << offset);
			offset++;
			ret |= (byte)((ident.SSRA ? 1 : 0) << offset);
			offset++;
			ret |= (byte)((ident.NIDE ? 1 : 0) << offset);
			offset++;
			ret |= (byte)((ident.BSE ? 1 : 0) << offset);
			offset++;
			ret |= (byte)((ident.MNE ? 1 : 0) << offset);
			
			return ret;
		}

		#endregion private methods
	}
}
