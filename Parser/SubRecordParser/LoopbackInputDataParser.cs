using br = Transport.EGTS.Helpers.ByteCombineHelper;
using Transport.EGTS.Enum;
using Transport.EGTS.Enum.Subrecord;
using Transport.EGTS.Model.Subrecord;

namespace Transport.EGTS.Parser.SubRecordParser
{
	internal class LoopbackInputDataParser: ISubrecordParser
	{
		#region Implementation of ISubrecordParser

		/// <summary>
		/// Gets or sets the SubrecordType.
		/// </summary>
		/// <value>
		/// The SubrecordType.
		/// </value>
		public EgtsSubrecordType SRT
		{
			get { return EgtsSubrecordType.LoopbackInputData; }
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
			var ret = new EgtsLoopbackInputData();

			byte byt = 0;
			if (!br.ReadNextByteBytes(ref byt, dataArray, ref remainsize, ref offset))
				return null;
			parseBitsToBools(ret.LIFE, byt);

			for (int i = 0; i < 4; i++)
			{
				var n = i*2;
				if (!ret.LIFE[n] && !ret.LIFE[n+1])
					continue;
				if (!br.ReadNextByteBytes(ref byt, dataArray, ref remainsize, ref offset))
					return null;
				ret.LIS[n] = combine4BitstoState(ref byt);
				ret.LIS[n + 1] = combine4BitstoState(ref byt);
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

		private void parseBitsToBools(bool[] arrToFill, byte b)
		{
			for (int i = 0; i < 8; i++)
			{
				arrToFill[i] = (b & 0x01) == 1;
				b >>= 1;
			}
		}

		private EgtsLoopInState combine4BitstoState(ref byte b)
		{
			var ret = EgtsLoopInState.Normal;

			if ((b & 0x01) == 1)
				ret |= EgtsLoopInState.Alarm;
			b >>= 1;
			if ((b & 0x01) == 1)
				ret |= EgtsLoopInState.Break;
			b >>= 1;
			if ((b & 0x01) == 1)
				ret |= EgtsLoopInState.EarthCircuit;
			b >>= 1;
			if ((b & 0x01) == 1)
				ret |= EgtsLoopInState.PowerCircuit;
			b >>= 1;

			return ret;
		}

		#endregion private methods
	}
}
