using System.Collections.Generic;
using Gameplay.Player;



public abstract class Upgrade
{
    public virtual string UpgradeName { get; protected set; }
    public virtual float UpgradeModifier { get; protected set; }
    public virtual int UpgradeTier { get; protected set; }
    public virtual string UpgradeDescription { get; protected set; }
    public virtual int UpgradeCost { get; protected set; }


    protected readonly List<float> RarityByTier = new() { 50f, 30f, 15f, 5f };
    protected readonly List<int> CostByTier = new() { 3, 5, 8, 12 };

    public abstract void GenerateUpgrade(int zeroBasedTier);

    public abstract void Apply(Player player);


    protected int GenerateZeroBasedTier()
    {
        var randomRoll = UnityEngine.Random.Range(0, 100);
        float cumulativeRarity = 0f;

        for (int i = 0; i < RarityByTier.Count - 1; i++)
        {
            cumulativeRarity += RarityByTier[i];
            if (randomRoll <= cumulativeRarity) return i;
        }

        return RarityByTier.Count - 1;
    }
}



public class DamageUpgrade : Upgrade
{

    private readonly List<float> _modifierByTier = new() { 1.03f, 1.085f, 1.16f, 1.30f };


    public DamageUpgrade()
    {
        int tier = GenerateZeroBasedTier();
        GenerateUpgrade(tier);
    }


    public override void GenerateUpgrade(int zeroBasedTier)
    {
        UpgradeTier = zeroBasedTier + 1;
        UpgradeModifier = _modifierByTier[zeroBasedTier];
        UpgradeCost = CostByTier[zeroBasedTier];


        UpgradeName = $"Damage Dealer {UpgradeTier}";
        float percentIncrease = (UpgradeModifier - 1) * 100f;
        UpgradeDescription = $"Increases damage output by {percentIncrease:0.##}%";
    }

    public override void Apply(Player player) => player.ModifyDamage(UpgradeModifier);
}



public class HealthUpgrade : Upgrade
{

    private readonly List<float> _modifierByTier = new() { 5f, 10f, 20f, 40f };


    public HealthUpgrade()
    {
        int tier = GenerateZeroBasedTier();
        GenerateUpgrade(tier);
    }


    public override void GenerateUpgrade(int zeroBasedTier)
    {
        UpgradeTier = zeroBasedTier + 1;
        UpgradeModifier = _modifierByTier[zeroBasedTier];
        UpgradeCost = CostByTier[zeroBasedTier];

        UpgradeName = $"Health Boost {UpgradeTier}";
        UpgradeDescription = $"Increases maximum health by {UpgradeModifier:0}";
    }

    public override void Apply(Player player) => player.ModifyHealth(UpgradeModifier);
}



public class AttackSpeedUpgrade : Upgrade
{

    private readonly List<float> _modifierByTier = new() { 0.97f, 0.95f, 0.90f, 0.82f };


    public AttackSpeedUpgrade()
    {
        int tier = GenerateZeroBasedTier();
        GenerateUpgrade(tier);
    }


    public override void GenerateUpgrade(int zeroBasedTier)
    {
        UpgradeTier = zeroBasedTier + 1;
        UpgradeModifier = _modifierByTier[zeroBasedTier];
        UpgradeCost = CostByTier[zeroBasedTier];

        UpgradeName = $"Attack Speed {UpgradeTier}";
        float percentIncrease = (1 - UpgradeModifier) * 100f;
        UpgradeDescription = $"Increases attack speed by {percentIncrease:0.##}%";
    }

    public override void Apply(Player player) => player.ModifyAttackSpeed(UpgradeModifier);
}



public class AttackConeUpgrade : Upgrade
{

    private readonly List<float> _modifierByTier = new() { 1f, 3f, 7f, 12f };


    public AttackConeUpgrade()
    {
        int tier = GenerateZeroBasedTier();
        GenerateUpgrade(tier);
    }


    public override void GenerateUpgrade(int zeroBasedTier)
    {
        UpgradeTier = zeroBasedTier + 1;
        UpgradeModifier = _modifierByTier[zeroBasedTier];
        UpgradeCost = CostByTier[zeroBasedTier];

        UpgradeName = $"Attack Cone {UpgradeTier}";
        UpgradeDescription = $"Increases attack cone by {UpgradeModifier:0} degrees";
    }

    public override void Apply(Player player) => player.ModifyAttackConeRadius(UpgradeModifier);
}


public class AttackDistanceUpgrade : Upgrade
{

    private readonly List<float> _modifierByTier = new() { 1.05f, 1.25f, 1.75f, 2f };


    public AttackDistanceUpgrade()
    {
        int tier = GenerateZeroBasedTier();
        GenerateUpgrade(tier);
    }


    public override void GenerateUpgrade(int zeroBasedTier)
    {
        UpgradeTier = zeroBasedTier + 1;
        UpgradeModifier = _modifierByTier[zeroBasedTier];
        UpgradeCost = CostByTier[zeroBasedTier];

        UpgradeName = $"Attack Distance {UpgradeTier}";
        float percentIncrease = (UpgradeModifier - 1) * 100f;
        UpgradeDescription = $"Increases attack distance by {percentIncrease:0.##}%";
    }

    public override void Apply(Player player) => player.ModifyAttackDistance(UpgradeModifier);

}


public class KnockbackUpgrade : Upgrade
{
    private readonly List<float> _modifierByTier = new() { 1.2f, 1.3f, 1.45f, 1.6f };


    public KnockbackUpgrade()
    {
        int tier = GenerateZeroBasedTier();
        GenerateUpgrade(tier);
    }

    public override void GenerateUpgrade(int zeroBasedTier)
    {
        UpgradeTier = zeroBasedTier + 1;
        UpgradeModifier = _modifierByTier[zeroBasedTier];
        UpgradeCost = CostByTier[zeroBasedTier];

        UpgradeName = $"Knockback Force {UpgradeTier}";
        float percentIncrease = (UpgradeModifier - 1) * 100f;
        UpgradeDescription = $"Increases knockback force by {percentIncrease:0.##}%";
    }

    public override void Apply(Player player) => player.ModifyKnockback(UpgradeModifier);
}