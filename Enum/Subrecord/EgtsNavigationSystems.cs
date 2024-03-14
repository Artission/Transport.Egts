
using System;

namespace Transport.EGTS.Enum.Subrecord
{
	/// <summary>
	/// битовые флаги, характеризующие используемые навигационные спутниковые системы.
	/// </summary>
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1008:EnumsShouldHaveZeroValue"), Flags]	// 0 есть и он задокументирован, не буду перименовывать в None
	public enum EgtsNavigationSystems
	{
		/// <summary>
		/// The undefined
		/// </summary>
		Undefined = 0,

		/// <summary>
		/// The glonass
		/// </summary>
		GLONASS = 1,

		/// <summary>
		/// The GPS
		/// </summary>
		GPS = 2,

		/// <summary>
		/// The galileo
		/// </summary>
		Galileo = 4,

		/// <summary>
		/// The compass
		/// </summary>
		Compass = 8,

		/// <summary>
		/// The beidou
		/// </summary>
		Beidou = 16,

		/// <summary>
		/// The doris
		/// </summary>
		DORIS = 32,

		/// <summary>
		/// The irnss
		/// </summary>
		IRNSS = 64,

		/// <summary>
		/// The QZSS
		/// </summary>
		QZSS = 128
	}
}
