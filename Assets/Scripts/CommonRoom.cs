using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[System.Serializable]
public class Slot
{

    // ints
    private int _lvl;

    // Strings
    private string _name;
    private string _class;


    // Constructor
    public Slot(string _name, string _class, int _lvl)
    {
        this._name = _name;
        this._class = _class;
        this._lvl = _lvl;
    }
}

public class CommonRoom : Room, TimeObserver
{

    [SerializeField]
    private ResourceManager _resourceManager;

    //
    // PC Creation
    //

    /* Mapping:
    * 0 - Thug (Axe)
    * 1 - Thug (Shield)
    * 2 - Thief (Bow)
    * 3 - Thief (Dagger)
    * 4 - Charlatan (Staff)
    * 5 - Charlatan (Totem)
    */

    // Sprites
    [SerializeField]
    private Sprite[] _charSprites;
    [SerializeField]
    private Sprite[] _charPortraits;

    //Names
    private string[] givenNames;
    private string[] lastNames;
    private string[] femaleNames;

    // Class Names
    private string[] _classNames;

    // Prefabs
    [SerializeField]
    private GameObject _PCprefab;

    //RNG
    private System.Random rnd = new System.Random();



    //
    // MANSION MODE
    //

    // Prefabs
    [SerializeField]
    private GameObject _slotPrefab;

    // Scroll Panel for Roster
    [SerializeField]
    private Transform _contentPanel;

    // RECRUITING

    private bool _recruit;

    // max roszer size
    private int max_roster_size = 12;

    private bool _canNoble = false;

    // List with all recruitable PCs
    [SerializeField]
    private Queue<PC> _pool;

    // List with all Player Characters
    //[SerializeField]
    //private List<PC> _roster;

    private Queue<GameObject> _rosterSlots = new Queue<GameObject>();
    [SerializeField]
    private SimpleObjectPool _rosterSlotPool;

    [SerializeField]
    private GameObject[] _recruitSlots;


    // MANAGING
    [SerializeField]
    private CommonRoomManageDisplay _manageDisplay;

    // Start is called before the first frame update
    void Start()
    {
        //_charSprites = new Texture2D[5];
        //_charPortraits = new Texture2D[5];
        _classNames = new string[6];
        _classNames[0] = "Thug";
        _classNames[1] = "Thug";
        _classNames[2] = "Thief";
        _classNames[3] = "Thief";
        _classNames[4] = "Charlatan";
        _classNames[5] = "Charlatan";

        _pool = new Queue<PC>();
        //_roster = new List<PC>();
        //_rosterSlots = new Queue<GameObject>();

        //Spawn first recruit
        MakeNames();
        PCIncubator(false);
        UpdateRecruitSlots();

        

        // Signup as Listener at TimeKeeper
        _timeKeeper.Signup(this);



        // Create starting roster
        AddPCtoRoster(PCIncubator(false));
    }

    void OnEnable()
    {
        UpdateRosterSlots();
        _uiManager.UpdateRosterSizeText("" + _resourceManager.roster.Count + "/" + max_roster_size);
    }

    private string GenerateName(bool female)
    {
        //MakeNames();
        //Debug.Log("generating Name");
        if (female)
        {
            string _name = femaleNames[rnd.Next(0, 9)] + " " + lastNames[rnd.Next(0, 9)];
            return _name;
        }
        else
        {
            string _name = givenNames[rnd.Next(0, 9)] + " " + lastNames[rnd.Next(0, 9)];
            return _name;
        }
            
    }

    public void MakeNames()
    {
        givenNames = new string[10];
        lastNames = new string[10];
        femaleNames = new string[10];

        givenNames[0] = "Hans";
        givenNames[1] = "Peter";
        givenNames[2] = "Hank";
        givenNames[3] = "Owyne";
        givenNames[4] = "Geoffrey";
        givenNames[5] = "Brandon";
        givenNames[6] = "James";
        givenNames[7] = "Nathaniel";
        givenNames[8] = "Baldwin";
        givenNames[9] = "Adam";

        lastNames[1] = "Burton";
        lastNames[2] = "Bishop";
        lastNames[3] = "Hunter";
        lastNames[4] = "Brown";
        lastNames[5] = "Castle";
        lastNames[6] = "Wilde";
        lastNames[7] = "Green";
        lastNames[8] = "Baker";
        lastNames[9] = "Draper";
        lastNames[0] = "Fogg";

        femaleNames[0] = "Hilda";
        femaleNames[1] = "Jane";
        femaleNames[2] = "Sybil";
        femaleNames[3] = "Alicia";
        femaleNames[4] = "Elizabeth";
        femaleNames[5] = "Judith";
        femaleNames[6] = "Hanna";
        femaleNames[7] = "Helen";
        femaleNames[8] = "Josephine";
        femaleNames[9] = "Beatrice";

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(_pool.Count);
    }

    // Spawns Slots in Roster Panel for all PCs in Roster. Called everytime a new PC is added or removed from Roster.
    private void UpdateRosterSlots()
    {

        int rsc = _rosterSlots.Count;
        
        for (int i = 0; i < rsc; i++)
        {
            GameObject slot = _rosterSlots.Dequeue();
            //Debug.Log("Slot returned");
            _rosterSlotPool.ReturnObject(slot);
        }

        for (int i = 0; i < _resourceManager.roster.Count; i++)
        {
            //Debug.Log("i 0 " + i + ". Getting new slot");
            PC chara = _resourceManager.roster[i];
            GameObject newSlot;

            newSlot = _rosterSlotPool.GetGameObject();
            //= GameObject.Instantiate(_slotPrefab, _contentPanel);
            RosterSlot rSlot = newSlot.GetComponent<RosterSlot>();
            newSlot.transform.SetParent(_contentPanel);
            rSlot.SetPC(chara);
            rSlot.PopulateSlot();
            _rosterSlots.Enqueue(newSlot);

            //GraftSlot graftSlot = newSlot.GetComponent<GraftSlot>();
            //graftSlot.Setup(slot, this);
        }
    }

    

