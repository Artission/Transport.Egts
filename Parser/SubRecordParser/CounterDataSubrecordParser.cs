using Transport.EGTS.Model.Subrecord;
using br = Transport.EGTS.Helpers.ByteCombineHelper;
using Transport.EGTS.Enum;
using Transport.EGTS.Enum.Subrecord;

namespace Transport.EGTS.Parser.SubRecordParser
{
	/// <summary>
	/// 
	/// </summary>
	internal class CounterDataSubrecordParser: ISubrecordParser
	{
		#region Implementation of ISubrecordParser

		/// <summary>
		/// Gets or sets the SRT.
		/// </summary>
		/// <value>
		/// The SRT.
		/// </value>
		public EgtsSubrecordType SRT
		{
			get { return EgtsSubrecordType.CountersData; }
		}

		/// <summary>
		/// Поскольку в документации всё делится на конкретно описанные сервиса
		/// (и в передаче данных тоже есть инфа именно по описанным сервисам, а не отдельным парсером как тут)
		/// надо пометить парсер типом (неизвестно, пригодится ли, но...)
		/// </summary>
		/// <value>
		/// The type of the service.
		/// </value>
		public EgtsServiceType ServiceType
		{
			get { return EgtsServiceType.TeleData; }
		}

		/// <summary>
		/// Reads the sub record from bytes.
		/// </summary>
		/// <param name="srl">The subrecord data length.</param>
		/// <param name="dataArray">The data array.</param>
		/// <param name="remainsize">The remainsize.</param>
		/// <param name="offset">The offset.</param>
		/// <returns></returns>
		public ISubrecordData ReadSubRecordFromBytes(ushort srl, byte[] dataArray, ref ushort remainsize, ref ushort offset)
		{
			var ret = new EgtsCountersData();
			byte byt = 0;
			if (!br.ReadNextByteBytes(ref byt, dataArray, ref remainsize, ref offset))
				return null;
			parseBitsToBools(ret.CFE, 0, byt);

			for (int i = 0; i < 8; i++)
			{
				if (!ret.CFE[i])
					continue;
				const ushort usSize = 3;	//там прям в документашке, не полный улонг а трёхбайтовый
				ulong uval = 0;
				if (!br.ReadValueFromBuffer(ref uval, usSize, dataArray, ref remainsize, offset))
					return null;
				offset += usSize;
				ret.CN[i] = (uint)uval;
			}
			return ret;
		}

		/// <summary>
		/// Writes the subrecord to bytes.
		/// SRL заполняется тут же
		/// </summary>
		/// <param name="subrecord">The subrecord.</param>
		/// <returns></returns>
		/// <exception cref="System.NotImplementedException"></exception>
		public byte[] WriteSubrecordToBytes(ISubrecordData subrecord)
		{
			throw new System.NotImplementedException();
		}

		#endregion

		#region private methods

		private void parseBitsToBools(bool[] arrToFill, int startIndex, byte b)
		{
			for (int i = 0; i < 8; i++)
			{
				arrToFill[startIndex + i] = (b & 0x01) == 1;
				b >>= 1;
			}
		}

		#endregion private methods

	}
}
