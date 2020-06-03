using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
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

    // Meal inventory
    private Dictionary<Meal, int> _mealInventory = new Dictionary<Meal, int>();

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
            _uiManager.FeedbackInsufficientFunds();
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

    // Apply passive exp gain to all idles
    public void ApplyPassiveXP(int xp)
    {
        foreach (PC pc in roster)
        {
            pc.AddXP(xp);
        }
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

    public void AddMeal(Meal meal)
    {
        if(_mealInventory.ContainsKey(meal))
        {
            int num = _mealInventory[meal];
            _mealInventory[meal] = num + 1;
        }
        else
        {
            _mealInventory.Add(meal, 1);
        }
        
    }

    public int GetMealCount(Meal meal)
    {
        if(_mealInventory.ContainsKey(meal))
        {
            return _mealInventory[meal];
        }
        else
        {
            return 0;
        }
    }

}
