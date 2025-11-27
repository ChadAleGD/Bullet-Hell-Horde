using System.Collections.Generic;
using UnityEngine;


public class ShopUI : MonoBehaviour
{

    [SerializeField] private List<Upgrade> _availableUpgrades = new(3);
    [SerializeField] private List<UIUpgradePanel> _upgradePanels;

    private static readonly int _totalUpgradeTypes = 6;

    [SerializeField] private GameObject _mainPanel;



    //------------------------------------------------------------------------------------------------//



    private void OnEnable()
    {
        EventBus.Subscribe(EventType.OnUpgradePanelOpen, ShowPanel);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(EventType.OnUpgradePanelOpen, ShowPanel);
    }



    //------------------------------------------------------------------------------------------------//



    private void ShowPanel()
    {
        if (_mainPanel.activeSelf) return;

        _mainPanel.SetActive(true);


        if (_availableUpgrades.Count != 0) return;
        GenerateUpgrades();
        PopulateTiles();
    }


    public void HidePanel()
    {
        _mainPanel.SetActive(false);

        EventBus.Publish(EventType.OnUpgradePanelClose);
    }


    public void RefreshUpgrades()
    {
        if (!CurrencyBank.TrySpend(1)) return;

        _availableUpgrades.Clear();
        GenerateUpgrades();
        PopulateTiles();
    }



    private void GenerateUpgrades()
    {

        for (int i = 0; i < 3; i++)
        {
            var rand = Random.Range(0, _totalUpgradeTypes);

            switch (rand)
            {
                case 0:
                    var healthUpgrade = new HealthUpgrade();
                    _availableUpgrades.Add(healthUpgrade);
                    break;
                case 1:
                    var damageUpgrade = new DamageUpgrade();
                    _availableUpgrades.Add(damageUpgrade);
                    break;
                case 2:
                    var attackSpeedUpgrade = new AttackSpeedUpgrade();
                    _availableUpgrades.Add(attackSpeedUpgrade);
                    break;
                case 3:
                    var attackRadiusUpgrade = new AttackConeUpgrade();
                    _availableUpgrades.Add(attackRadiusUpgrade);
                    break;
                case 4:
                    var attackDistanceUpgrade = new AttackDistanceUpgrade();
                    _availableUpgrades.Add(attackDistanceUpgrade);
                    break;
                case 5:
                    var knockbackUpgrade = new KnockbackUpgrade();
                    _availableUpgrades.Add(knockbackUpgrade);
                    break;
            }
        }
    }


    private void PopulateTiles()
    {
        var i = 0;

        foreach (var panel in _upgradePanels)
        {
            panel.InjectData(_availableUpgrades[i]);
            i++;
        }
    }


}

