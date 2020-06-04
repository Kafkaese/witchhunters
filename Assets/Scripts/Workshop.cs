using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Workshop : MonoBehaviour, TimeObserver
{
    // Production Queue
    private Queue<Item> _productionQueue = new Queue<Item>();

    // Slots for Production Queue
    private Queue<GameObject> _productionQueueSlots = new Queue<GameObject>();

    // SlotPool
    [SerializeField]
    private SimpleObjectPool _itemSlotPool;

    // Production Queue Conten Panel
    [SerializeField]
    private Transform _prodCueueContenPanel;


    // All producable meals
    [SerializeField]
    private List<Item> _availableItems;


    // Currently selected Item
    private Item _selectedItem;



    // Refs to Description Panel: Title, Description
    [SerializeField]
    private Text _titleText;
    [SerializeField]
    private Text _descrptionText;
    [SerializeField]
    private Button _craftButton;

    // Time Keeper
    [SerializeField]
    private TimeKeeper _timeKeeper;

    // ResourceManager
    [SerializeField]
    private ResourceManager _resourceManager;

    public void Start()
    {
        _timeKeeper.Signup(this);
    }

    public void OnEnable()
    {
        _craftButton.interactable = false;


    }

    public void SelectItem(Item item)
    {
        _selectedItem = item;
        PopulateInfoPanel(item);
        _craftButton.interactable = true;
    }

    public void PopulateInfoPanel(Item item)
    {

        _descrptionText.text = item.GetDescription();
        _titleText.text = item.GetName();
    }

    public void AddItemtoProductionQueue()
    {
        if (_resourceManager.DeductGold(_selectedItem.GetCost()))
        {
            Item newItem = new Item(_selectedItem);
            _productionQueue.Enqueue(newItem);
            RefreshProductionQueue();
        }

    }

    public void RemoveItemfromProductionQueue(Item item)
    {

        for (int i = 0; i < _productionQueue.Count; i++)
        {

            Item tmp = _productionQueue.Dequeue();
            if (!UnityEngine.Object.ReferenceEquals(tmp, item))
            {
                _productionQueue.Enqueue(tmp);
            }
        }
        _resourceManager.AddGold(item.GetCost() / 2);
        RefreshProductionQueue();
    }

    private void RefreshProductionQueue()
    {
        int cnt = _productionQueueSlots.Count;
        for (int i = 0; i < cnt; i++)
        {
            Debug.Log("Slots: " + _productionQueueSlots.Count);
            GameObject tmp = _productionQueueSlots.Dequeue();
            _itemSlotPool.ReturnObject(tmp);
        }

        foreach (Item item in _productionQueue)
        {
            Debug.Log("Item:" + _productionQueue.Count);
            GameObject slotGO = _itemSlotPool.GetGameObject();
            ProductionQueueSlotItem slot = slotGO.GetComponent<ProductionQueueSlotItem>();
            slot.AssignMeal(item);
            slotGO.transform.SetParent(_prodCueueContenPanel);
            _productionQueueSlots.Enqueue(slotGO);
        }
    }

    public void TimeStepSignal(string unit)
    {
        if (unit == "day")
        {
            //Debug.Log("Day!");
            if (_productionQueue.Count > 0)
            {
                Debug.Log("It aint empty.");
                Item item = _productionQueue.Dequeue();
                _resourceManager.AddItem(item);
                RefreshProductionQueue();
                RefreshMealCounts();
            }
        }
    }

    // Updates number of meals in stock for each unlocked meal
    public void RefreshMealCounts()
    {
        foreach (Item item in _availableItems)
        {
            item.UpdateStockCount(_resourceManager.GetItemCount(item));
        }
    }

    public void Upgrade(int level)
    {
        throw new NotImplementedException();
    }
}
