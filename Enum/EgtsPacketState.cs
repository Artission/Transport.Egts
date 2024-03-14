namespace Transport.EGTS.Enum
{
	/// <summary>
	/// 
	/// </summary>
	public enum EgtsPacketState
	{

		/// <summary>
		/// unknown
		/// est_unknown
		/// </summary>
		Unknown = 0,

		/// <summary>
		/// begin
		/// est_begin
		/// </summary>
		Begin = 1,

		/// <summary>
		/// PRV - protocol version set
		/// est_PRV
		/// </summary>
		PRV = 2,

		/// <summary>
		/// SKID - Security Key ID set
		/// est_SKID
		/// </summary>
		SKID = 3,

		/// <summary>
		/// BITS - bit fields set
		/// est_BITS
		/// </summary>
		BITS = 4,

		/// <summary>
		/// HL - header length set
		/// est_HL
		/// </summary>
		HL = 5,

		/// <summary>
		/// HE - header encoding
		/// est_HE
		/// </summary>
		HE,

		/// <summary>
		/// HCS
		/// est_HCS
		/// </summary>
		HCS,

		/// <summary>
		/// SFRD
		/// est_SFRD
		/// </summary>
		SFRD,

		/// <summary>
		/// SFRCS
		/// est_SFRCS
		/// </summary>
		SFRCS,

		/// <summary>
		/// Собственный флажок, для отметки что пакет полностью закончен
		/// </summary>
		Proceeded
	}
}
