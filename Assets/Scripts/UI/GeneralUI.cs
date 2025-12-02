using TMPro;
using UnityEngine;


public class GeneralUI : MonoBehaviour
{


    [SerializeField] private TMP_Text _currencyText;





    //------------------------------------------------------------------------------------------------//



    public void OnEnable()
    {
        CurrencyBank.OnCurrencyChanged += UpdateCurrencyDisplay;
    }


    public void OnDisable()
    {
        CurrencyBank.OnCurrencyChanged -= UpdateCurrencyDisplay;
    }



    //------------------------------------------------------------------------------------------------//



    public void UpdateCurrencyDisplay(int newAmount)
    {
        _currencyText.text = $"Coins: {newAmount}";
    }



}

