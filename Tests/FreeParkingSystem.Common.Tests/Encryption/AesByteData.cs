using System.Collections;
using System.Collections.Generic;

namespace FreeParkingSystem.Common.Tests.Encryption
{
	public class AesByteData:IEnumerable<object[]>
	{
		public IEnumerator<object[]> GetEnumerator()
		{
			yield return new object[]
			{
				new byte[] { 58, 214, 141, 118, 100, 6, 79, 235, 4, 236, 57, 230, 214, 79, 121, 202, 216, 244, 139, 161, 164, 244, 181, 239, 150, 166, 121, 65, 196, 230, 193, 227 },
				new byte[] { 21, 241, 37 }
			};

			yield return new object[]
			{
				new byte[] { 152, 78, 150, 255, 173, 84, 3, 83, 80, 223, 189, 74, 153, 224, 235, 127, 11, 64, 169, 114, 253, 23, 85, 204, 52, 130, 231, 196, 96, 244, 20, 7  },
				new byte[] { 182, 147, 233 }
			};

			yield return new object[]
			{
				new byte[] { 71, 188, 122, 125, 74, 223, 160, 5, 200, 249, 172, 167, 245, 156, 206, 60, 63, 178, 0, 73, 185, 223, 114, 58, 100, 216, 180, 66, 215, 26, 7, 5 },
				new byte[] { 211, 49, 249 }
			};

			yield return new object[]
			{
				new byte[] { 131, 183, 210, 218, 93, 150, 188, 192, 201, 255, 253, 113, 247, 152, 226, 26, 189, 115, 154, 130, 174, 153, 200, 56, 238, 235, 50, 97, 23, 91, 166, 152 },
				new byte[] { 18, 195, 166 }
			};

		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}