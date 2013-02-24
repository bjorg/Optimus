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
    public class IndexOf {

        //--- Methods ---

        [Test]
        public void Index_for_non_empty_string_in_empty_string_is_negative() {

            // setup
            var left = new StringRange("");
            var right = new StringRange("foo");

            // test
            var index = left.IndexOf(right);

            // validation
            Assert.AreEqual(-1, index);
        }
        
        [Test]
        public void Index_for_empty_string_in_empty_string_is_zero() {
            
            // setup
            var left = new StringRange("");
            var right = new StringRange("");
            
            // test
            var index = left.IndexOf(right);
            
            // validation
            Assert.AreEqual(0, index);
        }
        
        [Test]
        public void Index_for_empty_string_in_non_empty_string_is_zero() {
            
            // setup
            var left = new StringRange("foo");
            var right = new StringRange("");
            
            // test
            var index = left.IndexOf(right);
            
            // validation
            Assert.AreEqual(0, index);
        }

        [Test]
        public void Index_for_identical_strings_is_zero() {
            
            // setup
            var left = new StringRange("foo");
            var right = new StringRange("foo");
            
            // test
            var index = left.IndexOf(right);
            
            // validation
            Assert.AreEqual(0, index);
        }
        
        [Test]
        public void Index_for_differnt_strings_is_negative() {
            
            // setup
            var left = new StringRange("bar");
            var right = new StringRange("foo");
            
            // test
            var index = left.IndexOf(right);
            
            // validation
            Assert.AreEqual(-1, index);
        }
        
        [Test]
        public void Index_for_contained_string_is_positive() {
            
            // setup
            var left = new StringRange("barfoobar");
            var right = new StringRange("foo");
            
            // test
            var index = left.IndexOf(right);
            
            // validation
            Assert.AreEqual(3, index);
        }
        
        [Test]
        public void Index_for_contained_string_with_multiple_occurrences_is_positive() {
            
            // setup
            var left = new StringRange("barfoobarfoobar");
            var right = new StringRange("foo");
            
            // test
            var index = left.IndexOf(right);
            
            // validation
            Assert.AreEqual(3, index);
        }
    }
}

