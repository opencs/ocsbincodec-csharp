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
using OpenCS.BinCodec;

namespace OpenCS.BinCodec.Random
{
	[TestFixture()]
	public class RandomAlphabetGeneratorTest
	{

		private const string CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

		[Test]
		public void TestGenerateRandom() {
			char []src;
			char []dst; 

			// Initialize the source
			src = CHARS.ToCharArray();

			// Basic tests
			for (int size = 1; size < src.Length; size++) {
				dst = (char[])src.Clone();
				char [] m1 = RandomAlphabetGenerator.GenerateRandom(size, 4, dst, size);
				Assert.AreEqual(src, dst); // No changes in the input parameter
				Assert.AreEqual(size, m1.Length);
				foreach (char c in m1) {
					Assert.IsTrue(CHARS.IndexOf(c) >= 0);
				}

				char [] m2 = RandomAlphabetGenerator.GenerateRandom(size, 4, dst, size);
				Assert.AreEqual(m1, m2);			
			}
		}

		[Test]
		public void TestShuffle() {
			int count;
			char [] src;
			char [] dst; 

			// Initialize the source
			src = CHARS.ToCharArray();

			// No shuffle at all
			dst = (char[])src.Clone();
			RandomAlphabetGenerator.Shuffle(0, 0, dst);
			count = 0;
			for (int i = 0; i < src.Length; i++) {
				count += ((src[i] == dst[i])?0:1);
			}
			Assert.AreEqual(0, count);

			// For rounds
			dst = (char[])src.Clone();
			RandomAlphabetGenerator.Shuffle(0, 4, dst);
			count = 0;
			for (int i = 0; i < src.Length; i++) {
				count += ((src[i] == dst[i])?0:1);
			}
			Assert.IsTrue(count > (src.Length / 3));

			// Ensure that the same parameters will result in the same mapping
			dst = (char[])src.Clone();
			char [] dst2 = (char[]) src.Clone();

			RandomAlphabetGenerator.Shuffle(0, 4, dst);
			RandomAlphabetGenerator.Shuffle(0, 4, dst2);
			Assert.AreEqual(dst, dst2);
		}
	}
}

