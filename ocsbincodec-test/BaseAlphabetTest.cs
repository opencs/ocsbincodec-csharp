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
using System.Reflection;
using NUnit.Framework;

namespace OpenCS.BinCodec
{
	public class BaseAlphabetTest
	{

		public static Object GetFieldValue(object instance, String name)
		{
			Type t = instance.GetType();

			FieldInfo f = t.GetField(name, BindingFlags.Instance 
			                             | BindingFlags.NonPublic 
			                             | BindingFlags.Public );
			return f.GetValue( instance );
		}


		protected virtual void TestGetCharacterCore(IAlphabet a, string characters) {

			for (int i = 0; i < characters.Length; i++) {
				Assert.AreEqual(characters[i], a.GetCharacter(i));
			}		
		}	

		protected virtual void TestGetValueCore(IAlphabet a, string characters) {

			for (int c = 0; c < 256; c++) {
				int v = characters.IndexOf((char)c);
				if (v < 0) {
					try {
						Assert.AreEqual(v, a.GetValue(c));
						Assert.Fail();
					} catch (ArgumentException) {}
				} else {
					Assert.AreEqual(v, a.GetValue(c));
				}
			}		
		}

		protected virtual void TestGetValueCoreCaseInsensitive(IAlphabet a, string characters) {

			characters = characters.ToUpper();
			for (int c = 0; c < 256; c++) {
				int v = characters.IndexOf(char.ToUpper((char)c));
				if (v < 0) {
					try {
						Assert.AreEqual(v, a.GetValue(c));
						Assert.Fail();
					} catch (ArgumentException) {}
				} else {
					Assert.AreEqual(v, a.GetValue(c));
				}
			}		
		}
	}
}

