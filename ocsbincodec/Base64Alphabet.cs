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
	/// The alphabet of Base64 as defined by the RFC4648. It can use the standard and the URL safe variants.
	/// </summary>
	public class Base64Alphabet : ArrayAlphabet
	{
		private const string ALPHABET_PREFIX = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"; 
		private const string STANDARD_POSTFIX = "+/";
		private const string SAFE_POSTFIX = "-_";

		private readonly int c62;
		private readonly int c63;

		/// <summary>
		/// Creates a new instance of this class using the standard alphabet.
		/// </summary>
		public Base64Alphabet() : this(false){
		}

		/// <summary>
		/// Creates a new instance of this.
		/// </summary>
		/// <param name="urlSafe">If true, uses the URL safe alphabet otherwise used the standard one.</param>
		public Base64Alphabet(bool urlSafe) : base(ALPHABET_PREFIX + (urlSafe ? SAFE_POSTFIX : STANDARD_POSTFIX)) {

			if (urlSafe) {
				c62 = '-';
				c63 = '_';
			} else {
				c62 = '+';
				c63 = '/';
			}
		}

		public override int GetValue(int c) {

			if ((c >= 'A') && (c <= 'Z')) {
				return c - 'A';
			} else if ((c >= 'a') && (c <= 'z')) {
				return c - 'a' + 26;
			} else  if ((c >= '0') && (c <= '9')) {
				return c - '0' + 52;
			} else if (c == c62) {
				return 62;
			} else if (c == c63) {
				return 63;
			} else {
				throw new ArgumentException("Invalid Base64 character.");
			}
		}	
	}
}

