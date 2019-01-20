using System.Collections.Generic;

public static class ListExtension
{    
    /// <summary>
    /// Array change to list
    /// </summary>
    /// <typeparam name="T">the Type of the array and list</typeparam>
    /// <param name="list"></param>
    /// <param name="array"></param>
    public static void CopyFrom<T>(this List<T> list,T[] array)
    {
        foreach (var item in array)
        {
            list.Add(item);
        }
    }
}
