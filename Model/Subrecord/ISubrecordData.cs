using Transport.EGTS.Enum;

namespace Transport.EGTS.Model.Subrecord
{
	/// <summary>
	/// общий интерфейс для всех моделей представляющих сабрекорды
	/// </summary>
	public interface ISubrecordData
	{
		/// <summary>
		/// Subrecord Type
		/// </summary>
		/// <value>
		/// The SubrecordType.
		/// </value>
		EgtsSubrecordType SubrecordType { get; }
	}
}
