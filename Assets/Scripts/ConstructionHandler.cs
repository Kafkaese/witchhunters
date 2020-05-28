using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConstructionHandler : MonoBehaviour, TimeObserver
{

    // UI Panel Selection Stuff
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

    // Building Progress Stuff
    [SerializeField]
    private Building _underConstruction;

    private int _hoursTilComplete;

    [SerializeField]
    private TimeKeeper _timeKeeper;

    [SerializeField]
    private NotificationManager _notificationManager;

    private void Start()
    {
        _timeKeeper.Signup(this);
    }

    private void OnEnable()
    {
        _name.text = "";
        _description.text = "";
        _construct_Button.interactable = false;
        _constrcut_Button_Text.text = "";
    }

    public void PopoulateDescription(Building researchItem)
    {
        _building = researchItem;
        _name.text = _building.GetName();
        _description.text = _building.GetDescription();

        // Enable button if builing is unlocked.
        if (_underConstruction != null)
        {
            _construct_Button.interactable = false;
            if(_underConstruction == _building)
            {
                _constrcut_Button_Text.text = "Building...";
            }
            else
            {
                _constrcut_Button_Text.text = "Busy";
            }
            
        }
        else if(_building.IsCompleted())
        {
            _construct_Button.interactable = false;
            _constrcut_Button_Text.text = "Built";
        }
        else if(_building.IsUnlocked())
        {
            _construct_Button.interactable = true;
            _constrcut_Button_Text.text = "Construct \n (" + _building.Cost.ToString() + " gold)";
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
            if(_resourceManager.DeductGold(_building.Cost))
            {
                _underConstruction = _building;
                _hoursTilComplete = _building.GetTimeReq();
                PopoulateDescription(_building);
            }
        }
    }

    public void TimeStepSignal(string unit)
    {
        if ((_underConstruction != null) && (unit == "hour"))
        {
            _underConstruction.DeductTimeReq(1);
            _hoursTilComplete = _underConstruction.GetTimeReq();

            _constrcut_Button_Text.text = "" + _hoursTilComplete + " hours remaining";

            if(_hoursTilComplete < 1)
            {
                // Send message to NotificatinManager
                _notificationManager.SpawnMessage(_underConstruction.GetName() + " has been constructed successfully.", "Of_Construct", "Construction Completed!", false);

                // Build/Upgrade Room
                _underConstruction.ApplyResearchEffect();
                
                // Remove ref to tell script that no more construction is in progress
                _underConstruction = null;

                _constrcut_Button_Text.text = "" + _hoursTilComplete + "Built";

            }
        }
    }
}
