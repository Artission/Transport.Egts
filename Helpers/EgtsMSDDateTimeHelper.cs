using System;


namespace Transport.EGTS.Helpers
{
	/// <summary>
	/// Класс для заворачивания даты в инт, и наоборот, по протоколу ЕГТС для МНД(MSD)
	/// </summary>
	public class EgtsMSDDateTimeHelper
	{
		#region fields

		private DateTime _epoche = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		#endregion fields

		#region Methods

		/// <summary>
		/// Datetime to uint.
		/// </summary>
		/// <param name="date">The date.</param>
		/// <returns></returns>
		public uint DateTime2UInt(DateTime date)
		{
			var dt = date.ToUniversalTime();
			var ret = (uint)dt.Subtract(_epoche).TotalSeconds;
			return ret;
		}

		/// <summary>
		/// uint to datetime.
		/// </summary>
		/// <param name="secs">The secs.</param>
		/// <returns>DateTime in UTC</returns>
		public DateTime UInt2DateTime(uint secs)
		{
			var ret = _epoche.AddSeconds(secs);
			return ret;
		}

		#endregion Methods
	}
}
