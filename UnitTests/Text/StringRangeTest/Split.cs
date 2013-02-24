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

namespace OptimusTest.Text.StringRangeTest {

    [TestFixture]
    public class Split {

        //--- Methods ---

        [Test]
        public void Empty_string_has_only_one_split() {

            // setup
            var text = new StringRange("");

            // test
            var splits = text.Split(new[] { ',' }, int.MaxValue, StringSplitOptions.None).ToArray();

            // validation
            Assert.AreEqual(1, splits.Length);
            Assert.AreEqual("", splits[0].ToString());
        }
        
        [Test]
        public void Empty_string_has_no_splits_when_skipping_empty_entries() {
            
            // setup
            var text = new StringRange("");
            
            // test
            var splits = text.Split(new[] { ',' }, int.MaxValue, StringSplitOptions.RemoveEmptyEntries).ToArray();
            
            // validation
            Assert.AreEqual(0, splits.Length);
        }

        [Test]
        public void Consecutive_separator_chars_are_returned_as_empty_strings() {
            
            // setup
            var text = new StringRange(",,,");
            
            // test
            var splits = text.Split(new[] { ',' }, int.MaxValue, StringSplitOptions.None).ToArray();
            
            // validation
            Assert.AreEqual(4, splits.Length);
            Assert.AreEqual("", splits[0].ToString());
            Assert.AreEqual("", splits[1].ToString());
            Assert.AreEqual("", splits[2].ToString());
            Assert.AreEqual("", splits[3].ToString());
        }
        
        [Test]
        public void Consecutive_separator_chars_are_skipped_when_empty_entries_are_removed() {
            
            // setup
            var text = new StringRange(",,,");
            
            // test
            var splits = text.Split(new[] { ',' }, int.MaxValue, StringSplitOptions.RemoveEmptyEntries).ToArray();
            
            // validation
            Assert.AreEqual(0, splits.Length);
        }

        [Test]
        public void Split_without_limit_and_without_options() {
            
            // setup
            var text = new StringRange("a,b,c");
            
            // test
            var splits = text.Split(new[] { ',' }, int.MaxValue, StringSplitOptions.None).ToArray();
            
            // validation
            Assert.AreEqual(3, splits.Length);
            Assert.AreEqual("a", splits[0].ToString());
            Assert.AreEqual("b", splits[1].ToString());
            Assert.AreEqual("c", splits[2].ToString());
        }
        
        [Test]
        public void Split_returns_values_and_empty_entries() {
            
            // setup
            var text = new StringRange(",a,,b,c,");
            
            // test
            var splits = text.Split(new[] { ',' }, int.MaxValue, StringSplitOptions.None).ToArray();
            
            // validation
            Assert.AreEqual(6, splits.Length);
            Assert.AreEqual("", splits[0].ToString());
            Assert.AreEqual("a", splits[1].ToString());
            Assert.AreEqual("", splits[2].ToString());
            Assert.AreEqual("b", splits[3].ToString());
            Assert.AreEqual("c", splits[4].ToString());
            Assert.AreEqual("", splits[5].ToString());
        }
        
        [Test]
        public void Split_returns_values_and_skips_empty_entries() {
            
            // setup
            var text = new StringRange(",a,,b,c,");

            // test
            var splits = text.Split(new[] { ',' }, int.MaxValue, StringSplitOptions.RemoveEmptyEntries).ToArray();
            
            // validation
            Assert.AreEqual(3, splits.Length);
            Assert.AreEqual("a", splits[0].ToString());
            Assert.AreEqual("b", splits[1].ToString());
            Assert.AreEqual("c", splits[2].ToString());
        }
    }
}