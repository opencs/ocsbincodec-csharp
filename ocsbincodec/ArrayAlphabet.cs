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
	public class ArrayAlphabet : IAlphabet
	{
		protected readonly char [] alphabet;

		public ArrayAlphabet (char [] alphabet)
		{
			this.alphabet = (char []) alphabet.Clone();
		}

		public ArrayAlphabet(string alphabet)
		{
			this.alphabet = alphabet.ToCharArray();
		}

		/// <summary>
		/// The size of the alphabet.
		/// </summary>
		/// <value>The size of the alphabet in characters.</value>
		public int Size {
			get
			{
				return this.alphabet.Length;
			}
		}

		/// <summary>
		/// Returns the character for a given value.
		/// </summary>
		/// <returns>The character for the given value. </returns>
		/// <param name="v">The value. It must be a value between 0 and IAlphabet.Size - 1.</param>
		public virtual int GetCharacter(int v) 
		{
			return this.alphabet[v];
		}

		/// <summary>
		/// Returns the value of a given character.
		/// </summary>
		/// <remarks>
		/// This implementation uses a simple sequential scan to map
		/// the character back to its value. Subclasses are encouraged to
		/// provide the most efficient implementation possible.
		/// </remarks>
		/// <returns>The value of the character.</returns>
		/// <param name="c">The character.</param>
		/// <exception cref="System.ArgumentException">If c is not in the alphabet.</exception>
		public virtual int GetValue(int c) 
		{
			int v;

			// This implementation will use a very simple sequential scan
			for (v = 0; v < this.alphabet.Length; v++) {
				if (c == this.alphabet[v]) {
					return v;
				}
			}
			throw new ArgumentException("Invalid character.");
		}
	}
}

