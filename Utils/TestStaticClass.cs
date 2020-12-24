using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestStaticClass : MonoBehaviour
{
    void Start()
    {
        StartTestingCoroutines();
    }

    private static void StartTestingCoroutines()
    {
        CoroutineManager.StartCoroutine(DoSomeStuff(), "testObject");
        
    }

    public static IEnumerator LastPiece()
    {
        Debug.Log("Yap, seem to work");
        yield return new WaitForSeconds(1);
        Debug.Log("Aha, like a charm");
        CoroutineManager.StopCoroutine("SuperLastObject");
    }
    public static IEnumerator DoSomeStuff()
    {
        Debug.Log("Started Coroutine");
        yield return new WaitForSeconds(1);
        Debug.Log("waited 1 second in");
        yield return new WaitForSeconds(1);
        CoroutineManager.StartCoroutine(LastPiece(),"SuperLastObject");
        Debug.Log("Stopped the coroutine");
        CoroutineManager.StopCoroutine("testObject");
    }
}
