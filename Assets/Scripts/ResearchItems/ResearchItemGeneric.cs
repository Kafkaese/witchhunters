using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResearchItemGeneric : ResearchItem, ResearchEffect
{
    public void ApplyResearchEffect()
    {
        resourceManager.AddResearch(this);
        _completed = true;
        ColorBlock cb = gameObject.GetComponent<Button>().colors;
        cb.normalColor = Color.white;
        gameObject.GetComponent<Button>().colors = cb;
    }


}
