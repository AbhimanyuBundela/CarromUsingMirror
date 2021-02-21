using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutoffBoundTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Coin")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().Sleep();
            collision.gameObject.transform.position = new Vector3(0, 0, 0);
        }
    }

}
