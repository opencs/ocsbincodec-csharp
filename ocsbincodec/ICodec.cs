// /*
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
	/// This is the interface implemented by all binary-to-text codecs in this library.
	/// </summary>
	/// <remarks>
	/// Instances of Codec are expected to be thread safe.
	/// </remarks>
	public interface ICodec
	{

		/// <summary>
		/// Returns the expected number of bytes based on the size of the encoded size.
		/// </summary>
		/// <remarks>
		/// This value is always greater or equal to the actual decoded data. 
		/// </remarks>
		/// <returns>The expected decoded size.</returns>
		/// <param name="encSize">The encoded size characters.</param>
		int GetDecodedSize(int encSize);

		/// <summary>
		/// Returns the size of the encoded data based on the decoded size.
		/// </summary>
		/// <returns>The number of characters required to encode the data.</returns>
		/// <param name="decSize">The decoded size in bytes.</param>
		int GetEncodedSize(int decSize);

		/// <summary>
		/// Decodes the encoded data.
		/// </summary>
		/// <param name="src"> The encoded data.</param>
		/// <returns>The decoded value.</returns>
		/// <exception cref="System.ArgumentException">If src is invalid.</exception>
		byte[] Decode(string src);

		/// <summary>
		/// Decodes the encoded data.
		/// </summary>
		/// <param name="src"> The encoded data.</param>
		/// <returns>The decoded value.</returns>
		/// <exception cref="System.ArgumentException">If src is invalid.</exception>
		byte[] Decode(ICharSequence src);

		/// <summary>
		/// Decodes the encoded data.
		/// </summary>
		/// <param name="src">The encoded data.</param>
		/// <param name="srcOffs">The initial offset in src.</param>
		/// <param name="srcSize">The number of characters in src.</param>
		/// <returns>The decoded value.</returns>
		/// <exception cref="System.ArgumentException">If src is invalid.</exception>
		byte[] Decode(string src, int srcOffs, int srcSize);

		/// <summary>
		/// Decodes the encoded data.
		/// </summary>
		/// <param name="src">The encoded data.</param>
		/// <param name="srcOffs">The initial offset in src.</param>
		/// <param name="srcSize">The number of characters in src.</param>
		/// <returns>The decoded value.</returns>
		/// <exception cref="System.ArgumentException">If src is invalid.</exception>
		byte[] Decode(ICharSequence src, int srcOffs, int srcSize);

		/// <summary>
		/// Decodes the encoded data and puts the result in dst.
		/// </summary>
		/// <remarks>
		/// The array dst must be large enough to hold the decoded data.
		/// </remarks>
		/// <param name="src">The encoded data.</param>
		/// <param name="srcOffs">The initial offset in src.</param>
		/// <param name="srcSize">The number of characters in src.</param>
		/// <param name="dst">The output buffer.</param>
		/// <param name="dstOffs">The initial offset in the output buffer.</param>
		/// <returns>The number of byes added to dst.</returns>
		/// <exception cref="System.ArgumentException">If src is invalid.</exception>
		int Decode(ICharSequence src, int srcOffs, int srcSize, byte [] dst, int dstOffs);

		/// <summary>
		/// Encode the given data.
		/// </summary>
		/// <param name="src">The data to be encoded.</param>
		/// <returns>The encoded data.</returns>
		string Encode(byte [] src);

		/// <summary>
		/// Encode the given data.
		/// </summary>
		/// <param name="src">The data to be encoded.</param>
		/// <param name="srcOffs">The offset of the data in src.</param>
		/// <param name="srcSize">The number of bytes in src.</param>
		/// <returns>The encoded data.</returns>
		string encode(byte [] src, int srcOffs, int srcSize);

		/// <summary>
		/// Encode the given data.
		/// </summary>
		/// <param name="src">The data to be encoded.</param>
		/// <param name="srcOffs">The offset of the data in src.</param>
		/// <param name="srcSize">The number of bytes in src.</param>
		/// <param name="dst">The destination of the encoded data.</param>
		/// <returns>The encoded data.</returns>
		int encode(byte [] src, int srcOffs, int srcSize, StringBuilder dst);
	}
}

