using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeKeeper : MonoBehaviour
{
    // UI Manager
    [SerializeField]
    private UIManager _uiManager;

    // Months
    private string[] _monthNames;

    //Time keeping
    private bool _paused;

    private int _hours = 0;
    private int _days = 1;
    private int _months = 0;
    private int _years = 120;

    private int speedCap = 12;

    private int multiplier = 1;

    [SerializeField]
    private float _timeCHeckpoint;


    // Listeners
    [SerializeField]
    private List<TimeObserver> _listenerRooms = new List<TimeObserver>();

    // Start is called before the first frame update
    void Start()
    {
        _monthNames = new string[9];
        _monthNames[0] = "Karns Folly";
        _monthNames[1] = "Rites of Dawn";
        _monthNames[2] = "New Blossom";
        _monthNames[3] = "Sight of Eden";
        _monthNames[4] = "The Scroching";
        _monthNames[5] = "The Awakening";
        _monthNames[6] = "Crimson Tide";
        _monthNames[7] = "Somber Dusk";
        _monthNames[8] = "Ocryns Wrath";

        _timeCHeckpoint = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_paused)
        {
            if ((_timeCHeckpoint + 1/(float)multiplier) < Time.time)
            {
                _timeCHeckpoint = Time.time;
                _hours++;
                // send hour passed info to ALL THAT CARE
                TimeStepSignal("hour");

                if (_hours > 24)
                {
                    _hours = _hours % 24;
                    _days++;

                    //send day pased info to ALL THAT CARE
                    TimeStepSignal("day");
                    //TimeStepSignal("month");
                }
                if (_days > 28)
                {
                    _days = _days % 27;
                    _months++;

                    //send month pased info to ALL THAT CARE
                    TimeStepSignal("month");
                }
                if (_months > 9)
                {
                    _months = _months % 8;
                    _years++;

                    //send year pased info to ALL THAT CARE
                    TimeStepSignal("year");
                }
                SendDateTimetoUI();
            }
        }
        
    }

    // Update to Listeners
    private void TimeStepSignal(string unit)
    {
        foreach (TimeObserver observer in _listenerRooms)
        {
            observer.TimeStepSignal(unit);
        }
    }

    private void SendDateTimetoUI()
    {
        string dateTimeText = _hours + " Hours , " + _days + " Day of " + _monthNames[_months] + " " + _years + "E3";
        _uiManager.UpdateDateTime(dateTimeText);
    }

    public void changeMultiplier(bool direction)
    {
        if (!direction && (multiplier > 1))
        {
            multiplier -= 3;
        }
        else if (direction && multiplier < speedCap)
        {
            multiplier += 3;
        }
    }

    public void togglePauseTime()
    {
        _paused = !_paused;
    }

    public bool Signup(TimeObserver listener)
    {
        _listenerRooms.Add(listener);
        return true;
    }

    public string GetTimeStamp()
    {
        return _hours + " Hours , " + _days + " Day of " + _monthNames[_months] + " " + _years + "E3";
    }

}
