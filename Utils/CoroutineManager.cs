using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineManager
{
    private static GameObject couroutineHolderObject;
    private static Dictionary<string,IEnumerator> listOfStartedCoroutines = new Dictionary<string, IEnumerator>();

    static CoroutineManager()
    {
        InitializeCoroutineHolder();
    }

    /// <summary>
    /// Starts couroutines statically by creating separate Game object running Couroutine
    /// </summary>
    /// <param name="calledCouroutine"></param>
    /// <param name="objectID"></param>
    public static Coroutine StartCoroutine(IEnumerator calledCouroutine, string key)
    {
        if (couroutineHolderObject == null)
        {
            Debug.LogError("The coroutine holder hasn't initialized");
            return null;
        }
        listOfStartedCoroutines.Add(key, calledCouroutine);
        CoroutineHolder component;
        if (couroutineHolderObject.TryGetComponent(out component))
            return component.StartCoroutine(calledCouroutine);
        else
        {
            Debug.LogError("Somehow the coroutine holder doesn't have a CoroutineHolder component");
        }

        return null;
    }

    private static void InitializeCoroutineHolder()
    {
        couroutineHolderObject = new GameObject();
        couroutineHolderObject.AddComponent<CoroutineHolder>();
    }

    public static void StopCoroutine(string key)
    {
        if (couroutineHolderObject == null)
        {
            Debug.LogError("The coroutine holder hasn't initialized");
            return;
        }
        
        if (listOfStartedCoroutines.ContainsKey(key) == false)
        {
            Debug.Log("Coroutine with this key doesn't exist");
            return;
        }

        CoroutineHolder component;
        if (couroutineHolderObject.TryGetComponent(out component))
            component.StartCoroutine(listOfStartedCoroutines[key]);
        else
            Debug.Log("Somehow the coroutine holder doesn't have a CoroutineHolder component");
    }
}