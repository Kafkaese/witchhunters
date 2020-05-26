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

    [SerializeField]
    private Button _construct_Button;

    [SerializeField]
    private Text _constrcut_Button_Text;

    public void PopoulateDescription(Building researchItem)
    {
        _building = researchItem;
        _name.text = _building.GetName();
        _description.text = _building.GetDescription();

        // Enable button if builing is unlocked.
        if(_building.IsCompleted())
        {
            _construct_Button.interactable = false;
            _constrcut_Button_Text.text = "Built";
        }
        else if(_building.IsUnlocked())
        {
            _construct_Button.interactable = true;
            _constrcut_Button_Text.text = "Construct \n (" + _building.cost.ToString() + " gold)";
        }
        else
        {
            _construct_Button.interactable = false;
            _constrcut_Button_Text.text = "Locked";
        }
    }

    public void ConstructThis()
    {
        if(_building.IsUnlocked())
        {
            if(_resourceManager.DeductGold(_building.cost))
            {
                _building.ApplyResearchEffect();
            }
        }
    }


}
