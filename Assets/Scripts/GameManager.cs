using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameManager : MonoBehaviour
{


    private enum GameState
    {
        InCombat,
        CombatBreak,
        PauseMenu,
    }


    [SerializeField] private List<RoundData> _roundsData = new();
    private static int _roundIndex = 0;


    private WaitForSeconds _roundStartBuffer = new(1.5f);



    //------------------------------------------------------------------------------------------------//



    private void Start() => StartRound();


    private void OnEnable()
    {
        EventBus.Subscribe(EventType.OnExitShop, StartRound);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(EventType.OnExitShop, StartRound);
    }



    //------------------------------------------------------------------------------------------------//



    public void StartRound() => StartCoroutine(BufferStart());


    private IEnumerator BufferStart()
    {
        yield return _roundStartBuffer;
        EventBus.Publish(EventType.OnRoundStart, _roundsData[_roundIndex]);
        _roundIndex++;
    }




}