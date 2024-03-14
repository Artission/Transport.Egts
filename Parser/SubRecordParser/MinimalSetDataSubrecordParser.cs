using System.Text;
using Transport.EGTS.Enum;
using Transport.EGTS.Enum.Subrecord;
using bt = Transport.EGTS.Helpers.ByteCombineHelper;
using Transport.EGTS.Model;
using Transport.EGTS.Model.Subrecord;

namespace Transport.EGTS.Parser.SubRecordParser
{
	internal class MinimalSetDataSubrecordParser:ISubrecordParser
	{
		#region Implementation of ISubrecordParser

		/// <summary>
		/// Gets or sets the SRT.
		/// </summary>
		/// <value>
		/// The SubrecordType.
		/// </value>
		public EgtsSubrecordType SRT { get { return EgtsSubrecordType.MSDData; } }

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
			var ret = new EgtsMinimalSetData();
			byte byt = 0;
			ushort ush = 0;
			uint uin = 0;

			var begSize = remainsize;

			if (!bt.ReadNextByteBytes(ref byt,dataArray,ref remainsize,ref offset))
				return null;
			ret.FV = byt;
			if (!bt.ReadNextByteBytes(ref byt, dataArray, ref remainsize, ref offset))
				return null;
			ret.MI = byt;
			if (!bt.ReadNextByteBytes(ref byt, dataArray, ref remainsize, ref offset))
				return null;
			parseBitfields(ret, byt);

			var vinArr = new byte[Constants.VinLength];
			for (int i = 0; i < Constants.VinLength; i++)
			{
				if (!bt.ReadNextByteBytes(ref byt, dataArray, ref remainsize, ref offset))
					return null;
				vinArr[i] = byt;
			}
			ret.VIN = Encoding.ASCII.GetString(vinArr);

			if (!bt.ReadNextByteBytes(ref byt, dataArray, ref remainsize, ref offset))
				return null;
			ret.VPST = parsePropulsionTypeBitsToFlags(byt);

			if (!bt.ReadNextUintBytesReverse(ref uin, dataArray, ref remainsize, ref offset))
				return null;
			ret.TS = uin;
			if (!bt.ReadNextUintBytesReverse(ref uin, dataArray, ref remainsize, ref offset))
				return null;
			ret.PLAT = uin;
			if (!bt.ReadNextUintBytesReverse(ref uin, dataArray, ref remainsize, ref offset))
				return null;
			ret.PLON = uin;
			if (!bt.ReadNextByteBytes(ref byt, dataArray, ref remainsize, ref offset))
				return null;
			ret.VD = byt;

			var unreadBytesCount = srl - (begSize - remainsize);
			const int u16size = sizeof (ushort);
			if (unreadBytesCount >= u16size)
			{
				if (!bt.ReadNextUshortBytesReverse(ref ush, dataArray, ref remainsize, ref offset))
					return null;
				ret.RVPLATD1 = ush;
				unreadBytesCount -= u16size;
			}

			if (unreadBytesCount >= u16size)
			{
				if (!bt.ReadNextUshortBytesReverse(ref ush, dataArray, ref remainsize, ref offset))
					return null;
				ret.RVPLOND1 = ush;
				unreadBytesCount -= u16size;
			}

			if (unreadBytesCount >= u16size)
			{
				if (!bt.ReadNextUshortBytesReverse(ref ush, dataArray, ref remainsize, ref offset))
					return null;
				ret.RVPLATD2 = ush;
				unreadBytesCount -= u16size;
			}

			if (unreadBytesCount >= u16size)
			{
				if (!bt.ReadNextUshortBytesReverse(ref ush, dataArray, ref remainsize, ref offset))
					return null;
				ret.RVPLOND2 = ush;
				unreadBytesCount -= u16size;
			}

			if (unreadBytesCount >= u16size)
			{
				if (!bt.ReadNextByteBytes(ref byt, dataArray, ref remainsize, ref offset))
					return null;
				ret.NOP = byt;
				unreadBytesCount -= sizeof(byte);
			}

			if (unreadBytesCount > 0)
			{
				if (unreadBytesCount>Constants.MaxMDSAdditionalDataLength)
					return null;	// something gone wrong
				for (int i = 0; i < unreadBytesCount; i++)
				{
					if (!bt.ReadNextByteBytes(ref byt, dataArray, ref remainsize, ref offset))
						return null;
					ret.AdditionalData.Add(byt);
				}
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

		private void parseBitfields(EgtsMinimalSetData cd, byte b)
		{
			cd.ACT = (b & 0x01) == 1;
			b >>= 1;
			cd.CLT = (b & 0x01) == 1;
			b >>= 1;
			cd.POCN = (b & 0x01) == 1;
			b >>= 1;
			cd.VT = (EgtsVehicleType) (b & 0x0F);
		}

		private EgtsVehiclePropulsionStorageType parsePropulsionTypeBitsToFlags(byte u)
		{
			var ret = EgtsVehiclePropulsionStorageType.None;
			if ((u & 0x01) == 1)
				ret |= EgtsVehiclePropulsionStorageType.Benzin;
			u >>= 1;
			if ((u & 0x01) == 1)
				ret |= EgtsVehiclePropulsionStorageType.Dizel;
			u >>= 1;
			if ((u & 0x01) == 1)
				ret |= EgtsVehiclePropulsionStorageType.CNG;
			u >>= 1;
			if ((u & 0x01) == 1)
				ret |= EgtsVehiclePropulsionStorageType.LPG;
			u >>= 1;
			if ((u & 0x01) == 1)
				ret |= EgtsVehiclePropulsionStorageType.Elecricity;
			u >>= 1;
			if ((u & 0x01) == 1)
				ret |= EgtsVehiclePropulsionStorageType.Hydrogen;

			return ret;
		}

		#endregion private methods

	}
}
