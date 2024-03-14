using System;
using System.Collections.Generic;
using System.IO;
using Transport.EGTS.Enum;
using Transport.EGTS.Exceptions;
using Transport.EGTS.Helpers;
using Transport.EGTS.Model;
using Transport.EGTS.Parser;
using br = Transport.EGTS.Helpers.ByteCombineHelper;

namespace Transport.EGTS
{
	/// <summary>
	/// Парсер байтиков в пакет 
	/// основной метод взят из файла egts_rx.c
	/// </summary>
	public class EgtsPacketReciever
	{
		#region fields

		#region contants

		private const int constMaxErrorCount = 100;

		#endregion contants

		private readonly List<byte> _buffer = new List<byte>();
		private int _currentReadPosition;
		private EgtsPacket _currentPacket;
		private readonly Queue<EgtsPacket> _processedPackets = new Queue<EgtsPacket>();
		
		private readonly object _procPacketLock = new object();
		private readonly object _bufferLock = new object();
		private readonly object _byteGotLock = new object();

		private readonly HeaderEncodingHelper _headerEncodingHelper = new HeaderEncodingHelper();
		private readonly DataCompressionHelper _compressionHelper = new DataCompressionHelper();
		private readonly DataEncodingHelper _encodingHelper = new DataEncodingHelper();

		private readonly CrcHelper _crcHelper = new CrcHelper();
		private readonly Byte2RecordsParser _recordsParser = new Byte2RecordsParser();

		#endregion fields

		#region Properties

		/// <summary>
		/// The version
		/// EGTS_VERSION
		/// </summary>
		public static byte Version {get { return (0x01); }} 

		/// <summary>
		/// Количество уже распарсенных пакетов, доставать с помощью GetPacket
		/// </summary>
		/// <value>
		/// The processed packets count.
		/// </value>
		public int ProcessedPacketsCount
		{
			get
			{
				lock (_procPacketLock)
				{
					return _processedPackets.Count;
				}
			}
		}

		/// <summary>
		/// Gets the length of the buffer.
		/// </summary>
		/// <value>
		/// The length of the buffer.
		/// </value>
		public int BufferLength
		{
			get
			{
				lock (_bufferLock)
				{
					return _buffer.Count;
				}
			}
		}

		/// <summary>
		/// Gets the state of the current packet.
		/// </summary>
		/// <value>
		/// The state of the current packet.
		/// </value>
		public EgtsPacketState CurrentPacketState
		{
			get
			{
				if (_currentPacket!=null)
					return _currentPacket.State;
				return EgtsPacketState.Unknown;
			}
		}

		/// <summary>
		/// Gets or sets the errors.
		/// </summary>
		/// <value>
		/// The errors.
		/// </value>
		public List<EgtsPacketError> Errors
		{
			get { return _packetErrs; }
		}
		private readonly List<EgtsPacketError> _packetErrs = new List<EgtsPacketError>();

		#endregion Properties

		#region public methods

		/// <summary>
		/// Puts the next bytes.
		/// </summary>
		/// <param name="bytes">The bytes.</param>
		public void PutNextBytes(byte[] bytes)
		{
			if (bytes == null)
				throw new ArgumentNullException("bytes");
			lock (_bufferLock)
			{
				_buffer.AddRange(bytes);
				
				if (_buffer.Count > Constants.MaxBufferSize)
				{
					_buffer.Clear();
					_currentReadPosition = 0;
					_currentPacket = null;
					throw new InternalBufferOverflowException("Buffer overflow, reset state");
				}
			}
			if (_currentPacket == null)
				_currentPacket = new EgtsPacket();
			workBuffer();
		}

		/// <summary>
		/// изъять пакет из уже распарсенных
		/// </summary>
		/// <returns></returns>
		public EgtsPacket GetPacket()
		{
			lock (_procPacketLock)
			{
				if (_processedPackets.Count==0)
					return null;
				return _processedPackets.Dequeue();
			}
		}

