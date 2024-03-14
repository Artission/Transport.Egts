using System;

namespace Transport.EGTS.Helpers
{
	/// <summary>
	/// конвертация uint в координаты, и наоборот
	/// </summary>
	public class EgtsMSDCoordinateHelper
	{
		/// <summary>
		/// Uint2s the coordinate absolute.
		/// PLAT, PLON
		/// </summary>
		/// <param name="coord">The coord.</param>
		/// <returns></returns>
		public double Uint2CoordinateAbsolute(uint coord)
		{
			var dms = getDMSFromUintAbsolute(coord);
			var ms = dms.MiliSeconds/1000.0;
			var sec = (dms.Seconds + ms)/60.0;
			var min = (dms.Minutes + sec)/60.0;
			var deg = dms.Degrees + (dms.Degrees > 0 ? min : -min);
			return deg;
		}

		/// <summary>
		/// Uint2s the coordinate relative.
		/// RVP_Latd_1, RVPLATD2...etc
		///  В ОТНОСИТЕЛЬНЫХ КООРДИНАТАХ НЕТ ГРАДУСОВ!
		/// </summary>
		/// <param name="coord">The coord.</param>
		/// <returns></returns>
		public double UShort2CoordinateRelative(ushort coord)
		{
			var dms = getDMSFromUintRelative(coord);
			var ms = dms.MiliSeconds / 1000.0;
			var sec = (dms.Seconds + ms) / 60.0;
			var min = (Math.Abs(dms.Minutes) + sec) / 60.0;

			return dms.Minutes > 0 ? min : -min;
			//return dms.Degrees + dms.Minutes / 60 + dms.Seconds / 60 + dms.MiliSeconds / 1000;
		}

		/// <summary>
		/// Coordinate2s the uint absolute.
		/// </summary>
		/// <param name="coordinate">The coordinate.</param>
		/// <returns></returns>
		public uint Coordinate2UintAbsolute(double coordinate)
		{
			var dms = getDmsFromCoordinate(coordinate);
			if (dms.Degrees >= 0)
			{
				return (uint)(dms.Degrees * 3600000U + dms.Minutes * 60000U + dms.Seconds * 1000U + dms.MiliSeconds);
			}
			return ~((uint)((-dms.Degrees) * 3600000U + dms.Minutes * 60000U + dms.Seconds * 1000U + dms.MiliSeconds)) + 1U;
		}

		/// <summary>
		/// Coordinate2s the uint relative.
		/// </summary>
		/// <param name="coordinate">The coordinate.</param>
		/// <returns></returns>
		public ushort Coordinate2UshortRelative(double coordinate)
		{
			var dms = getDmsFromCoordinate(coordinate);
			if (dms.Minutes >= 0)
			{
				return (ushort)(dms.Minutes * 600U + dms.Seconds * 10U + dms.MiliSeconds / 100);
			}
			var tt = ~(ushort)((-dms.Minutes) * 600U + dms.Seconds * 10U + dms.MiliSeconds / 100) + 1;
			var ret = (ushort) tt;
			return ret;
		}

		#region private methods

		private DMSCoordinate getDMSFromUintAbsolute(uint mdsCoordinate)
		{
			var neg = false;
			if ((mdsCoordinate & 0x80000000U)!=0)
			{
				neg = true;
				mdsCoordinate = ~mdsCoordinate + 1;
			}
			var ret = new DMSCoordinate();
			ret.MiliSeconds = (ushort)(mdsCoordinate % 1000U);
			mdsCoordinate /= 1000;
			ret.Seconds = (byte)(mdsCoordinate % 60);
			mdsCoordinate /= 60;
			ret.Minutes = (short)(mdsCoordinate % 60);
			mdsCoordinate /= 60;

			if (neg)
				ret.Degrees = -(int)mdsCoordinate;
			else
				ret.Degrees = (int)mdsCoordinate;

			return ret;
		}

		private DMSCoordinate getDMSFromUintRelative(ushort mdsCoordinate)
		{
			var neg = false;
			if ((mdsCoordinate & 0x8000U)!=0)
			{
				neg = true;
				mdsCoordinate = (ushort)(~mdsCoordinate + 1);
			}
			var ret = new DMSCoordinate();
			ret.MiliSeconds = (ushort) ((mdsCoordinate%10U)*100);
			mdsCoordinate /= 10;
			ret.Seconds = (byte)(mdsCoordinate % 60);
			mdsCoordinate /= 60;

			if (neg)
				ret.Minutes = (short)(-(byte)mdsCoordinate);
			else
				ret.Minutes = (byte)mdsCoordinate;

			return ret;
		}

		private DMSCoordinate getDmsFromCoordinate(double coordinate)
		{
			/*
			north = Latitude >= 0;
			double a = Math.Abs(coordinate);
			latDeg = (int)Math.Floor(a);
			a -= latDeg;
			latMin = (int)Math.Floor(Math.Round(a * 60.0, 6));
			latSec = (Math.Round(a * 60.0, 6) - latMin) * 60.0;*/

			var ret = new DMSCoordinate();
			double a = Math.Abs(coordinate);
			var latDeg = (int)Math.Floor(a);
			ret.Degrees = coordinate > 0 ? latDeg : -latDeg;
			a -= latDeg;
			var rndMin = Math.Round(a*60.0, 9);
			ret.Minutes = (short) Math.Floor(rndMin);
			if (ret.Degrees == 0 && coordinate < 0)
				ret.Minutes = (short)-ret.Minutes;
			var minusMin = (rndMin - Math.Abs(ret.Minutes));
			ret.Seconds = (byte) (minusMin*60);
			var minusSec = minusMin * 60.0 - ret.Seconds;
			ret.MiliSeconds = (ushort)Math.Round(minusSec * 1000);
			return ret;
		}

		#endregion private methods


		#region private classes

		private class DMSCoordinate
		{
			public int Degrees { get; set; }
			public short Minutes { get; set; }
			public byte Seconds { get; set; }
			public ushort MiliSeconds { get; set; }
		}

		#endregion private classes
	}
}
