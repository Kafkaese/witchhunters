using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConstructionHandler : MonoBehaviour
{
    private Building _building;

    [SerializeField]
    private ResourceManager _resourceManager;

    [SerializeField]
    private Text _description;

    [SerializeField]
    private Text _name;

    public void PopoulateDescription(Building researchItem)
    {
        _building = researchItem;
        _name.text = _building.GetName();
        _description.text = _building.GetDescription();


    }

    public void ConstructThis()
    {
        if(!_building.IsLocked())
        {
            if(_resourceManager.DeductGold(_building.cost))
            {
                _building.ApplyResearchEffect();
            }
        }
    }


}
