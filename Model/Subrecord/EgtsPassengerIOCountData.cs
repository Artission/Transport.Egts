
namespace Transport.EGTS.Model.Subrecord
{
	/// <summary>
	/// 
	/// </summary>
	public class EgtsPassengerIOCountData 
	{
		/// <summary>
		/// Gets or sets the door number.
		/// </summary>
		/// <value>
		/// The door number.
		/// </value>
		public byte DoorNumber { get; set; }

		/// <summary>
		/// количество вошедших пассажиров
		/// </summary>
		/// <value>
		/// The ipq.
		/// </value>
		public byte IPQ { get; set; }

		/// <summary>
		/// количество вышедших пассажиров
		/// </summary>
		/// <value>
		/// The opq.
		/// </value>
		public byte OPQ { get; set; }
	}
}
