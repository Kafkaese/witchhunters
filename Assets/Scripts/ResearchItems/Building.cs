using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : ResearchItem, ResearchEffect
{
    public int cost;

    public abstract void ApplyResearchEffect();
}
