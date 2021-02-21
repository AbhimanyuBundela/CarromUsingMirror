using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogicManager : MonoBehaviour
{
    [SerializeField] private Transform PlayerTurnGameObjects = null;
    [SerializeField] private List<Transform> PlayerPositions = null;


    private int _nextTurn = 0;
    private bool _isPlayerScored = false;

    public bool GetIsPlayerScored()
    {
        return _isPlayerScored;
    }
    public void SetIsPlayerScored(bool isPLayerScored)
    {
        _isPlayerScored = isPLayerScored;
    }

    public void ChangeTurn()
    {
        _nextTurn = (_nextTurn + 1) % PlayerPositions.Count;
        if (PlayerPositions.Count > 1)
        {
            PlayerTurnGameObjects.eulerAngles = PlayerPositions[_nextTurn].eulerAngles;
        }
    }
}
