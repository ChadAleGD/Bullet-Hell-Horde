using System.Collections;
using Gameplay.Player;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerTests
{





    [Test]
    public void Damage_Taken()
    {
        GameObject playerGO = new();
        var player = playerGO.AddComponent<Player>();

        Damage damage = new()
        {
            Potency = 5,
        };

        player.TakeDamage(damage);

        Assert.AreEqual(45, player.Health);
    }


}