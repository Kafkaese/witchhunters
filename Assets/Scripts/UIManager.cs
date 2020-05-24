﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text dateTimeText;

    [SerializeField]
    private GameObject _outline;


    [SerializeField]
    private GameObject[] _allUI= new GameObject[2];

    // TOOLBAR TOP
    [SerializeField]
    private Text _goldText;

    // BACKROOM

    [SerializeField]
    private GameObject _backRoom1_UI;

    [SerializeField]
    private GameObject _backRoom0_UI;

    [SerializeField]
    private GameObject _BR_income_UI;

    [SerializeField]
    private GameObject _BR_research_UI;

    [SerializeField]
    private Text _BR_IncomeInfo_Tex;

    [SerializeField]
    private Text _BR_IncomeSize_Text;


    // COMMON ROOM

    [SerializeField]
    private GameObject _commonRoom;
    [SerializeField]
    private CommonRoom _commonRoom_Script;

    [SerializeField]
    private GameObject _Manage_UI;

    [SerializeField]
    private GameObject _Recruit_UI;

    [SerializeField]
    private Text _rosterSize;


    // MANAGE INFO REFS
    [SerializeField]
    private Image _infoSprite;

    [SerializeField]
    private Text _infoText;

    [SerializeField]
    private CommonRoomManageDisplay _crManage;

    [SerializeField]
    private GameObject _CR_dismissConfirmPanel;



    // Start is called before the first frame update
    void Start()
    {
        dateTimeText.text = " 1st Karos Folly 120E3 ";

        _allUI[0] = _commonRoom;
        _allUI[1] = _backRoom1_UI;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateDateTime(string dateTime)
    {
        dateTimeText.text = dateTime;
    }

    public void DrawOutline(float posx, float posy, float width, float height)
    {
        _outline.gameObject.transform.position = new Vector3(posx, posy);
        _outline.gameObject.transform.localScale = new Vector3(width , height);
    }

    public void BR_UpdateIncomeInfoText(int income)
    {
        _BR_IncomeInfo_Tex.text = "Daily income: " + income + " gold.";
    }

    public void BR_UpdateIncomeSizeText(string text)
    {
        _BR_IncomeSize_Text.text = text;
    }

    public void CR_Manage()
    {
        _Recruit_UI.SetActive(false);
        _Manage_UI.SetActive(true);
    }

    public void CR_Recruit()
    {
        _Manage_UI.SetActive(false);
        _Recruit_UI.SetActive(true);
    }

    public void EnableBackRoomUI(bool built)
    {
        Debug.Log("EnableBackRoomUI");
        DisabelAllWindows();
        if(built)
        {
            Debug.Log("built == true");
            _backRoom1_UI.SetActive(true);
        }
        else
        {
            _backRoom0_UI.SetActive(true);
        }
    }

    public void EnableCommonRoomUI()
    {
        DisabelAllWindows();
        _commonRoom.SetActive(true);
    }

    public void UpdateInfoWinodowCR(Sprite app, string infoText)
    {
        _infoSprite.sprite = app;
        _infoText.text = infoText;
    }

    public void UpdateGold(int amount)
    {
        _goldText.text = "" + amount + "";
    }

    private void DisabelAllWindows()
    {
        foreach (GameObject uiWin in _allUI)
        {
            uiWin.SetActive(false);
        }
    }

    // Feeds reference to PC in management info window to the CR manager
    public void FeedPCRefToCRManage(PC pc)
    {
        _crManage.AssignPC(pc);
    }

    public void UpdateRosterSizeText(string text)
    {
        _rosterSize.text = text;
    }

    public void BR_Income()
    {

    }

    public void BR_Research()
    {

    }


    public void CR_ShowDismiss()
    {
        _CR_dismissConfirmPanel.SetActive(true);
    }

    public void CR_DsimissConfirm(bool confirm)
    {

        _CR_dismissConfirmPanel.SetActive(false);
        if (confirm)
        {
            _commonRoom_Script.RemovePCfromRoster();
        }
    }
}