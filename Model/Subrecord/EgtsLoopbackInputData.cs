using Transport.EGTS.Enum;
using Transport.EGTS.Enum.Subrecord;

namespace Transport.EGTS.Model.Subrecord
{
	/// <summary>
	/// значения шлейфовых входов
	/// EGTS_SR_LOOPIN_DATA
	/// </summary>
	public class EgtsLoopbackInputData : ISubrecordData
	{
		/// <summary>
		/// Subrecord Type
		/// </summary>
		/// <value>
		/// The SubrecordType.
		/// </value>
		public EgtsSubrecordType SubrecordType
		{
			get { return EgtsSubrecordType.LoopbackInputData; }
		}
		/// <summary>
		/// LIFE 1 ... LIFE 8 - (Loop In Field Exists) битовые флаги, определяющие наличие информации о состоянии шлейфовых входов;
		/// </summary>
		/// <value>
		/// The life.
		/// </value>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
		public bool[] LIFE { get { return _life; } }
		private readonly bool[] _life = new bool[8];

		/// <summary>
		/// LIS n ... LIS n+7 - (Loop In State) значение состояния соответствующего шлейфового входа.
		/// </summary>
		/// <value>
		/// The lis.
		/// </value>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
		public EgtsLoopInState[] LIS { get { return _lis; } }
		private readonly EgtsLoopInState[] _lis = new EgtsLoopInState[8];
	}
}
