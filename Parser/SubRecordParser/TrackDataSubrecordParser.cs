using Transport.EGTS.Enum;
using Transport.EGTS.Enum.Subrecord;
using br = Transport.EGTS.Helpers.ByteCombineHelper;
using Transport.EGTS.Model.Subrecord;

namespace Transport.EGTS.Parser.SubRecordParser
{
	internal class TrackDataSubrecordParser:ISubrecordParser
	{
		#region Implementation of ISubrecordParser

		/// <summary>
		/// Gets or sets the SRT.
		/// </summary>
		/// <value>
		/// The SRT.
		/// </value>
		public EgtsSubrecordType SRT { get { return EgtsSubrecordType.TrackData; } }

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
			var ret = new EgtsTrackData();

			byte byt = 0;
			uint uin = 0;

			if (!br.ReadNextByteBytes(ref byt, dataArray, ref remainsize, ref offset))
				return null;
			ret.SA = byt;

			if (!br.ReadNextUintBytes(ref uin, dataArray, ref remainsize, ref offset))
				return null;
			ret.ATM = uin;

			for (int i = 0; i < ret.SA; i++)
			{
				var nxtem = new EgtsTrackItem();
				if (!br.ReadNextByteBytes(ref byt, dataArray, ref remainsize, ref offset))
					return null;	// something wrong
				parseTrackItemBits(nxtem, byt);
				ret.TrackItems.Add(nxtem);
				if (!nxtem.TNDE)
					continue;

				if (!br.ReadNextUintBytes(ref uin, dataArray, ref remainsize, ref offset))
					return null;
				nxtem.LAT = uin;
				if (!br.ReadNextUintBytes(ref uin, dataArray, ref remainsize, ref offset))
					return null;
				nxtem.LONG = uin;

				if (!br.ReadNextByteBytes(ref byt, dataArray, ref remainsize, ref offset))
					return null;
				var spdl = byt;
				if (!br.ReadNextByteBytes(ref byt, dataArray, ref remainsize, ref offset))
					return null;
				var dirhspdh = byt;
				if (!br.ReadNextByteBytes(ref byt, dataArray, ref remainsize, ref offset))
					return null;
				var dirl = byt;
				nxtem.SPD = (ushort) (spdl | ((dirhspdh & 0x7f) << 8));
				nxtem.DIR = (ushort) (((dirhspdh & 0x80) << 1) | dirl);
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

		private void parseTrackItemBits(EgtsTrackItem trk, byte b)
		{
			trk.RTM = (byte) (b & 0x1F);
			b >>= 5;
			trk.LOHS = (b & 0x01) == 1;
			b >>= 1;
			trk.LAHS = (b & 0x01) == 1;
			b >>= 1;
			trk.TNDE = (b & 0x01) == 1;
		}

		#endregion private methods
	}
}
