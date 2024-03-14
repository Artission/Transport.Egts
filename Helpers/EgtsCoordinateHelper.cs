using System;

namespace Transport.EGTS.Helpers
{
	/// <summary>
	/// хелпер который позволяет взять целые координаты из трековых данных и обратить их в нормальные, и наоборот
	/// </summary>
	public static class EgtsCoordinateHelper
	{
		/// <summary>
		/// Uints the latitude2 double.
		/// </summary>
		/// <param name="latitude">The latitude.</param>
		/// <returns></returns>
		public static double UintLatitude2Double(uint latitude)
		{
			var latp = ((long) latitude*90);
			var mod = (double) 0xFFFFFFFF;
			return latp/mod;
		}

		/// <summary>
		/// Uints the longitude2 double.
		/// </summary>
		/// <param name="longitude">The longitude.</param>
		/// <returns></returns>
		public static double UintLongitude2Double(uint longitude)
		{
			return ((long)longitude * 180) / (double)0xFFFFFFFF;
		}

		/// <summary>
		/// Doubles the latitude2 uint.
		/// </summary>
		/// <param name="latitude">The latitude.</param>
		/// <returns></returns>
		public static uint DoubleLatitude2Uint(double latitude)
		{
			return (uint) ((Math.Abs(latitude)/90.0)*0xFFFFFFFF);
		}

		/// <summary>
		/// Doubles the longitude2 uint.
		/// </summary>
		/// <param name="longitude">The longitude.</param>
		/// <returns></returns>
		public static uint DoubleLongitude2Uint(double longitude)
		{
			return (uint)((Math.Abs(longitude) / 180.0) * 0xFFFFFFFF);
		}
	}
}
