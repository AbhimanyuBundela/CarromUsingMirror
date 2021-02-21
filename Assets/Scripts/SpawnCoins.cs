using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCoins : MonoBehaviour
{

    public void SpawnCoin(GameObject coin)
    {
        if (coin != null)
        {
            Instantiate(coin, new Vector3(0, 0, 0), Quaternion.identity);
        }
    }
}
