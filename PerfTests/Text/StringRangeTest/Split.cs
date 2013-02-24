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
using NUnit.Framework;
using System;
using System.Linq;

using Optimus.Text;
using System.Diagnostics;

namespace OptimusTest.Text.StringRangeTest {

    [TestFixture]
    public class Split {

        //--- Types ---
        public delegate void VoidDelegate();

        public struct Measurement {

            //--- Fields ---
            public long MemoryUsage;
            public TimeSpan Duration;

            //--- Constructors ---
            public Measurement(long memoryUsage, TimeSpan duration) {
                this.MemoryUsage = memoryUsage;
                this.Duration = duration;
            }
        }

        //--- Class Fields ---
        private static readonly int[] _iterations = new[] {
            10,
            100,
            1000,
            10000,
            100000,
            1000000
        };

        //--- Class Methods ---
        private static Measurement[] Measure(string testname, VoidDelegate f) {
            var result = new Measurement[_iterations.Length];
            for(var j = 0; j < 100; ++j) {
                f();
            }
            for(int i = 0; i < _iterations.Length; ++i) {
                var count = _iterations [i];
                var sw = new Stopwatch();
                var baseMemory = GC.GetTotalMemory(true);
                sw.Start();
                for(var j = 0; j < count; ++j) {
                    f();
                }
                sw.Stop();
                var memory = GC.GetTotalMemory(false);
                result[i] = new Measurement(memory - baseMemory, sw.Elapsed);
            }
            Console.WriteLine("--- {0} ---", testname);
            for(var i = 0; i < result.Length; ++i) {
                Console.WriteLine("#{0}: {1:#,##0} bytes, {2:#,##0.000} ms", i, result[i].MemoryUsage, result[i].Duration.TotalMilliseconds);
            }
            return result;
        }

        //--- Methods ---

        [Test]
        public void Split_short_strings() {
            const string text = "123,456,789,123,456,789,123,456,789,123,456,789,123,456,789,123,456,789,123,456,789,123,456,789";
            Measure("String.Split(short-strings) with loop", () => {
                foreach(var item in text.Split(',')) {
                    // nothing
                }
            });
            Measure("StringRange.Split(short-strings) with loop", () => {
                foreach(var item in new StringRange(text).Split(',')) {
                    // nothing
                }
            });
        }
        
        [Test]
        public void Split_long_strings() {
            const string text = "123456789012345678901234567890,123456789012345678901234567890,123456789012345678901234567890,123456789012345678901234567890,123456789012345678901234567890,123456789012345678901234567890,123456789012345678901234567890,123456789012345678901234567890,123456789012345678901234567890,123456789012345678901234567890,123456789012345678901234567890,123456789012345678901234567890,123456789012345678901234567890,123456789012345678901234567890,123456789012345678901234567890";
            Measure("String.Split(long-strings) with loop", () => {
                foreach(var item in text.Split(',')) {
                    // nothing
                }
            });
            Measure("StringRange.Split(long-strings) with loop", () => {
                foreach(var item in new StringRange(text).Split(',')) {
                    // nothing
                }
            });
        }
    }
}