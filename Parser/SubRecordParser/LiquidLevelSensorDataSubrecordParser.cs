using br = Transport.EGTS.Helpers.ByteCombineHelper;
using Transport.EGTS.Enum;
using Transport.EGTS.Enum.Subrecord;
using Transport.EGTS.Model.Subrecord;

namespace Transport.EGTS.Parser.SubRecordParser
{
	internal class LiquidLevelSensorDataSubrecordParser: ISubrecordParser
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
			get { return EgtsSubrecordType.LiquidLevelSensorData; }
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
			var ret = new EgtsLiquidLevelSensorData();

			var begSize = remainsize;
			byte byt = 0;
			ushort ush = 0;
			if (!br.ReadNextByteBytes(ref byt, dataArray, ref remainsize, ref offset))
				return null;
			parseBitfields(ret, byt);
			if (!br.ReadNextUshortBytes(ref ush,dataArray,ref remainsize, ref offset))
				return null;
			ret.MADDR = ush;
			ret.LiquidSensorData.Clear();

			var toread = ret.RDF?sizeof (uint): (uint)(srl - (begSize - remainsize));
			var arr = br.ReadRawByteArray(toread, dataArray, ref remainsize, ref offset);
			if (arr == null)
				return null;
			ret.LiquidSensorData.AddRange(arr);

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

		private void parseBitfields(EgtsLiquidLevelSensorData lls, byte b)
		{
			lls.LLSN = (byte)(b & 0x07);
			b >>= 3;
			lls.RDF = (b & 0x01) == 1;
			b >>= 1;
			lls.LLSVU = (EgtsLiquidLevelSensorValueUnit) (b & 0x03);
			b >>= 2;
			lls.LLSEF = (b & 0x01) == 1;
		}

		#endregion private methods
	}
}
