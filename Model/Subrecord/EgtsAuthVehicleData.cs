using Transport.EGTS.Enum;
using Transport.EGTS.Enum.Subrecord;

namespace Transport.EGTS.Model.Subrecord
{
	/// <summary>
	/// информация о двигателе
	/// egts_AUTH_VEHICLE_DATA_t
	/// </summary>
	public class EgtsAuthVehicleData : ISubrecordData
	{
		/// <summary>
		/// Subrecord Type
		/// </summary>
		/// <value>
		/// The SRT.
		/// </value>
		public EgtsSubrecordType SubrecordType { get { return EgtsSubrecordType.VehicleData; } }

		/// <summary>
		/// Gets or sets the vin.
		/// </summary>
		/// <value>
		/// The vin.
		/// </value>
		public string VIN { get; set; }

		/// <summary>
		/// Gets or sets the VHT.
		/// </summary>
		/// <value>
		/// The VHT.
		/// </value>
		public EgtsVehicleType VHT { get; set; }

		/// <summary>
		/// Gets or sets the VPST.
		/// </summary>
		/// <value>
		/// The VPST.
		/// </value>
		public EgtsVehiclePropulsionStorageType VPST { get; set; }
	}
}
