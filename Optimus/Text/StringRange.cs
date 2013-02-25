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
using System.Globalization;
using System.Collections.Generic;
using System.Collections;

namespace Optimus.Text {

    // TODO: docs missing
    public struct StringRange {

        //--- Types ---
        private sealed class Enumerable : IEnumerable<StringRange> {
            
            //--- Fields ---
            internal readonly string Source;
            internal readonly int StartIndex;
            internal readonly int Count;
            internal readonly Predicate<char> IsSeparator;
            internal readonly int SplitCount;
            internal readonly bool KeepEmptyEntries;
            
            //--- Constructors ---
            public Enumerable(string source, int startIndex, int count, Predicate<char> isSeparator, int splitCount, StringSplitOptions options) {
                Source = source;
                StartIndex = startIndex;
                Count = count;
                IsSeparator = isSeparator;
                SplitCount = splitCount;
                KeepEmptyEntries = ((options & StringSplitOptions.RemoveEmptyEntries) == 0);
            }
            
            //--- Methods ---
            public IEnumerator<StringRange> GetEnumerator() {
                return new Enumerator(this, SplitCount);
            }
            
            //--- IEnumerator Members ---
            IEnumerator IEnumerable.GetEnumerator() {
                return this.GetEnumerator();
            }
        }
        
        private sealed class Enumerator : IEnumerator<StringRange> {
            
            //--- Fields ---
            private readonly Enumerable _enumerable;
            private int _splitCount;
            private int _start;
            private int _current;
            private int _end;
            
            //--- Constructors ---
            public Enumerator(Enumerable enumerable, int splitCount) {
                _enumerable = enumerable;
                Reset();
            }
            
            //--- Methods ---
            public bool MoveNext() {
                if((_current == _end) || (_splitCount == 0)) {
                    return false;
                }
                if(_splitCount == 1) {
                    _splitCount = 0;
                    return true;
                }
                _start = _current + 1;
                for(_current = _start; _current < _end; ++_current) {
                    if(_enumerable.IsSeparator(_enumerable.Source[_current])) {
                        if((_current > _start) || _enumerable.KeepEmptyEntries) {
                            --_splitCount;
                            return true;
                        }
                        _start = _current + 1;
                    }
                }
                return (_end > _start) || _enumerable.KeepEmptyEntries;
            }
            
            public void Reset() {
                _splitCount = _enumerable.SplitCount;
                _current = _enumerable.StartIndex - 1;
                _end = _start + _enumerable.Count;
            }
            
            public void Dispose() { }
            
            public StringRange Current {
                get {
                    if(_splitCount == 0) {
                        return new StringRange(_enumerable.Source, _start, _end - _start, true);
                    }
                    return new StringRange(_enumerable.Source, _start, _current - _start, true);
                }
            }
            
            //--- IEnumerator Members ---
            object IEnumerator.Current {
                get {
                    throw new NotImplementedException();
                }
            }
        }

        //--- Class Fields ---

        // TODO: docs missing
        public static readonly StringRange Empty = new StringRange("");
        private static readonly StringRange Null = new StringRange();

        //--- Fields ---
        private string _source;
        private int _startIndex;
        private int _count;

        //--- Constructors ---

        // TODO: docs missing
        public StringRange(string source) : this(source, 0, (source != null) ? source.Length : 0) { }

        // TODO: docs missing
        public StringRange(string source, int startIndex, int count) {
            if(source == null) {
                throw new ArgumentNullException("text");
            }
            if((startIndex < 0) || (startIndex > source.Length)) {
                throw new ArgumentOutOfRangeException("startIndex");
            }
            if((count < 0) || ((source.Length - startIndex) < count)) {
                throw new ArgumentOutOfRangeException("count");
            }
            _source = source;
            _startIndex = startIndex;
            _count = count;
        }

        // TODO: docs missing
        internal StringRange(string source, int startIndex, int count, bool @private) {
            _source = source;
            _startIndex = startIndex;
            _count = count;
        }

        // TODO: text.EndsWith("foo");
        // TODO: text.Equals("foo");
        // TODO: text.GetEnumerator();
        // TODO: text.GetHashCode();
        // TODO: text.IndexOfAny(new[] { 'a' });
        // TODO: text.Insert(0, "foo");
        // TODO: text.LastIndexOf("foo");
        // TODO: text.LastIndexOfAny(new[] { 'a' });
        // TODO: text.PadLeft(5);
        // TODO: text.PadRight(5);
        // TODO: text.Remove(1, 2);
        // TODO: text.Replace("a", "b");
        // TODO: text.StartsWith("foo");
        // TODO: text.Split(new[] { ",", ";" }, StringSplitOptions.RemoveEmptyEntries)
        // TODO: text.Split(new[] { ",", ";" }, 2, StringSplitOptions.RemoveEmptyEntries)
        // TODO: text.Substring(2);
        // TODO: text.ToCharArray();
        // TODO: text.ToLower();
        // TODO: text.ToLowerInvariant();
        // TODO: text.ToString();
        // TODO: text.ToUpper();
        // TODO: text.ToUpperInvariant();
        // TODO: text.Trim();
        // TODO: text.TrimEnd();
        // TODO: text.TrimStart();

