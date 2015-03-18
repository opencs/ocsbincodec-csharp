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
using System.IO;

namespace OpenCS.BinCodec.Random
{
	public class IntegerFileReader: IDisposable
	{
		private TextReader reader;

		public int LineNumber 
		{
			get;
			private set;
		}

		public IntegerFileReader(string fileName) : this(new FileStream(fileName, FileMode.Open))
		{
		}

		public IntegerFileReader (Stream inp) : this(new StreamReader(inp, System.Text.Encoding.UTF8))
		{
		}

		public IntegerFileReader (TextReader reader)
		{
			this.reader = reader;
			this.LineNumber = 0;
		}

		public void Close()
		{
			this.reader.Close();
		}

		private string ReadNextLine()
		{
			this.LineNumber++;
			return this.reader.ReadLine();
		}

		private string GetValidLine()
		{
			string line;
			bool stop;

			stop = false;
			do {
				line = ReadNextLine();
				if (line == null) {
					stop = true;
				} else {
					line = line.Trim();
					stop = ((line.Length != 0) && (!line.StartsWith("#")));
				}
			} while (!stop);
			return line;
		}

		public int NextInt
		{
			get 
			{
				string line = GetValidLine();
				if (line == null) {
					throw new EndOfStreamException();
				} else {
					try {
						return int.Parse(line);
					} catch (Exception e) {
						throw new  IOException(string.Format("Invalid integer on line {0}.", this.LineNumber), e);
					}
				}
			}
		}

		public void Dispose()
		{
			this.reader.Dispose();
		}
	}
}
