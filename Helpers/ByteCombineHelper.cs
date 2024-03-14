
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("EgtsUnitTest")]
namespace Transport.EGTS.Helpers
{
	/// <summary>
	/// помошник парсинга байтиков в циферки
	/// TODO пока используется статическим, он внутренний, так что это нестрашно. при всей нелюбви к статическим классам, ничего лучше придумать не удалось
	/// </summary>
	internal static class ByteCombineHelper
	{
		#region straight (Little Endian by Default)

		/// <summary>
		/// взять следующие 4 байтика и считать их в uint
		/// </summary>
		/// <param name="val">The value.</param>
		/// <param name="dataArr">The data arr.</param>
		/// <param name="remainSize">Size of the remain.</param>
		/// <param name="offset">The offset.</param>
		/// <returns></returns>
		internal static bool ReadNextUintBytes(ref uint val, byte[] dataArr, ref ushort remainSize, ref ushort offset)
		{
			const ushort usSize = sizeof(uint);
			ulong uval = 0;
			if (ReadValueFromBuffer(ref uval, usSize, dataArr, ref remainSize, offset))
			{
				val = (uint)uval;
				offset += usSize;
				return true;
			}
			return false;
		}

		/// <summary>
		/// взять следующие два байтика и считать их в ushort
		/// </summary>
		/// <param name="val">The value.</param>
		/// <param name="dataArr">The data arr.</param>
		/// <param name="remainSize">Size of the remain.</param>
		/// <param name="offset">The offset.</param>
		/// <returns></returns>
		internal static bool ReadNextUshortBytes(ref ushort val, byte[] dataArr, ref ushort remainSize, ref ushort offset)
		{
			const ushort usSize = sizeof(ushort);
			ulong uval = 0;
			if (ReadValueFromBuffer(ref uval, usSize, dataArr, ref remainSize, offset))
			{
				val = (ushort)uval;
				offset += usSize;
				return true;
			}
			return false;
		}

		/// <summary>
		/// взять следующий байтик
		/// </summary>
		/// <param name="val">The value.</param>
		/// <param name="dataArr">The data arr.</param>
		/// <param name="remainSize">Size of the remain.</param>
		/// <param name="offset">The offset.</param>
		/// <returns></returns>
		internal static bool ReadNextByteBytes(ref byte val, byte[] dataArr, ref ushort remainSize, ref ushort offset)
		{
			const ushort btSize = sizeof(byte);
			ulong uval = 0;
			if (ReadValueFromBuffer(ref uval, btSize, dataArr, ref remainSize, offset))
			{
				val = (byte)uval;
				offset += btSize;
				return true;
			}
			return false;
		}

		/// <summary>
		/// считывает байтики, до нулевого, возвращает строку.
		/// если ноль сразу, вернёт пустую строку, в случае ошибки вернёт нулл
		/// </summary>
		/// <param name="maxSize">The maximum size.</param>
		/// <param name="dataArr">The data arr.</param>
		/// <param name="remainSize">Size of the remain.</param>
		/// <param name="offset">The offset.</param>
		/// <returns></returns>
		internal static string ReadStringToDelimiter(uint maxSize, byte[] dataArr, ref ushort remainSize, ref ushort offset)
		{
			var bytArr = new byte[maxSize];
			var i = 0;
			byte byt = 0;
			if (!ReadNextByteBytes(ref byt, dataArr, ref remainSize, ref offset))
				return null;
			if (byt == 0)
				return "";
			while (byt != 0)
			{
				bytArr[i] = byt;
				if (!ReadNextByteBytes(ref byt, dataArr, ref remainSize, ref offset))
					return null;
				i++;
				if (i == maxSize)
					return null;
			}

			return Encoding.ASCII.GetString(bytArr.Take(i).ToArray());
		}

