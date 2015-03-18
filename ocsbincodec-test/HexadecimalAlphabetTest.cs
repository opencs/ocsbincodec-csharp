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
	public class HexadecimalAlphabetTest: BaseAlphabetTest
	{
		private const string CHARACTERS = "0123456789ABCDEF";
		private static readonly char[] CHARACTERS_ARRAY = CHARACTERS.ToCharArray();

		private const String CHARACTERS_LC = "0123456789abcdef";
		private static readonly char[] CHARACTERS_ARRAY_LC = CHARACTERS_LC.ToCharArray();

		[Test]
		public void TestHexadecimalAlphabet() {
			HexadecimalAlphabet a;

			a = new HexadecimalAlphabet();
			Assert.AreEqual(CHARACTERS_ARRAY, GetFieldValue(a, "alphabet"));
		}

		[Test]
		public void TestHexadecimalBoolean() {
			HexadecimalAlphabet a;

			a = new HexadecimalAlphabet(false);
			Assert.AreEqual(CHARACTERS_ARRAY, GetFieldValue(a, "alphabet"));

			a = new HexadecimalAlphabet(true);
			Assert.AreEqual(CHARACTERS_ARRAY_LC, GetFieldValue(a, "alphabet"));
		}	

		[Test]
		public void TestSize(){
			HexadecimalAlphabet a;

			a = new HexadecimalAlphabet();
			Assert.AreEqual(16, a.Size);

			a = new HexadecimalAlphabet(false);
			Assert.AreEqual(16, a.Size);

			a = new HexadecimalAlphabet(true);
			Assert.AreEqual(16, a.Size);
		}

		[Test]
		public void TestGetValue(){
			HexadecimalAlphabet a;

			a = new HexadecimalAlphabet(false);
			TestGetValueCoreCaseInsensitive(a, CHARACTERS);
			a = new HexadecimalAlphabet(true);
			TestGetValueCoreCaseInsensitive(a, CHARACTERS_LC);
		}

		[Test]
			public void TestGetCharacter(){
			HexadecimalAlphabet a;

			a = new HexadecimalAlphabet(false);
			TestGetCharacterCore(a, CHARACTERS);
			a = new HexadecimalAlphabet(true);
			TestGetCharacterCore(a, CHARACTERS_LC);
		}
	
	}
}

