using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Library : Room, TimeObserver
{
    // UI Panel Selection Stuff
    private ResearchItem _research;

    [SerializeField]
    private ResourceManager _resourceManager;

    [SerializeField]
    private Text _description;

    [SerializeField]
    private Text _name;

    [SerializeField]
    private Button _startResearch_Button;

    [SerializeField]
    private Text _startResearch_Button_Text;


    // Research Choice Stuff
    

    

    private int _researchFactor = 1;


    // Research Progress Stuff
    private ResearchItem _researchUnderway;

    private int _hoursTilComplete;

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
        _startResearch_Button.interactable = false;
        _startResearch_Button_Text.text = "";
    }

    public void PopoulateDescription(ResearchItem researchItem)
    {
        _research = researchItem;
        _name.text = _research.GetName();
        _description.text = _research.GetDescription();

        // Enable button if builing is unlocked.
        if (_researchUnderway != null)
        {
            _startResearch_Button.interactable = false;
            if (_researchUnderway == _research)
            {
                _startResearch_Button_Text.text = "Researching...";
            }
            else
            {
                _startResearch_Button_Text.text = "Busy";
            }

        }
        else if (_research.IsCompleted())
        {
            _startResearch_Button.interactable = false;
            _startResearch_Button_Text.text = "Completed";
        }
        else if (_research.IsUnlocked())
        {
            _startResearch_Button.interactable = true;
            _startResearch_Button_Text.text = "Research \n (" + _research.Cost.ToString() + " gold)";
        }
        else
        {
            _startResearch_Button.interactable = false;
            _startResearch_Button_Text.text = "Locked";
        }
    }

    public void ResearchThis(int multiplier)
    {
        _researchFactor = multiplier;

        if (_research.IsUnlocked())
        {
            // INSERT RESEARCHER CHOICE WINDOW SOMEWHERE AROUND HERE
            if (_resourceManager.DeductGold(_research.Cost))
            {
                _researchUnderway = _research;
                _hoursTilComplete = _research.GetTimeReq();
                PopoulateDescription(_research);
            }
        }
    }

    public void TimeStepSignal(string unit)
    {
        if ((_researchUnderway != null) && (unit == "hour"))
        {
            _researchUnderway.DeductTimeReq(_researchFactor);
            _hoursTilComplete = _researchUnderway.GetTimeReq();

            _startResearch_Button_Text.text = "" + _hoursTilComplete + " hours remaining";

            if (_hoursTilComplete < 1)
            {
                // Send message to NotificatinManager
                _notificationManager.SpawnMessage(_researchUnderway.GetName() + " has been researched successfully.", "Lib_research", "Research Completed!", false);

                // Apply research effect
                // (Dangerzone)
                ResearchEffect _effect = (ResearchEffect)_researchUnderway;
                _effect.ApplyResearchEffect();

                // Remove ref to tell script that no more research is in progress
                _researchUnderway = null;

                _startResearch_Button_Text.text = "" + _hoursTilComplete + "Researched";

            }
        }
    }

    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            _uiManager.Lib_Research();

        }
        
    }
}
