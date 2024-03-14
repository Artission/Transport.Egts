using br = Transport.EGTS.Helpers.ByteCombineHelper;
using Transport.EGTS.Enum;
using Transport.EGTS.Enum.Subrecord;
using Transport.EGTS.Model.Subrecord;

namespace Transport.EGTS.Parser.SubRecordParser
{
	internal class ExtraPositionalDataSubrecordParser: ISubrecordParser
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
			get { return EgtsSubrecordType.ExtraPositionalData; }
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
			var ret = new EgtsExtraPositionalData();

			byte byt = 0;
			ushort ush = 0;

			if (!br.ReadNextByteBytes(ref byt, dataArray, ref remainsize, ref offset))
				return null;
			parseBitfields(ret, byt);

			if (ret.VFE)
			{
				if (!br.ReadNextUshortBytes(ref ush, dataArray, ref remainsize, ref offset))
					return null;
				ret.VDOP = ush;
			}
			if (ret.HFE)
			{
				if (!br.ReadNextUshortBytes(ref ush, dataArray, ref remainsize, ref offset))
					return null;
				ret.HDOP = ush;
			}
			if (ret.PFE)
			{
				if (!br.ReadNextUshortBytes(ref ush, dataArray, ref remainsize, ref offset))
					return null;
				ret.PDOP = ush;
			}
			if (ret.SFE)
			{
				if (!br.ReadNextByteBytes(ref byt, dataArray, ref remainsize, ref offset))
					return null;
				ret.SAT = byt;
			}
			if (ret.NSFE)
			{
				if (!br.ReadNextUshortBytes(ref ush, dataArray, ref remainsize, ref offset))
					return null;
				ret.NS = combineNavigationSystem(ush);
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

		private void parseBitfields(EgtsExtraPositionalData ext, byte b)
		{
			ext.VFE = (b & 0x01) == 1;
			b >>= 1;
			ext.HFE = (b & 0x01) == 1;
			b >>= 1;
			ext.PFE = (b & 0x01) == 1;
			b >>= 1;
			ext.SFE = (b & 0x01) == 1;
			b >>= 1;
			ext.NSFE = (b & 0x01) == 1;
		}

		private EgtsNavigationSystems combineNavigationSystem(ushort ns)
		{
			var ret = EgtsNavigationSystems.Undefined;

			if ((ns & 0x01) == 1)
				ret |= EgtsNavigationSystems.GLONASS;
			if ((ns & 0x02) == 1)
				ret |= EgtsNavigationSystems.GPS;
			if ((ns & 0x04) == 1)
				ret |= EgtsNavigationSystems.Galileo;
			if ((ns & 0x08) == 1)
				ret |= EgtsNavigationSystems.Compass;
			if ((ns & 0x10) == 1)
				ret |= EgtsNavigationSystems.Beidou;
			if ((ns & 0x20) == 1)
				ret |= EgtsNavigationSystems.DORIS;
			if ((ns & 0x40) == 1)
				ret |= EgtsNavigationSystems.IRNSS;
			if ((ns & 0x80) == 1)
				ret |= EgtsNavigationSystems.QZSS;

			//несмотря на то что мы анализируем только 1 байт, а передаём 2, оставшиеся поля зарезервированы(документашка)

			return ret;
		}

		#endregion private methods
	}
}
