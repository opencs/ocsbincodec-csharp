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
using System.Text;

namespace OpenCS.BinCodec
{

	/// <summary>
	/// This class implements a generic base 2^n codec.
	/// </summary>
	public class Base2NCodec : AbstractCodec
	{
		public static readonly char [] IGNORE_SPACES = " \t\n\r".ToCharArray();

		public static readonly char [] IGNORE_NONE = new char[0];

		private readonly IAlphabet alphabet;
		private readonly int size;
		private readonly int clearMask;
		private readonly int paddingChar;
		private readonly int paddingBLockSize;
		private readonly char [] ignored;

		/// <summary>
		/// Creates a new instance of this class. This constructor disables the padding and the ignore list.
		/// </summary>
		/// <param name="alphabet">The alphabet to be used. The size of the alphabet must be a power of two.</param>
		public Base2NCodec(IAlphabet alphabet) : this(alphabet, 0, 0, null) {
		}

		/// <summary>
		/// Creates a new instance of this class. This constructor disables the ignore list.
		/// </summary>
		/// <param name="alphabet">The alphabet to be used. The size of the alphabet must be a power of two.</param>
		/// <param name="paddingChar">The padding character.</param>
		/// <param name="paddingBlockSize">The size of the padding block. Any value smaller than 2 disables the padding.</param>
		public Base2NCodec(IAlphabet alphabet, int paddingChar, int paddingBlockSize) : 
				this(alphabet, paddingChar, paddingBlockSize, null) {
		}

		/// <summary>
		/// Creates a new instance of this class. This constructor disables the ignore list.
		/// </summary>
		/// <param name="alphabet">The alphabet to be used. The size of the alphabet must be a power of two.</param>
		/// <param name="paddingChar">The padding character.</param>
		/// <param name="paddingBlockSize">The size of the padding block. Any value smaller than 2 disables the padding.</param>
		/// <param name="ignored">The list of ignored characters.</param>
		public Base2NCodec(IAlphabet alphabet, int paddingChar, int paddingBlockSize, char [] ignored) {

			this.alphabet = alphabet;
			this.paddingBLockSize = paddingBlockSize;
			if (paddingBLockSize != 0) {
				this.paddingChar = paddingChar;
			} else {
				this.paddingChar = 0;
			}
			this.size = GetCharacterSize(alphabet);
			this.clearMask = (1 << this.size) - 1;
			if (ignored != null) {
				this.ignored = (char [])ignored.Clone();
			} else {
				this.ignored = IGNORE_NONE;
			}
		}

		private static int GetCharacterSize(IAlphabet alphabet) {

			switch (alphabet.Size) {
				case 2:
					return 1;
				case 4:
					return 2;
				case 8:
					return 3;
				case 16:
					return 4;
				case 32:
					return 5;
				case 64:
					return 6;
				case 126:
					return 7;
				default:
					throw new ArgumentException("Invalid alphabet.");
			}
		}

		public override int GetDecodedSize(int encSize) {
			return (encSize * size) / 8;
		}

		/// <summary>
		/// Returns the size of the padding.
		/// </summary>
		/// <returns>The size of the padding. It is always zero if the padding is not used.</returns>
		/// <param name="totalSize">The total size of the output.</param>
		protected int GetPaddingSize(int totalSize) {

			if (this.UsesPadding) {
				return (this.paddingBLockSize - (totalSize % this.paddingBLockSize)) % this.paddingBLockSize;
			} else {
				return 0;
			}
		}

		public override int GetEncodedSize(int decSize) {
			int totalSize;

			totalSize = (((decSize * 8) + size - 1) / size); 
			return totalSize + GetPaddingSize(totalSize);
		}

		/// <summary>
		/// Verifies if this instance uses padding or not.
		/// </summary>
		/// <value>true if the padding is used of false otherwise.</value>
		public bool UsesPadding {
			get
			{
				return (this.paddingBLockSize >= 2);
			}
		}

