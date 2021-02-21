using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerToggle : MonoBehaviour
{
    private MoverStriker _moverStriker = null;

    // Update is called once per frame
    void Update()
    {
        GameObjectAssign();

        if (_moverStriker != null)
        {
            if (!_moverStriker.isStrikerSet)
            {
                gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
            }
            else
                gameObject.GetComponent<CircleCollider2D>().isTrigger = false;
        }
    }

    void GameObjectAssign()
    {
        _moverStriker = GameObject.FindObjectOfType<MoverStriker>();
    }
}