		/// <summary>
		/// Parses the packet.
		/// </summary>
		/// <param name="packet">The packet.</param>
		/// <returns></returns>
		public byte[] ParsePacket(EgtsPacket packet)
		{
			lock (_byteGotLock)
			{
				var ret = new List<byte>();

				ret.Add(packet.Header.PRV);
				ret.Add(packet.Header.SKID);

				#region encode\parse data

				// сразу обрабатываем данные, чтоб посчитать их длину
				var addArr = new List<byte>();
				if (packet.Header.PT == EgtsPacketType.SignedAppData)
				{
					if (packet.SignatureBytes.Count == 0)
						throw new EgtsProceedException("No signature assigned in packet marked as signed");
					packet.IsSigned = true;
					var sign = br.WriteValueToBuffer((ulong) packet.SignatureBytes.Count, sizeof (ushort));
					addArr.AddRange(sign);
					addArr.AddRange(packet.SignatureBytes);
						// TODO посколько нет в сырцах проверялки подписей, то пока только подразумеваем что нужно байтики положить в пакет
				}
				if (packet.Header.PT == EgtsPacketType.Response)
				{
					var resp = br.WriteValueToBuffer(packet.ResponceHeader.RPID, sizeof (ushort));
					addArr.AddRange(resp);
					resp = br.WriteValueToBuffer(packet.ResponceHeader.PR, sizeof (byte));
					addArr.AddRange(resp);
				}

				foreach (var egtsPacketRecord in packet.Records)
				{
					addArr.AddRange(_recordsParser.WriteNextRecord(egtsPacketRecord));
				}

				var cmpDec = _compressionHelper.CompressData(addArr.ToArray());
				cmpDec = _encodingHelper.EncodeBytes(cmpDec);

				packet.Header.FDL = (ushort) cmpDec.Length;

				#endregion encode\parse data

				var bitsByte = headerBitfields2Byte(packet.Header);
				ret.Add(bitsByte);

				var arr = _headerEncodingHelper.EncodeHeader(packet.Header, packet.Header.HE);
				if (arr == null)
					throw new EgtsProceedException("Ошибка зашифровывания заголовка");
				packet.Header.HL =
					(byte) (Constants.FixedHeaderLength + (packet.Header.RTE ? Constants.HeaderRouteLength : 0) + arr.Length + 1);
				ret.Add(packet.Header.HL);
				ret.Add(packet.Header.HE);
				ret.AddRange(arr);

				var crc = _crcHelper.CalculateHeaderCrc(packet.Header, arr, bitsByte, arr.Length);
				ret.Add(crc);

				ret.AddRange(cmpDec);
				var crcUsh = _crcHelper.CalculateDataCrc(cmpDec, (short) cmpDec.Length);
				var buf = ByteCombineHelper.WriteValueToBuffer(crcUsh, sizeof (ushort));
				ret.AddRange(buf);

				return ret.ToArray();
			}
		}


		#endregion public methods

		#region private methods

