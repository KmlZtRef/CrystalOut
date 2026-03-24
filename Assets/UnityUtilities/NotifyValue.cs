using System;
using UnityEngine;

namespace UnityUtilities
{
    public class NotifyValue<T>
    {
        private T _value;
        public event Action<T> OnValueChanged;
        public T Value
        {
            get => _value;
            set
            {
                if (_value == null || _value.Equals(value) == false)
                {
                    _value = value;
                    OnValueChanged?.Invoke(_value);
                }
            }
        }
        public NotifyValue(T initialValue = default)
        {
            _value = initialValue;
        }
    }
}
