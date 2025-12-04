using Gameplay.Player;
using UnityEngine;



public static class Bootstrapper
{
    private const string PLAYERTRANSFORMKEYNAME = "PlayerTransform";
    private static BlackboardKey PLAYERTRANSFORMKEY;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void PreInitialize()
    {
        GameManager.GlobalBlackboard = new();
        PLAYERTRANSFORMKEY = GameManager.GlobalBlackboard.TryGetOrAddKey(PLAYERTRANSFORMKEYNAME);
        GameManager.PlayerTransformKey = PLAYERTRANSFORMKEY;
    }


    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void PostInitialize()
    {
        //TODO: Less friendly if there are multiple players, but works for singleplayer
        var playerPos = Object.FindAnyObjectByType<Player>();

        GameManager.GlobalBlackboard.ModifyValue(PLAYERTRANSFORMKEY, playerPos.transform);
    }
}