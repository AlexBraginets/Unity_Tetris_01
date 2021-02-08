using System.Collections;
using System.Collections.Generic;
using System;
public static class LoopUtil
{
    public static void LoopAction(Action<int> action, int count)
    {
        for(int i = 0; i < count; i++)
        {
            action.Invoke(i);
        }
    }
    public static void LoopAction(Action<int, int> action, int xCount, int yCount)
    {
        for (int x = 0; x < xCount; x++)
        {
            for (int y = 0; y < yCount; y++)
            {
                action.Invoke(x, y);
            }
        }
    }
    public static T[] LoopFunc<T>(Func<int,T> func, int count)
    {
        T[] array = new T[count];
        for (int i = 0; i < count; i++)
        {
            array[i] = func.Invoke(i);
        }
        return array;
    }
}