		/// <summary>
		/// положить\распарсить следующий байтик пакета
		/// egts_packet_rx_byte
		/// </summary>
		private void workBuffer()
		{
			try
			{
				byte uc = 0;
				while (popNextByte(ref uc))
				{
					lock(_bufferLock)
						_currentPacket.OriginalBytes.Add(uc);
					switch (_currentPacket.State)
					{
						case EgtsPacketState.Unknown: //пакет только пришёл ещё ничего не писалось
							_currentPacket.State = EgtsPacketState.Begin;
							pushByteBack();
							break;

							#region header reading

						case EgtsPacketState.Begin:
							_currentPacket.Header.PRV = uc;
							if (_currentPacket.Header.PRV != Version)
							{
								/* remain state est_begin, just skip 1 byte */
							/*	if (_currentPacket.PRVSkipped == 0)
									putError(string.Format("Invalid PRV: {0}, bufferPosition:{1}, nearBytes:{2}", uc, _currentReadPosition,
										_buffer.GetRange((_currentReadPosition >= 5 ? _currentReadPosition : 5) - 5, (_currentReadPosition + 5))
											.Aggregate("", (prev, cur) => prev + " " + cur)));*/
								_currentPacket.PRVSkipped++;
								rewindBuffer(false);
								//	egts_sync( estate );		// не знаю пока имеет ли это значение
								break;
							}
							if (_currentPacket.PRVSkipped > 0)
							{
								putError(string.Format("Skipped by PRV {0}", _currentPacket.PRVSkipped));
								_currentPacket.PRVSkipped = 0;
							}
							_currentPacket.State = EgtsPacketState.PRV; //считали версию
							break;
						case EgtsPacketState.PRV:
							_currentPacket.Header.SKID = uc;
							_currentPacket.State = EgtsPacketState.SKID;
							break;
						case EgtsPacketState.SKID:
							_currentPacket.Header.BitfieldsByte = uc;
							parseHeaderBits(_currentPacket.Header, uc);
							if (_currentPacket.Header.PRF != Constants.PRF)
								// Параметр PRF определяет префикс заголовка Транспортного уровня и Должен быть 00(документация)
							{
								putError("Invalid PRF");
								rewindBuffer();
								break;
							}
							_currentPacket.State = EgtsPacketState.BITS;
							break;
						case EgtsPacketState.BITS:
							_currentPacket.Header.HL = uc;
							_currentPacket.State = EgtsPacketState.HL;
							break;
						case EgtsPacketState.HL:
							_currentPacket.Header.HE = uc;
							if (_currentPacket.Header.HE != 0)
								// отсутствие енкодинга, покуда и в исходниках нет поддержки никакой кодировки, енума тоже нет(
							{
								if (!_currentPacket.Header.RTE)
								{
									/* has optional route fields PRA,RCA,TTL (+5 byte) */
									if (_currentPacket.Header.HL != Constants.HeaderLengthWithRoute)
									{
										putError("invalid HL (HE=0,RTE=1)");
										rewindBuffer();
										break;
									}
								}
								else
								{
									/* no optional route fields */
									if (_currentPacket.Header.HL != Constants.HeaderLengthWithoutRoute)
									{
										putError("invalid HL (HE=0,RTE=0)");
										rewindBuffer();
										break;
									}
								}
							}
							else
							{
								_currentPacket.IsHeaderEncoded = true;
								/* some encoding, unknown header length */
								if (_currentPacket.Header.HL > Constants.MaxHeaderLength)
								{
									putError("too big HL (HE!=0)");
									rewindBuffer();
									break;
								}
							}
							_currentPacket.State = EgtsPacketState.HE;
							break;
						case EgtsPacketState.HE:
							if ((Constants.FixedHeaderLength + _currentPacket.HeaderBytes.Count + 1) < _currentPacket.Header.HL)
							{
								_currentPacket.HeaderBytes.Add(uc);
							}
							else
							{
								_currentPacket.State = EgtsPacketState.HCS;
								pushByteBack();
							}
							break;
						case EgtsPacketState.HCS:
							_currentPacket.Header.HCS = uc;
							var headerCrc = _crcHelper.CalculateHeaderCrc(_currentPacket.Header, _currentPacket.HeaderBytes.ToArray(),
								_currentPacket.Header.BitfieldsByte, _currentPacket.HeaderBytes.Count);
							if (headerCrc == _currentPacket.Header.HCS)
							{
								if (!decodeHeader(_currentPacket))
									// тут и дешифруются(внутри метода решает, дешифровать или так достать) и заполняет поля FDL, PID, PT, PRA, RCA, TTL
								{
									putError("header encoding failure");
									rewindBuffer();
									break;
								}
								if (_currentPacket.Header.FDL > Constants.MaxDataLength)
								{
									putError("FDL too big");
									rewindBuffer();
									break;
								}
								_currentPacket.State = EgtsPacketState.SFRD;
								_currentPacket.IsDataEncoded = (_currentPacket.Header.ENA != 0);
								// пока проверка условная ибо не знаем о способах кодирования данных(и нет енума) но развилку уже запиливаем;
								break;
							}

							putError("header CRC failure");
							rewindBuffer();
							break;

							#endregion header reading

							#region data reading and proceeding

						case EgtsPacketState.SFRD:
							if (_currentPacket.Header.FDL > _currentPacket.DataEncodedBytes.Count)
								_currentPacket.DataEncodedBytes.Add(uc);
							else
							{
								_currentPacket.State = EgtsPacketState.SFRCS;
								pushByteBack();
							}
							break;
						case EgtsPacketState.SFRCS:
							_currentPacket.DataCRCBytes.Add(uc);
							if (_currentPacket.DataCRCBytes.Count < sizeof (ushort))
								break;
							ulong val = 0;
							ushort size = sizeof (ushort);
							br.ReadValueFromBuffer(ref val, sizeof (ushort), _currentPacket.DataCRCBytes.ToArray(), ref size, 0);
							_currentPacket.DataCRC = (ushort) val;
							var calcCrc = _crcHelper.CalculateDataCrc(_currentPacket.DataEncodedBytes.ToArray(),
								(short) _currentPacket.DataEncodedBytes.Count);
							if (calcCrc != _currentPacket.DataCRC)
							{
								putError(string.Format("Data CRC failure. Enc:{0}, Cmp:{1}", _currentPacket.IsDataEncoded,
									_currentPacket.Header.CMP));
								rewindBuffer();
								break;
							}

							_currentPacket.DataBytes.Clear();
							if (_currentPacket.IsDataEncoded)
							{
								if (!decodeData(_currentPacket))
								{
									rewindBuffer();
									break;
								}
							}
							else
							{
								_currentPacket.DataBytes.AddRange(_currentPacket.DataEncodedBytes);
							}
							if (_currentPacket.Header.CMP)
							{
								var dbt = _currentPacket.DataBytes.ToArray();
								_currentPacket.DataBytes.Clear();
								_currentPacket.DataBytes.AddRange(_compressionHelper.DecompressData(dbt));
							}
							proceedCurrentPacket();
							_currentPacket.State = EgtsPacketState.Proceeded;
							clearCurrentPacketBytes();
							putProceededPacket();
							break;

							#endregion data reading and proceeding
					}
				}
			}
			catch (EgtsProceedException prc)
			{
				prc.Packet = _currentPacket;
				clearCurrentPacketBytes();
				_currentPacket = null;
				throw new EgtsRecieveException("Message recieved but parsing data failed. See inner exception for details.", prc);
			}
			catch (Exception ex)
			{
				_currentReadPosition = 0;		// чтот пошло крепко не так, попробуем почитать буфер сначала
				throw new EgtsRecieveException("Unknown recieving error.", ex);
			}
		}

