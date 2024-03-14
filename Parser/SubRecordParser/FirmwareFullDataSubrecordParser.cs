using Transport.EGTS.Enum;
using Transport.EGTS.Enum.Subrecord;
using br = Transport.EGTS.Helpers.ByteCombineHelper;
using Transport.EGTS.Model;
using Transport.EGTS.Model.Subrecord;

namespace Transport.EGTS.Parser.SubRecordParser
{
	internal class FirmwareFullDataSubrecordParser:ISubrecordParser
	{
		#region Implementation of ISubrecordParser

		/// <summary>
		/// Gets or sets the SRT.
		/// </summary>
		/// <value>
		/// The SRT.
		/// </value>
		public EgtsSubrecordType SRT { get { return EgtsSubrecordType.FirmwareFullData; } }

		/// <summary>
		/// Поскольку в документации всё делится на конкретно описанные сервиса
		/// (и в передаче данных тоже есть инфа именно по описанным сервисам, а не отдельным парсером как тут)
		/// надо пометить парсер типом (неизвестно, пригодится ли, но...)
		/// </summary>
		/// <value>
		/// The type of the service.
		/// </value>
		public EgtsServiceType ServiceType { get { return EgtsServiceType.Firmware; } }

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
			var ret = new EgtsFirmwareFullData();
			var head = new EgtsFirmwareHeader();

			ushort ush = 0;
			byte byt = 0;
			var begSize = remainsize;

			if (!br.ReadNextByteBytes(ref byt, dataArray, ref remainsize, ref offset))
				return null;
			parseHeaderBitFields(head, byt);
			if (!br.ReadNextByteBytes(ref byt, dataArray, ref remainsize, ref offset))
				return null;
			head.CMI = byt;
			if (!br.ReadNextUshortBytes(ref ush, dataArray, ref remainsize, ref offset))
				return null;
			head.VER = ush;
			if (!br.ReadNextUshortBytes(ref ush, dataArray, ref remainsize, ref offset))
				return null;
			head.WOS = ush;

			head.FN = br.ReadStringToDelimiter(Constants.MaxFirmwareFileNameLength, dataArray, ref remainsize, ref offset);
			if (head.FN==null)
				return null;
			
			ret.Header = head;

			var unreadSize = srl - (begSize - remainsize);

			for (int i = 0; i < unreadSize; i++)
			{
				if (!br.ReadNextByteBytes(ref byt, dataArray, ref remainsize, ref offset))
					return null;
				ret.ObjectData.Add(byt);
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
			throw new System.NotImplementedException();
		}

		#endregion

		#region private methods

		private void parseHeaderBitFields(EgtsFirmwareHeader head, byte b)
		{
			head.MT = (byte)(b & 0x03);
			b >>= 2;
			head.OT = (byte)(b & 0x03);  
		}

		#endregion private methods
	}
}
