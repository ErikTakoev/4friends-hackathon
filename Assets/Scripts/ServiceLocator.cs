using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ServiceLocator
{
    private static Dictionary<object, object> locators = new Dictionary<object, object>();

    public static void Clear()
    {
        foreach (var o in locators.Select(s=>s.Value))
        {
            if (o is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
        
        locators = new Dictionary<object, object>();
    }

    public static void Add<T>(T data)
    {
        var type = typeof(T);
        
        if (locators.ContainsKey(type))
        {
            Debug.LogError($"{type} is Exist");
            
            return;
        }
        
        locators.Add(type, data);    
    }

    public static T Get<T>()
    {
        var type = typeof(T);

        if (locators.TryGetValue(type, out var locator))
        {
            return (T) locator;
        }

        return default;
    }
}