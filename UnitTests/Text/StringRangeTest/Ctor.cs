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
    public class Ctor {

        [Test]
        public void Empty_string() {

            // setup
            var text = "";

            // test
            var value = new StringRange(text);

            // validation
            Assert.AreEqual(0, value.Length);
            Assert.AreEqual("", value.ToString());
        }
        
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Null_string() {
            
            // setup
            string text = null;
            
            // test
            var value = new StringRange(text);
            
            // validation
            Assert.Fail("exception expected");
            Assert.AreEqual(0, value.Length);
        }

        [Test]
        public void Zero_length_string() {
            
            // setup
            var text = "foo";

            // test
            var value = new StringRange(text, 0, 0);
            
            // validation
            Assert.AreEqual(0, value.Length);
            Assert.AreEqual("", value.ToString());
        }
        
        [Test]
        public void Full_string() {
            
            // setup
            var text = "foo";
            
            // test
            var value = new StringRange(text);
            
            // validation
            Assert.AreEqual(text.Length, value.Length);
            Assert.AreEqual(text, value.ToString());
        }
        
        [Test]
        public void Substring() {
            
            // setup
            var text = "barfoobar";
            
            // test
            var value = new StringRange(text, 3, 3);
            
            // validation
            Assert.AreEqual(3, value.Length);
            Assert.AreEqual("foo", value.ToString());
        }
        
        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void String_with_negative_startIndex() {
            
            // setup
            var text = "foo";
            
            // test
            var value = new StringRange(text, -1, 0);
            
            // validation
            Assert.Fail("exception expected");
            Assert.AreEqual(0, value.Length);
        }

        [Test]
        public void Zero_length_string_with_startIndex_equal_to_string_length() {
            
            // setup
            var text = "foo";
            
            // test
            var value = new StringRange(text, text.Length, 0);
            
            // validation
            Assert.AreEqual(0, value.Length);
            Assert.AreEqual("", value.ToString());
        }
        
        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void String_with_startIndex_beyond_string_length() {
            
            // setup
            var text = "foo";
            
            // test
            var value = new StringRange(text, text.Length + 1, 0);
            
            // validation
            Assert.Fail("exception expected");
            Assert.AreEqual(0, value.Length);
        }
        
        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void String_with_count_greater_than_string_length() {
            
            // setup
            var text = "foo";
            
            // test
            var value = new StringRange(text, 0, text.Length + 1);
            
            // validation
            Assert.Fail("exception expected");
            Assert.AreEqual(0, value.Length);
        }
        
        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void String_with_combined_startIndex_plut_count_greater_than_string_length() {
            
            // setup
            var text = "foo";
            
            // test
            var value = new StringRange(text, 1, text.Length);
            
            // validation
            Assert.Fail("exception expected");
            Assert.AreEqual(0, value.Length);
        }
    }
}

