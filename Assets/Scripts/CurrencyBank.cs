using System;
using UnityEngine;


public static class CurrencyBank
{


    private static int _coinCount = 0;


    public static event Action<int> OnCurrencyChanged;



    //------------------------------------------------------------------------------------------------//




    public static void AddCoins(int amount)
    {
        _coinCount += amount;
        OnCurrencyChanged?.Invoke(_coinCount);
    }


    public static bool TrySpend(int amount)
    {
        if (_coinCount < amount) return false;

        _coinCount -= amount;
        OnCurrencyChanged?.Invoke(_coinCount);
        return true;
    }



}

