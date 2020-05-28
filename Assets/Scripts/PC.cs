using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC : MonoBehaviour, TimeObserver
{
    // INFO

    private string _name;

    private string _className;

    private int _cost = 2000;


    // STATS
    private int _strength;
    private int _constitution;
    private int _dexterity;
    private int _willpower;

    private int _exp;
    private int _level;

    private bool _noble = false;

    // 0 - None
    // 1 -
    // 2 -
    // 3 - 
    private int _faith;


    // BASE MANAGEMNT
    private bool _locked;

    private TimeKeeper _timeKeeper;


    //
    // MANSION MODE
    //

    // Appearance
    [SerializeField]
    private Sprite _avatar;
    [SerializeField]
    private Sprite _UIsprite;


    // Pseudoconstructor
    public void Construct(string name, string classname, int str, int dex, int con, int will, Sprite ava, Sprite sprite)
    {
        _name = name;
        _className = classname;

        _strength = str;
        _dexterity = dex;
        _willpower = will;
        _constitution = con;

        _avatar = ava;
        _UIsprite = sprite;

        if (_noble)
        {
            _cost = 5000;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

        _timeKeeper = GameObject.Find("TimeKeeper").GetComponent<TimeKeeper>();
        if (_timeKeeper != null)
        {
            _timeKeeper.Signup(this);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Initilaize UI on spawn in mission scene
    private void Spawn()
    {
        // Init UI including attacks, skills and equipment
    }

    public Sprite GetAvatar()
    {
        return _avatar;
    }

    public string GetClass()
    {
        return _className;
    }

    public int GetCost()
    {
        return _cost;
    }

    public int GetLevel()
    {
        return _level;
    }

    public string GetName()
    {
        return _name;
    }

    public string GetRecruitInfo()
    {
        return "<b>Name</b>" + _name + '\n' + "<b>Class</b>: " + _className + "\n" + "<i>Strength:</i> " + _strength.ToString() + "\n COST: " + _cost + " Gold";
    }

    public (Sprite, string, string, int) getRosterSlotInfo()
    {
        Debug.Log("PC.GetRosterSlotInfo()");
        Debug.Log(_name);
        return (_avatar, _name, _className, _level);
    }

    public (int, int, int, int) GetStats()
    {
        return (_strength, _dexterity, _willpower, _constitution);
    }

    public Sprite GetSprite()
    {
        return _UIsprite;
    }

    public void Lock(bool lockit)
    {
        _locked = lockit;
    }

    public bool GetLocked()
    {
        return _locked;
    }

    public void TimeStepSignal(string unit)
    {
        if (unit == "month")
        {
            _locked = false;
        }
    }
}
