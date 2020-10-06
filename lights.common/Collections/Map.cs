using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace lights.common.Collections
{
    public class Map<T1, T2> : IEnumerable<KeyValuePair<T1, T2>>
    {
        private Dictionary<T1, T2> _forward = new Dictionary<T1, T2>();
        private Dictionary<T2, T1> _backward = new Dictionary<T2, T1>();

        public Map()
        {
        }

        public Map(IDictionary<T1, T2> dict)
        {
            _forward = dict.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            _backward = dict.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);
        }

        public Map(IDictionary<T2, T1> dict)
        {
            _backward = dict.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            _forward = dict.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);
        }

        public void Add(T1 value1, T2 value2)
        {
            _forward.Add(value1, value2);
            _backward.Add(value2, value1);
        }

        public T2 Forward(T1 key)
        {
            return _forward[key];
        }

        public T1 Backward(T2 key)
        {
            return _backward[key];
        }

        public IEnumerator<KeyValuePair<T1, T2>> GetEnumerator()
        {
            return _forward.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
