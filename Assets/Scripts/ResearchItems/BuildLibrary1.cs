using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildLibrary1 : Building
{

    [SerializeField]
    private GameObject _library1;

    [SerializeField]
    private int _level;

    // Activates the Backroom1 building, and removes the BAckroom0 placeholder object.


    public override void ApplyResearchEffect()
    {
        if (_level == 1)
        {
            _library1.SetActive(true);
            GameObject.Find("Library0").SetActive(false);
            GameObject.Find("TBB_Lib_Research_Button").GetComponent<Button>().interactable = true;
        }
        else
        {
            _library1.GetComponent<Library>().Upgrade(_level);
        }
        resourceManager.AddResearch(this);
        _completed = true;
        ColorBlock cb = gameObject.GetComponent<Button>().colors;
        cb.normalColor = Color.white;
        gameObject.GetComponent<Button>().colors = cb;
    }
}
