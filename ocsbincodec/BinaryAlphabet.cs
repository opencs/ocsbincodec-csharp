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
	/// This class implements the binary alphabet.
	/// </summary>
	public class BinaryAlphabet: IAlphabet
	{

		private readonly int zero;
		private readonly int one;

		/// <summary>
		/// Creates a new instance of this class using '0' as zero and '1' as one.
		/// </summary>
		public BinaryAlphabet() : this('0', '1') 
		{
		}

		/// <summary>
		/// Creates a new instance of this class using custom characters for zero and one.
		/// </summary>
		/// <param name="zero">The character for zero.</param>
		/// <param name="one">The character for one.</param>
		public BinaryAlphabet(char zero, char one) {

			if (zero == one) {
				throw new ArgumentException("One must not be equal to zero.");
			}
			this.zero = zero;
			this.one = one;
		}

		/// <summary>
		/// Returns the character for zero.
		/// </summary>
		/// <value>The character for zero.</value>
		public int Zero {
			get 
			{
				return this.zero;
			}
		}

		/// <summary>
		/// Returns the character for one.
		/// </summary>
		/// <value>The character for one.</value>
		public int One {
			get
			{
				return this.one;
			}
		}

		public int Size {
			get 
			{
				return 2;
			}
		}

		public int GetCharacter(int v) {

			if (v == 0) {
				return zero;
			} else {
				return one;
			}
		}

		public int GetValue(int c) {

			if (c == zero) {
				return 0;
			} else if (c == one) {
				return 1;
			} else {
				throw new ArgumentException("Invalid character.");
			}
		}
	}
}

