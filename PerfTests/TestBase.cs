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
using System.Diagnostics;
using System.Reflection;

namespace OptimusTest {
    public abstract class TestBase {
        
        //--- Types ---
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
        protected static Measurement[] Measure(string testname, Func<int> callback) {
            var result = new Measurement[_iterations.Length];
            for(var j = 0; j < 100; ++j) {
                callback();
            }
            for(int i = 0; i < _iterations.Length; ++i) {
                var count = _iterations [i];
                var sw = new Stopwatch();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                var memory = GC.GetTotalMemory(true);
                sw.Start();
                for(var j = 0; j < count; ++j) {
                    callback();
                }
                sw.Stop();
                memory = GC.GetTotalMemory(false) - memory;
                result[i] = new Measurement(memory, sw.Elapsed);
            }
            Console.WriteLine();
            Console.WriteLine("        --- {0} ---", testname);
            for(var i = 0; i < result.Length; ++i) {
                Console.WriteLine("        #{0}: {1:#,##0} bytes, {2:#,##0.000} ms", i + 1, result[i].MemoryUsage, result[i].Duration.TotalMilliseconds);
            }
            return result;
        }

        //--- Methods ---
        public void Test() {
            var type = GetType();
            var methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            foreach(var method in methods) {
                Console.WriteLine("**Running {0}.{1} ...**", type.FullName.Replace("_", "\\_"), method.Name.Replace("_", "\\_"));
                try {
                    method.Invoke(this, new object[0]);
                } catch(Exception e) {
                    Console.WriteLine("ERROR: {0}", e.Message);
                    Console.WriteLine(e.StackTrace);
                }
                Console.WriteLine();
            }
        }
    }
}

