using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FPSCounter
{
    public static int s_smoothingWindowSize = 100;

    private static Queue<float> s_measures = new Queue<float>(s_smoothingWindowSize);
    private static int s_lastProcessedFrame = -1;
    private static int s_lastCalculatedFrame = -1;

    private static float s_minFrameTime;
    private static float s_maxFrameTime;
    private static float s_averageFrameTime;

    public static float MinFrameTime
    {
        get
        {
            _Process();
            return s_minFrameTime;
        }
    }

    public static float MaxFrameTime
    {
        get
        {
            _Process();
            return s_maxFrameTime;
        }
    }

    public static float AverageFrameTime
    {
        get
        {
            _Process();
            return s_averageFrameTime;
        }
    }

    private static void _Process()
    {
        if (s_lastCalculatedFrame >= Time.frameCount)
        {
            return;
        }

        s_lastCalculatedFrame = Time.frameCount;

        s_minFrameTime = float.MaxValue;
        s_maxFrameTime = float.MinValue;

        var sum = 0f;
        foreach (var frameTime in s_measures)
        {
            sum += frameTime;
            if (frameTime < s_minFrameTime) s_minFrameTime = frameTime;
            if (frameTime > s_maxFrameTime) s_maxFrameTime = frameTime;
        }
        s_averageFrameTime = sum / s_measures.Count;

    }

    public static void UpdateManually()
    {
        if (Time.frameCount <= s_lastProcessedFrame)
        {
            return;
        }

        s_measures.Enqueue(1.0f / Time.deltaTime);
        s_lastProcessedFrame = Time.frameCount;

        while (s_measures.Count > s_smoothingWindowSize)
        {
            s_measures.Dequeue();
        }
    }
}
