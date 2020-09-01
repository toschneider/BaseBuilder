using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MyScheduler<T, U> : IEnumerable where U : Enum where T : new()
{
    private readonly T[] _array;
    private readonly int _lower;
    public int length;

    public MyScheduler()
    {
        _lower = Convert.ToInt32(Enum.GetValues(typeof(U)).Cast<U>().Min());
        int upper = Convert.ToInt32(Enum.GetValues(typeof(U)).Cast<U>().Max());
        int size = 1 + upper - _lower;
        _array = new T[size];
		for (int i = 0; i < size; i++)
		{
            _array[i] = new T();
		}
        length = size;
    }

    public T this[U key]
    {
        get { return _array[Convert.ToInt32(key) - _lower]; }
        set { _array[Convert.ToInt32(key) - _lower] = value; }
    }

    public IEnumerator GetEnumerator()
    {
        return Enum.GetValues(typeof(U)).Cast<U>().Select(i => this[i]).GetEnumerator();
    }
}
