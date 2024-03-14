using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Transport.EGTS.Helpers
{
	/// <summary>
	/// 
	/// </summary>
	public class EgtsCommandValueHelper
	{
		/// <summary>
		/// Parses the digit to bytes.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public byte[] ParseUint2Bytes(uint value)
		{
			return ByteCombineHelper.WriteValueToBuffer(value, sizeof (uint));
		}

		/// <summary>
		/// Parses the digit to bytes.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public byte[] ParseUshort2Bytes(ushort value)
		{
			return ByteCombineHelper.WriteValueToBuffer(value, sizeof(ushort));
		}

		/// <summary>
		/// Parses the string to bytes. Adds delimeter in the end
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="enc">The enc.</param>
		/// <returns></returns>
		public byte[] ParseString2Bytes(string value,Encoding enc)
		{
			if (string.IsNullOrEmpty(value))
				return null;
			var len = value.Length+1;
			var ret = new byte[len];
			enc.GetBytes(value, 0, value.Length, ret, 0);
			ret[len - 1] = 0;
			return ret;
		}

		/// <summary>
		/// Parses the string array2 bytes.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="enc">The enc.</param>
		/// <returns></returns>
		public byte[] ParseStringArray2Bytes(string[] value, Encoding enc)
		{
			if (value == null)
				return null;
			var nonEmptVals = value.Where(w => !string.IsNullOrEmpty(w)).ToList();
			var len = nonEmptVals.Sum(s => s.Length + 1);
			var ret = new byte[len];
			var offset = 0;
			foreach (var s in nonEmptVals)
			{
				var buf = new byte[s.Length];
				enc.GetBytes(s, 0, s.Length, buf, 0);
				buf.CopyTo(ret,offset);
				ret[offset+s.Length] = 0;
				offset += s.Length + 1;
			}
			return ret;
		}

		/// <summary>
		/// Gets the digit from bytes.
		/// </summary>
		/// <param name="bytes">The bytes.</param>
		/// <returns></returns>
		public uint GetDigitFromBytes(byte[] bytes)
		{
			uint ret = 0;
			ushort remSZ = sizeof (uint);
			ushort offs = 0;
			if (bytes==null || bytes.Length<remSZ)
				return ret;
			ByteCombineHelper.ReadNextUintBytes(ref ret, bytes, ref remSZ, ref offs);
			return ret;
		}

		/// <summary>
		/// Gets the string from bytes.
		/// </summary>
		/// <param name="bytes">The bytes.</param>
		/// <param name="enc">The enc.</param>
		/// <returns></returns>
		public string GetStringFromBytes(byte[] bytes, Encoding enc)
		{
			if (bytes==null || bytes.Length==0)
				return null;
			return enc.GetString(bytes);
		}

		/// <summary>
		/// Gets the string array from bytes.
		/// </summary>
		/// <param name="bytes">The bytes.</param>
		/// <param name="enc">The enc.</param>
		/// <returns></returns>
		public string[] GetStringArrayFromBytes(byte[] bytes, Encoding enc)
		{
			if (bytes == null || bytes.Length == 0)
				return null;
			var ret = new List<string>();
			var workList = new List<byte>(bytes);
			var alrParsed = 0;
			while (alrParsed<bytes.Length)
			{
				var strBytes = workList.Skip(alrParsed).TakeWhile(w => w != 0);
				var str = enc.GetString(strBytes.ToArray());
				ret.Add(str);
				alrParsed += str.Length+1;
			}
			return ret.ToArray();
		}
	}
}
