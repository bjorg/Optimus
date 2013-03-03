/*
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *     http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 * 
 */

using System;
using System.Text;

namespace Optimus.Text {
    public static class XString {
                
        /// <summary>
        /// Replace all occurences of a number of strings.
        /// </summary>
        /// <param name="source">Source string.</param>
        /// <param name="replacements">Array of strings to match and their replacements. Each string to be replaced at odd index i must have a replacement value at index i+1.</param>
        /// <returns>String with replacements performed on it.</returns>
        public static string ReplaceAll(this string source, params string[] replacements) {
            throw new NotImplementedException();
        }
    }
}

