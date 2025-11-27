using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Gameplay.Player;


public class UIUpgradePanel : MonoBehaviour
{


    public Upgrade AssociatedUpgrade;

    [SerializeField] private TMP_Text _upgradeName;
    [SerializeField] private TMP_Text _upgradeDescription;
    [SerializeField] private TMP_Text _upgradeCost;
    [SerializeField] private Button _selectButton;


    private Player _player;


    //------------------------------------------------------------------------------------------------//


    private void Start()
    {
        _player = FindAnyObjectByType<Player>();
    }




    public void InjectData(Upgrade upgrade)
    {
        _upgradeName.text = upgrade.UpgradeName;
        _upgradeDescription.text = upgrade.UpgradeDescription;
        _upgradeCost.text = upgrade.UpgradeCost.ToString();
        _selectButton.interactable = true;


        _selectButton.onClick.RemoveAllListeners();
        _selectButton.onClick.AddListener(() =>
        {
            if (CurrencyBank.TrySpend(upgrade.UpgradeCost))
            {
                upgrade.Apply(_player);
                PurchasedUpgrade();
            }
        });
    }


    private void PurchasedUpgrade()
    {
        _upgradeName.text = "Purchased";
        _upgradeDescription.text = "";
        _upgradeCost.text = "";
        _selectButton.interactable = false;
    }


}

