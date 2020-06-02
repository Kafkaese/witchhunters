using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Kitchen : MonoBehaviour, TimeObserver
{
    // Production Queue
    private Queue<Meal> _productionQueue = new Queue<Meal>();

    // Slots for Production Queue
    private Queue<GameObject> _productionQueueSlots = new Queue<GameObject>();

    // SlotPool
    [SerializeField]
    private SimpleObjectPool _mealSlotPool;

    // Production Queue Conten Panel
    [SerializeField]
    private Transform _prodCueueContenPanel;


    // All producable meals
    [SerializeField]
    private List<Meal> _availableMeals;


    // Currently selected Meal
    private Meal _selectedMeal;



    // Refs to Description Panel: Title, Description
    [SerializeField]
    private Text _titleText;
    [SerializeField]
    private Text _descrptionText;
    [SerializeField]
    private Button _cookButton;

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
        _cookButton.interactable = false;


    }

    public void SelectMeal(Meal meal)
    {
        _selectedMeal = meal;
        PopulateInfoPanel(meal);
        _cookButton.interactable = true;
    }

    public void PopulateInfoPanel(Meal meal)
    {

        _descrptionText.text = meal.GetDescription();
        _titleText.text = meal.GetName();
    }

    public void AddMealtoProductionQueue()
    {
        if (_resourceManager.DeductGold(_selectedMeal.GetCost()))
        {
            _productionQueue.Enqueue(_selectedMeal);
            RefreshProductionQueue();
        }

    }

    public void RemoveMealfromProductionQueue()
    {
        for (int i = 0; i < _productionQueue.Count; i++)
        {
            
            Meal tmp = _productionQueue.Dequeue();
            if (tmp != _selectedMeal)
            {
                _productionQueue.Enqueue(tmp);
            }
        }
        _resourceManager.AddGold(_selectedMeal.GetCost() / 2);
    }

    private void RefreshProductionQueue()
    {
        int cnt = _productionQueueSlots.Count;
        for (int i = 0; i < cnt; i++)
        {
            Debug.Log("Slots: " + _productionQueueSlots.Count);
            GameObject tmp = _productionQueueSlots.Dequeue();
            _mealSlotPool.ReturnObject(tmp);
        }

        foreach(Meal meal in _productionQueue)
        {
            Debug.Log("Meal:" + _productionQueue.Count);
            GameObject slotGO = _mealSlotPool.GetGameObject();
            ProductionQueueSlot slot = slotGO.GetComponent<ProductionQueueSlot>();
            slot.AssignMeal(meal);
            slotGO.transform.SetParent(_prodCueueContenPanel);
            _productionQueueSlots.Enqueue(slotGO);
        }
    }

    public void TimeStepSignal(string unit)
    {
        if (unit == "day")
        {
            //Debug.Log("Day!");
            if(_productionQueue.Count > 0)
            {
                Debug.Log("It aint empty.");
                Meal meal = _productionQueue.Dequeue();
                _resourceManager.AddMeal(meal);
                RefreshProductionQueue();
                RefreshMealCounts();
            }
        }
    }

    // Updates number of meals in stock for each unlocked meal
    public void RefreshMealCounts()
    {
        foreach(Meal meal in _availableMeals)
        {
            meal.UpdateStockCount(_resourceManager.GetMealCount(meal));
        }
    }
}
