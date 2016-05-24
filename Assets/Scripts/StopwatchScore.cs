using System;
using UnityEngine;
using UnityEngine.UI;

/**
    I'm not that proud of it.
**/
public class StopwatchScore : MonoBehaviour {

    public Text _StopwatchDisplay;
    public string _TimeFormatString = "{0:00}:{1:00}";

    private bool _Stopped;
    private DateTime _StartTime;
    private DateTime _EndTime;

    public TimeSpan Elapsed { get { return _EndTime - _StartTime; } }

    public void Stop()
    {
        _Stopped = true;
    }

    public void Awake()
    {
        _StartTime = DateTime.Now;
    }

    public void Update()
    {
        if(!_Stopped)
        {
            _EndTime = DateTime.Now;
        }
        var elapsed = Elapsed;
        _StopwatchDisplay.text = String.Format(_TimeFormatString, (int)elapsed.TotalSeconds, elapsed.Milliseconds / 10);
    }
}
