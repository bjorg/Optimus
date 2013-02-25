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
using System.Linq;

using Optimus.Text;
using System.Diagnostics;

namespace OptimusTest.Text {
    public class StringRangeTest : TestBase {

        //--- Methods ---
        public void Split_short_strings() {
            const string text = "123,456,789,123,456,789,123,456,789,123,456,789,123,456,789,123,456,789,123,456,789,123,456,789";
            Measure("String.Split(short-strings)", () => {
                var result  = 0;
                foreach(var item in text.Split(',')) {
                    result += item.Length;
                }
                return result;
            });
            Measure("StringRange.Split(short-strings)", () => {
                var result  = 0;
                foreach(var item in new StringRange(text).Split(',')) {
                    result += item.Length;
                }
                return result;
            });
        }
        
        public void Split_long_strings() {
            const string text = "123456789012345678901234567890,123456789012345678901234567890,123456789012345678901234567890,123456789012345678901234567890,123456789012345678901234567890,123456789012345678901234567890,123456789012345678901234567890,123456789012345678901234567890,123456789012345678901234567890,123456789012345678901234567890,123456789012345678901234567890,123456789012345678901234567890,123456789012345678901234567890,123456789012345678901234567890,123456789012345678901234567890";
            Measure("String.Split(long-strings)", () => {
                var result  = 0;
                foreach(var item in text.Split(',')) {
                    result += item.Length;
                }
                return result;
            });
            Measure("StringRange.Split(long-strings)", () => {
                var result  = 0;
                foreach(var item in new StringRange(text).Split(',')) {
                    result += item.Length;
                }
                return result;
            });
        }
    }
}