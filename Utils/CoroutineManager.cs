using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineManager
{
    private static Dictionary<string, GameObject> _referencedObjects = new Dictionary<string, GameObject>();
    /// <summary>
    /// Starts couroutines statically by creating separate Game object running Couroutine
    /// </summary>
    /// <param name="calledCouroutine"></param>
    /// <param name="objectID"></param>
    public static void StartCoroutine(IEnumerator calledCouroutine, string objectID)
    {
        if (_referencedObjects.ContainsKey(objectID))
        {
            CoroutineHolder component;
            if (_referencedObjects[objectID].TryGetComponent(out component))
                 component.StartCoroutine(calledCouroutine);
            else
                 _referencedObjects[objectID].AddComponent<CoroutineHolder>().StartCoroutine(calledCouroutine);
        }
        else
        {
            _referencedObjects.Add(objectID, new GameObject());
            
            CoroutineHolder component;
            if (_referencedObjects[objectID].TryGetComponent(out component))
                 component.StartCoroutine(calledCouroutine);
            else
                 _referencedObjects[objectID].AddComponent<CoroutineHolder>().StartCoroutine(calledCouroutine);
        }
    }

    public static void StopCoroutine(IEnumerator coroutineToStop, string objectID)
    {
        if(_referencedObjects.ContainsKey(objectID) == false)
        {
            Debug.Log("There is no such object containing this coroutine");
            return;
        }

        CoroutineHolder component;
        if (_referencedObjects[objectID].TryGetComponent(out component))
            component.StopCoroutine(coroutineToStop);
        else
            Debug.Log("Specified object doesn't have such couroutine");
    }
    
}