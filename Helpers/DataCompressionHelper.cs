
namespace Transport.EGTS.Helpers
{
	/// <summary>
	/// 
	/// </summary>
	internal class DataCompressionHelper
	{
		/// <summary>
		/// Decompresses the data.
		/// </summary>
		/// <param name="compressedData">The compressed data.</param>
		/// <returns></returns>
		public byte[] DecompressData(byte[] compressedData)
		{
			return compressedData;		// ну нет пока декомпрессии(в сырцах тоже нет, есть импровизированное смещение байтов и то в дебаге только)
		}

		/// <summary>
		/// Compresses the data.
		/// </summary>
		/// <param name="rawBytes">The raw bytes.</param>
		/// <returns></returns>
		public byte[] CompressData(byte[] rawBytes)
		{
			return rawBytes;		// ну нет пока компрессии(в сырцах тоже нет, есть импровизированное смещение байтов и то в дебаге только)
		}
	}
}
