using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField]
    protected UIManager _uiManager;

    [SerializeField]
    protected TimeKeeper _timeKeeper;

    private bool _build = false;

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseEnter()
    {
        //Debug.Log("Mouse Enter");
        Vector2 sizes = gameObject.GetComponent<SpriteRenderer>().size;

        _uiManager.DrawOutline(gameObject.transform.position.x , gameObject.transform.position.y, sizes.x + .1f, sizes.y + .1f);
    }

    private void OnMouseExit()
    {
        _uiManager.DrawOutline(0, 0, 0, 0);
    }

    private void OnMouseDown()
    {
        
    }

  
}
