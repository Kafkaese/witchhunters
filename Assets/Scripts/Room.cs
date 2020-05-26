using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Room : MonoBehaviour
{
    // UI Manager
    [SerializeField]
    protected UIManager _uiManager;

    // TimeKeeper
    [SerializeField]
    protected TimeKeeper _timeKeeper;

    // Images for Upgrade
    [SerializeField]
    private Sprite[] _upgradeSprites = new Sprite[3];

    
    // Draws outline at mouse enter
    void OnMouseEnter()
    {
        //Debug.Log("Mouse Enter");
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            Vector2 sizes = gameObject.GetComponent<SpriteRenderer>().size;

            _uiManager.DrawOutline(gameObject.transform.position.x, gameObject.transform.position.y, sizes.x + .1f, sizes.y + .1f);
        }
    }

    // Removes outline on mouse exit
    private void OnMouseExit()
    {
        _uiManager.DrawOutline(0, 0, 0, 0);
    }

    // Upgrades Room Sprite from array
    public void Upgrade(int lvl)
    {
        if (lvl > 1 & lvl < 5)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = _upgradeSprites[lvl - 2];
        }
    }

}
