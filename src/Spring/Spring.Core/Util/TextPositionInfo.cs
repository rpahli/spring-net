#region License

/*
 * Copyright © 2002-2011 the original author or authors.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

#endregion

#region Imports

#endregion

namespace Spring.Util
{
    /// <summary>
    ///     Holds text position information for e.g. error reporting purposes.
    /// </summary>
    /// <seealso cref="ConfigXmlElement" />
    /// <seealso cref="ConfigXmlAttribute" />
    public class TextPositionInfo : ITextPosition
    {
        /// <summary>
        ///     Creates a new TextPositionInfo instance.
        /// </summary>
        public TextPositionInfo(string filename, int lineNumber, int linePosition)
        {
            Filename = filename;
            LineNumber = lineNumber;
            LinePosition = linePosition;
        }

        /// <summary>
        ///     Creates a new TextPositionInfo instance, copying values from another instance.
        /// </summary>
        public TextPositionInfo(ITextPosition other)
        {
            if (other != null)
            {
                Filename = other.Filename;
                LineNumber = other.LineNumber;
                LinePosition = other.LinePosition;
            }
        }

        /// <summary>
        ///     The filename related to this text position
        /// </summary>
        public string Filename { get; }

        /// <summary>
        ///     The line number related to this text position
        /// </summary>
        public int LineNumber { get; }

        /// <summary>
        ///     The line position related to this text position
        /// </summary>
        public int LinePosition { get; }
    }
}