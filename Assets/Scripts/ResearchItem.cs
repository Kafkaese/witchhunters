﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ResearchItem : MonoBehaviour, ResearchObserver
{
    [SerializeField]
    private string _name;

    [SerializeField]
    private int _timeReq;

    [SerializeField]
    private string _description;
    
    [SerializeField]
    private List<ResearchItem> _prerequisits= new List<ResearchItem>();

    [SerializeField]
    protected ResearchUpdater researchUpdater;

    [SerializeField]
    protected bool _unlocked = false;

    public void Start()
    {
        researchUpdater.Signup(this);
    }

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

    public string GetDescription()
    {
        string preqFormat = "";
        foreach (ResearchItem item in _prerequisits)
        {
            string check;
            preqFormat = preqFormat + "/n -" + item.GetName();
        }

        return "<b>" + _name + "</b> /n" + _description + "/n" + "<b>Prerequisits:</b>" + preqFormat;
    }

    public string GetName()
    {
        return _name;
    }


}