using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CoroutineManager
{
    public static ManagerComponent Instance;

    static CoroutineManager()
    {
        ManagerComponent.Create();
    }
}

public class ManagerComponent : MonoBehaviour
{
    // What to do with this? Use hash table or dictionary with reflection? Do we even need to have ability to stop them?
    private static List<Coroutine> s_coroutines;

    #region Implementation

    internal static void Create()
    {
        if (CoroutineManager.Instance != null) return;

        GameObject go = new GameObject("[CoroutineManager]");
        DontDestroyOnLoad(go);
        CoroutineManager.Instance = go.AddComponent<ManagerComponent>();

        s_coroutines = new List<Coroutine>();
    }

    #endregion

    #region Public

    public void DoRoutine(IEnumerator routine)
    {
        var coroutine = StartCoroutine(routine);
        s_coroutines.Add(coroutine);
    }

    #endregion


    #region SeemsToBeUselessNow

    public void Do(Action before, Action after, YieldInstruction instruction)
    {
        var coroutine = StartCoroutine(_Coroutine(before, after, instruction));
        s_coroutines.Add(coroutine);
    }

    public void DoAfter(Action action, YieldInstruction instruction)
    {
        Do(null, action, instruction);
    }

    public void DoBefore(Action action, YieldInstruction instruction)
    {
        Do(action, null, instruction);
    }

    private IEnumerator _Coroutine(Action before = null, Action after = null, YieldInstruction instruction = null)
    {
        before?.Invoke();
        yield return instruction;
        after?.Invoke();
    }

    #endregion
}
