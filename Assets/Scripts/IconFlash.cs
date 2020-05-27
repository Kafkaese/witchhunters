using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconFlash : MonoBehaviour
{
    [SerializeField]
    private Image _Icon;

    [SerializeField]
    private bool newMessage = false;

    public bool NewMessage { get => newMessage; set => newMessage = value; }

    [SerializeField]
    private float _alpha = 1f;
    
    [SerializeField]
    private bool _up = true;

    // Update is called once per frame
    void Update()
    {

        

        if (!newMessage)
        {
            Color col = _Icon.color;
            col.a = .5f;
            _Icon.color = col;
        }
        else
        {
            if (_alpha <= .5)
            {
                _up = true;
            }

            if (_up)
            {
                _alpha += .01f;
            }
            else
            {
                _alpha -= .01f;
            }

            if (_alpha >= 1)
            {
                _up = false;
            }


            Color col = _Icon.color;
            col.a = _alpha;
            _Icon.color = col;
        }
    }
}
