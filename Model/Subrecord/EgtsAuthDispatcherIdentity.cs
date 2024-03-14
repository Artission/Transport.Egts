using Transport.EGTS.Enum;

namespace Transport.EGTS.Model.Subrecord
{
	/// <summary>
	/// 
	/// </summary>
	public class EgtsAuthDispatcherIdentity : ISubrecordData
	{
		#region Implementation of ISubrecordData

		/// <summary>
		/// Subrecord Type
		/// </summary>
		/// <value>
		/// The SubrecordType.
		/// </value>
		public EgtsSubrecordType SubrecordType
		{
			get { return EgtsSubrecordType.DispatcherIdentity;}
		}

		#endregion

		/// <summary>
		/// тип диспетчера;
		/// </summary>
		/// <value>
		/// The dt.
		/// </value>
		public byte DT { get; set; }

		/// <summary>
		///  уникальный идентификатор диспетчера
		/// </summary>
		/// <value>
		/// The did.
		/// </value>
		public uint DID { get; set; }

		/// <summary>
		/// Gets or sets the DSCR.
		/// </summary>
		/// <value>
		/// The DSCR.
		/// </value>
		public string DSCR { get; set; }
	}
}
