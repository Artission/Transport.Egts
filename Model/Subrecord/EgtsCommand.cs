using System.Collections.Generic;
using Transport.EGTS.Enum.Subrecord;

namespace Transport.EGTS.Model.Subrecord
{
	/// <summary>
	/// сама команда
	/// egts_COMMAND_t
	/// </summary>
	public class EgtsCommand
	{
		/// <summary>
		/// Gets or sets the Address.
		/// </summary>
		/// <value>
		/// The adr.
		/// </value>
		public ushort ADR { get; set; }

		/// <summary>
		/// Gets or sets the size.
		/// может быть не задано для подтверждения
		/// </summary>
		/// <value>
		/// The sz.
		/// </value>
		public byte SZ { get; set; }

		/// <summary>
		/// Gets or sets the action.
		/// отсутсвует для подтверждения
		/// </summary>
		/// <value>
		/// The act.
		/// </value>
		public EgtsCommandAction? ACT { get; set; }
		
		/// <summary>
		/// Gets or sets the command code.
		/// </summary>
		/// <value>
		/// The CCD.
		/// </value>
		public EgtsCommandCode CCD { get; set; }

		/// <summary>
		/// Gets the additional data.
		/// должны быть данный по документашки, но в сырцах не заполняется, и вообще комменчено
		/// </summary>
		/// <value>
		/// The additional data.
		/// </value>
		public List<byte> AdditionalData
		{
			get { return _addData; }
		}
		private readonly List<byte> _addData = new List<byte>();
	}
}
