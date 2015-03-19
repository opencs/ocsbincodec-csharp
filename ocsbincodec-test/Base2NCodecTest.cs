// Copyright (c) 2015, Open Communications Security
// All rights reserved.
//
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
//
// * Redistributions of source code must retain the above copyright notice, this
//   list of conditions and the following disclaimer.
//
// * Redistributions in binary form must reproduce the above copyright notice,
//   this list of conditions and the following disclaimer in the documentation
//   and/or other materials provided with the distribution.
//
// * Neither the name of ocsbincodec-csharp nor the names of its
//   contributors may be used to endorse or promote products derived from
//   this software without specific prior written permission.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
// FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
// DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
// SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
// CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
// OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
// OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
//
using NUnit.Framework;
using System;
using System.Text;

namespace OpenCS.BinCodec
{
	[TestFixture()]
	public class Base2NCodecTest: BaseCodecTest
	{

		private static readonly Encoding CHARSET = Encoding.UTF8;

		private static readonly Base64Alphabet ALPHABET = new Base64Alphabet();

		// Test vectors extracted from RFC4648 and other sources
		private static readonly string [][] SAMPLES = new string[][]{
			new string[]{ "", "", "" },
			new string[]{ "f", "Zg==", "Zg" },
			new string[]{ "fo", "Zm8=", "Zm8" },
			new string[]{ "foo", "Zm9v", "Zm9v" },
			new string[]{ "foob", "Zm9vYg==", "Zm9vYg" },
			new string[]{ "fooba", "Zm9vYmE=", "Zm9vYmE" },
			new string[]{ "foobar", "Zm9vYmFy", "Zm9vYmFy"},
			new string[]{ "This is just a test...\n", "VGhpcyBpcyBqdXN0IGEgdGVzdC4uLgo=", "VGhpcyBpcyBqdXN0IGEgdGVzdC4uLgo"}
		};

		[Test]
			public void TestBase2NCodecAlphabet() {
			Base2NCodec c;

			c = new Base2NCodec(ALPHABET);


			Assert.IsFalse(c.UsesPadding);
		}

		[Test]
			public void TestBase2NCodecAlphabetIntInt() {
			Base2NCodec c;

			c = new Base2NCodec(ALPHABET, '=', 4);
			Assert.IsTrue(c.UsesPadding);
		}

		[Test]
			public void TestBase2NCodecAlphabetIntIntCharArray() {
			Base2NCodec c;

			c = new Base2NCodec(ALPHABET, '=', 4);
			Assert.IsTrue(c.UsesPadding);
		}	

		[Test]
			public void TestGetDecodedSize() {
			Base2NCodec c;

			c = new Base2NCodec(ALPHABET);
			for (int encSize = 0; encSize < 1024; encSize++) {
				int expectedSize = (encSize * 6) / 8;
				Assert.AreEqual(expectedSize, c.GetDecodedSize(encSize));
			}

			c = new Base2NCodec(ALPHABET, '=', 4);
			for (int encSize = 0; encSize < 1024; encSize++) {
				int expectedSize = (encSize * 6) / 8;
				Assert.AreEqual(expectedSize, c.GetDecodedSize(encSize));
			}

			c = new Base2NCodec(ALPHABET, '=', 4, Base2NCodec.IGNORE_SPACES);
			for (int encSize = 0; encSize < 1024; encSize++) {
				int expectedSize = (encSize * 6) / 8;
				Assert.AreEqual(expectedSize, c.GetDecodedSize(encSize));
			}
		}

		[Test]
			public void TestGetEncodedSize() {
			Base2NCodec c;

			c = new Base2NCodec(ALPHABET);
			for (int decSize = 0; decSize < 1024; decSize++) {
				int expectedSize = ((decSize * 8) + 5) / 6;
				Assert.AreEqual(expectedSize, c.GetEncodedSize(decSize));
			}

			c = new Base2NCodec(ALPHABET, '=', 4);
			for (int decSize = 0; decSize < 1024; decSize++) {
				int expectedSize = ((decSize * 8) + 5) / 6;
				expectedSize = expectedSize + ((4 - expectedSize % 4) % 4);
				Assert.AreEqual(expectedSize, c.GetEncodedSize(decSize));
			}

			c = new Base2NCodec(ALPHABET, '=', 4, Base2NCodec.IGNORE_SPACES);
			for (int decSize = 0; decSize < 1024; decSize++) {
				int expectedSize = ((decSize * 8) + 5) / 6;
				expectedSize = expectedSize + ((4 - expectedSize % 4) % 4);
				Assert.AreEqual(expectedSize, c.GetEncodedSize(decSize));
			}
		}

