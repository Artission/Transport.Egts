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
	internal class AuthVehicleDataSubrecordParser:ISubrecordParser
	{
		#region Implementation of ISubrecordParser

		/// <summary>
		/// Gets or sets the SubrecordType.
		/// </summary>
		/// <value>
		/// The SubrecordType.
		/// </value>
		public EgtsSubrecordType SRT { get { return EgtsSubrecordType.VehicleData; } }

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
			var ret = new EgtsAuthVehicleData();

			uint uin = 0;

			var str = br.ReadFixedSizeString(Constants.VinLength, dataArray, ref remainsize, ref offset);
			if (str==null)
				return null;
			ret.VIN = str;

			if (!br.ReadNextUintBytes(ref uin, dataArray, ref remainsize, ref offset))
				return null;
			ret.VHT = (EgtsVehicleType) uin;
			if (!br.ReadNextUintBytes(ref uin, dataArray, ref remainsize, ref offset))
				return null;
			ret.VPST = parsePropulsionTypeBitsToFlags(uin);

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
			var obj = subrecord as EgtsAuthVehicleData;
			if (obj == null)
				throw new EgtsProceedException(string.Format("Wrong data passed in parser. Expected type {0}, passed type {1}", SRT,
					subrecord.SubrecordType));
			var ret = new List<byte>();

			var uinSize = sizeof(uint);
			if (string.IsNullOrWhiteSpace(obj.VIN) || obj.VIN.Length > Constants.VinLength)
				throw new EgtsProceedException("Wrong VIN length", SRT);
			ret.AddRange(Encoding.ASCII.GetBytes(obj.VIN));

			ret.AddRange(br.WriteValueToBuffer((uint) obj.VHT, uinSize));
			ret.AddRange(br.WriteValueToBuffer(bytePropulsionFlags(obj.VPST), uinSize));
			
			return ret.ToArray();
		}

		#endregion

		#region private methods

		private EgtsVehiclePropulsionStorageType parsePropulsionTypeBitsToFlags(uint u)
		{
			var ret = EgtsVehiclePropulsionStorageType.None;
			if ((u & 0x01) == 1)
				ret |=EgtsVehiclePropulsionStorageType.Benzin;
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

		private uint bytePropulsionFlags(EgtsVehiclePropulsionStorageType val)
		{
			uint ret = 0;
			var offset = 0;
			if ((val & EgtsVehiclePropulsionStorageType.Benzin) != 0)
				ret |= (byte)(1 << offset);
			offset++;
			if ((val & EgtsVehiclePropulsionStorageType.Dizel) != 0)
				ret |= (byte)(1 << offset);
			offset++;
			if ((val & EgtsVehiclePropulsionStorageType.CNG) != 0)
				ret |= (byte)(1 << offset);
			offset++;
			if ((val & EgtsVehiclePropulsionStorageType.LPG) != 0)
				ret |= (byte)(1 << offset);
			offset++;
			if ((val & EgtsVehiclePropulsionStorageType.Elecricity) != 0)
				ret |= (byte)(1 << offset);
			offset++;
			if ((val & EgtsVehiclePropulsionStorageType.Hydrogen) != 0)
				ret |= (byte)(1 << offset);
			
			return ret;
		}

		#endregion private methods
	}
}
