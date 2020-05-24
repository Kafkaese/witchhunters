using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchUpdater : MonoBehaviour
{

    // Observers
    private List<ResearchObserver> _researchObserverList = new List<ResearchObserver>();

    // ALL COMPLETED RESEARCH
    [SerializeField]
    public List<ResearchItem> _completedResearch = new List<ResearchItem>();

    public void Signup(ResearchObserver observer)
    {
        _researchObserverList.Add(observer);
    }

    // Add new ResearchItem to List of completed redearch and inform all Observers of new ResearchItem added.
    public void AddNewResearch(ResearchItem item)
    {
        _completedResearch.Add(item);

        foreach(ResearchObserver observer in _researchObserverList)
        {
            observer.ResearchUpdate(item);
        }
    }
}
