using Transport.EGTS.Enum;
using Transport.EGTS.Enum.Subrecord;

namespace Transport.EGTS.Model
{
	/// <summary>
	/// описанные поля в записях в данных пакета
	/// </summary>
	public class EgtsRecordHeader
	{
		/// <summary>
		/// Record Length 
		/// при запарсивании пакета в байтики, заполняется автоматически
		/// </summary>
		public ushort RL { get; set; }

		/// <summary>
		///  Record Number 
		/// </summary>
		public ushort RN { get; set; }


		#region Bitfields

		/// <summary>
		///  1  Source Service On Device 
		/// я названия в хедерах сохранил, но это фактически определяет отправителя
		/// если тру то отправитель АС
		/// </summary>
		public bool SSOD { get; set; }

		/// <summary>
		///  1  Recipient Service On Device 
		/// я названия в хедерах сохранил, но это фактически определяет получателя
		/// если тру то получатель это АС
		/// </summary>
		public bool RSOD { get; set; }

		/// <summary>
		///  1  Group 
		/// если выставлено то данные принадлежат группе код которой указан в OID
		/// </summary>
		public bool GRP { get; set; }

		/// <summary>
		///  2  Record Processing Priority 
		/// </summary>
		public EgtsProcessingPriority RPP { get; set; }

		/// <summary>
		///  1  Time Field Exists 
		/// задано ли поле TM
		/// </summary>
		public bool TMFE { get; set; }

		/// <summary>
		///  1  Event ID Field  Exists 
		/// задано ли поле EVID
		/// </summary>
		public bool EVFE { get; set; }

		/// <summary>
		///  1  Object ID  Field Exists 
		/// задано ли поле OID
		/// </summary>
		public bool OBFE { get; set; }

		#endregion Bitfields

		#region optional part

		/// <summary>
		///  Object Identifier, optional , switched on by OBFE 
		/// </summary>
		public uint? OID { get; set; }

		/// <summary>
		///  Event Identifier, optional , switched on by EVFE 
		/// </summary>
		public uint? EVID { get; set; }

		/// <summary>
		///  Time, optional , seconds from 00:00:00 01.01.2010 UTC , switched on by TMFE 
		/// </summary>
		public uint? TM { get; set; }

		#endregion optional part

		/// <summary>
		///  Source Service Type 
		/// </summary>
		public EgtsServiceType SST { get; set; }

		/// <summary>
		///  Recipient Service Type 
		/// </summary>
		public EgtsServiceType RST { get; set; }

	}
}
