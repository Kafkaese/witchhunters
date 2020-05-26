using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    [SerializeField]
    private UIManager _uiManager; 

    // GOLD AMOUNT
    [SerializeField]
    private int _gold = 0;

    // LIST OF ALL AVAILABLE CHARACTERS IN ROSTER
    [SerializeField]
    public List<PC> roster = new List<PC>();

    // ALL COMPLETED RESEARCH
    [SerializeField]
    private List<ResearchItem> _completedResearch = new List<ResearchItem>();



    public void AddGold(int amount)
    {
        _gold += amount;
        _uiManager.UpdateGold(_gold);
    }

    // Retunrs false if not enough gold available
    public bool DeductGold(int amount)
    {
        if (_gold >= amount)
        {
            _gold -= amount;
            _uiManager.UpdateGold(_gold);
            return true;
        }
        else
        {
            return false;
        }
        
    }

    public int GetGoldAmount()
    {
        return _gold;
    }

    public void AddResearch(ResearchItem item)
    {
        _completedResearch.Add(item);
    }

    public bool IsResearchCompleted(ResearchItem item)
    {
        if(_completedResearch.Contains(item))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
