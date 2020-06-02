using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductionQueueSlot : MonoBehaviour
{
    [SerializeField]
    private Image _itemImage;

    private Meal _item;

    public void AssignMeal(Meal meal)
    {
        _item = meal;
        _itemImage.sprite = meal.GetSprite();
    }
}
