using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResearcherSlot : MonoBehaviour
{
    private PC _researcher;

    [SerializeField]
    private Image _avatar;

    private UIManager _uiManager;


    // ROSTER SLOT REFS
    [SerializeField]
    private Text _charInfoText;

    [SerializeField]
    private Text _charLevelText;


    public PC Researcher { get => _researcher; set => _researcher = value; }

    public void Start()
    {
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    public void PopulateSlot()
    {
        //Debug.Log("PopulateSlot()");
        (Sprite ava, string name, string classs, int lvl) = _researcher.getRosterSlotInfo();
        _avatar.sprite = (Sprite)ava;
        _charInfoText.text = "<b>" + name + "</b>" + "\n " + classs;
        _charLevelText.text = lvl.ToString();
    }

    public void OnClick()
    {
        _uiManager.FeedPCtoResearchChoser(Researcher, false);
    }
}
