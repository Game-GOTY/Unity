using System.Collections;
using UnityEngine;
using System;

public class TimerUtility : MonoBehaviour
{
    private static TimerUtility _instance;
    
    // Dictionary to keep track of active coroutines
    private System.Collections.Generic.Dictionary<int, Coroutine> activeTimers = 
        new System.Collections.Generic.Dictionary<int, Coroutine>();
    private int nextId = 0;

    // Singleton pattern to ensure one instance exists
    private static TimerUtility Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = new GameObject("TimerUtility");
                _instance = obj.AddComponent<TimerUtility>();
                DontDestroyOnLoad(obj);
            }
            return _instance;
        }
    }

    /// <summary>
    /// Starts a timer that will execute the given action after the specified delay
    /// </summary>
    /// <param name="seconds">Delay in seconds</param>
    /// <param name="action">Action to execute after delay</param>
    /// <returns>Timer ID that can be used to cancel the timer</returns>
    public static int SetTimeout(float seconds, Action action)
    {
        return Instance.StartTimer(seconds, action);
    }

    /// <summary>
    /// Cancels a timer before it executes
    /// </summary>
    /// <param name="timerId">ID of the timer to cancel</param>
    public static void ClearTimeout(int timerId)
    {
        Instance.StopTimer(timerId);
    }

    private int StartTimer(float seconds, Action action)
    {
        int timerId = nextId++;
        Coroutine coroutine = StartCoroutine(TimerCoroutine(seconds, action, timerId));
        activeTimers.Add(timerId, coroutine);
        return timerId;
    }

    private void StopTimer(int timerId)
    {
        if (activeTimers.TryGetValue(timerId, out Coroutine coroutine))
        {
            StopCoroutine(coroutine);
            activeTimers.Remove(timerId);
        }
    }

    private IEnumerator TimerCoroutine(float seconds, Action action, int timerId)
    {
        yield return new WaitForSeconds(seconds);
        
        // Only execute if timer wasn't cancelled
        if (activeTimers.ContainsKey(timerId))
        {
            action?.Invoke();
            activeTimers.Remove(timerId);
        }
    }
}