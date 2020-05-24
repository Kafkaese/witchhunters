using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BR_RosterSlot : MonoBehaviour
{
    [SerializeField]
    private UIManager _uiManager;

    [SerializeField]
    private BackRoom _backRoom;

    [SerializeField]
    private PC _referencePC;

    [SerializeField]
    private Image _avatar;


    // ROSTER SLOT REFS
    [SerializeField]
    private Text _charInfoText;

    [SerializeField]
    private Text _charLevelText;

    public void Start()
    {
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        _backRoom = GameObject.Find("Backroom1").GetComponent<BackRoom>();
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
    }

    // Populate information in manage window of Common Room
    public void OnMouseDown()
    {
        _backRoom.SelectWorker(_referencePC);

    }
}
