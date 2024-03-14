
using Transport.EGTS.Enum;

namespace Transport.EGTS.Model.Subrecord
{
	/// <summary>
	/// EGTS_SR_ABS_LOOPIN_DATA
	/// </summary>
	public class EgtsSingleLoopbackInputData : ISubrecordData
	{
		/// <summary>
		/// Subrecord Type
		/// </summary>
		/// <value>
		/// The SubrecordType.
		/// </value>
		public EgtsSubrecordType SubrecordType
		{
			get { return EgtsSubrecordType.SingleLoopbackInputData; }
		}
		/// <summary>
		/// номер шлейфового входа;
		/// </summary>
		/// <value>
		/// The lin.
		/// </value>
		public ushort LIN { get; set; }

		/// <summary>
		/// значение состояния шлейфового входа
		/// </summary>
		/// <value>
		/// The lis.
		/// </value>
		public byte LIS { get; set; }
	}
}