		protected bool IsPadding(int c) {

			if (UsesPadding) {
				return (c == this.paddingChar);
			} else {
				return false;
			}
		}

		/// <summary>
		/// Verifies if c is inside the ignored list.
		/// </summary>
		/// <returns>true if c must be ignored or false otherwise.</returns>
		/// <param name="c">The character to be verified.</param>
		protected bool IsIgnored(int c) {

			for (int i = 0; i < this.ignored.Length; i++) {
				if (c == this.ignored[i]) {
					return true;
				}
			}
			return false;			
		}

		public override int Decode(ICharSequence src, int srcOffs, int srcSize, byte [] dst, int dstOffs) {
			int bitBuffer;
			int bitBufferSize;
			int srcEndOffs;
			int c;
			int oldDstOffs;
			bool stop;
			int paddingSize;
			int srcTrueSize;

			// Scan the src
			bitBuffer = 0;
			bitBufferSize = 0;
			oldDstOffs = dstOffs;
			paddingSize = 0;
			srcTrueSize = 0;
			srcEndOffs = srcOffs + srcSize;
			stop = srcOffs >= srcEndOffs;
			while (!stop) {
				// Get a character from source
				c = src.CharAt(srcOffs);
				srcOffs++;
				stop = srcOffs == srcEndOffs;

				// Check if the character must be ignored or not
				if (!IsIgnored(c)) {
					srcTrueSize++;
					// Process the character.
					if (IsPadding(c)) {
						stop = true;
						paddingSize = 1;
					} else {
						// Add it to the bit buffer
						bitBuffer = (bitBuffer << size) | (alphabet.GetValue(c));
						bitBufferSize += size;

						// Add bytes to dst
						while (bitBufferSize >= 8) {
							bitBufferSize -= 8;
							dst[dstOffs] = (byte)((bitBuffer >> bitBufferSize) & 0xFF);
							dstOffs++;
						}
					}
				}
			}

			// Check padding, if required
			if (UsesPadding) {
				if (paddingSize > 0) {
					// Remove the padding
					stop = (srcOffs == srcEndOffs);
					while (!stop) {
						// Get a character from source
						c = src.CharAt(srcOffs);
						srcOffs++;
						stop = (srcOffs == srcEndOffs);
						// Verify if it has a valid padding or not
						if (!IsIgnored(c)) {
							srcTrueSize++;
							if (!IsPadding(c)) {
								throw new ArgumentException("Invalid padding.");
							}
						}					
					}
				}
				if ((srcTrueSize % this.paddingBLockSize) != 0) {
					throw new ArgumentException("The input is not properly padded.");
				}
			}

			return  dstOffs - oldDstOffs;
		}

		public override int Encode(byte [] src, int srcOffs, int srcSize, StringBuilder dst) {
			int bitBuffer;
			int bitBufferSize;
			int srcEndOffs;
			int dstSize;

			bitBuffer = 0;
			bitBufferSize = 0;
			srcEndOffs = srcOffs + srcSize;
			dstSize = 0;
			while (srcOffs < srcEndOffs) {
				// Get byte from source
				bitBuffer = (bitBuffer << 8) | (src[srcOffs] & 0xFF);
				srcOffs++;
				bitBufferSize += 8;

				// Add characters to dst
				while (bitBufferSize >= size) {
					bitBufferSize -= size;
					dst.Append((char)this.alphabet.GetCharacter((bitBuffer >> bitBufferSize) & clearMask));
					dstSize++;
				}			
			}

			// Add the last one
			if (bitBufferSize > 0) {
				bitBuffer = bitBuffer << (size - bitBufferSize);
				dst.Append((char)this.alphabet.GetCharacter(bitBuffer & clearMask));
				dstSize++;
			}

			// Add padding if required
			int paddingSize = GetPaddingSize(dstSize);
			dstSize += paddingSize;
			while (paddingSize > 0) {
				dst.Append((char)this.paddingChar);
				paddingSize--;
			}
			return dstSize;
		}	
	}
}

