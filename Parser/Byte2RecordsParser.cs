using System.Collections.Generic;
using Transport.EGTS.Enum;
using Transport.EGTS.Enum.Subrecord;
using Transport.EGTS.Exceptions;
using Transport.EGTS.Model;
using Transport.EGTS.Parser.SubRecordParser;
using br = Transport.EGTS.Helpers.ByteCombineHelper;

namespace Transport.EGTS.Parser
{
	/// <summary>
	/// Парсер уже развёрнутых данных в рекорды
	/// </summary>
	internal class Byte2RecordsParser
	{
		#region fields

		private readonly Dictionary<EgtsSubrecordType, ISubrecordParser> _subrecordParsers = new Dictionary
			<EgtsSubrecordType, ISubrecordParser>
		{
			{EgtsSubrecordType.CommandData, new CommandDataSubrecordParser()},
			{EgtsSubrecordType.Response, new ResponceSubrecordParser()},
			{EgtsSubrecordType.AccelData, new AccelerometerDataSubrecordParser()},
			{EgtsSubrecordType.MSDData, new MinimalSetDataSubrecordParser()},
			{EgtsSubrecordType.RawMSDData, new RawMSDSubrecordParser()},
			{EgtsSubrecordType.TrackData, new TrackDataSubrecordParser()},
			{EgtsSubrecordType.FirmwareFullData, new FirmwareFullDataSubrecordParser()},
			{EgtsSubrecordType.FirmwarePartData, new FirmwarePartDataSubrecordParser()},
			{EgtsSubrecordType.AuthInfo, new AuthInfoSubrecordParser()},
			{EgtsSubrecordType.AuthParams, new AuthParametersSubrecordParser()},
			{EgtsSubrecordType.DispatcherIdentity, new AuthDispatcherIdentitySubrecordParser()},
			{EgtsSubrecordType.ModuleData, new AuthTerminalModuleDataSubrecordParser()},
			{EgtsSubrecordType.TerminalIdentity, new AuthTerminalIdentitySubrecordParser()},
			{EgtsSubrecordType.VehicleData, new AuthVehicleDataSubrecordParser()},
			{EgtsSubrecordType.ServiceInfo, new AuthServiceInfoSubrecordParser()},
			{EgtsSubrecordType.ResultCode, new AuthResultSubrecordParser()},
			{EgtsSubrecordType.PositionalData, new PositionalDataSubrecordParser()},
			{EgtsSubrecordType.AnalogDigitalSensorData, new AnalogDigitalDataSubrecordParser()},
			{EgtsSubrecordType.ExtraPositionalData, new ExtraPositionalDataSubrecordParser()},
			{EgtsSubrecordType.CountersData, new CounterDataSubrecordParser()},
			{EgtsSubrecordType.TerminalState, new TerminalStateDataSubrecordParser()},
			{EgtsSubrecordType.LoopbackInputData, new LoopbackInputDataParser()},
			{EgtsSubrecordType.SingleDigitalSensorData, new SingleDigitalSensorSubrecordParser()},
			{EgtsSubrecordType.SingleAnalogSensorData, new SingleAnalogSensorSubrecordParser()},
			{EgtsSubrecordType.SingleCounterData, new SingleCounterSubrecordParser()},
			{EgtsSubrecordType.SingleLoopbackInputData, new SingleLoopbackInputSubrecordParser()},
			{EgtsSubrecordType.LiquidLevelSensorData, new LiquidLevelSensorDataSubrecordParser()},
			{EgtsSubrecordType.PassengerCount, new PassengerCountDataSubrecordParser()}
		};

		#endregion fields