		protected byte[] ToBytes(String s, int startOffs, int addtionalSize) {
			byte [] ret;

			byte [] tmp = CHARSET.GetBytes(s);
			ret = new byte[tmp.Length + addtionalSize];
			Array.Copy(tmp, 0, ret, startOffs, tmp.Length);

			return ret;
		}

		private void TestDecodeCharSequenceIntIntByteArrayIntCore(Base2NCodec c, int encIndex) {

			// Plain - no offset
			foreach (String []s in SAMPLES) {
				int expectedSize = s[0].Length;
				byte []expected = ToBytes(s[0], 0, 0);
				String enc = s[encIndex];
				byte [] dst = new byte[expected.Length];

				int size = c.Decode(enc, 0, enc.Length, dst, 0);
				Assert.AreEqual(expectedSize, size, enc);
				Assert.AreEqual(expected, dst);
			}

			// Offset on output
			foreach (String []s in SAMPLES) {
				int expectedSize = s[0].Length;
				byte []expected = ToBytes(s[0], 1, 2);
				String enc = s[encIndex];
				byte []dst = new byte[expected.Length];

				int size = c.Decode(enc, 0, enc.Length, dst, 1);
				Assert.AreEqual(expectedSize, size, enc);
				Assert.AreEqual(expected, dst);
			}		

			// Plain - offset on input
			foreach (String []s in SAMPLES) {
				int expectedSize = s[0].Length;
				byte []expected = ToBytes(s[0], 0, 0);
				String enc = s[encIndex];
				byte []dst = new byte[expected.Length];

				int size = c.Decode(" " + enc + " ", 1, enc.Length, dst, 0);
				Assert.AreEqual(expectedSize, size, enc);
				Assert.AreEqual(expected, dst);
			}

			// Offset on input and output
			foreach (String []s in SAMPLES) {
				int expectedSize = s[0].Length;
				byte []expected = ToBytes(s[0], 1, 2);
				String enc = s[encIndex];
				byte []dst = new byte[expected.Length];

				int size = c.Decode(" " + enc + " ", 1, enc.Length, dst, 1);
				Assert.AreEqual(expectedSize, size, enc);
				Assert.AreEqual(expected, dst);
			}		
		}	

		private void TestDecodeWithIgnoreCore(Base2NCodec c, char [] ignoreList) {
			System.Random random = new System.Random();

			// Plain - no offset
			for (int srcSize = 1; srcSize <= 1024; srcSize++) {
				byte [] src;

				// Generate src
				src = new byte[srcSize];
				random.NextBytes(src);

				// Encode and add ignores
				StringBuilder enc = new StringBuilder(c.Encode(src));
				for (int i = 0; i < ignoreList.Length; i++) {
					int offs = random.Next(0, enc.Length - 1);
					enc.Insert(offs, ignoreList[i]);
				}

				byte [] dst = c.Decode(enc.ToString());
				Assert.AreEqual(src, dst);
			}	
		}

		[Test]
			public void TestDecodeCharSequenceIntIntByteArrayIntNoPadding() {
			Base2NCodec c;

			c = new Base2NCodec(ALPHABET);
			TestDecodeCharSequenceIntIntByteArrayIntCore(c, 2);
		}

		[Test]
			public void TestDecodeCharSequenceIntIntByteArrayIntPadding() {
			Base2NCodec c;

			c = new Base2NCodec(ALPHABET, '=', 4);
			TestDecodeCharSequenceIntIntByteArrayIntCore(c, 1);		
		}


		[Test]
			public void TestDecodeWithIgnoreNoPadding() {
			Base2NCodec c;

			c = new Base2NCodec(ALPHABET, '=', 0, Base2NCodec.IGNORE_SPACES);
			TestDecodeWithIgnoreCore(c, Base2NCodec.IGNORE_SPACES);
		}

		[Test]
			public void TestDecodeWithIgnorePadding() {
			Base2NCodec c;

			c = new Base2NCodec(ALPHABET, '=', 4, Base2NCodec.IGNORE_SPACES);
			TestDecodeWithIgnoreCore(c, Base2NCodec.IGNORE_SPACES);
		}	

		private void TestEncodeByteArrayIntIntAppendableCore(Base2NCodec c, int encIndex) {
			c = new Base2NCodec(ALPHABET);

			// Plain - no offset
			foreach (String [] s in SAMPLES) {
				int expectedSize = s[encIndex].Length;
				String expected = s[encIndex];
				byte []src = ToBytes(s[0], 0, 0);
				int srcSize = s[0].Length;
				int srcOffs = 0;

				StringBuilder sb = new StringBuilder();
				int size = c.Encode(src, srcOffs, srcSize, sb);
				Assert.AreEqual(expectedSize, size, expected);
				Assert.AreEqual(expected, sb.ToString());
			}

			// Plain - offset on src
			foreach (String [] s in SAMPLES) {
				int expectedSize = s[encIndex].Length;
				String expected = s[encIndex];
				byte []src = ToBytes(s[0], 1, 2);
				int srcSize = s[0].Length;
				int srcOffs = 1;

				StringBuilder sb = new StringBuilder();
				int size = c.Encode(src, srcOffs, srcSize, sb);
				Assert.AreEqual(expectedSize, size, expected);
				Assert.AreEqual(expected, sb.ToString());
			}		
		}


