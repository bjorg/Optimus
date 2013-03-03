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

namespace OptimusTest {
    public class Program {

        //--- Class Methods ---
        public static void Main() {
            var gcDisabled = Environment.GetEnvironmentVariable("GC_DONT_GC");
            if(gcDisabled != "1") {
                Console.WriteLine("WARNING: GC is NOT disabled! Memory usage results will not be accurate.");
                Console.WriteLine();
            }
            Console.WriteLine("Enumerating performances tests...");
            var types = typeof(Program).Assembly.GetTypes().Where(t => !t.IsAbstract && typeof(TestBase).IsAssignableFrom(t)).ToArray();
            Console.WriteLine("{0} performance tests found", types.Length);
            Console.WriteLine();
            foreach(var type in types) {
                try {
                    ((TestBase)Activator.CreateInstance(type)).Test();
                } catch(Exception e) {
                    Console.WriteLine("ERROR {0}: {1}", type.FullName, e.Message);
                }
            }
        }
    }
}

