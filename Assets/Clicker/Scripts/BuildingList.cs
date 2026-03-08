using System;
using UnityEngine;

public class BuildingList : MonoBehaviour
{
    public ClickerBuilding[] Buildings;
    public static BuildingList Instance;

	public void RefreshBuildingList()
	{
		var nextBuilding = 0;

		foreach (ClickerBuilding building in Buildings)
		{
			if (building.Level >= 1)
			{
				nextBuilding++;
				building.ChangeState(ClickerBuilding.State.PURCHASED);
			}
			else
			{
				building.ChangeState(ClickerBuilding.State.HIDDEN);
			}
		}
		Buildings[nextBuilding].ChangeState(ClickerBuilding.State.REVEALED);
	}

	private void Start()
	{
		Instance = this;
		
		RefreshBuildingList();
	}
}
