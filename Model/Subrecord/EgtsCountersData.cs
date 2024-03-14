
using Transport.EGTS.Enum;

namespace Transport.EGTS.Model.Subrecord
{
	/// <summary>
	/// данные счётчиков
	/// egts_TELEDATA_COUNTERS_DATA_t
	/// </summary>
	public class EgtsCountersData : ISubrecordData
	{
		/// <summary>
		/// Subrecord Type
		/// </summary>
		/// <value>
		/// The SubrecordType.
		/// </value>
		public EgtsSubrecordType SubrecordType
		{
			get { return EgtsSubrecordType.CountersData; }
		}
		/// <summary>
		/// CFE1 ... CFE8 - (Counter Field Exists) битовые флаги определяют наличие соответствующих полей счетных входов:
		/// </summary>
		/// <value>
		/// The cfe.
		/// </value>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
		public bool[] CFE { get { return _cfe; } }
		private readonly bool[] _cfe = new bool[8];

		/// <summary>
		/// CN1 ... CN8 - значение счетных входов с 1 по 8 соответственно.
		/// </summary>
		/// <value>
		/// The cn.
		/// </value>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
		public uint[] CN { get { return _cn; } }
		private readonly uint[] _cn = new uint[8];
	}
}
