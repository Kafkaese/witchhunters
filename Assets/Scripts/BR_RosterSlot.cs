using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BR_RosterSlot : MonoBehaviour, TimeObserver
{
    [SerializeField]
    private UIManager _uiManager;

    [SerializeField]
    private BackRoom _backRoom;

    private TimeKeeper _timeKeeper;

    [SerializeField]
    private PC _referencePC;

    [SerializeField]
    private Image _avatar;

    [SerializeField]
    private Image _background;


    // ROSTER SLOT REFS
    [SerializeField]
    private Text _charInfoText;

    [SerializeField]
    private Text _charLevelText;

    public void Start()
    {
        _background = gameObject.GetComponent<Image>();
        if(_background == null)
        {
            Debug.Log("Background not found!");
        }
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        _backRoom = GameObject.Find("Backroom1").GetComponent<BackRoom>();
        _timeKeeper = GameObject.Find("TimeKeeper").GetComponent<TimeKeeper>();
        if (_timeKeeper != null)
        {
            _timeKeeper.Signup(this);
        }
        
    }

    public void SetPC(PC pc)
    {
        //Debug.Log("SetPC()");
        _referencePC = pc;
    }

    public void PopulateSlot()
    {
        //Debug.Log("PopulateSlot()");
        (Sprite ava, string name, string classs, int lvl) = _referencePC.getRosterSlotInfo();
        //Debug.Log(name);
        _avatar.sprite = (Sprite)ava;
        _charInfoText.text = "<b>" + name + "</b>" + "\n " + classs;
        _charLevelText.text = lvl.ToString();
        if (_referencePC.GetLocked())
        {
            _background.color = Color.red;
        }
    }

    // Populate information in manage window of Common Room
    public void OnMouseDown()
    {
        _backRoom.SelectWorker(_referencePC);

    }

    public void TimeStepSignal(string unit)
    {
        if (unit == "month")
        {
            _background.color = Color.white;
        }
    }
}
