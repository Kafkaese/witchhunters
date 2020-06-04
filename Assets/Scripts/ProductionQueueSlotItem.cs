using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductionQueueSlotItem : MonoBehaviour
{
    [SerializeField]
    private Image _itemImage;

    private Item _item;

    private UIManager _uiManager;

    [SerializeField]
    private Workshop _workshop;

    public void AssignMeal(Item item)
    {
        _item = item;
        _itemImage.sprite = item.GetSprite();
    }

    public void AssignItem(Item item)
    {
        _item = item;
        _itemImage.sprite = item.GetSprite();
    }

    private void Start()
    {
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        _workshop = GameObject.Find("Workshop1").GetComponent<Workshop>();
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
        _workshop.RemoveItemfromProductionQueue(_item);
    }
}
