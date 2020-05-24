using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    [SerializeField]
    private UIManager _uiManager; 

    private int _gold = 0;

    [SerializeField]
    public List<PC> roster = new List<PC>();



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

    public void Update()
    {
        if(Input.GetKeyDown("c"))
        {
            Debug.Log("c key");
            AddGold(10000);
        }
    }
}
