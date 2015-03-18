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

namespace OpenCS.BinCodec.Random
{
	[TestFixture()]
	public class RandomSourceTest
	{
		[Test()]
		public void TestRandomSource ()
		{
			RandomSource r;

			r = new RandomSource(0);
			Assert.IsNotNull(r);
		}

		[Test]
		public void TestNext(){
			TestNextCore("RandomSequence1.txt");
			TestNextCore("RandomSequence2.txt");
			TestNextCore("RandomSequence3.txt");
		}

		private static void TestNextCore(String fileName) {
			RandomSource r;

			using (IntegerFileReader expected = new IntegerFileReader(fileName))
			{
				r = new RandomSource(expected.NextInt);
				for (int i = 0; i < 1000; i++) {
					Assert.AreEqual(expected.NextInt, r.Next);
				}
			}
		}	


		[Test]
		public void TestSync()
		{
			for (int seed = 0; seed < 1000; seed++) {
				RandomSource r1 = new RandomSource(seed);
				RandomSource r2 = new RandomSource(seed);
				for (int j = 0; j < 1000; j++) {
					Assert.AreEqual(r1.Next, r2.Next);
				}
			}
		}
	}
}

