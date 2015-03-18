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
using System;

namespace OpenCS.BinCodec
{
	/// <summary>
	/// The alphabet of Base 32 as defined by the RFC4648.
	/// </summary>
	public class Base32Alphabet: ArrayAlphabet
	{

		private const string ALPHABET = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";

		/// <summary>
		/// Creates a new instance of this class. The method 
		/// GetCharacter() will always return upper case characters.
		/// </summary>
		public Base32Alphabet(): this(false) {
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="OpenCS.BinCodec.Base32Alphabet"/> class.
		/// </summary>
		/// <param name="lowerCase">Determines the behavior of getCharacter(). If true, it will
		/// return lower case characters otherwise it will return upper case characters.</param>
		public Base32Alphabet(bool lowerCase): base(lowerCase?ALPHABET.ToLower():ALPHABET) {
		}

		public override int GetValue(int c) {

			if ((c >= 'a') && (c <= 'z')) {
				return c - 'a';
			} else if ((c >= 'A') && (c <= 'Z')) {
				return c - 'A';
			} else if ((c >= '2') && (c <= '7')) {
				return c - '2' + 26;
			} else {
				throw new ArgumentException("Invalid Base 32 character.");
			}
		}	
	}
}