		/// <summary>
		/// Reads the fixed size string.
		/// </summary>
		/// <param name="sizeToRead">The size to read.</param>
		/// <param name="dataArr">The data arr.</param>
		/// <param name="remainSize">Size of the remain.</param>
		/// <param name="offset">The offset.</param>
		/// <returns></returns>
		internal static string ReadFixedSizeString(uint sizeToRead, byte[] dataArr, ref ushort remainSize, ref ushort offset)
		{
			var bytArr = new byte[sizeToRead];
			byte byt = 0;
			for (int i = 0; i < sizeToRead; i++)
			{
				if (!ReadNextByteBytes(ref byt, dataArr, ref remainSize, ref offset))
					return null;
				bytArr[i] = byt;
			}

			return Encoding.ASCII.GetString(bytArr);
		}

		/// <summary>
		/// считать голяком readCount байтиков в массивчик
		/// при неудаче вернёт нулл
		/// </summary>
		/// <param name="readCount">The read count.</param>
		/// <param name="dataArr">The data arr.</param>
		/// <param name="remainSize">Size of the remain.</param>
		/// <param name="offset">The offset.</param>
		/// <returns></returns>
		internal static byte[] ReadRawByteArray(uint readCount, byte[] dataArr, ref ushort remainSize, ref ushort offset)
		{
			byte byt = 0;
			var ret = new byte[readCount];
			for (int i = 0; i < readCount; i++)
			{
				if (ReadNextByteBytes(ref byt, dataArr, ref remainSize, ref offset))
					ret[i] = byt;
				else
					return null;
			}
			return ret;
		}

		#endregion straight (Little Endian by Default)

		#region reverse (Big Endian by default)

		/// <summary>
		/// взять следующие 4 байтика и считать их в uint в обратном порядке
		/// </summary>
		/// <param name="val">The value.</param>
		/// <param name="dataArr">The data arr.</param>
		/// <param name="remainSize">Size of the remain.</param>
		/// <param name="offset">The offset.</param>
		/// <returns></returns>
		internal static bool ReadNextUintBytesReverse(ref uint val, byte[] dataArr, ref ushort remainSize, ref ushort offset)
		{
			const ushort usSize = sizeof(uint);
			ulong uval = 0;
			if (ReadValueFromBufferReverse(ref uval, usSize, dataArr, ref remainSize, offset))
			{
				val = (uint)uval;
				offset += usSize;
				return true;
			}
			return false;
		}

		/// <summary>
		/// взять следующие два байтика и считать их в ushort  в обратном порядке
		/// </summary>
		/// <param name="val">The value.</param>
		/// <param name="dataArr">The data arr.</param>
		/// <param name="remainSize">Size of the remain.</param>
		/// <param name="offset">The offset.</param>
		/// <returns></returns>
		internal static bool ReadNextUshortBytesReverse(ref ushort val, byte[] dataArr, ref ushort remainSize, ref ushort offset)
		{
			const ushort usSize = sizeof(ushort);
			ulong uval = 0;
			if (ReadValueFromBufferReverse(ref uval, usSize, dataArr, ref remainSize, offset))
			{
				val = (ushort)uval;
				offset += usSize;
				return true;
			}
			return false;
		}

		#endregion reverse (Big Endian by default)

		#region Core methods of straight and reverse reading/writing

		/// <summary>
		/// Reads the value from buffer.
		/// возвращает ulong который дальше надо приводить к другому типу
		/// </summary>
		/// <param name="val">The value.</param>
		/// <param name="valueSize">Size of the value.</param>
		/// <param name="data">The data.</param>
		/// <param name="datasz">The datasz.</param>
		/// <param name="offset">The offset.</param>
		/// <returns></returns>
		internal static bool ReadValueFromBuffer(ref ulong val, int valueSize, byte[] data, ref ushort datasz, int offset)
		{
			//TODO по идее есть ещё вариант работать с BigEndian не знаю пока зачем и как мутится проверка(дефайн называется EGTS_CPU_BE), поэтому пока только Little
			return readfromBufferLittleEndian(ref val, valueSize, data, ref datasz, offset);
		}