		/// <summary>
		/// Pops the next byte.
		/// egts_pop
		/// </summary>
		/// <param name="ret">сюда запишется байт считанный.</param>
		/// <returns></returns>
		private bool popNextByte(ref byte ret)
		{
			lock (_bufferLock)
			{
				if (_buffer.Count == 0 || _currentReadPosition>=_buffer.Count)		// значит в буффер ещё не сунули нужного
					return false;
				ret = _buffer[_currentReadPosition];
				_currentReadPosition = (int)((_currentReadPosition + 1) % Constants.MaxBufferSize);
				return true;
			}
		}

		/// <summary>
		/// Pushes the byte back.
		/// egts_pushback
		/// </summary>
		private void  pushByteBack()
		{
			lock (_bufferLock)
			{
				var new_cur = (int) ((Constants.MaxBufferSize + _currentReadPosition - 1U)%Constants.MaxBufferSize);
				//_buffer[new_cur] = uc;
				_currentReadPosition = new_cur;
				if (_currentPacket.OriginalBytes.Count > 0)
					_currentPacket.OriginalBytes.RemoveAt(_currentPacket.OriginalBytes.Count - 1);
			}
		}

		private void clearCurrentPacketBytes()
		{
			if (_currentReadPosition==0)
				return;
			lock (_bufferLock)
			{
				_buffer.RemoveRange(0,_currentReadPosition);
				_currentReadPosition = 0;
			}
		}

		/// <summary>
		/// throw the packet combining Exception
		/// </summary>
		private void rewindBuffer(bool dropPacket = true)
		{
			lock (_bufferLock)
			{
				_buffer.RemoveAt(0);
				_currentReadPosition = 0;
			}
			if (dropPacket)
				_currentPacket = new EgtsPacket();
		}

		/// <summary>
		/// распарсить битовые флаги из байта хедера
		/// </summary>
		/// <param name="header">The header.</param>
		/// <param name="b">The b.</param>
		private void parseHeaderBits(EgtsPacketHeader header,byte b)
		{
			var prInt = b & 0x03;

			header.PR = (EgtsProcessingPriority)prInt;
			b >>= 2;
			header.CMP = (b & 0x01)==1;
			b >>= 1;
			header.ENA = (byte)(b & 0x03);
			b >>= 2;
			header.RTE = (b & 0x01U)==1;
			b >>= 1;
			header.PRF = (byte)(b & 0x03U);
		}

