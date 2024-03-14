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
	internal class AuthTerminalModuleDataSubrecordParser: ISubrecordParser
	{
		#region Implementation of ISubrecordParser

		/// <summary>
		/// Gets or sets the SubrecordType.
		/// </summary>
		/// <value>
		/// The SubrecordType.
		/// </value>
		public EgtsSubrecordType SRT { get { return EgtsSubrecordType.ModuleData; } }

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
			var ret = new EgtsAuthTerminalModuleData();

			ushort ush = 0;
			byte byt = 0;
			uint uin = 0;

			if (!br.ReadNextByteBytes(ref byt, dataArray, ref remainsize, ref offset))
				return null;
			ret.MT = (EgtsModuleType)byt;
			if (!br.ReadNextUintBytes(ref uin, dataArray, ref remainsize, ref offset))
				return null;
			ret.VID = uin;
			if (!br.ReadNextUshortBytes(ref ush, dataArray, ref remainsize, ref offset))
				return null;
			ret.FWV = ush;
			if (!br.ReadNextUshortBytes(ref ush, dataArray, ref remainsize, ref offset))
				return null;
			ret.SWV = ush;
			if (!br.ReadNextByteBytes(ref byt, dataArray, ref remainsize, ref offset))
				return null;
			ret.MD = byt;
			if (!br.ReadNextByteBytes(ref byt, dataArray, ref remainsize, ref offset))
				return null;
			ret.ST = (EgtsProcessingState)byt;

			var str = br.ReadStringToDelimiter(Constants.MaxSerialNumberLength, dataArray, ref remainsize, ref offset);
			if (str==null)
				return null;
			ret.SRN = str;
			str = br.ReadStringToDelimiter(Constants.MaxDescriptionLength, dataArray, ref remainsize, ref offset);
			if (str == null)
				return null;
			ret.DSCR = str;

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
			var obj = subrecord as EgtsAuthTerminalModuleData;
			if (obj == null)
				throw new EgtsProceedException(string.Format("Wrong data passed in parser. Expected type {0}, passed type {1}", SRT,
					subrecord.SubrecordType));
			var ret = new List<byte>();
			var ushSize = sizeof(ushort);
			var uinSize = sizeof (uint);

			ret.Add((byte)obj.MT);
			ret.AddRange(br.WriteValueToBuffer(obj.VID, uinSize));
			ret.AddRange(br.WriteValueToBuffer(obj.FWV, ushSize));
			ret.AddRange(br.WriteValueToBuffer(obj.SWV, ushSize));
			ret.Add(obj.MD);
			ret.Add((byte) obj.ST);
			if (obj.SRN == null)
				obj.SRN = "";
			if (obj.DSCR == null)
				obj.DSCR = "";
			if (obj.SRN.Length > Constants.MaxSerialNumberLength)
				throw new EgtsProceedException("TerminalModuleData. SRN is to big");
			if (obj.DSCR.Length > Constants.MaxDescriptionLength)
				throw new EgtsProceedException("TerminalModuleData. DSCR is to big");

			ret.AddRange(Encoding.ASCII.GetBytes(obj.SRN));
			ret.Add(0);
			ret.AddRange(Encoding.ASCII.GetBytes(obj.DSCR));
			ret.Add(0);
			
			return ret.ToArray();
		}

		#endregion
	}
}
