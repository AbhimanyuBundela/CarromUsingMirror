using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Update is called once per frame
    void ResetCoinPosition()
    {
        gameObject.transform.position = new Vector3(0, 0, 0);
    }
}
