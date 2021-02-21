using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverStriker : MonoBehaviour
{
    [HideInInspector]
    public bool isStrikerSet = false;
    [HideInInspector]
    public bool isStrikerHit = false;

    // Update is called once per frame
    /*void Update()
    {
        if(!isStrikerSet && !isStrikerHit)
        {
            TrackMouse();
        }
    }*/
    private void OnMouseDrag()
    {
        isStrikerHit = false;
        isStrikerSet = false;
        TrackMouse();
    }

    void TrackMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        //mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos = Camera.main.ScreenToViewportPoint(mousePos);

        float clampedX = Mathf.Clamp(mousePos.x, -1.36f, 1.36f);

        transform.localPosition = new Vector2(clampedX, transform.localPosition.y);
        //transform.position = new Vector2(clampedX, transform.position.y);
    }
}
