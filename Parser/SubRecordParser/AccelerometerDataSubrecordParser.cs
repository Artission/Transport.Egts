using Transport.EGTS.Enum;
using Transport.EGTS.Enum.Subrecord;
using Transport.EGTS.Model.Subrecord;
using br = Transport.EGTS.Helpers.ByteCombineHelper;

namespace Transport.EGTS.Parser.SubRecordParser
{
	internal class AccelerometerDataSubrecordParser:ISubrecordParser
	{
		#region Implementation of ISubrecordParser

		/// <summary>
		/// Gets or sets the SubrecordType.
		/// </summary>
		/// <value>
		/// The SubrecordType.
		/// </value>
		public EgtsSubrecordType SRT { get { return EgtsSubrecordType.AccelData; } }

		/// <summary>
		/// Поскольку в документации всё делится на конкретно описанные сервиса
		/// (и в передаче данных тоже есть инфа именно по описанным сервисам, а не отдельным парсером как тут)
		/// надо пометить парсер типом (неизвестно, пригодится ли, но...)
		/// </summary>
		/// <value>
		/// The type of the service.
		/// </value>
		public EgtsServiceType ServiceType { get { return EgtsServiceType.Ecall; } }

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
			var ret = new EgtsAccelerometerData();
			byte byt = 0;
			ushort ush = 0;
			uint uin = 0;
			var begSize = remainsize;

			if (!br.ReadNextByteBytes(ref byt, dataArray, ref remainsize, ref offset))
				return null;
			ret.SA = byt;
			if (!br.ReadNextUintBytes(ref uin,dataArray,ref remainsize,ref offset))
				return null;
			ret.ATM = uin;
			// TODO Грешновато, но гранит шлёт пакеты с акселерометром с заявленной длиной в 5 байт, что подразумевает 0 записей ниже, а при этом в SA пишет 2 записи
			var remDat = srl - (begSize - remainsize);
			if (remDat == 0)
			{
				return ret;
			}
			for (int i = 0; i < ret.SA; i++)
			{
				var nxtem = new EgtsAccelerometerItem();
				if (!br.ReadNextUshortBytes(ref ush, dataArray, ref remainsize, ref offset))
					return null;
				nxtem.RTM = ush;
				if (!br.ReadNextUshortBytes(ref ush, dataArray, ref remainsize, ref offset))
					return null;
				nxtem.XAAV = (short) ush;
				if (!br.ReadNextUshortBytes(ref ush, dataArray, ref remainsize, ref offset))
					return null;
				nxtem.YAAV = (short)ush;
				if (!br.ReadNextUshortBytes(ref ush, dataArray, ref remainsize, ref offset))
					return null;
				nxtem.ZAAV = (short)ush;
				ret.AccelerometerRecords.Add(nxtem);
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

	}
}
