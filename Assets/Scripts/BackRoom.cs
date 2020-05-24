using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BackRoom : Room, TimeObserver
{

    [SerializeField]
    private CommonRoom _commonRoom;

    private Queue<GameObject>_rosterSlots = new Queue<GameObject>();

    [SerializeField]
    private SimpleObjectPool _rosterSlotPool;

    [SerializeField]
    private Transform _rosterSlotsPanel;

    [SerializeField]
    private GameObject _rosterSlotPrefab;

    [SerializeField]
    private PC _selectedWorker;



    // PASSIVE INCOME

    [SerializeField]
    private ResourceManager _resourceManager;

    [SerializeField]
    private Transform _incomeWorkerPanel;

    private int _income = 0;

    private int _numIncomeWorkers;

    private int _incomeMultiplier = 1;

    private int _maxIncomeWorkers = 6;

    private List<PC> _incomeWorkers = new List<PC>();

    private Queue<GameObject> _incomeWorkerSlots = new Queue<GameObject>();

    [SerializeField]
    private SimpleObjectPool _incomeWorkerSlotPool;

    

    // RESEARCH

    [SerializeField]
    private GameObject _researchWorkerPanel;

    private int _numResearchWorkers;

    private int _maxResearchWorkers;

    private List<PC> _researchWorkers = new List<PC>();


    public void Start()
    {
        // Signup with timekeeper
        _timeKeeper.Signup(this);
    }

    public void OnEnable()
    {
        
    }

    public void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            OpenBRUI();

        }
    }

    public void OpenBRUI()
    {

        _uiManager.EnableBackRoomUI(true);
        UpdateRosterSlots();
    }

    public void TimeStepSignal(string unit)
    {
        // Add inocem to resource manager every day
        if (unit == "day")
        {
            _resourceManager.AddGold(_income);

        }
    }

    public void AddIncomeWorker()
    {
        if (_resourceManager.roster.Contains(_selectedWorker))
        {
            if ((_selectedWorker.GetClass() == "Thief") && (_numIncomeWorkers < _maxIncomeWorkers))
            {
                _incomeWorkers.Add(_selectedWorker);
                _numIncomeWorkers = _incomeWorkers.Count;
                _incomeMultiplier += _selectedWorker.GetLevel();
                UpdateIncome();
                _resourceManager.roster.Remove(_selectedWorker);
                _selectedWorker.Lock(true);
                UpdateRosterSlots();
                UpdateIncomeWorkerSlots();
                _uiManager.BR_UpdateIncomeSizeText("" + _numIncomeWorkers + "/" + _maxIncomeWorkers + "");
            }
        }
    }

    public bool AddResearchWorker(PC worker)
    {
        if ((worker.GetClass() == "Thief") && (_numResearchWorkers < _maxResearchWorkers))
        {
            _researchWorkers.Add(worker);
            _numResearchWorkers = _researchWorkers.Count;
            _resourceManager.roster.Remove(worker);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void RemoveIncomeWorker()
    {
        if (_incomeWorkers.Contains(_selectedWorker) && !_selectedWorker.GetLocked())
        {
            _incomeWorkers.Remove(_selectedWorker);
            _numIncomeWorkers = _incomeWorkers.Count;
            _resourceManager.roster.Add(_selectedWorker);
            _incomeMultiplier -= _selectedWorker.GetLevel();
            UpdateIncome();
            UpdateIncomeWorkerSlots();
            UpdateRosterSlots();
            _uiManager.BR_UpdateIncomeSizeText("" + _numIncomeWorkers + "/" + _maxIncomeWorkers + "");
        }
    }


    public void RemoveResearchWorker(PC worker)
    {
        _researchWorkers.Remove(worker);
        _numResearchWorkers = _researchWorkers.Count;
        _resourceManager.roster.Add(worker);
    }

    public void SelectWorker(PC worker)
    {
        _selectedWorker = worker;
    }


    // Updates income accord to formula.
    private void UpdateIncome()
    {
        _income = 10 *_numIncomeWorkers * _incomeMultiplier;
        _uiManager.BR_UpdateIncomeInfoText(_income);
    }

    // Spawns Slots in Roster Panel for all PCs in Roster. Called everytime a new PC is added or removed from Roster.
    private void UpdateIncomeWorkerSlots()
    {
        Debug.Log("UpdateIncomeWorkerSlots()");
        int rsc = _incomeWorkerSlots.Count;

        for (int i = 0; i < rsc; i++)
        {
            Debug.Log("UpdateRosterSlots: Dequeing");
            GameObject slot = _incomeWorkerSlots.Dequeue();
            //Debug.Log("Slot returned");
            _incomeWorkerSlotPool.ReturnObject(slot);
        }

        for (int i = 0; i < _incomeWorkers.Count; i++)
        {
            Debug.Log("UpdateIncomeWorkerSlots: Enquing");
            //Debug.Log("i 0 " + i + ". Getting new slot");
            PC chara = _incomeWorkers[i];
            GameObject newSlot;

            newSlot = _incomeWorkerSlotPool.GetGameObject();
            //= GameObject.Instantiate(_slotPrefab, _contentPanel);
            BR_RosterSlot rSlot = newSlot.GetComponent<BR_RosterSlot>();
            newSlot.transform.SetParent(_incomeWorkerPanel);
            rSlot.SetPC(chara);
            rSlot.PopulateSlot();
            _incomeWorkerSlots.Enqueue(newSlot);

            //GraftSlot graftSlot = newSlot.GetComponent<GraftSlot>();
            //graftSlot.Setup(slot, this);
        }
    }

    // Spawns Slots in Roster Panel for all PCs in Roster. Called everytime a new PC is added or removed from Roster.
    private void UpdateRosterSlots()
    {
        Debug.Log("UpdateRosterSlots()");
        int rsc = _rosterSlots.Count;

        for (int i = 0; i < rsc; i++)
        {
            Debug.Log("UpdateRosterSlots: Dequeing");
            GameObject slot = _rosterSlots.Dequeue();
            //Debug.Log("Slot returned");
            _rosterSlotPool.ReturnObject(slot);
        }

        for (int i = 0; i < _resourceManager.roster.Count; i++)
        {
            Debug.Log("UpdateRosterSlots: Enquing");
            //Debug.Log("i 0 " + i + ". Getting new slot");
            PC chara = _resourceManager.roster[i];
            if (chara.GetClass() == "Thief")
            {
                GameObject newSlot;

                newSlot = _rosterSlotPool.GetGameObject();
                //= GameObject.Instantiate(_slotPrefab, _contentPanel);
                BR_RosterSlot rSlot = newSlot.GetComponent<BR_RosterSlot>();
                newSlot.transform.SetParent(_rosterSlotsPanel);
                rSlot.SetPC(chara);
                rSlot.PopulateSlot();
                _rosterSlots.Enqueue(newSlot);
            }

            //GraftSlot graftSlot = newSlot.GetComponent<GraftSlot>();
            //graftSlot.Setup(slot, this);
        }
    }

}