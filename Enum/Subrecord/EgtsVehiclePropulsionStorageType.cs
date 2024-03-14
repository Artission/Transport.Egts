using System;

namespace Transport.EGTS.Enum.Subrecord
{
	/// <summary>
	/// тип энергоносителя транспортного средства
	/// </summary>
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1714:FlagsEnumsShouldHavePluralNames"), Flags]
	public enum EgtsVehiclePropulsionStorageType
	{
		/// <summary>
		/// The none
		/// </summary>
		None = 0,

		/// <summary>
		/// The benzin
		/// </summary>
		Benzin =1,

		/// <summary>
		/// The dizel
		/// </summary>
		Dizel = 2,

		/// <summary>
		/// сжиженный природный газ (CNG)
		/// </summary>
		CNG =4,

		/// <summary>
		/// жидкий пропан (LPG)
		/// </summary>
		LPG = 8,

		/// <summary>
		/// электричество (более 42 v and 100 Ah)
		/// </summary>
		Elecricity = 16,

		/// <summary>
		/// водород
		/// </summary>
		Hydrogen = 32
	}
}
