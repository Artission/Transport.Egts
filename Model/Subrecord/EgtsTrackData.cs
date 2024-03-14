using System.Collections.Generic;
using Transport.EGTS.Enum;

namespace Transport.EGTS.Model.Subrecord
{
	/// <summary>
	/// данные трека
	/// egts_ECALL_TRACK_DATA_t
	/// </summary>
	public class EgtsTrackData : ISubrecordData
	{
		/// <summary>
		/// Subrecord Type
		/// </summary>
		/// <value>
		/// The SubrecordType.
		/// </value>
		public EgtsSubrecordType SubrecordType { get { return EgtsSubrecordType.TrackData; } }

		/// <summary>
		/// Gets or sets the Structures Amount.
		/// </summary>
		/// <value>
		/// The sa.
		/// </value>
		public byte SA { get; set; }

		/// <summary>
		/// Gets or sets the Absolute Time.
		/// </summary>
		/// <value>
		/// The atm.
		/// </value>
		public uint ATM { get; set; }

		/// <summary>
		/// Gets or sets the track items.
		/// </summary>
		/// <value>
		/// The track items.
		/// </value>
		public List<EgtsTrackItem> TrackItems { get { return _trackItems; } }
		private readonly List<EgtsTrackItem> _trackItems = new List<EgtsTrackItem>();
	}
}
