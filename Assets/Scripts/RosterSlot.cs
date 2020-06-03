using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RosterSlot : MonoBehaviour
{
    [SerializeField]
    private UIManager _uiManager;

    [SerializeField]
    private PC referencePC;

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
    }

    public void SetPC(PC pc)
    {
        //Debug.Log("SetPC()");
        referencePC = pc;
    }

    public void PopulateSlot()
    {
        //Debug.Log("PopulateSlot()");
        (Sprite ava, string name, string classs, int lvl) = referencePC.getRosterSlotInfo();
        _avatar.sprite = (Sprite)ava;
        _charInfoText.text = "<b>" + name + "</b>" + "\n " + classs;
        _charLevelText.text = lvl.ToString();
    }

    // Populate information in manage window of Common Room
    public void OnClickCR()
    {
        //Debug.Log("Clicked on Dude in Slot");
        _uiManager.UpdateInfoWinodowCR(referencePC.GetSprite(), "<b>" + referencePC.GetName() + "</b>" + "\n " + "<i>" + referencePC.GetClass() + "</i> \n \n Physique:");
        _uiManager.CR_Manage();
        _uiManager.FeedPCRefToCRManage(referencePC);

    }

    public PC GetPC()
    {
        return referencePC;
    }

    // USED IN RESERCH CHOSER (LIBRARY)
    public void OnClick()
    {
        _uiManager.FeedPCtoResearchChoser(referencePC, true);
    }

    public void OnClickOfficer()
    {
        _uiManager.FeedPCtoOfficerChoser(referencePC);
    }
}
