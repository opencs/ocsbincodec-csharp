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
	public abstract class AbstractCodec : ICodec
	{

		public abstract int GetDecodedSize(int encSize);

		public abstract int GetEncodedSize(int decSize);

		public virtual byte[] Decode(string src)
		{
			return this.Decode(src, 0, src.Length);
		}

		public virtual byte[] Decode(char [] src)
		{
			return this.Decode(src, 0, src.Length);
		}

		public virtual byte[] Decode(ICharSequence src) {
			return this.Decode(src, 0, src.Length);
		}

		public virtual byte[] Decode(string src, int srcOffs, int srcSize) 
		{
			return this.Decode(new StringCharSequence(src), srcOffs, srcSize);
		}

		public virtual byte[] Decode(char [] src, int srcOffs, int srcSize)
		{
			return this.Decode(new ArrayCharSequence(src), srcOffs, srcSize);
		}

		public virtual byte[] Decode(ICharSequence src, int srcOffs, int srcSize) 
		{
			byte [] ret;
			int decSize;

			// Allocate the output array based on the size of getDecodedSize()
			ret = new byte[GetDecodedSize(srcSize)];
			decSize = Decode(src, srcOffs, srcSize, ret, 0);

			// Truncate ret if required
			if (decSize == ret.Length) {
				return ret;
			} else {
				byte [] newRet;
				newRet = new byte[decSize];
				Array.Copy(ret, 0, newRet, 0, newRet.Length);
				return newRet;
			}
		}

		public virtual int Decode(string src, int srcOffs, int srcSize, byte [] dst, int dstOffs) 
		{
			return this.Decode(new StringCharSequence(src), srcOffs, srcSize, dst, dstOffs);
		}

		public virtual int Decode(char [] src, int srcOffs, int srcSize, byte [] dst, int dstOffs) 
		{
			return this.Decode(new ArrayCharSequence(src), srcOffs, srcSize, dst, dstOffs);
		}

		public abstract int Decode(ICharSequence src, int srcOffs, int srcSize, byte [] dst, int dstOffs);

		/// <summary>
		/// Encode the given data.
		/// </summary>
		/// <param name="src">The data to be encoded.</param>
		/// <returns>The encoded data.</returns>
		public virtual string Encode(byte [] src)
		{
			return this.Encode(src, 0, src.Length);
		}

		public virtual string Encode(byte [] src, int srcOffs, int srcSize) 
		{
			StringBuilder sb = new StringBuilder(this.GetEncodedSize(srcSize));
			Encode(src, srcOffs, srcSize, sb);
			return sb.ToString();
		}

		public abstract int Encode(byte [] src, int srcOffs, int srcSize, StringBuilder dst);

	}
}

