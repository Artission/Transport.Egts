
namespace Transport.EGTS.Enum.Subrecord
{
	/// <summary>
	/// 
	/// </summary>
	public enum EgtsCommandConfirmationType
	{
		/// <summary>
		/// успешное выполнение, положительный ответ
		/// CC_OK
		/// </summary>
		OK = 0,
		/// <summary>
		/// обработка завершилась ошибкой
		/// CC_ERROR
		/// </summary>
		Error = 1,
		/// <summary>
		/// команда не может быть выполнена по причине отсутствия в списке разрешенных (определенных протоколом) команд или отсутствия разрешения на выполнение данной команды
		/// CC_ILL
		/// </summary>
		Forbidden = 2,
		/// <summary>
		/// команда успешно удалена
		/// CC_DEL
		/// </summary>
		Deleted = 3,
		/// <summary>
		/// команда для удаления не найдена
		/// CC_NFOUND
		/// </summary>
		NotFoundToDelete = 4,
		/// <summary>
		/// успешное выполнение, отрицательный ответ
		/// CC_NCONF
		/// </summary>
		NegativeConfirmation = 5,
		/// <summary>
		/// команда передана на обработку, но для ее выполнения требуется длительное время (результат выполнения еще не известен)
		/// CC_INPROG
		/// </summary>
		Progress = 6
	}
}