		/// <summary>
		/// Parses the next record.
		/// </summary>
		/// <param name="packet">The packet.</param>
		/// <param name="offset">The offset.</param>
		/// <param name="remainsize">The remainsize.</param>
		internal bool ParseNextRecord(EgtsPacket packet, ref ushort offset, ref ushort remainsize)
		{
			ushort val = 0;
			byte btval = 0;
			uint ival = 0;
			var dataArr = packet.DataBytes.ToArray();
			var record = new EgtsPacketRecord();
			
			if (br.ReadNextUshortBytes(ref val, dataArr, ref remainsize, ref offset))
				record.RecordHeader.RL = val;
			if (br.ReadNextUshortBytes(ref val, dataArr, ref remainsize, ref offset))
				record.RecordHeader.RN = val;

			if (br.ReadNextByteBytes(ref btval, dataArr, ref remainsize, ref offset))
				parseRecordBitFields(record,btval);

			if (record.RecordHeader.OBFE)
			{
				if (br.ReadNextUintBytes(ref ival, dataArr, ref remainsize, ref offset))
					record.RecordHeader.OID = ival;
			}
			if (record.RecordHeader.EVFE)
			{
				if (br.ReadNextUintBytes(ref ival, dataArr, ref remainsize, ref offset))
					record.RecordHeader.EVID = ival;
			}
			if (record.RecordHeader.TMFE)
			{
				if (br.ReadNextUintBytes(ref ival, dataArr, ref remainsize, ref offset))
					record.RecordHeader.TM = ival;
			}

			if (br.ReadNextByteBytes(ref btval, dataArr, ref remainsize, ref offset))
				record.RecordHeader.SST = (EgtsServiceType)btval;
			if (br.ReadNextByteBytes(ref btval, dataArr, ref remainsize, ref offset))
				record.RecordHeader.RST = (EgtsServiceType)btval;

			if (record.RecordHeader.RL > remainsize)
			{
				throw new EgtsProceedException("Invalid Record Length");
			}
			
			packet.Records.Add(record);
			// TODO Проверка максимального количества сабрекордов технически есть, но был тут пакет, странный по составу но CRC сошлись, 
			// и записи распарсились идеально, и их было ровно 59*2, прям именно так, будто парами шли точка+датчики возможно это количество должно считаться не так
			//var totalSubrecordsCount = packet.Records.Sum(t => t.SubRecords.Count);
			var recordRemainSize = record.RecordHeader.RL;
			remainsize -= recordRemainSize;
			while (recordRemainSize != 0)
			{
				if (!parseNextSubRecord(record, dataArr, ref recordRemainSize, ref offset))
					return false;

				/*	totalSubrecordsCount++;
				if (totalSubrecordsCount >= Constants.MaxPacketSubrecordsCount)
				{
					throw new EgtsProceedException("Too many subrecords");
				}*/
			}
			return true;
		}

		/// <summary>
		/// Writes the next record.
		/// </summary>
		/// <param name="record">The record.</param>
		/// <returns></returns>
		internal byte[] WriteNextRecord(EgtsPacketRecord record)
		{
			var ret = new List<byte>();

			var ushSize = sizeof (ushort);
			var bytSize = sizeof (byte);
			var uinSize = sizeof (uint);
			byte[] arr;

			#region parse data
			// заранее данные расчитываем, чтобы длину записать
			var rd = new List<byte>();
			foreach (var subRecord in record.SubRecords)
			{
				var parser = getSubrecordParser(subRecord.SRD.SubrecordType);
				if (parser == null)
					throw new EgtsProceedException(string.Format("No parser for this data type {0}", subRecord.SRD.SubrecordType));
				subRecord.SRT = parser.SRT;

				var arrD = parser.WriteSubrecordToBytes(subRecord.SRD);
				if (arrD == null)
					throw new EgtsProceedException("Error writing subrecord to bytes");
				subRecord.SRL = (ushort)arrD.Length;

				arr = br.WriteValueToBuffer((byte)subRecord.SRT, bytSize);
				if (arr == null)
					throwBytingRecordError();
				rd.AddRange(arr);
				arr = br.WriteValueToBuffer(subRecord.SRL, ushSize);
				if (arr == null)
					throwBytingRecordError();
				rd.AddRange(arr);

				rd.AddRange(arrD);
			}
			record.RecordHeader.RL = (ushort) rd.Count;
			#endregion parse data

			arr = br.WriteValueToBuffer(record.RecordHeader.RL, ushSize);
			if (arr==null)
				throwBytingRecordError();
			ret.AddRange(arr);

			arr = br.WriteValueToBuffer(record.RecordHeader.RN, ushSize);
			if (arr == null)
				throwBytingRecordError();
			ret.AddRange(arr);
			arr = br.WriteValueToBuffer(byteRecordBitfields(record), bytSize);
			if (arr == null)
				throwBytingRecordError();
			ret.AddRange(arr);

			if (record.RecordHeader.OBFE)
			{
				if (record.RecordHeader.OID==null)
					throw new EgtsProceedException("OBFE flag set to true, but OID is null");
				arr = br.WriteValueToBuffer(record.RecordHeader.OID.Value, uinSize);
				if (arr == null)
					throwBytingRecordError();
				ret.AddRange(arr);
			}
			if (record.RecordHeader.EVFE)
			{
				if (record.RecordHeader.EVID == null)
					throw new EgtsProceedException("EVFE flag set to true, but EVID is null");
				arr = br.WriteValueToBuffer(record.RecordHeader.EVID.Value, uinSize);
				if (arr == null)
					throwBytingRecordError();
				ret.AddRange(arr);
			}
			if (record.RecordHeader.TMFE)
			{
				if (record.RecordHeader.TM == null)
					throw new EgtsProceedException("TMFE flag set to true, but TM is null");
				arr = br.WriteValueToBuffer(record.RecordHeader.TM.Value, uinSize);
				if (arr == null)
					throwBytingRecordError();
				ret.AddRange(arr);
			}

			arr = br.WriteValueToBuffer((byte)record.RecordHeader.SST, bytSize);
			if (arr == null)
				throwBytingRecordError();
			ret.AddRange(arr);
			arr = br.WriteValueToBuffer((byte)record.RecordHeader.RST, bytSize);
			if (arr == null)
				throwBytingRecordError();
			ret.AddRange(arr);
			
			ret.AddRange(rd);
			return ret.ToArray();
		}

