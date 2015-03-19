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
	public class AbstractCodecTest : BaseCodecTest
	{
		private static readonly  byte[] BIN = {
			(byte)'J', (byte)'u', (byte)'s', (byte)'t', (byte)' ', (byte)'f', (byte)'o', (byte)'r',
			(byte)' ', (byte)'f', (byte)'u', (byte)'n', (byte)'!'
		};
		private const string TXT = "Just for fun!";

		[Test]
		public void TestGetDecodedSize ()
		{
			TestCodec c;

			c = new TestCodec ();
			for (int size = 0; size < 1024; size++) {
				Assert.AreEqual (size, c.GetDecodedSize (size));
			}
		}

		[Test]
		public void TestGetEncodedSize ()
		{
			TestCodec c;

			c = new TestCodec ();
			for (int size = 0; size < 1024; size++) {
				Assert.AreEqual (size, c.GetEncodedSize (size));
			}
		}

		[Test]
		public void TestDecodeString ()
		{
			TestCodec c;
			byte[] dst;

			c = new TestCodec ();
			dst = c.Decode (TXT);
			Assert.AreEqual (BIN, dst);		
		}

		[Test]
		public void TestDecodeCharArray ()
		{
			TestCodec c;
			byte[] dst;

			c = new TestCodec ();
			dst = c.Decode (TXT.ToCharArray());
			Assert.AreEqual (BIN, dst);		
		}

		[Test]
		public void TestDecodeCharSequence ()
		{
			TestCodec c;
			byte[] dst;

			c = new TestCodec ();
			dst = c.Decode (new StringCharSequence(TXT));
			Assert.AreEqual (BIN, dst);		
		}

		[Test]
		public void TestDecodeStringIntInt ()
		{
			TestCodec c;
			byte[] dst;

			c = new TestCodec ();

			dst = c.Decode (TXT, 0, TXT.Length);
			Assert.AreEqual (BIN, dst);		

			dst = c.Decode (" " + TXT + " ", 1, TXT.Length);
			Assert.AreEqual (BIN, dst);		
		}

		[Test]
		public void TestDecodeCharArrayIntInt ()
		{
			TestCodec c;
			byte[] dst;

			c = new TestCodec ();

			dst = c.Decode (TXT.ToCharArray(), 0, TXT.Length);
			Assert.AreEqual (BIN, dst);		

			dst = c.Decode ((" " + TXT + " ").ToCharArray(), 1, TXT.Length);
			Assert.AreEqual (BIN, dst);		
		}

		[Test]
		public void TestDecodeCharSequenceIntInt ()
		{
			TestCodec c;
			byte[] dst;

			c = new TestCodec ();

			dst = c.Decode (new StringCharSequence(TXT), 0, TXT.Length);
			Assert.AreEqual (BIN, dst);		

			dst = c.Decode (new StringCharSequence(" " + TXT + " "), 1, TXT.Length);
			Assert.AreEqual (BIN, dst);		
		}

		[Test]
		public void TestDecodeStringIntIntByteArrayInt ()
		{
			TestCodec c;
			byte[] dst;
			int size;

			c = new TestCodec ();

			dst = new byte[BIN.Length];
			size = c.Decode (TXT, 0, TXT.Length, dst, 0);
			Assert.AreEqual (BIN.Length, size);
			Assert.AreEqual (BIN, dst);

			dst = new byte[BIN.Length + 2];
			size = c.Decode (TXT, 0, TXT.Length, dst, 1);
			Assert.AreEqual (BIN.Length, size);
			Assert.AreEqual (0, dst [0]);
			Assert.AreEqual (0, dst [dst.Length - 1]);


			Assert.AreEqual (BIN, CopyOfRange (dst, 1, dst.Length - 2));

			dst = new byte[BIN.Length];
			size = c.Decode (" " + TXT + " ", 1, TXT.Length, dst, 0);
			Assert.AreEqual (BIN.Length, size);
			Assert.AreEqual (BIN, dst);

			dst = new byte[BIN.Length + 2];
			size = c.Decode (" " + TXT + " ", 1, TXT.Length, dst, 1);
			Assert.AreEqual (BIN.Length, size);
			Assert.AreEqual (0, dst [0]);
			Assert.AreEqual (0, dst [dst.Length - 1]);
			Assert.AreEqual (BIN, CopyOfRange (dst, 1, dst.Length - 2));
		}

		[Test]
		public void TestDecodeCharArrayIntIntByteArrayInt ()
		{
			TestCodec c;
			byte[] dst;
			int size;

			c = new TestCodec ();

			dst = new byte[BIN.Length];
			size = c.Decode (TXT.ToCharArray(), 0, TXT.Length, dst, 0);
			Assert.AreEqual (BIN.Length, size);
			Assert.AreEqual (BIN, dst);

			dst = new byte[BIN.Length + 2];
			size = c.Decode (TXT.ToCharArray(), 0, TXT.Length, dst, 1);
			Assert.AreEqual (BIN.Length, size);
			Assert.AreEqual (0, dst [0]);
			Assert.AreEqual (0, dst [dst.Length - 1]);


			Assert.AreEqual (BIN, CopyOfRange (dst, 1, dst.Length - 2));

			dst = new byte[BIN.Length];
			size = c.Decode ((" " + TXT + " ").ToCharArray(), 1, TXT.Length, dst, 0);
			Assert.AreEqual (BIN.Length, size);
			Assert.AreEqual (BIN, dst);

			dst = new byte[BIN.Length + 2];
			size = c.Decode ((" " + TXT + " ").ToCharArray(), 1, TXT.Length, dst, 1);
			Assert.AreEqual (BIN.Length, size);
			Assert.AreEqual (0, dst [0]);
			Assert.AreEqual (0, dst [dst.Length - 1]);
			Assert.AreEqual (BIN, CopyOfRange (dst, 1, dst.Length - 2));
		}

		[Test]
		public void TestDecodeCharSequenceIntIntByteArrayInt ()
		{
			TestCodec c;
			byte[] dst;
			int size;

			c = new TestCodec ();

			dst = new byte[BIN.Length];
			size = c.Decode (new StringCharSequence(TXT), 0, TXT.Length, dst, 0);
			Assert.AreEqual (BIN.Length, size);
			Assert.AreEqual (BIN, dst);

			dst = new byte[BIN.Length + 2];
			size = c.Decode (new StringCharSequence(TXT), 0, TXT.Length, dst, 1);
			Assert.AreEqual (BIN.Length, size);
			Assert.AreEqual (0, dst [0]);
			Assert.AreEqual (0, dst [dst.Length - 1]);


			Assert.AreEqual (BIN, CopyOfRange (dst, 1, dst.Length - 2));

			dst = new byte[BIN.Length];
			size = c.Decode (new StringCharSequence(" " + TXT + " "), 1, TXT.Length, dst, 0);
			Assert.AreEqual (BIN.Length, size);
			Assert.AreEqual (BIN, dst);

			dst = new byte[BIN.Length + 2];
			                 size = c.Decode (new StringCharSequence(" " + TXT + " "), 1, TXT.Length, dst, 1);
			Assert.AreEqual (BIN.Length, size);
			Assert.AreEqual (0, dst [0]);
			Assert.AreEqual (0, dst [dst.Length - 1]);
			Assert.AreEqual (BIN, CopyOfRange (dst, 1, dst.Length - 2));
		}

		[Test]
		public void TestEncodeByteArray ()
		{
			TestCodec c;
			String dst;

			c = new TestCodec ();

			dst = c.Encode (BIN);
			Assert.AreEqual (TXT, dst);
		}

		[Test]
		public void TestEncodeByteArrayIntInt ()
		{
			TestCodec c;
			String dst;

			c = new TestCodec ();

			dst = c.Encode (BIN, 0, BIN.Length);
			Assert.AreEqual (TXT, dst);

			byte[] tmp = new byte[BIN.Length + 2];

			Array.Copy (BIN, 0, tmp, 1, BIN.Length);
			dst = c.Encode (tmp, 1, BIN.Length);
			Assert.AreEqual (TXT, dst);
		}

		[Test]
		public void TestEncodeByteArrayIntIntAppendable ()
		{
			TestCodec c;
			StringBuilder dst;
			int size;

			c = new TestCodec ();

			dst = new StringBuilder ();
			size = c.Encode (BIN, 0, BIN.Length, dst);
			Assert.AreEqual (TXT.Length, size);
			Assert.AreEqual (TXT, dst.ToString ());

			byte[] tmp = new byte[BIN.Length + 2];
			Array.Copy (BIN, 0, tmp, 1, BIN.Length);
			dst = new StringBuilder ();
			size = c.Encode (tmp, 1, BIN.Length, dst);
			Assert.AreEqual (TXT.Length, size);
			Assert.AreEqual (TXT, dst.ToString ());
		}
	}
}

