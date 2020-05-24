using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveControl : MonoBehaviour
{

    public Camera orthoCam;

    public float aspecRatio;

    public SpriteRenderer background;
    private float bgWidth;
    private float bgHeight;

    private float scrollSpeed = 5f;

    private int borderMargin = 5;

    // Zoom vars
    private float zoomSpeed = .2f;
    private float maxZoom = 5f;
    private float minZoom = 1f;


    // Start is called before the first frame update
    void Start()
    {
        bgWidth = background.bounds.size.x;
        bgHeight = background.bounds.size.y;

        aspecRatio = 16f / 9f;
        
    }

    // Update is called once per frame
    void Update()
    {

        // Planar movement at mouse on screen edge
        if (Input.mousePosition.y >= Screen.height - borderMargin )
        {
            if (transform.position.y < (bgHeight / 2 - orthoCam.orthographicSize))
            {
                transform.position += Vector3.up * Time.deltaTime * scrollSpeed;
            }
        }
        // right movement
        if (Input.mousePosition.x >= Screen.width - borderMargin)
        {

            if (transform.position.x < (bgWidth / 2 - orthoCam.orthographicSize * aspecRatio))
            {
                transform.position += Vector3.right * Time.deltaTime * scrollSpeed;
            }
        }
        if (Input.mousePosition.y <= 0 + borderMargin)
        {
            if (transform.position.y > (-1 * bgHeight / 2 + orthoCam.orthographicSize))
            {
                transform.position += -1 * Vector3.up * Time.deltaTime * scrollSpeed;
            }
        }

        // left movement
        if (Input.mousePosition.x <= 0 + borderMargin)
        {
            if (transform.position.x > (-1 * bgWidth / 2 + orthoCam.orthographicSize * aspecRatio))
            {
                transform.position += -1 * Vector3.right * Time.deltaTime * scrollSpeed;
            }
        }

        // Zoom on mousewheel
        if (Input.mouseScrollDelta.y > 0 && orthoCam.orthographicSize > minZoom)
        {
            orthoCam.orthographicSize -= zoomSpeed;
        }
        else if (Input.mouseScrollDelta.y < 0 && orthoCam.orthographicSize < maxZoom)
        {
            orthoCam.orthographicSize += zoomSpeed;
        }
    }
}