		/// <summary>
		/// Writes the value to buffer.
		/// возвращает массив из количества байт valueSize соответствующий val
		/// </summary>
		/// <param name="val">The value.</param>
		/// <param name="valueSize">Size of the value.</param>
		/// <returns></returns>
		internal static byte[] WriteValueToBuffer(ulong val, int valueSize)
		{
			//TODO по идее есть ещё вариант работать с BigEndian не знаю пока зачем и как мутится проверка(дефайн называется EGTS_CPU_BE), поэтому пока только Little
			return writeToBufferLittleEndian(val, valueSize);
		}

		/// <summary>
		/// Reads the value from buffer reverse.
		/// </summary>
		/// <param name="val">The value.</param>
		/// <param name="valueSize">Size of the value.</param>
		/// <param name="data">The data.</param>
		/// <param name="datasz">The datasz.</param>
		/// <param name="offset">The offset.</param>
		/// <returns></returns>
		internal static bool ReadValueFromBufferReverse(ref ulong val, int valueSize, byte[] data, ref ushort datasz, int offset)
		{
			//TODO наоброт это и значит BigEndian, опять же, если дефайн EGTS_CPU_BE, то наоборот, тут будет литл
			return readfromBufferBigEndian(ref val, valueSize, data, ref datasz, offset);
		}

		/// <summary>
		/// Writes the value to buffer reverse.
		/// возвращает массив из количества байт valueSize соответствующий val в обратном порядке
		/// </summary>
		/// <param name="val">The value.</param>
		/// <param name="valueSize">Size of the value.</param>
		/// <returns></returns>
		internal static byte[] WriteValueToBufferReverse(ulong val, int valueSize)
		{
			//TODO наоброт это и значит BigEndian, опять же, если дефайн EGTS_CPU_BE, то наоборот, тут будет литл
			return writeToBufferBigEndian(val, valueSize);
		}

		#endregion Core methods of straight and reverse reading/writing

		#region private methods

		private static bool readfromBufferLittleEndian(ref ulong puv, int spuv, byte[] ppuc, ref ushort psz, int offset)
		{
			int pos = 0;
			ulong result = 0;
			var offsetSize = offset + spuv;
			if (ppuc.Length < offsetSize)
				return false;
			for (int i = offset; i < offsetSize; i++)
			{
				var rgtPart = (((ulong)ppuc[i]) << pos);
				result |= rgtPart;
				pos += 8;
				psz--;
				spuv--;
			}

			puv = result;
			if (spuv != 0U)
			{
				return false;
			}
			return true;
		}

		private static bool readfromBufferBigEndian(ref ulong puv, int spuv, byte[] ppuc, ref ushort psz, int offset)
		{
			int pos = 0;//(spuv-1)*8;
			ulong result = 0;
			var offsetSize = offset + spuv-1;
			if (ppuc.Length < offsetSize)
				return false;
			for (int i = offsetSize; i >= offset; i--)
			{
				result |=  (((ulong)ppuc[i]) << pos);
				pos += 8;
				psz--;
				spuv--;
			}

			puv = result;
			if (spuv != 0U)
			{
				return false;
			}
			return true;
		}

		private static byte[] writeToBufferLittleEndian(ulong puv, int spuv)
		{
			var ppuc = new byte[spuv];
			var offsetSize = spuv;

			for (int i = 0; i < offsetSize; i++)
			{
				ppuc[i] = (byte)(puv & 0xff);
				puv >>= 8;
				spuv--;
			}

			if (spuv != 0U)
			{
				return null;
			}
			return ppuc;
		}

		private static byte[] writeToBufferBigEndian(ulong puv, int spuv)
		{
			var ppuc = new byte[spuv];
			var offsetSize = spuv - 1;

			for (int i = offsetSize; i >= 0; i--)
			{
				ppuc[i] = (byte)(puv & 0xff);
				puv >>= 8;
				spuv--;
			}

			if (spuv != 0U)
			{
				return null;
			}
			return ppuc;
		}

		#endregion private methods
	}
}
