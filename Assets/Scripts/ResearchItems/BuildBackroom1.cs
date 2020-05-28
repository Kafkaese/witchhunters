using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class BuildBackroom1 : Building
{

    [SerializeField]
    private GameObject _backroom1;

    [SerializeField]
    private int _level;


    // Activates the Backroom1 building, and removes the BAckroom0 placeholder object.
    public override void ApplyResearchEffect()
    {

        // Room appearance and button enabling
        if (_level == 1)
        {
            _backroom1.SetActive(true);
            //GameObject.Find("Backroom0").SetActive(false);
            GameObject.Find("TBB_BR_Income_Button").GetComponent<Button>().interactable = true;
            GameObject.Find("TBB_BR_Spy_Button").GetComponent<Button>().interactable = true;

        }
        else
        {
            _backroom1.GetComponent<BackRoom>().Upgrade(_level);
        }

        // Construction UI and ResearchManager
        resourceManager.AddResearch(this);
        _completed = true;
        ColorBlock cb = gameObject.GetComponent<Button>().colors;
        cb.normalColor = Color.white;
        gameObject.GetComponent<Button>().colors = cb;



    }
}
