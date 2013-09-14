/*
    Copyright (C) 2012-2013 de4dot@gmail.com

    Permission is hereby granted, free of charge, to any person obtaining
    a copy of this software and associated documentation files (the
    "Software"), to deal in the Software without restriction, including
    without limitation the rights to use, copy, modify, merge, publish,
    distribute, sublicense, and/or sell copies of the Software, and to
    permit persons to whom the Software is furnished to do so, subject to
    the following conditions:

    The above copyright notice and this permission notice shall be
    included in all copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
    EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
    MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
    IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY
    CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
    TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
    SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

﻿using System;
using dnlib.IO;

namespace dnlib.DotNet.MD {
	/// <summary>
	/// Represents the #GUID stream
	/// </summary>
	public sealed class GuidStream : DotNetStream {
		/// <inheritdoc/>
		public GuidStream() {
		}

		/// <inheritdoc/>
		public GuidStream(IImageStream imageStream, StreamHeader streamHeader)
			: base(imageStream, streamHeader) {
		}

		/// <inheritdoc/>
		public override bool IsValidIndex(uint index) {
			return index == 0 || (index <= 0x10000000 && IsValidOffset((index - 1) * 16, 16));
		}

		/// <summary>
		/// Read a <see cref="Guid"/>
		/// </summary>
		/// <param name="index">Index into this stream</param>
		/// <returns>A <see cref="Guid"/> or <c>null</c> if <paramref name="index"/> is 0 or invalid</returns>
		public Guid? Read(uint index) {
			if (index == 0 || !IsValidIndex(index))
				return null;
			imageStream.Position = (index - 1) * 16;
			return new Guid(imageStream.ReadBytes(16));
		}
	}
}
