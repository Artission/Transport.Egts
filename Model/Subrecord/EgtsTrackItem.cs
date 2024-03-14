namespace Transport.EGTS.Model.Subrecord
{
	/// <summary>
	/// записи данных трека по ЕГТС
	/// egts_TRACK_t
	/// </summary>
	public class EgtsTrackItem
	{
		#region bitfields

		/// <summary>
		/// :1 Track Node Data Exist
		/// </summary>
		public bool TNDE { get; set; }

		/// <summary>
		/// битовый флаг определяет полушарие долготы:
		///	0 - восточная долгота;
		///	1 - западная долгота;
		/// </summary>
		public bool LOHS { get; set; }

		/// <summary>
		/// битовый флаг определяет полушарие широты:
		///	0 - северная широта;
		///	1 - южная широта;
		/// </summary>
		public bool LAHS { get; set; }

		/// <summary>
		/// :5 Relative Time, in 0.1 sec
		/// </summary>
		public byte RTM { get; set; }

		#endregion bitfields

		/// <summary>
		/// Latitude , degree,  (WGS - 84) / 90 * 0xFFFFFFFF
		/// </summary>
		public uint LAT { get; set; }

		/// <summary>
		/// Longitude , degree,  (WGS - 84) / 180 * 0xFFFFFFFF
		/// </summary>
		public uint LONG { get; set; }

		/// <summary>
		/// Speed, in 0.01 km/h
		/// </summary>
		public ushort SPD { get; set; }

		/// <summary>
		/// Direction , in degree
		/// </summary>
		public ushort DIR { get; set; }
	}
}
