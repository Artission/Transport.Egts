using Transport.EGTS.Enum;
using Transport.EGTS.Enum.Subrecord;

namespace Transport.EGTS.Model.Subrecord
{
	/// <summary>
	/// информация о поддерживаемом\запрашиваемом сервисе
	/// egts_AUTH_SERVICE_INFO_t
	/// </summary>
	public class EgtsAuthServiceInfo : ISubrecordData
	{
		/// <summary>
		/// Subrecord Type
		/// </summary>
		/// <value>
		/// The SRT.
		/// </value>
		public EgtsSubrecordType SubrecordType
		{
			get { return EgtsSubrecordType.ServiceInfo; }
		}
		/// <summary>
		/// ServiceType
		/// </summary>
		/// <value>
		/// The st.
		/// </value>
		public EgtsServiceType ST { get; set; }

		/// <summary>
		/// Service STate
		/// </summary>
		/// <value>
		/// The SST.
		/// </value>
		public EgtsServiceState SST { get; set; }

		#region bitfields

		/// <summary>
		/// false - поддерживаемый сервис, true - запрашиваемый
		/// </summary>
		/// <value>
		///   <c>true</c> if srva; otherwise, <c>false</c>.
		/// </value>
		public bool SRVA { get; set; }

		// пропуск 5 битов и...

		/// <summary>
		///  приоритет.
		/// </summary>
		/// <value>
		/// The SRVRP.
		/// </value>
		public EgtsProcessingPriority SRVRP { get; set; }

		#endregion bitfields
	}
}