    private PC PCIncubator(bool noble)
    {
        //Debug.Log("PCInubator called with " + noble);
        // ROLL STATS
       
                int _strength = Random.Range(40, 70);
                int _dexterity = Random.Range(40, 70);
                int _willpower = Random.Range(40, 70);
                int _constitution = Random.Range(40, 70);
        
        if (noble)
        {
            _strength = Random.Range(50, 80);
            _dexterity = Random.Range(50, 80);
            _willpower = Random.Range(50, 80);
            _constitution = Random.Range(50, 80);
        }

        // DETERMINE CLASS
        int _class = Random.Range(0, 5);
        //int _class = 4;


        //
        // REMOVE
        //
        _class = 0;
        //
        // REMOVE
        //

        // Construct PC instance
        PC tmp = Instantiate(_PCprefab).GetComponent<PC>();
        string name_tmp = GenerateName(false);
        if (_class == 2 || _class == 3)
        {
            name_tmp = GenerateName(true);
        }
        tmp.Construct(name_tmp, _classNames[_class], _strength, _dexterity, _constitution, _willpower, _charPortraits[_class], _charSprites[_class]);
        _pool.Enqueue(tmp);



        return tmp;
    }

    // Update recruit slot display
    private void UpdateRecruitSlots()
    {
        foreach (GameObject slot in _recruitSlots)
        {
            slot.gameObject.SetActive(false);
        }
        for (int i = 0; i < _pool.Count; i++)
        {
            PC temp = _pool.Dequeue();
            GameObject tPanel = _recruitSlots[i];

            // FIll info to correct panel 
            tPanel.gameObject.SetActive(true);
            tPanel.transform.GetChild(0).GetComponent<Image>().sprite = temp.GetAvatar();
            tPanel.transform.GetChild(1).GetComponent<Text>().text = temp.GetRecruitInfo();
            _pool.Enqueue(temp);
        }
    }

    // Add specific PC back to Roster
    // Called by Library, Backroom and Workshop
    // Also called after mission
    public void AddPCtoRoster(PC pc)
    {
        _resourceManager.roster.Add(pc);
    }

    // Adds recruit to Roster, adds a slot item for it on roster UI, and removes the recruit from the pool
    public void AddPCtoRoster(int panelNUmber)
    {


        if (_resourceManager.roster.Count < max_roster_size)
        {

            // Deque each consequtive recruit in pool and requeue again, unless it is the one in position panelNumber.
            // The one in position panelNUmber is not returened to queue, instead added to roster.
            int initQueueSize = _pool.Count;
            for (int i = 0; i < initQueueSize; i++)
            {
                //Debug.Log("i = " + i + "panelNum = " + panelNUmber);
                PC tmp = _pool.Dequeue();
                if (i == panelNUmber)
                {
                    // Check cost
                    int _cost = tmp.GetComponent<PC>().GetCost();
                    
                    //Debug.Log("Match!");
                    // Only add if enough gold
                    if (_resourceManager.DeductGold(_cost))
                    {
                        _resourceManager.roster.Add(tmp);
                    }
                    else
                    {
                        _pool.Enqueue(tmp);
                    }
                    


                }
                else
                {
                    _pool.Enqueue(tmp);
                }
            }


            // Add Slot to Roster UI Bar
            UpdateRosterSlots();
            UpdateRecruitSlots();
            _uiManager.UpdateRosterSizeText("" + _resourceManager.roster.Count + "/" + max_roster_size);
        }
        else
        {
            //return false;
        }
    }

    // Removes specific PC from Roster
    // Called by Library, Backroom and Workshop
    // Also called beforre mission
    public void RemovePCfromRoster(PC pc)
    {
        _resourceManager.roster.Remove(pc);
    }

    // Gets PC that is currently displayed in management window
    // Removes that PC from the roster list
    // Updates Roster Display
    // Defaults to recruit screen
    public void RemovePCfromRoster()
    {
        Debug.Log("Remove");
        PC pcToRemove = _manageDisplay.GetPC();
        _resourceManager.roster.Remove(pcToRemove);
        UpdateRosterSlots();
        _uiManager.CR_Recruit();
        _uiManager.UpdateRosterSizeText("" + _resourceManager.roster.Count + "/" + max_roster_size);
    }

    // Update from TimeKeeper
    public void TimeStepSignal(string unit)
    {
        //Debug.Log("TimeStepSignal on CommonRoome called with " + unit);
        // If new day, add new Recruit to pool, remove oldest one if already full pool of 4
        if (unit == "day")
        {
            if (_pool.Count > 3)
            {
                PC old = _pool.Dequeue();
                Destroy(old);

                PCIncubator(false);
                UpdateRecruitSlots();
            }
            else
            {
                PCIncubator(false);
                UpdateRecruitSlots();
            }
        }
    }

    public void OnMouseDown()
    {

        if (!EventSystem.current.IsPointerOverGameObject())
        {
            OpenCRUI();

        }
    }

    public void OpenCRUI()
    {
        _uiManager.EnableCommonRoomUI();
        UpdateRosterSlots();
        UpdateRecruitSlots();
    }
}
