using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeList : MonoBehaviour
{
    public static UpgradeList Instance;
    
    public ClickerUpgrade[] Upgrades;
    public List<ClickerUpgrade> ShownUpgrades;

    private void Start()
    {
        Instance = this;
        
        RefreshUpgradeList();
    }

    public void RefreshUpgradeList()
    {
        foreach (var Upgrade in Upgrades)
        {
            if (ShownUpgrades.Contains(Upgrade)) continue;
            
            if (Upgrade.MeetsRequirements())
            {
                ShownUpgrades.Add(Upgrade);
                Upgrade.gameObject.SetActive(true);
            }
            else
            {
                Upgrade.gameObject.SetActive(false);
            }
        }
    }
}
