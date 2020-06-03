using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainingGround : Room, TimeObserver
{
    // Officers

    private PC _lieutnant;
    private PC _sergeant;
    private PC _corporal;

    private PC _masterAtArms;
    private PC _drillSergeant;
    private PC _instructor;


    // XP boni

    private bool _idles = false;
    private int _passiveXP_gain = 0;
    

    private int _missionXP_bonus = 0;


    // UI Refs

    [SerializeField]
    private Text _descriptionTitle;

    [SerializeField]
    private Text _descriptionText;

    [SerializeField]
    private Image _lieutnantPortrait;
    [SerializeField]
    private Image _masterAtArmsPortrait;
    [SerializeField]
    private Text _leitnantName;
    [SerializeField]
    private Text _masterAtArmsName;


    [SerializeField]
    private GameObject _officerChooser;



    // Script Refs

    [SerializeField]
    private ResourceManager _resourceManager;

    [SerializeField]
    private ResearcherChoser _researchers;

    [SerializeField]
    private BackRoom _backroom;


    // Office Choosing
    private int _current_id;

    private PC _selectedPC;

    private Queue<GameObject> _rosterSlots = new Queue<GameObject>();

    [SerializeField]
    private SimpleObjectPool _rosterSlotPool;

    [SerializeField]
    private Transform _rosterSlotPanel;

    [SerializeField]
    private Image _selectedOfficer;

    [SerializeField]
    private Button _officerCandidate;



    // Private Methods
    
    private void RefreshSlotList()
    {
        int rsc = _rosterSlots.Count;

        for (int i = 0; i < rsc; i++)
        {
            //Debug.Log("UpdateRosterSlots: Dequeing");
            GameObject slot = _rosterSlots.Dequeue();
            //Debug.Log("Slot returned");
            _rosterSlotPool.ReturnObject(slot);
        }

        for (int i = 0; i < _resourceManager.roster.Count; i++)
        {
            //ebug.Log("UpdateRosterSlots: Enquing");
            //Debug.Log("i 0 " + i + ". Getting new slot");
            PC chara = _resourceManager.roster[i];
            if (chara.IsBaseClass(0))
            {
                GameObject newSlot;

                newSlot = _rosterSlotPool.GetGameObject();
                //= GameObject.Instantiate(_slotPrefab, _contentPanel);
                RosterSlot rSlot = newSlot.GetComponent<RosterSlot>();
                newSlot.transform.SetParent(_rosterSlotPanel);
                rSlot.SetPC(chara);
                rSlot.PopulateSlot();
                _rosterSlots.Enqueue(newSlot);
            }

        }
    }

    private void CalculateXPbonus()
    {

    }

    private void CalculatePassiceXPgain()
    {

    }

    private void Start()
    {
        _timeKeeper.Signup(this);
    }



    // Public Methods
    
    // IDs:
    // 0 = Lieutnant
    // 1 = MasterAtArms
    
    // Make selected PC officer based on current id.
    public void AddOfficer()
    {
        if (_current_id == 0)
        {
            if (_selectedPC != null)
            {
                _lieutnant = _selectedPC;
                _leitnantName.text = _selectedPC.GetName();
                _lieutnantPortrait.sprite = _selectedPC.GetAvatar();
            }
            else 
            {
                _lieutnant = null;
                _leitnantName.text = "Vacant";
                _lieutnantPortrait.sprite = null;
            }
            CalculateXPbonus();

        }
        else if (_current_id == 1)
        {
            if (_selectedPC != null)
            {
                _masterAtArms = _selectedPC;
                _masterAtArmsName.text = _selectedPC.GetName();
                _masterAtArmsPortrait.sprite = _selectedPC.GetAvatar();
            }
            else
            {
                _masterAtArms = null;
                _masterAtArmsName.text = "Vacant";
                _masterAtArmsPortrait.sprite = null;
            }
            CalculatePassiceXPgain();
        }

        _selectedPC = null;
        _officerCandidate.interactable = false;
        _selectedOfficer.sprite = null;
        _officerChooser.SetActive(false);
    }

    // Close officer chooser UI panel; unselect PC if selected and return to roster list.
    public void CancelOfficerPcking()
    {
        if (_selectedPC != null)
        {
            if ((_selectedPC == _lieutnant) || _selectedPC == _masterAtArms)
            {
                _selectedPC = null;
            }
            else
            {
                _resourceManager.roster.Add(_selectedPC);
                _selectedPC = null;
            }
        }
        _officerChooser.SetActive(false);
    }

    public void ChoosePCasOfficer(PC pc)
    {

        _selectedPC = pc;
        _resourceManager.roster.Remove(pc);
        _selectedOfficer.sprite = pc.GetAvatar();
        _officerCandidate.interactable = true;
        RefreshSlotList();
    }

    public void UnchoosePCasOfficer()
    {

        _resourceManager.roster.Add(_selectedPC);
        _selectedPC = null;
        _officerCandidate.interactable = false;
        _selectedOfficer.sprite = null;
        RefreshSlotList();
    }



    // Opnes UI Panel for selecting pc as officer
    public void PickOfficer()
    {
        _officerChooser.SetActive(true);
        if (_current_id == 0)
        {
            if(_lieutnant != null)
            {
                _selectedPC = _lieutnant;
                _officerCandidate.interactable = true;
                _selectedOfficer.sprite = _lieutnant.GetAvatar();
            }
        }
        else if (_current_id == 1)
        {
            if (_masterAtArms != null)
            {
                _selectedPC = _masterAtArms;
                _officerCandidate.interactable = true;
                _selectedOfficer.sprite = _masterAtArms.GetAvatar();
            }
        }

        RefreshSlotList();
    }

    // Populate Information Panel in UI with information regarding the selected Officer Slot.
    public void SelectOfficer(int id)
    {
        _current_id = id;

        if(id == 0)
        {
            _descriptionTitle.text = "Lieutnant";
            _descriptionText.text = "The lieutnant gives valuable advice and delegates during mission. The entire squad gets a bonus to their experience gain during missions based on the lieutnants level.";
        }
        if (id == 1)
        {
            _descriptionTitle.text = "Master-at-arms";
            _descriptionText.text = "The master-at-arms makes sure that the witchhunters stay sharp inbetween missions. Every non-idle memeber gains experience passively. If NAME has been researched all idles witchhunters gain this bonus also.";
        }
    }


    public void TimeStepSignal(string unit)
    {
        if (_passiveXP_gain > 0)
        {
            if (unit == "week")
            {
                _researchers.ApplyPassiveXP(_passiveXP_gain);
                _backroom.ApplyPassiveXP(_passiveXP_gain);
                // officers ?

            if (_idles)
                {
                    _resourceManager.ApplyPassiveXP(_passiveXP_gain);
                }

            }
        }
    }
}