        //--- Properties ---

        // TODO: docs missing
        public char this[int index] {
            get {
                if((index < 0) || (index >= _count)) {
                    throw new ArgumentOutOfRangeException("index");
                }
                return _source[_startIndex + index];
            }
        }

        // TODO: docs missing
        public int Length { get { return _count; } }

        //--- Methods ---

        // TODO: docs missing
        public bool Contains(StringRange value) {
            return IndexOf(value) >= 0;
        }

        // TODO: docs missing
        public unsafe int IndexOf(StringRange value) {
            if(value._count == 0) {
                return 0;
            }
            if((Length == 0) || (Length < value._count)) {
                return -1;
            }
            fixed(char* leftStart = _source, rightStart = value._source) {
                var leftEnd = leftStart + _count;
                var rightEnd = rightStart + value._count;
                var stop = _startIndex + _count - value._count;
                for(int position = _startIndex; position <= stop; ++position) {
                    var left = leftStart + position;
                    var right = rightStart + value._startIndex;
                    for(; (right < rightEnd) && (left < leftEnd) && (*left == *right); ++left, ++right);
                    if(right == rightEnd) {
                        return position - _startIndex;
                    }
                }
            }
            return -1;
        }

        // TODO: docs missing
        public IEnumerable<StringRange> Split(params char[] separator) {
            return Split(separator, int.MaxValue, StringSplitOptions.None);
        }

        // TODO: docs missing
        public IEnumerable<StringRange> Split(char[] separator, StringSplitOptions options) {
            return Split(separator, int.MaxValue, options);
        }

        // TODO: docs missing
        public IEnumerable<StringRange> Split(char[] separator, int count) {
            return Split(separator, count, StringSplitOptions.None);
        }

        // TODO: docs missing
        public IEnumerable<StringRange> Split(char[] separator, int count, StringSplitOptions options) {
            if((separator == null) || (separator.Length == 0)) {
                return Split(char.IsWhiteSpace, count, options);
            }
            return Split(c => Array.IndexOf(separator, c) >= 0, count, options);
        }

        // TODO: docs missing
        public IEnumerable<StringRange> Split(Predicate<char> isSeparator, int count, StringSplitOptions options) {
            return Split_Candidate1(isSeparator, count, options);
        }

        private IEnumerable<StringRange> Split_Candidate2(Predicate<char> isSeparator, int count, StringSplitOptions options) {
            return new Enumerable(_source, _startIndex, _count, isSeparator, count, options);
        }

        private IEnumerable<StringRange> Split_Candidate1(Predicate<char> isSeparator, int count, StringSplitOptions options) {
            if(isSeparator == null) {
                throw new ArgumentNullException("isSeparator");
            }
            if(count < 0) {
                throw new ArgumentOutOfRangeException("count");
            }
            if(count == 0) {
                yield break;
            }
            if(count == 1) {
                yield return this;
                yield break;
            }
            var start = _startIndex;
            var end = _startIndex + _count;
            var keepEmptyEntries = ((options & StringSplitOptions.RemoveEmptyEntries) == 0);
            int length;
            for(var current = start; current < end; ++current) {
                if(isSeparator(_source[current])) {
                    length = current - start;
                    if((length > 0) || keepEmptyEntries) {
                        yield return new StringRange(_source, start, length, true);
                        --count;
                    }
                    start = current + 1;
                    if(count == 1) {
                        yield return new StringRange(_source, start, end - start, true);
                        yield break;
                    }
                }
            }
            length = end - start;
            if((length > 0) || keepEmptyEntries) {
                yield return new StringRange(_source, start, length, true);
            }
        }

        public bool Split(char[] separator, out StringRange found, out StringRange next) {
            if((separator == null) || (separator.Length == 0)) {
                return Split(char.IsWhiteSpace, StringSplitOptions.None, out found, out next);
            }
            return Split(c => Array.IndexOf(separator, c) >= 0, StringSplitOptions.None, out found, out next);
        }

        private bool Split(Predicate<char> isSeparator, StringSplitOptions options, out StringRange found, out StringRange next) {
            if(isSeparator == null) {
                throw new ArgumentNullException("isSeparator");
            }
            if(_source == null) {
                found = Null;
                next = Null;
                return false;
            }
            var start = _startIndex;
            var end = _startIndex + _count;
            var keepEmptyEntries = ((options & StringSplitOptions.RemoveEmptyEntries) == 0);
            int length;
            for(var current = start; current < end; ++current) {
                if(isSeparator(_source[current])) {
                    length = current - start;
                    if((length > 0) || keepEmptyEntries) {
                        found = new StringRange(_source, start, length, true);
                        if(start + length < end) {
                            next = new StringRange(_source, start + length + 1, end - (start + length + 1), true);
                        } else {
                            next = Null;
                        }
                        return true;
                    }
                    start = current + 1;
                }
            }
            length = end - start;
            if((length > 0) || keepEmptyEntries) {
                found = new StringRange(_source, start, length, true);
                next = Null;
                return true;
            }
            found = Null;
            next = Null;
            return false;
        }

        // TODO: docs missing
        public override string ToString() {
            return (_count == 0) ? "" : _source.Substring(_startIndex, _count);
        }
    }
}