		private bool decodeHeader(EgtsPacket packet)
		{
			return _headerEncodingHelper.DecodeHeaderBytes(packet.Header, packet.HeaderBytes.ToArray(), (ushort) packet.HeaderBytes.Count);
		}

		private bool decodeData(EgtsPacket packet)
		{
			try
			{
				packet.DataBytes.AddRange(_encodingHelper.DecodedBytes(packet.DataEncodedBytes.ToArray()));
				return true;
			}
			catch (Exception ex)
			{
				putError( "Decoding error " + ex.Message);
				return false;
			}
		}

		/// <summary>
		/// все получательные работы с пакетом завершены, тут парсинг данных в рекорды
		/// </summary>
		private void proceedCurrentPacket()
		{
			var packet = _currentPacket;
			var remainDataSize = (ushort) packet.DataBytes.Count;
			var dataArray = packet.DataBytes.ToArray();
			ushort offset = 0;
			if (packet.Header.PT == EgtsPacketType.SignedAppData)
			{
				packet.IsSigned = true;
				//check signature
				ulong val = 0;
				ushort signOffset = sizeof(ushort);

				if (!br.ReadValueFromBuffer(ref val, signOffset, dataArray, ref remainDataSize, 0))
				{
					putError("Error reading signature length");
				}
				var signatureLength = (ushort) val;
				if (signatureLength > remainDataSize || signatureLength > Constants.MaxSignatureLength || signatureLength == 0)
				{
					putError("Wrong signature length");
					return;
				}
				offset = signOffset;
				for (int i = 0; i < signatureLength; i++)
				{
					packet.SignatureBytes.Add(packet.DataBytes[signOffset + i]);
				}
				if (!checkSignature())
				{
					putError("Signature check failure");
					return;
				}
			}
			if (packet.Header.PT == EgtsPacketType.Response)
			{
				// fill responce header
				if (packet.DataBytes.Count < sizeof (short) + sizeof (byte))
				{
					putError("Wrong responce length");
					return;
				}
				packet.ResponceHeader = new EgtsResponceHeader();
				ushort val = 0;
				if (!br.ReadNextUshortBytes(ref val,dataArray,ref remainDataSize,ref  offset))
				{
					putError("Response packet ID reading failure");
					return;
				}
				
				packet.ResponceHeader.RPID = val;
				byte byt = 0;

				if (!br.ReadNextByteBytes(ref byt, dataArray, ref remainDataSize, ref offset))
				{
					putError("Processing result reading failure");
					return;
				}

				packet.ResponceHeader.PR = byt;
			}
			// fill records
			while (remainDataSize>0)
			{
				if (!_recordsParser.ParseNextRecord(packet, ref offset, ref remainDataSize))
					break;
			}
		}

		private bool checkSignature()
		{
			return true;		// вот былоб клёво проверять подпись, но хз как, в сырцах проверки нет
		}

		/// <summary>
		/// Puts the error.
		/// </summary>
		/// <param name="error">The error.</param>
		private void putError(string error)
		{
			_packetErrs.Add(new EgtsPacketError {ErrorText = error, PID = _currentPacket.Header.PID});
			if (_packetErrs.Count == constMaxErrorCount + 1)
				_packetErrs.RemoveAt(0);
		}

		private void putProceededPacket()
		{
			lock (_procPacketLock)
			{
				_processedPackets.Enqueue(_currentPacket);
			}
			_currentPacket = new EgtsPacket();
		}

		private byte headerBitfields2Byte(EgtsPacketHeader header)
		{
			byte ret = 0;
			ret |= (byte)header.PR;
			var offset = 2;
			ret |= (byte)((header.CMP ? 1 : 0) << offset);
			offset++;
			ret |= (byte)(header.ENA << offset);
			offset += 2;
			ret |= (byte)((header.RTE ? 1 : 0) << offset);
			offset++;
			ret |= (byte)(header.PRF << offset);
			return ret;
		}

		#endregion private methods
	}
}
