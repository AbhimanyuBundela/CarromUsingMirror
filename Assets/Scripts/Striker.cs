using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Striker : MonoBehaviour
{
    public float minStrikerSpeed = 50f;
    public float maxStrikerSpeed = 350f;
    private bool isOverlapping = false;

    Vector2 direction;
    Vector2 strikerDirection;

    Vector2 initialMouse;
    Vector2 finalMouse;

    [SerializeField] private MoverStriker _moverStriker;
    [SerializeField] private Text _message = null;
    [SerializeField] private GameObject _arrow = null;

    private GameLogicManager _gameLogicManager = null;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Coin")
        {
            isOverlapping = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Coin")
        {
            isOverlapping = false;
        }
    }

    void Start()
    {
        _gameLogicManager = GameObject.FindGameObjectWithTag("GameSceneManager").GetComponent<GameLogicManager>();
        _message = GameObject.Find("Messages").GetComponent<Text>();
    }

    void Update()
    {
        Vector2 moverStrikerPosition = _moverStriker.gameObject.transform.localPosition;
        //Vector2 moverStrikerPosition = _moverStriker.gameObject.transform.position;
        if (!_moverStriker.isStrikerSet && !_moverStriker.isStrikerHit)
        {
            transform.localPosition = new Vector2(moverStrikerPosition.x, transform.localPosition.y);
            //transform.position = new Vector2(moverStrikerPosition.x, transform.position.y);
        }

        if (!_moverStriker.isStrikerSet && Input.GetMouseButtonUp(0) && isOverlapping)
        {
            _message.text = "Striker and Token can't collide reset";
            StartCoroutine(EraseMessage());
        }

        if (!_moverStriker.isStrikerSet && Input.GetMouseButtonUp(0) && !isOverlapping)
        {
            _moverStriker.isStrikerSet = true;
        }
        else
            if (!_moverStriker.isStrikerHit && _moverStriker.isStrikerSet)
        {
            if (Input.GetMouseButtonDown(0))
            {
                initialMouse = Input.mousePosition;
                initialMouse = Camera.main.ScreenToWorldPoint(initialMouse);
                _arrow.SetActive(true);
            }

            if (Input.GetMouseButton(0))
            {
                finalMouse = Input.mousePosition;
                finalMouse = Camera.main.ScreenToWorldPoint(finalMouse);
                direction = initialMouse - finalMouse;
            }

            if (Input.GetMouseButtonUp(0))
            {
                finalMouse = Input.mousePosition;
                finalMouse = Camera.main.ScreenToWorldPoint(finalMouse);
                direction = initialMouse - finalMouse;
                this.GetComponent<Rigidbody2D>().AddForce(Mathf.Min(Mathf.Max(direction.magnitude * minStrikerSpeed, minStrikerSpeed), maxStrikerSpeed) * direction.normalized);
                _moverStriker.isStrikerHit = true;
                _arrow.SetActive(false);
            }

            if (_arrow != null)
            {
                if (direction.magnitude != 0)
                {
                    float yScaleValue = Mathf.Clamp(direction.magnitude, 0.5f, 3f);
                    float rotationAlongZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    _arrow.transform.localEulerAngles = new Vector3(0, 0, rotationAlongZ - (Camera.main.transform.localEulerAngles.z + 90f));
                    //_arrow.transform.localEulerAngles = new Vector3(0, 0, rotationAlongZ - 90f);
                    _arrow.transform.localScale = new Vector3(1, yScaleValue, 1);
                }
            }
        }
        else
            if(_moverStriker.isStrikerHit && _moverStriker.isStrikerSet)
                StartCoroutine(CheckRestPosition());



    }

    public void ResetStriker()
    {
        //Other Reset Logic but for now just a predefined position
        transform.localPosition = new Vector2(0, 2.18f);
        //transform.position = new Vector2(0, -1.73f);
        transform.localEulerAngles = new Vector3(0, 0, 0);
        _arrow.transform.localEulerAngles = new Vector3(0, 0, 0);
        _arrow.transform.localScale = new Vector3(1, 0.5f, 1);
        _moverStriker.isStrikerSet = false;
        _moverStriker.isStrikerHit = false;
        direction = Vector2.zero;
    }

    IEnumerator CheckRestPosition()
    {
        yield return new WaitForSeconds(1.0f);

        if (this.GetComponent<Rigidbody2D>().IsSleeping() && _moverStriker.isStrikerHit && _moverStriker.isStrikerSet)
        {
            ResetStriker();
            if (!_gameLogicManager.GetIsPlayerScored())
            {
                _gameLogicManager.ChangeTurn();
            }
            else
            {
                _gameLogicManager.SetIsPlayerScored(false);
            }
        }
        
    }

    IEnumerator EraseMessage()
    {
        yield return new WaitForSeconds(2f);

        _message.text = null;
    }
}
