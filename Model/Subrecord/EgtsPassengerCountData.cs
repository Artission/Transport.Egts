using System.Collections.Generic;
using Transport.EGTS.Enum;

namespace Transport.EGTS.Model.Subrecord
{
	/// <summary>
	/// количество пассажиров
	/// </summary>
	public class EgtsPassengerCountData:ISubrecordData
	{
		/// <summary>
		/// Gets the SubrecordType.
		/// </summary>
		/// <value>
		/// The SubrecordType.
		/// </value>
		public EgtsSubrecordType SubrecordType
		{
			get { return EgtsSubrecordType.PassengerCount; }
		}
		/// <summary>
		/// RDF (Raw Data Flag) - флаг, определяющий формат поля PCD данной подзаписи:
		/// 0 - поле PCD имеет формат, определяемый полем DPR (представлен в Таблице N 18);
		/// 1 - поле PCD содержит данные счетчика пассажиропотока в неизменном виде, как они поступили из внешнего порта абонентского терминала 
		/// (размер поля PD при этом определяется исходя из общей длины данной подзаписи и размеров расположенных перед PD полей).
		/// </summary>
		public bool RDF { get; set; }

		/// <summary>
		/// (Doors Presented) битовое поле, определяющее наличие счетчиков на дверях и структуру поля PCD 
		/// (бит 0 определяет наличие счетчика на 1-й двери, бит 1 на 2-й и т.д.). Если бит имеет значение 1, то счетчик используется, если 0 - не используется;
		/// </summary>
		/// <value>
		/// The DPR.
		/// </value>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
		public bool[] DPR { get { return _dpr; } }
		private readonly bool[] _dpr = new bool[8];

		/// <summary>
		/// (Doors Released) битовое поле, определяющее двери, которые открывались и закрывались при подсчете пассажиров 
		/// (например, 00000000 - ни одна из дверей не открывалась, 00000001 - открывалась только 1-я дверь, 00001001 - открывались 1-я и 4-я дверь);
		/// </summary>
		/// <value>
		/// The DRL.
		/// </value>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
		public bool[] DRL { get { return _drl; } }
		private readonly bool[] _drl = new bool[8];

		/// <summary>
		/// адрес модуля, данные от счетчиков пассажиропотока с которого поступили в абонентский терминал (номер внешнего порта абонентского терминала)
		/// </summary>
		/// <value>
		/// The maddr.
		/// </value>
		public ushort MADDR { get; set; }

		/// <summary>
		/// Gets the passenger count data.
		/// </summary>
		/// <value>
		/// The passenger count data.
		/// </value>
		public List<EgtsPassengerIOCountData> PassengerCountData
		{
			get { return _pcd; }
		}
		private readonly List<EgtsPassengerIOCountData> _pcd = new List<EgtsPassengerIOCountData>();
		
	}
}
