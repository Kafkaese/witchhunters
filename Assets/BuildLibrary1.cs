using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildLibrary1 : Building
{

    [SerializeField]
    private GameObject _library1;

    // Activates the Backroom1 building, and removes the BAckroom0 placeholder object.


    public override void ApplyResearchEffect()
    {
        _library1.SetActive(true);
        GameObject.Find("Library0").SetActive(false);
        GameObject.Find("TBB_Lib_Button").GetComponent<Button>().interactable = true;

        resourceManager.AddResearch(this);
        _completed = true;
        ColorBlock cb = gameObject.GetComponent<Button>().colors;
        cb.normalColor = Color.white;
        gameObject.GetComponent<Button>().colors = cb;
    }
}
