using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator
{
    private static Dictionary<object, object> locators = new Dictionary<object, object>();

    public static void Clear()
    {
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
        return (T)locators[typeof(T)];
    }
}