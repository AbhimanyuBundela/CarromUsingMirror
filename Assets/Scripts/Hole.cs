using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Hole : MonoBehaviour
{
    [SerializeField] private Text _message = null;

    private GameLogicManager _gameLogicManager = null;

    void Start()
    {
        _gameLogicManager = GameObject.FindGameObjectWithTag("GameSceneManager").GetComponent<GameLogicManager>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Striker")
        {
            _message.text = "Penalty";
            collision.gameObject.GetComponent<Rigidbody2D>().Sleep();
            StartCoroutine(EraseMessage());
            //Retreat to original position and penalties
        }
        else if(collision.transform.tag == "Coin")
        {
            //Add Scores and other events
            if (_gameLogicManager != null)
            {
                _gameLogicManager.SetIsPlayerScored(true);
            }
            Destroy(collision.transform.gameObject);
        }
    }

    IEnumerator EraseMessage()
    {
        yield return new WaitForSeconds(2f);

        _message.text = null;
    }
}
