using Transport.EGTS.Enum;
using Transport.EGTS.Enum.Subrecord;

namespace Transport.EGTS.Model.Subrecord
{
	/// <summary>
	/// доп данные по местоположению
	/// egts_TELEDATA_EXT_POS_DATA_t
	/// </summary>
	public class EgtsExtraPositionalData : ISubrecordData
	{
		/// <summary>
		/// Subrecord Type
		/// </summary>
		/// <value>
		/// The SubrecordType.
		/// </value>
		public EgtsSubrecordType SubrecordType
		{
			get { return EgtsSubrecordType.ExtraPositionalData; }
		}
		#region mandatory bitfields
		/// <summary>
		/// (Navigation System Field Exists) определяет наличие данных о типах используемых навигационных спутниковых систем:
		/// </summary>
		public bool NSFE { get; set; }
		/// <summary>
		/// (Satellites Field Exists) определяет наличие данных о текущем количестве видимых спутников SAT и типе используемой навигационной спутниковой системы NS
		/// </summary>
		public bool SFE { get; set; }
		/// <summary>
		/// PFE - (PDOP Field Exists) определяет наличие поля PDOP
		/// </summary>
		public bool PFE { get; set; }
		/// <summary>
		/// (HDOP Field Exists) определяет наличие поля HDOP
		/// </summary>
		public bool HFE { get; set; }
		/// <summary>
		/// (VDOP Field Exists) определяет наличие поля VDOP:
		/// </summary>
		public bool VFE { get; set; }

		#endregion mandatory bitfields

		/// <summary>
		/// снижение точности в вертикальной плоскости (значение, умноженное на 100);
		/// </summary>
		public ushort VDOP { get; set; }

		/// <summary>
		/// снижение точности в горизонтальной плоскости (значение, умноженное на 100);
		/// </summary>
		public ushort HDOP { get; set; }

		/// <summary>
		/// снижение точности по местоположению (значение, умноженное на 100);
		/// </summary>
		public ushort PDOP { get; set; }

		/// <summary>
		/// количество видимых спутников;
		/// </summary>
		public byte SAT { get; set; }

		/// <summary>
		/// битовые флаги, характеризующие используемые навигационные спутниковые системы
		/// </summary>
		public EgtsNavigationSystems NS { get; set; }
	}
}
