using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class ResearcherChoser : MonoBehaviour
{
    // Research Slots
    private Queue<GameObject> _researcherSlots = new Queue<GameObject>();

    // Roster Slots
    private Queue<GameObject> _rosterSlots = new Queue<GameObject>();


    // PCs currently assigned to this research project -> need to be attached to Researcher Slots and displayed
    private List<PC> _activeResearchers = new List<PC>();
    

    // Slot Pool for Researcher Slots
    [SerializeField]
    private SimpleObjectPool _researcherSlotPool;

    // Slot Pool for Roster Slots
    [SerializeField]
    private SimpleObjectPool _rosterSlotPool;


    // Ref to Panel for Roster Slots
    [SerializeField]
    private Transform _rosterSlotPanel;

    // Ref to Panel for Researcher Slots
    [SerializeField]
    private Transform _researcherSlotPanel;

    // Ref to Resource Manager
    [SerializeField]
    private ResourceManager _resourceManager;

    // Ref to Library Script
    [SerializeField]
    private Library _library;

    // Ref to UI Manager
    [SerializeField]
    private UIManager _uiManager;

    // Ref to Textfield showing curretn number of active researchers vs maximum number
    [SerializeField]
    private Text _researcherNumberText;


    // Maximum number of allowed Researchers in _activeResearchers
    private int _maxResearchers = 3;

    private void OnEnable()
    {
        RefreshSlotLists();
        _researcherNumberText.text = "" + _activeResearchers.Count + "/" + _maxResearchers;

    }

    public void AddResearcher(PC _researcher)
    {
        // Get PC from RosterSlot
        //PC _researcher = slot.GetPC();

        // Remove PC from Roster
        _resourceManager.roster.Remove(_researcher);

        // Add PC to _activeResearchers
        _activeResearchers.Add(_researcher);

        //Refresh Researcher Slot Panel
        RefreshSlotLists();

        // Refresh Display of Number of Researchers
        _researcherNumberText.text = "" + _activeResearchers.Count + "/" + _maxResearchers;

    }

    public void RemoveResearcher(PC _researcher)
    {
        // Get PC attached to ResearchSlot
        //PC _researcher = slot.Researcher;

        // Remove PC from _activeResearchers
        _activeResearchers.Remove(_researcher);

        // Add PC back to Roster
        _resourceManager.roster.Add(_researcher);

        // Refreesh Slot Panels
        RefreshSlotLists();

        // Refresh Dsiplay of Number of Researchers
        _researcherNumberText.text = "" + _activeResearchers.Count + "/" + _maxResearchers;

    }

    public void RefreshSlotLists()
    {
        // Roster Slots FIRST
        //Debug.Log("UpdateRosterSlots()");
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
            if (chara.IsBaseClass(2))
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

        //
        // Researcher Slots SECOND
        //

        rsc = _researcherSlots.Count;

        for (int i = 0; i < rsc; i++)
        {
            Debug.Log("UpdateRosterSlots: Dequeing");
            GameObject slot = _researcherSlots.Dequeue();
            Debug.Log("Slot returned");
            _researcherSlotPool.ReturnObject(slot);
        }

        for (int i = 0; i < _activeResearchers.Count; i++)
        {
            Debug.Log("UpdateRosterSlots: Enquing");
            Debug.Log("i 0 " + i + ". Getting new slot");
            PC chara = _activeResearchers[i];
               
            GameObject newSlot;

            
            newSlot = _researcherSlotPool.GetGameObject();
            
            //= GameObject.Instantiate(_slotPrefab, _contentPanel);
            
            ResearcherSlot rSlot = newSlot.GetComponent<ResearcherSlot>();
            newSlot.transform.SetParent(_researcherSlotPanel);
            rSlot.Researcher = chara;
            rSlot.PopulateSlot();
            _researcherSlots.Enqueue(newSlot);
            

        }

    }

        public void CancelAndRevert()
    {
        // Add all PCs back to Roster
        foreach (PC _researcher in _activeResearchers)
        {
            _resourceManager.roster.Add(_researcher);
        }

        // Clear Active Researchers
        _activeResearchers.Clear();

        // Close Research Choser Window
        _uiManager.Lib_ResearchChoser(false);
    }

    public void ConfirmAndStart()
    {
        int multiplier = 1;

        foreach (PC researcher in _activeResearchers)
        {
            multiplier += researcher.GetLevel();
        }
        _library.ResearchThis(multiplier);
    }
}
