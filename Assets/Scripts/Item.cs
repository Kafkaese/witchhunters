using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField]
    private string _name;

    [SerializeField]
    private string _description;

    [SerializeField]
    private int price;

    [SerializeField]
    private int time;

    [SerializeField]
    private Image _icon;

    [SerializeField]
    private Text _stockCount;


    // Copy Constructor
    public Item(Item oldItem)
    {
        _name = oldItem.GetName();
        price = oldItem.GetCost();
        _icon = oldItem.GetImage();

    }


    public int GetCost()
    {
        return price;
    }

    public string GetDescription()
    {
        return _description + "\n <b>Cost:</b> " + price.ToString() + "\n <b>Time:</b> " + time.ToString();
    }

    public string GetName()
    {
        return _name;
    }

    public Sprite GetSprite()
    {
        return _icon.sprite;
    }

    public Image GetImage()
    {
        return _icon;
    }

    public void UpdateStockCount(int count)
    {
        _stockCount.text = "In Stock: " + count.ToString();
    }
}
