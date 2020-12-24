using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestStaticClass : MonoBehaviour
{
    void Start()
    {
        
    }

    private static void StartTestingCoroutines()
    {
        CoroutineManager.StartCoroutine(DoSomeStuff, "testObject");
    }

    public IEnumerator DoSomeStuff()
    {
        Debug.Log("Started Coroutine from " + this.name);
        yield return new WaitForSeconds(1);
        Debug.Log("waited 1 second in " + this.name);
    }
}
