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
using System.Reflection;

namespace OpenCS.BinCodec
{
	[TestFixture()]
	public class ArrayAlphabetTest: BaseAlphabetTest
	{
		private const string CHARACTERS = "abcdefghij";
		private static readonly char [] CHARACTERS_ARRAY = CHARACTERS.ToCharArray();

		class ArrayAlphabetEx : ArrayAlphabet {

			public ArrayAlphabetEx(string alphabet): base(alphabet) 
			{
			}

			public ArrayAlphabetEx(char [] alphabet): base(alphabet) 
			{
			}

			public char [] Alphabet 
			{
				get 
				{
					return this.alphabet;
				}
			}
		}

		[Test]
		public void TestArrayAlphabetString() {
			ArrayAlphabetEx b = new ArrayAlphabetEx(CHARACTERS);

			Assert.AreEqual(CHARACTERS_ARRAY, b.Alphabet);
		}

		[Test]
		public void TestArrayAlphabetCharArray() {
			ArrayAlphabetEx b = new ArrayAlphabetEx(CHARACTERS_ARRAY);

			Assert.AreNotSame(CHARACTERS_ARRAY, b.Alphabet);
			Assert.AreEqual(CHARACTERS_ARRAY, b.Alphabet);
		}

		[Test]
		public void TestSize(){
			ArrayAlphabet a;

			a = new ArrayAlphabet(CHARACTERS_ARRAY);
			Assert.AreEqual(CHARACTERS.Length, a.Size);
			a = new ArrayAlphabet(CHARACTERS);
			Assert.AreEqual(CHARACTERS.Length, a.Size);
		}

		[Test]
		public void TestGetValue() {
			ArrayAlphabet a;

			a = new ArrayAlphabet(CHARACTERS_ARRAY);
			TestGetValueCore(a, CHARACTERS);
			a = new ArrayAlphabet(CHARACTERS);
			TestGetValueCore(a, CHARACTERS);
		}

		[Test]
		public void TestGetCharacter(){
			ArrayAlphabet a;

			a = new ArrayAlphabet(CHARACTERS_ARRAY);
			TestGetCharacterCore(a, CHARACTERS);
			a = new ArrayAlphabet(CHARACTERS);
			TestGetCharacterCore(a, CHARACTERS);
		}
	
	}
}

