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

namespace OptimusTest.Text.XStringTest {
    public class ReplaceAll : TestBase {

        //--- Constants ---
        private const string SHORT_STRING = "The quick brown fox jumped over the lazy dog.";
        private const string LONG_STRING = @"If you're a sports fan of any type, then it's likely that you've become familiar with a cryptically-named, 
poorly designed site called ATDHE that lists video streams of nearly any and every sporting event on TV. 
It was totally awesome and totally legally questionable. And now, it's totally taken down by Homeland Security.

Of course, the story of ATDHE's takedown and subsequent re-emergence is another example of the resilience of 
information online and the amusing game of Whac-A-Mole that is illegal information on the Internet.

TechDirt's Mike Masnick wrote about a spate of domain name seizures over Thanksgiving Weekend last November 
much like today's. According to CrunchGear's Nicholas Deleon, ATDHE isn't alone, with rojadirecta.org suffering 
a similar fate.

As we saw with WikiLeaks, however, when people want something on the Internet to stay on the Internet badly 
enough, it's only a matter of moments before a mirror pops up in another location. One tweet illustrated the 
situation, saying ""RIP atdhe.net while celebrating the birth of atdhe.me.""

The first tweets mentioning the disappearance of ATDHE started at around noon pacific. By two in the afternoon, 
tweets began mentioning ATDHE.me, the replacement site. In much the same way, the ATDHE Twitter account also 
quickly tweeted out the IP address to use until the site went back online under a different name.

Another site taken down today first tweeted at 2:12 that it was down. At 2:17, it tweeted again to say that 
it could be found at an alternate domain name. As I said: Whac-A-Mole.

What makes the whole thing even more interesting is that ATDHE.net didn't host any illegal content. It was 
a list of links to homemade video streams on sites like Justin.tv. As we saw with content takedowns on Twitter 
last April, however, direct hosting of content may not be necessary to remove something - just linking may be 
enough. With ATDHE, streams consisted of someone literally pointing a camera on a tripod at their television 
and streaming that. Where does the illegality come into play here and is seizing the domain name the answer?

There's one thing for sure, it's surely the ineffective answer on the part of the authorities, but I bet 
sports fans won't be complaining.";

        //--- Methods ---
        public void Baseline_short_string_replace_with_5_substitutions() {
            Measure("Baseline_string_replace", () => {
                var tmp = SHORT_STRING;
                tmp = tmp.Replace("The", "A");
                tmp = tmp.Replace("over", "under");
                tmp = tmp.Replace("fox", "cat");
                tmp = tmp.Replace("jumped", "burrowed");
                tmp = tmp.Replace("lazy", "snoring");
                return tmp.Length;
            });
        }
        
        public void Baseline_long_string_replace_with_5_substitutions() {
            Measure("Baseline_string_replace", () => {
                var tmp = LONG_STRING;
                tmp = tmp.Replace("The", "A");
                tmp = tmp.Replace("over", "under");
                tmp = tmp.Replace("fox", "cat");
                tmp = tmp.Replace("jumped", "burrowed");
                tmp = tmp.Replace("lazy", "snoring");
                return tmp.Length;
            });
        }
        
        public void Variant1_short_string_ReplaceAll_with_5_substitutions() {
            var replacements = new[] { 
                "The", "A", 
                "over", "under", 
                "fox", "cat", 
                "jumped", "burrowed", 
                "lazy", "snoring" 
            };
            Measure("Variant1_ReplaceAll", () => {
                var tmp = Variant1_ReplaceAll(SHORT_STRING, replacements);
                return tmp.Length;
            });
        }
        
        public void Variant1_long_string_ReplaceAll_with_5_substitutions() {
            var replacements = new[] { 
                "The", "A", 
                "over", "under", 
                "fox", "cat", 
                "jumped", "burrowed", 
                "lazy", "snoring" 
            };
            Measure("Variant1_ReplaceAll", () => {
                var tmp = Variant1_ReplaceAll(LONG_STRING, replacements);
                return tmp.Length;
            });
        }

        private static string Variant1_ReplaceAll(string source, params string[] replacements) {
            if((replacements.Length & 1) != 0) {
                throw new ArgumentException("length of string replacements must be even", "replacements");
            }
            if(string.IsNullOrEmpty(source) || (replacements.Length == 0)) {
                return source;
            }
            
            // loop over each character in source string
            StringBuilder result = null;
            var currentIndex = 0;
            var lastIndex = 0;
            for(; currentIndex < source.Length; ++currentIndex) {
                
                // loop over all replacement strings
                for(int j = 0; j < replacements.Length; j += 2) {
                    
                    // check if we found a matching replacement at the current position
                    if(string.CompareOrdinal(source, currentIndex, replacements[j], 0, replacements[j].Length) == 0) {
                        result = result ?? new StringBuilder(source.Length * 2);
                        
                        // append any text we've skipped over so far
                        if(lastIndex < currentIndex) {
                            result.Append(source, lastIndex, currentIndex - lastIndex);
                        }
                        
                        // append replacement
                        result.Append(replacements[j + 1]);
                        currentIndex += replacements[j].Length - 1;
                        lastIndex = currentIndex + 1;
                        goto next;
                    }
                }
            next:
                continue;
            }
            
            // append any text we've skipped over so far
            if(lastIndex < currentIndex) {
                
                // check if nothing has been replaced; in that case, return the original string
                if(lastIndex == 0) {
                    return source;
                }
                result = result ?? new StringBuilder(source.Length * 2);
                result.Append(source, lastIndex, currentIndex - lastIndex);
            }
            return result.ToString();
        }
    }
}