		private void throwBytingRecordError()
		{
			throw new EgtsProceedException("Error in parsing record into bytes");
		}

		#region private methods

		private bool parseNextSubRecord(EgtsPacketRecord record, byte[] dataArray, ref ushort remainsize, ref ushort offset)
		{
			var subRecord = new EgtsPacketSubRecord();
			ushort usval = 0;
			byte btval = 0;
			if (br.ReadNextByteBytes(ref btval, dataArray, ref remainsize, ref offset))
				subRecord.SRT = (EgtsSubrecordType) btval;

			if (br.ReadNextUshortBytes(ref usval, dataArray, ref remainsize, ref offset))
				subRecord.SRL = usval;

			if (subRecord.SRL>remainsize)
				throw new EgtsProceedException("Too big subrecord length", subRecord.SRT);

			var parser = getSubrecordParser(subRecord.SRT);

			if (parser==null)
			{
				return false;
				//throw new EgtsProceedException("No parser for this data type ", subRecord.SRT);
				
			}

			subRecord.SRD = parser.ReadSubRecordFromBytes(subRecord.SRL, dataArray, ref remainsize, ref offset);
			if (subRecord.SRD == null)
				throw new EgtsProceedException("Wrong data bytes, not parsed.", subRecord.SRT);

			record.SubRecords.Add(subRecord);
			return true;
		}

		private ISubrecordParser getSubrecordParser(EgtsSubrecordType type)
		{
			if (!_subrecordParsers.ContainsKey(type))
				return null;
			return _subrecordParsers[type];
		}

		private void parseRecordBitFields(EgtsPacketRecord precord, byte b)
		{
			precord.RecordHeader.OBFE = (b & 0x01) == 1;
			b >>= 1;
			precord.RecordHeader.EVFE = (b & 0x01) == 1;
			b >>= 1;
			precord.RecordHeader.TMFE = (b & 0x01) == 1;
			b >>= 1;
			precord.RecordHeader.RPP = (EgtsProcessingPriority)(b & 0x03);
			b >>= 2;
			precord.RecordHeader.GRP = (b & 0x01) == 1;
			b >>= 1;
			precord.RecordHeader.RSOD = (b & 0x01) == 1;
			b >>= 1;
			precord.RecordHeader.SSOD = (b & 0x01) == 1;
		}

		private byte byteRecordBitfields(EgtsPacketRecord record)
		{
			byte ret = 0;
			ret |= (byte)((record.RecordHeader.OBFE ? 1 : 0));
			var offset = 1;
			ret |= (byte) ((record.RecordHeader.EVFE ? 1 : 0) << offset);
			offset++;
			ret |= (byte)((record.RecordHeader.TMFE ? 1 : 0) << offset);
			offset++;
			ret |= (byte)(((byte)record.RecordHeader.RPP) << offset);
			offset+=2;
			ret |= (byte)((record.RecordHeader.GRP ? 1 : 0) << offset);
			offset++;
			ret |= (byte)((record.RecordHeader.RSOD ? 1 : 0) << offset);
			offset++;
			ret |= (byte)((record.RecordHeader.SSOD ? 1 : 0) << offset);
			
			return ret;
		}

		#endregion private methods
	}
}
