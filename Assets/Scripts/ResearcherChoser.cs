using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class ResearcherChoser : MonoBehaviour
{
    private List<ResearcherSlot> _researcherSlots = new List<ResearcherSlot>();

    private List<PC> _activeResearchers = new List<PC>();

    [SerializeField]
    private SimpleObjectPool _researcherSlotPool;

    [SerializeField]
    private SimpleObjectPool _rosterSlotPool;


    public void AddResearcher(RosterSlot slot)
    {
        // Remove PC from Roster

        // Add PC to _activeResearchers

        //Refresh Researcher Slot Panel

        // Refresh Display of Number of Researchers


    }

    public void RemoveResearcher(ResearcherSlot slot)
    {
        // Remove PC from _activeResearchers

        // Add PC back to Roster

        // Refreesh Roster Slot Panel

        //Refresh Researcher Slot Panel

        // Refresh Dsiplay of Number of Researchers
    }

    public void RefreshSlotLists()
    {
        // All the magic
    }
}
