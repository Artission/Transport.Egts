using System.Collections.Generic;
using Transport.EGTS.Enum;

namespace Transport.EGTS.Model.Subrecord
{
	/// <summary>
	/// параметры аутентификации
	/// egts_AUTH_AUTH_PARAMS_t
	/// </summary>
	public class EgtsAuthParameters : ISubrecordData
	{
		/// <summary>
		/// Subrecord Type
		/// </summary>
		/// <value>
		/// The SubrecordType.
		/// </value>
		public EgtsSubrecordType SubrecordType { get { return EgtsSubrecordType.AuthParams; } }

		#region bitfields

		/// <summary>
		/// битовый флаг, определяет наличие поля EXP и следующего за ним разделителя D (если 1, то поля присутствуют)
		/// </summary>
		public bool EXE { get; set; }

		/// <summary>
		/// битовый флаг, определяет наличие поля SS и следующего за ним разделителя D (если 1, то поля присутствуют)
		/// </summary>
		public bool SSE { get; set; }

		/// <summary>
		/// битовый флаг, определяет наличие поля MSZ (если 1, то поле присутствует)
		/// </summary>
		public bool MSE { get; set; }

		/// <summary>
		/// битовый флаг, определяет наличие поля ISL (если 1, то поле присутствует)
		/// </summary>
		public bool ISLE { get; set; }

		/// <summary>
		/// битовый флаг, определяет наличие полей PKL и PBK (если 1, то поля присутствуют)
		/// </summary>
		public bool PKE { get; set; }

		/// <summary>
		/// битовое поле, определяющее требуемый алгоритм шифрования пакетов. Если данное поле содержит значение 0 0, 
		/// то шифрование не применяется, и подзапись EGTS_SR_AUTH_PARAMS содержит только один байт, иначе, в зависимости от типа алгоритма, 
		/// наличие дополнительных параметров определяется остальными битами поля FLG
		///		Странное поле, в документашке значится 2битовым, в сырцах 1битовым, скорее всего как и в самом пакете, подразумевается тоже шифрование
		///	потому что и там и там есть лишь сравнение с 0, и никакой дешифровки не поддерживается
		/// </summary>
		public byte ENA { get; set; }

		#endregion bitfields

		#region optional part

		/// <summary>
		/// Public Key Length , optional , switched on by PKE 
		/// </summary>
		public ushort PKL { get; set; }

		/// <summary>
		/// Public Key , optional , switched on by PKE 
		/// </summary>
		public List<byte> PublicKeyBytes
		{
			get { return _pbkData; }
		}
		private readonly List<byte> _pbkData = new List<byte>();

		/// <summary>
		/// Identity String Length , optional , switched on by ISLE
		/// </summary>
		public ushort ISL { get; set; }

		/// <summary>
		/// Mod Size , optional , switched on by MSE
		/// </summary>
		public ushort MSZ { get; set; }

		/// <summary>
		/// Server Sequence, optional , switched on by SSE 
		/// </summary>
		public string SS { get; set; }

		/// <summary>
		/// Exp (Expression?) optional , switched on by EXE 
		/// </summary>
		public string EXP { get; set; }

		#endregion optional part
	}
}
