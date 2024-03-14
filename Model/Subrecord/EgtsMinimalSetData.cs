using System.Collections.Generic;
using Transport.EGTS.Enum;
using Transport.EGTS.Enum.Subrecord;

namespace Transport.EGTS.Model.Subrecord
{
	/// <summary>
	/// Минимальный Набор Данных, Minimal Set of Data
	/// egts_ECALL_MSD_DATA_t
	/// </summary>
	public class EgtsMinimalSetData : ISubrecordData
	{
		/// <summary>
		/// Subrecord Type
		/// </summary>
		/// <value>
		/// The SubrecordType.
		/// </value>
		public EgtsSubrecordType SubrecordType { get { return EgtsSubrecordType.MSDData; } }

		/// <summary>
		/// Format Version
		/// </summary>
		public byte FV { get; set; }

		/// <summary>
		/// Message Identifier , 1++
		/// </summary>
		public byte MI { get; set; }

		#region Bitfields

		/// <summary>
		/// 4  Vehicle Type
		/// </summary>
		public EgtsVehicleType VT  { get; set; }

		/// <summary>
		/// 1  Position Confidence
		/// </summary>
		public bool POCN  { get; set; }

		/// <summary>
		/// 1  Call Type
		/// </summary>
		public bool CLT  { get; set; }

		/// <summary>
		/// 1  Activation Type
		/// </summary>
		public bool ACT  { get; set; }

		#endregion Bitfields

		/// <summary>
		/// Vehicle Identification Number
		/// </summary>
		public string VIN { get; set; }

		/// <summary>
		/// Vehicle Propulsion Storage Type
		/// </summary>
		public EgtsVehiclePropulsionStorageType VPST { get; set; }

		/// <summary>
		/// Time Stamp , seconds from 00:00:00 01.01.1970 BIG-ENDIAN
		/// </summary>
		public uint TS { get; set; }

		/// <summary>
		/// Position Latitude
		/// </summary>
		public uint PLAT { get; set; }

		/// <summary>
		/// Position Longitude
		/// </summary>
		public uint PLON { get; set; }

		/// <summary>
		/// Vehicle Direction
		/// </summary>
		public byte VD { get; set; }

		#region optional data

		/// <summary>
		/// Optional, switched on by size, Recent Vehicle Position n-1 Latitude Delta , optional , -512...512, BIG-ENDIAN
		/// </summary>
		public ushort? RVPLATD1 { get; set; }

		/// <summary>
		/// Optional, switched on by size, Recent Vehicle Position n-1 Longitude Delta , optional , -512...512, BIG-ENDIAN
		/// </summary>
		public ushort? RVPLOND1 { get; set; }

		/// <summary>
		/// Optional, switched on by size, Recent Vehicle Position n-2 Latitude Delta , optional , -512...512, BIG-ENDIAN
		/// </summary>
		public ushort? RVPLATD2 { get; set; }

		/// <summary>
		/// Optional, switched on by size, Recent Vehicle Position n-2 Longitude Delta , optional , -512...512, BIG-ENDIAN
		/// </summary>
		public ushort? RVPLOND2 { get; set; }

		/// <summary>
		/// Optional, switched on by size, Number Of Passengers , optional 
		/// </summary>
		public byte? NOP { get; set; }

		/// <summary>
		/// Optional, switched on by size, Additional Data
		/// </summary>
		public List<byte> AdditionalData
		{
			get { return _addData; }
		}
		private readonly List<byte> _addData = new List<byte>();

		#endregion optional data
	}
}
