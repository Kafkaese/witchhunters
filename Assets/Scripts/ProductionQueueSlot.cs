using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductionQueueSlot : MonoBehaviour
{
    [SerializeField]
    private Image _itemImage;

    private Meal _item;

    private UIManager _uiManager;

    [SerializeField]
    private Kitchen _kitchen;

    public void AssignMeal(Meal meal)
    {
        _item = meal;
        _itemImage.sprite = meal.GetSprite();
    }

    public void AssignItem(Meal meal)
    {
        _item = meal;
        _itemImage.sprite = meal.GetSprite();
    }

    private void Start()
    {
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        _kitchen = GameObject.Find("Kitchen_UI").GetComponent<Kitchen>();
    }

    public void PointerEnter()
    {

        _uiManager.MouseOverText(_item.GetName());
    }

    public void PointerExit()
    {

        _uiManager.MouseOverTextExit();
    }

    public void PoinerClick()
    {
        Debug.Log("Click");
        _kitchen.RemoveMealfromProductionQueue(_item);
    }
}