		[Test]
			public void TestEncodeByteArrayIntIntAppendableNoPadding() {
			Base2NCodec c;

			c = new Base2NCodec(ALPHABET);
			TestEncodeByteArrayIntIntAppendableCore(c, 2);
		}

		[Test]
			public void TestEncodeByteArrayIntIntAppendablePadding() {
			Base2NCodec c;

			c = new Base2NCodec(ALPHABET, '=', 4);
			TestEncodeByteArrayIntIntAppendableCore(c, 2);
		}

		[Test]
			public void TestEncodeDecodeNoPadding(){
			Base2NCodec c;

			c = new Base2NCodec(ALPHABET);
			TestEncodeDecodeCore(c);
		}

		[Test]
			public void TestEncodeDecodePadding(){
			Base2NCodec c;

			c = new Base2NCodec(ALPHABET, '=', 4);
			TestEncodeDecodeCore(c);
		}	

		[Test]
			public void TestEncodeDecodePadding2(){
			Base2NCodec c;

			// Exotic configuration
			c = new Base2NCodec(ALPHABET, '?', 8);
			TestEncodeDecodeCore(c);
		}	

		[Test]
			public void TestGetPaddingSize() {
			Base2NCodec c;

			c = new Base2NCodec(ALPHABET);
			for (int encSize = 0; encSize < 1024; encSize++) {
				Assert.AreEqual(0, c.GetPaddingSize(encSize));
			}

			c = new Base2NCodec(ALPHABET, '=', 4);
			for (int encSize = 0; encSize < 1024; encSize++) {
				int paddingSize = (4 - (encSize % 4)) % 4;
				Assert.AreEqual(paddingSize, c.GetPaddingSize(encSize));
			}

			c = new Base2NCodec(ALPHABET, '=', 4, Base2NCodec.IGNORE_SPACES);
			for (int encSize = 0; encSize < 1024; encSize++) {
				int paddingSize = (4 - (encSize % 4)) % 4;
				Assert.AreEqual(paddingSize, c.GetPaddingSize(encSize));
			}
		}

		[Test]
			public void TestUsesPadding() {
			Base2NCodec c;

			c = new Base2NCodec(ALPHABET);
			Assert.IsFalse(c.UsesPadding);

			c = new Base2NCodec(ALPHABET, '=', 4);
			Assert.IsTrue(c.UsesPadding);

			c = new Base2NCodec(ALPHABET, '=', 4, Base2NCodec.IGNORE_SPACES);
			Assert.IsTrue(c.UsesPadding);		
		}

		[Test]
			public void TestIsPadding() {
			Base2NCodec c;

			c = new Base2NCodec(ALPHABET);
			for (int ch = 0; ch < 256; ch++) {
				Assert.IsFalse(c.IsPadding(ch));
			}

			c = new Base2NCodec(ALPHABET, '=', 4);
			for (int ch = 0; ch < 256; ch++) {
				if (ch != '=') {
					Assert.IsFalse(c.IsPadding(ch));
				} else {
					Assert.IsTrue(c.IsPadding(ch));
				}
			}

			c = new Base2NCodec(ALPHABET, '=', 4, Base2NCodec.IGNORE_SPACES);
			for (int ch = 0; ch < 256; ch++) {
				if (ch != '=') {
					Assert.IsFalse(c.IsPadding(ch));
				} else {
					Assert.IsTrue(c.IsPadding(ch));
				}
			}
		}

		[Test]
			public void TestIsIgnored() {
			Base2NCodec c;

			c = new Base2NCodec(ALPHABET);
			for (int ch = 0; ch < 256; ch++) {
				Assert.IsFalse(c.IsIgnored(ch));
			}

			c = new Base2NCodec(ALPHABET, '=', 4);
			for (int ch = 0; ch < 256; ch++) {
				Assert.IsFalse(c.IsIgnored(ch));
			}

			c = new Base2NCodec(ALPHABET, '=', 4, Base2NCodec.IGNORE_SPACES);
			string ignored = new string(Base2NCodec.IGNORE_SPACES);
			for (int ch = 0; ch < 256; ch++) {
				if (ignored.IndexOf((char)ch) < 0) {
					Assert.IsFalse(c.IsIgnored(ch));
				} else {
					Assert.IsTrue(c.IsIgnored(ch));
				}
			}
		}
	}
}

