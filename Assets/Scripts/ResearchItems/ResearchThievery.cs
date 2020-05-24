using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchThievery : ResearchItem, ResearchEffect
{
    [SerializeField]
    private int _multiplier = 0;

    public void ApplyResearchEffect()
    {
        if (_unlocked)
        {
            BackRoom br = GameObject.Find("Backroom1").GetComponent<BackRoom>();
            if (br != null)
            {
                br.IncreaseIncomeMultiplier(_multiplier);
            }
            else
            {
                Debug.Log("Backroom not found!");
            }
            researchUpdater.AddNewResearch(this);
        }
    }

}
