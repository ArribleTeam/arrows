using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPSView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_fpsText;
    [Range(0.05f, 1.0f)] [SerializeField] private float m_updateTime = 0.25f;

    private Coroutine m_fpsCoroutine;

    #region Implementation

    private void Start()
    {
        if (m_fpsCoroutine != null) StopCoroutine(m_fpsCoroutine);
        m_fpsCoroutine = StartCoroutine(_CounterRoutine());
    }

    private void OnDestroy()
    {
        if (m_fpsCoroutine != null) StopCoroutine(m_fpsCoroutine);
    }

    #endregion

    #region Private

    private IEnumerator _CounterRoutine()
    {
        while (true)
        {
            int min = Mathf.RoundToInt(FPSCounter.MinFPS);
            int average = Mathf.RoundToInt(FPSCounter.AverageFPS);
            m_fpsText.text = $"FPS: {min} / {average}";
            yield return new WaitForSeconds(m_updateTime);
        }
    }

    #endregion
}
