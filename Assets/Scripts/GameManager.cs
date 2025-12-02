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





    //------------------------------------------------------------------------------------------------//




    //TODO: Change this later to be ran through states
    private void Start()
    {
        EventBus.Publish(EventType.OnRoundStart);
    }

}