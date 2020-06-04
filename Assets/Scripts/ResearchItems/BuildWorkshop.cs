using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class BuildWorkshop : Building
{

    [SerializeField]
    private GameObject _workshop1;

    [SerializeField]
    private int _level;


    // Activates the Backroom1 building, and removes the BAckroom0 placeholder object.
    public override void ApplyResearchEffect()
    {

        // Room appearance and button enabling
        if (_level == 1)
        {
            _workshop1.SetActive(true);
            //GameObject.Find("Backroom0").SetActive(false);
            GameObject.Find("TBB_WS_Production_Button").GetComponent<Button>().interactable = true;
            
        }
        else
        {
            _workshop1.GetComponent<Workshop>().Upgrade(_level);
        }

        // Construction UI and ResearchManager
        resourceManager.AddResearch(this);
        _completed = true;
        ColorBlock cb = gameObject.GetComponent<Button>().colors;
        cb.normalColor = Color.white;
        gameObject.GetComponent<Button>().colors = cb;



    }
}
