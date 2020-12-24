using UnityEngine;

/// <summary>
/// Class for containing coroutines
/// </summary>
public class CoroutineHolder : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}