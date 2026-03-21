using System.Collections.Generic;
using UnityEngine;

public class ClickerUpgrade : MonoBehaviour
{
    public string Name;
    public ClickerBuilding Building;
    public int LevelRequirement;
    
    public float Cost;
    public float Impact;

    public bool MeetsRequirements()
    {
        if (Building.Level >= LevelRequirement)
        {
            return true;
        }
        return false;
    }

    public void Purchase()
    {
        if (ResourceManager.Instance.ResourceCount < Cost) return;
        
        ResourceManager.Instance.UpdateResources(-Cost);
        
        Building.ResourceStarting *= Impact;

        var newUpgrades = new List<ClickerUpgrade>();
        foreach (var Upgrade in UpgradeList.Instance.Upgrades)
        {
            if (Upgrade == this) continue;
            newUpgrades.Add(Upgrade);
        }
        
        UpgradeList.Instance.Upgrades = newUpgrades.ToArray();
        
        UpgradeList.Instance.ShownUpgrades.Remove(this);
        UpgradeList.Instance.RefreshUpgradeList();
        
        transform.gameObject.SetActive(false);
        
        Building.RefreshUI();
    }
}
