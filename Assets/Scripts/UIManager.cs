using System.Collections;
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
    private GameObject[] _allUI;

    // TOOLBAR TOP
    [SerializeField]
    private Text _goldText;

    [SerializeField]
    private Image _goldImage;

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


    // OFFICE

    [SerializeField]
    private GameObject _office_UI;

    [SerializeField]
    private GameObject _Of_Construct_UI;
    
    [SerializeField]
    private GameObject _Of_Notification_UI;

    [SerializeField]
    private GameObject _Of_Mission_UI;


    // Library
    [SerializeField]
    private GameObject _Lib_Research_UI;

    [SerializeField]
    private ResearcherChoser _Lib_ResearchChoser;

    [SerializeField]
    private GameObject _Lib_ResearchChoserGO;


    // Kitchen
    [SerializeField]
    private GameObject _Kitchen_UI;


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
        _allUI = new GameObject[5];
        _allUI[0] = _commonRoom;
        _allUI[1] = _backRoom1_UI;
        _allUI[2] = _office_UI;
        _allUI[3] = _Lib_Research_UI;
        _allUI[4] = _Kitchen_UI;
    }




    public void DrawOutline(float posx, float posy, float width, float height)
    {
        _outline.gameObject.transform.position = new Vector3(posx, posy);
        _outline.gameObject.transform.localScale = new Vector3(width , height);
    }

    public void BR_Income()
    {
        _BR_income_UI.SetActive(true);
        _BR_research_UI.SetActive(false);
        EnableBackRoomUI(true);
    }

    public void BR_Research()
    {
        _BR_income_UI.SetActive(false);
        _BR_research_UI.SetActive(true);
        EnableBackRoomUI(true);
    }

    public void BR_UpdateIncomeInfoText(int income)
    {
        _BR_IncomeInfo_Tex.text = "Daily income: " + income + " gold.";
    }

    public void BR_UpdateIncomeSizeText(string text)
    {
        _BR_IncomeSize_Text.text = text;
    }


    // C#mon theres gottta be a smartr way.
    public void CallMethodWithString(string methodName)
    {
        if (methodName == "Of_Construct")
        {
            Of_Construct();
        }
        else if (methodName == "Lib_research")
        {
            Lib_Research();
        }

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

    public void CR_Manage()
    {
        _Recruit_UI.SetActive(false);
        _Manage_UI.SetActive(true);
        EnableCommonRoomUI();
    }

    public void CR_Recruit()
    {
        _Manage_UI.SetActive(false);
        _Recruit_UI.SetActive(true);
        EnableCommonRoomUI();
    }

    public void DisabelAllWindows()
    {
        foreach (GameObject uiWin in _allUI)
        {
            uiWin.SetActive(false);
        }
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

    public void EnableOfficeUI()
    {
        DisabelAllWindows();
        _office_UI.SetActive(true);
    }

    public void FeedbackInsufficientFunds()
    {

        StartCoroutine(Flash(_goldImage));

    }

    // Feeds reference to PC in management info window to the CR manager
    public void FeedPCRefToCRManage(PC pc)
    {
        _crManage.AssignPC(pc);
    }

    public void FeedPCtoResearchChoser(PC pc, bool add)
    {
        if (add)
        {
            _Lib_ResearchChoser.AddResearcher(pc);
        }
        else
        {
            _Lib_ResearchChoser.RemoveResearcher(pc);
        }

    }

    public void KitchenUI()
    {
        _Kitchen_UI.SetActive(true);
    }

    public void Lib_Research()
    {
        DisabelAllWindows();
        _Lib_Research_UI.SetActive(true);
    }

    public void Lib_ResearchChoser(bool active)
    {
        _Lib_ResearchChoserGO.SetActive(active);
    }

    public void Of_Construct()
    {
        _Of_Mission_UI.SetActive(false);
        _Of_Notification_UI.SetActive(false);
        _Of_Construct_UI.SetActive(true);
        EnableOfficeUI();
    }

    public void Of_Mission()
    {
        _Of_Notification_UI.SetActive(false);
        _Of_Construct_UI.SetActive(false);
        _Of_Mission_UI.SetActive(true);
        EnableOfficeUI();
    }

    public void Of_Notification()
    {
        _Of_Construct_UI.SetActive(false);
        _Of_Mission_UI.SetActive(false);
        _Of_Notification_UI.SetActive(true);
        EnableOfficeUI();
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


    public void UpdateRosterSizeText(string text)
    {
        _rosterSize.text = text;
    }



    public void UpdateDateTime(string dateTime)
    {
        dateTimeText.text = dateTime;
    }


    //
    // COROUTINES
    //

    // Works only on graphics with green value == blue value
    IEnumerator Flash(Graphic _graphic)
    {
        // times 2 total
        int nframes = 20;

        // Get alpha and green/blue values
        float alpha_v = _graphic.color.a;
        float gb_v = _graphic.color.g;

        // Calculate stepsize based on difference betwen current value und target values (0 for green/blue & 1 for aplha)
        float stepsize_gb = gb_v / nframes;
        float stepsize_alpha = (1 - alpha_v) / nframes;

        // seperate index variable for alpha
        float i_a = alpha_v;

        // One way: increase alpha while decreasing green/blue
        for (float i = gb_v; i > 0; i -= stepsize_gb)
        {
            Color cola = _graphic.color;
            
            cola.g = i;
            cola.b = i;
            cola.a = i_a;

            _graphic.color = cola;

            i_a += stepsize_alpha;

            yield return null;
        }

        // Returen index for alpha to 1 wegen sicherheit
        i_a = 1;

        // Other way: increase green/blue while decreasing alpha
        for (float i = 0; i < gb_v; i += stepsize_gb)
        {
            Color cola = _graphic.color;

            cola.g = i;
            cola.b = i;
            cola.a = i_a;

            _graphic.color = cola;

            i_a -= stepsize_alpha;

            yield return null;
        }

        // Reset initial values for alpha and green/blue
        Color col = _graphic.color;
        col.a= alpha_v;
        col.b = gb_v;
        col.g = gb_v;
        _graphic.color = col;

    }
}
