
using Transport.EGTS.Enum;
using Transport.EGTS.Enum.Subrecord;

namespace Transport.EGTS.Model.Subrecord
{
	/// <summary>
	/// информация о модуле
	/// egts_AUTH_TERM_MODULE_DATA_t
	/// </summary>
	public class EgtsAuthTerminalModuleData : ISubrecordData
	{
		/// <summary>
		/// Subrecord Type
		/// </summary>
		/// <value>
		/// The SubrecordType.
		/// </value>
		public EgtsSubrecordType SubrecordType { get { return EgtsSubrecordType.ModuleData; } }

		/// <summary>
		/// Module Type 
		/// </summary>
		public EgtsModuleType MT { get; set; }

		/// <summary>
		/// Vendor Identifier 
		/// </summary>
		public uint VID { get; set; }

		/// <summary>
		/// Firmware Version 
		/// </summary>
		public ushort FWV { get; set; }

		/// <summary>
		/// Software Version 
		/// </summary>
		public ushort SWV { get; set; }

		/// <summary>
		/// Modification 
		/// </summary>
		public byte MD { get; set; }

		/// <summary>
		/// State , 1=ON,0=OFF, >127=EGTS_PC_XXX 
		/// </summary>
		public EgtsProcessingState ST { get; set; }

		/// <summary>
		/// Serial Number , optional 
		/// </summary>
		public string SRN { get; set; }

		/// <summary>
		/// Description , optional 
		/// </summary>
		public string DSCR { get; set; }
	}
}
