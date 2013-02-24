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

// http://freebsd.active-venture.com/FreeBSD-srctree/newsrc/libkern/
// http://faculty.edcc.edu/paul.bladek/c_string_functions.htm
// http://www.codeproject.com/Articles/1099/Writing-Unsafe-code-using-C

namespace Optimus {
    public static class CharEx {

        //--- Class Methods ---
        public static char[] strcpy(char[] s1, char[] s2) {
            for(var i = 0u; (i < s1.Length) && (i < s2.Length) && (s2[i] != '\0'); ++i) {
                s1[i] = s2[i];
            }
            return s1;
        }

        public static char[] strncpy(char[] s1, char[] s2, uint n) {
            for(var i = 0u; (i < n) && (i < s1.Length) && (i < s2.Length) && (s2[i] != '\0'); ++i) {
                s1[i] = s2[i];
            }
            return s1;
        }

        public static char[] strcat(char[] s1, char[] s2) {
            for(uint i = strlen(s1), j = 0u; (i < s1.Length) && (j < s2.Length) && (s2[j] != '\0'); ++i, ++j) {
                s1[i] = s2[i];
            }
            return s1;
        }

        public static char[] strncat(char[] s1, char[] s2, uint n) {
            for(uint i = strlen(s1), j = 0u; (j < n) && (i < s1.Length) && (j < s2.Length) && (s2[j] != '\0'); ++i, ++j) {
                s1[i] = s2[i];
            }
            return s1;
        }

        public static int strcmp(char[] s1, char[] s2) {
            for(var i = 0u; (i < s1.Length) && (i < s2.Length); ++i) {
                var diff = s1[i] - s2[i];
                if(diff != 0) {
                    return diff;
                }
                if(s1[i] == '\0') {
                    return -1;
                }
                if(s2[i] == '\0') {
                    return 1;
                }
            }
            return 0;
        }

        public static int strncmp(char[] s1, char[] s2, uint n) {
            for(var i = 0u; (i < n) && (i < s1.Length) && (i < s2.Length); ++i) {
                var diff = s1[i] - s2[i];
                if(diff != 0) {
                    return diff;
                }
                if(s1[i] == '\0') {
                    return -1;
                }
                if(s2[i] == '\0') {
                    return 1;
                }
            }
            return 0;
        }

        public static uint strlen(char[] s) {
            var i = 0u;
            for(; (i < s.Length) && (s[i] != '\0'); ++i) { }
            return i;
        }

        //public static char* strchr(char[] s, char c) {}
        //public static char* strrchr(char[] s, char c) {}
        //public static uint strspn(char[] s1, char[] s2) {}
        //public static uint strcspn(char[] s1, char[] s2) {}
        //public static char* strprk(char[] s1, char[] s2) {}
        //public static char* strstr(char[] s1, char[] s2) {}
        //public static char* strtok(char[] s1, char[] s2) {}
    }
}

