﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ResearchItem : MonoBehaviour
{
    [SerializeField]
    private string _name;

    [SerializeField]
    protected int _timeReq;

    protected int _initTimeReq;

    [SerializeField]
    private string _description;
    
    [SerializeField]
    private List<ResearchItem> _prerequisits= new List<ResearchItem>();

    [SerializeField]
    protected ResourceManager resourceManager;

    [SerializeField]
    protected bool _completed = false;

    [SerializeField]
    private int cost;

    public int Cost { get => cost; set => cost = value; }



    // Observer Pattern NOT USED ANYMORE
    /*
    public void ResearchUpdate(ResearchItem item)
    {
        if (_prerequisits.Contains(item))
        {
            _prerequisits.Remove(item);

            if(_prerequisits.Count == 0)
            {
                _unlocked = true;
            }
        }
    }
    */
    private void Start()
    {
        _initTimeReq = _timeReq;
    }

    public string GetDescription()
    {
        string preqFormat = "";
        foreach (ResearchItem item in _prerequisits)
        {
            
            if(resourceManager.IsResearchCompleted(item))
            {
                preqFormat = preqFormat + "\n - <color=green>" + item.GetName() + "</color>";
            }
            else 
            {
                preqFormat = preqFormat + "\n - <color=brown>" + item.GetName() + "</color>";
            }
            
        }
        if (_prerequisits.Count == 0)
        {
            preqFormat = "None";
        }

        return _description + "\n" + "<b>Prerequisits:</b>" + preqFormat + " \n <b>Time:</b> \n" + _timeReq + " hours"; ;
    }

    public string GetName()
    {
        return _name;
    }

    public bool IsUnlocked()
    {
        bool unlocked = true;
        foreach(ResearchItem preq in _prerequisits)
        {
            if(resourceManager.IsResearchCompleted(preq) == false)
            {
                unlocked = false;
            }
        }
        return unlocked;
    }

    public bool IsCompleted()
    {
        return _completed;
    }

    public int GetTimeReq()
    {
        return _timeReq;
    }

    public void DeductTimeReq(int hours)
    {
        _timeReq -= hours;
    }

    public void ResetConstructionTime()
    {
        _timeReq = _initTimeReq;
    }

}
