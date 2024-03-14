
using System.Collections.Generic;
using Transport.EGTS.Enum;
using Transport.EGTS.Enum.Subrecord;

namespace Transport.EGTS.Model.Subrecord
{
	/// <summary>
	/// Модель данных команды
	/// egts_COMMANDS_COMMAND_DATA_t
	/// </summary>
	public class EgtsCommandData : ISubrecordData
	{
		/// <summary>
		/// Subrecord Type
		/// </summary>
		/// <value>
		/// The SubrecordType.
		/// </value>
		public EgtsSubrecordType SubrecordType { get { return EgtsSubrecordType.CommandData; } }

		#region mandatory header part

		/// <summary>
		/// Gets or sets the command type.
		/// </summary>
		/// <value>
		/// The ct.
		/// </value>
		public EgtsCommandType CT { get; set; }

		/// <summary>
		/// Gets or sets the Command Confirmation Type.
		/// </summary>
		/// <value>
		/// The CCT.
		/// </value>
		public EgtsCommandConfirmationType CCT { get; set; }
		
		/// <summary>
		/// Command ID
		/// </summary>
		/// <value>
		/// The cid.
		/// </value>
		public uint CID { get; set; }

		/// <summary>
		/// Source ID
		/// идентификатор отправителя (уровня прикладного ПО) данной команды или подтверждения
		/// </summary>
		/// <value>
		/// The sid.
		/// </value>
		public uint SID { get; set; }

		/// <summary>
		/// (Authorization Code Field Exists) битовый флаг, определяющий наличие полей ACL и AC в подзаписи
		/// </summary>
		/// <value>
		///   <c>true</c> if acfe; otherwise, <c>false</c>.
		/// </value>
		public bool ACFE { get; set; }

		/// <summary>
		/// (Charset Field Exists) битовый флаг, определяющий наличие поля CHS в подзаписи
		/// </summary>
		/// <value>
		///   <c>true</c> if chsfe; otherwise, <c>false</c>.
		/// </value>
		public bool CHSFE { get; set; }

		#endregion mandatory header part

		#region optional part

		/// <summary>
		/// кодировка символов, используемая в поле CD, содержащем тело команды. При отсутствии данного поля по умолчанию используется кодировка CP-1251.
		/// </summary>
		/// <value>
		/// The CHS.
		/// </value>
		public EgtsSymbolEncoding? CHS { get; set; }

		/// <summary>
		/// authorization code length
		/// </summary>
		/// <value>
		/// The acl.
		/// </value>
		public byte? ACL { get; set; }

		/// <summary>
		/// Gets the authorization code data.
		/// AC
		/// </summary>
		/// <value>
		/// The authorization code data.
		/// </value>
		public List<byte> AuthorizationCodeData
		{
			get { return _authData; }
		}
		private readonly List<byte> _authData = new List<byte>();


		#endregion optional part

		#region parsed command data (optional

		/// <summary>
		/// Gets or sets the command.
		/// </summary>
		/// <value>
		/// The command.
		/// </value>
		public EgtsCommand Command { get; set; }

		#endregion parsed command data (optional

	}
}
