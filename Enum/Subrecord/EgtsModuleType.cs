
namespace Transport.EGTS.Enum.Subrecord
{
	/// <summary>
	/// 
	/// </summary>
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1008:EnumsShouldHaveZeroValue")]//обязательно к заполнению, 0 неприемлимо
	public enum EgtsModuleType
	{
		/// <summary>
		/// основной модуль
		/// </summary>
		Main = 1,

		/// <summary>
		/// модуль ввода вывода
		/// </summary>
		IO = 2,

		/// <summary>
		/// модуль навигационного приёмника
		/// </summary>
		Navigation = 3,

		/// <summary>
		/// модуль беспроводной связи
		/// </summary>
		WirelessConnection = 4
	}
}
