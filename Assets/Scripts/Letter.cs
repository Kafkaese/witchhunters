﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Letter : MonoBehaviour
{
    // Room Instance this notification letter was spawned from
    private Room _source;

    // Type of message for further navigation in source UI
    private string _type;

    // Message content
    private string _message;

    // Is it importan? True for time sensitive events like missions
    private bool _important;

    // Timestamp
    private string _timestamp;

    // Has the letter been opened?
    private bool _seen;

    // Refto icon
    [SerializeField]
    private Image _letterheadIcon;

    public bool Seen { get => _seen; set => _seen = value; }
    public string Type { get => _type; set => _type = value; }
    public string Message { get => _message; set => _message = value; }

    public void SetLetterHeadIcon(Sprite icon)
    {
        _letterheadIcon.sprite = icon;
    }
}