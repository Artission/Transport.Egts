using Transport.EGTS.Model.Subrecord;
using br = Transport.EGTS.Helpers.ByteCombineHelper;
using Transport.EGTS.Enum;
using Transport.EGTS.Enum.Subrecord;

namespace Transport.EGTS.Parser.SubRecordParser
{
	internal class PassengerCountDataSubrecordParser:ISubrecordParser
	{
		#region Implementation of ISubrecordParser

		/// <summary>
		/// Gets or sets the SubrecordType.
		/// </summary>
		/// <value>
		/// The SubrecordType.
		/// </value>
		public EgtsSubrecordType SRT
		{
			get { return EgtsSubrecordType.PassengerCount; }
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
			get { return EgtsServiceType.TeleData; }
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
			var ret = new EgtsPassengerCountData();

			byte byt = 0;
			ushort ush = 0;
			if (!br.ReadNextByteBytes(ref byt, dataArray, ref remainsize, ref offset))
				return null;
			ret.RDF = (byt & 0x01) == 1;
			if (!br.ReadNextByteBytes(ref byt, dataArray, ref remainsize, ref offset))
				return null;
			parseBitsToBools(ret.DPR, byt);
			if (!br.ReadNextByteBytes(ref byt, dataArray, ref remainsize, ref offset))
				return null;
			parseBitsToBools(ret.DRL, byt);
			if (!br.ReadNextUshortBytes(ref ush, dataArray, ref remainsize, ref offset))
				return null;
			ret.MADDR = ush;

			for (int i = 0; i < 8; i++)
			{
				if (!ret.DPR[i])
					continue;
				var nx = new EgtsPassengerIOCountData {DoorNumber = (byte) (i + 1)};
				if (!br.ReadNextByteBytes(ref byt, dataArray, ref remainsize, ref offset))
					return null;
				nx.IPQ = byt;
				if (!br.ReadNextByteBytes(ref byt, dataArray, ref remainsize, ref offset))
					return null;
				nx.OPQ = byt;
				ret.PassengerCountData.Add(nx);
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

		private void parseBitsToBools(bool[] arrToFill, byte b)
		{
			for (int i = 0; i < 8; i++)
			{
				arrToFill[i] = (b & 0x01) == 1;
				b >>= 1;
			}
		}

		#endregion private methods
	}
}
