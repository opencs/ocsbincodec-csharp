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

namespace OpenCS.BinCodec
{
	[TestFixture()]
	public class Base32AlphabetTest : BaseAlphabetTest
	{
		private const string ALPHABET = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";
		private const string ALPHABET_LC = "abcdefghijklmnopqrstuvwxyz234567";

		[Test]
		public void TestBase32Alphabet() {
			Base32Alphabet a;

			a = new Base32Alphabet();
			Assert.AreEqual(ALPHABET.ToCharArray(), GetFieldValue(a, "alphabet"));
		}

		[Test]
		public void TestBase32AlphabetBoolean() {
			Base32Alphabet a;

			a = new Base32Alphabet();
			Assert.AreEqual(ALPHABET.ToCharArray(), GetFieldValue(a, "alphabet"));

			a = new Base32Alphabet(false);
			Assert.AreEqual(ALPHABET.ToCharArray(), GetFieldValue(a, "alphabet"));

			a = new Base32Alphabet(true);
			Assert.AreEqual(ALPHABET_LC.ToCharArray(), GetFieldValue(a, "alphabet"));
		}

		[Test]
		public void TestSize(){
			Base32Alphabet a;

			a = new Base32Alphabet();
			Assert.AreEqual(ALPHABET.Length, a.Size);

			a = new Base32Alphabet(false);
			Assert.AreEqual(ALPHABET.Length, a.Size);

			a = new Base32Alphabet(true);
			Assert.AreEqual(ALPHABET_LC.Length, a.Size);
		}

		[Test]
		public void TestGetCharacter(){
			Base32Alphabet a;

			a = new Base32Alphabet();
			TestGetCharacterCore(a, ALPHABET);

			a = new Base32Alphabet(false);
			TestGetCharacterCore(a, ALPHABET);

			a = new Base32Alphabet(true);
			TestGetCharacterCore(a, ALPHABET_LC);
		}

		[Test]
		public void TestGetValue() {
			Base32Alphabet a;

			a = new Base32Alphabet();
			TestGetValueCoreCaseInsensitive(a, ALPHABET);

			a = new Base32Alphabet(false);
			TestGetValueCoreCaseInsensitive(a, ALPHABET);

			a = new Base32Alphabet(true);
			TestGetValueCoreCaseInsensitive(a, ALPHABET);
		}
	}
}

