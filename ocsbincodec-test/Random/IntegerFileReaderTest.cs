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
using System.IO;

namespace OpenCS.BinCodec.Random
{
	[TestFixture()]
	public class IntegerFileReaderTest
	{
		[Test]
		public void TestIntegerFileReaderString ()
		{
			using (IntegerFileReader inp = new IntegerFileReader("IntegerFileReaderTest1.txt"))
			{
			}
		}

		[Test]
		public void TestIntegerFileReaderStream ()
		{
			using (IntegerFileReader inp = new IntegerFileReader(new FileStream("IntegerFileReaderTest1.txt", FileMode.Open)))
			{
			}
		}

		[Test]
		public void TestIntegerFileReaderReader ()
		{
			using (IntegerFileReader inp = new IntegerFileReader(new FileStream("IntegerFileReaderTest1.txt", FileMode.Open)))
			{
			}
		}

		[Test]
		public void TestNextInt() {
			// 0-9, no newline on last line
			TestNextIntCore("IntegerFileReaderTest1.txt");
			// 0-9, new line on the no newline on last line
			TestNextIntCore("IntegerFileReaderTest2.txt");
			// 0-9, with empty lines
			TestNextIntCore("IntegerFileReaderTest3.txt");
			// 0-9, with empty lines and comments
			TestNextIntCore("IntegerFileReaderTest4.txt");
		}

		protected static void TestNextIntCore(string fileName) {


			using (IntegerFileReader r = new IntegerFileReader(fileName))
			{
				for (int i = 0; i < 10; i++) {
					Assert.AreEqual(i, r.NextInt);
				}
				try {
					int n = r.NextInt;
					Assert.Fail();
				} catch (EndOfStreamException) {}
			}
		}

		[Test]
		public void TestNextIntBroken() {

			using (IntegerFileReader r = new IntegerFileReader("IntegerFileReaderTest5.txt"))
			{
				for (int i = 0; i < 7; i++) {
					Assert.AreEqual(i, r.NextInt);
				}
				try {
					int n = r.NextInt;
					Assert.Fail();
				} catch (IOException e) {
					Assert.AreEqual("Invalid integer on line 10.", e.Message);
				}
			}
		}
	}
}

