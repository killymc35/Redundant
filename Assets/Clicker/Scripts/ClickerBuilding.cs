using System;
using TMPro;
using UnityEngine;

public abstract class ClickerBuilding : MonoBehaviour
{
    public string Name;
    public int Level = 0;

    public const float CostMultiplier = 1.15f;
    
    [Header("UI")]
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI CostText;
    public TextMeshProUGUI ResourceText;
    public TextMeshProUGUI LevelText;
    
    public enum State
    {
        HIDDEN,
        REVEALED,
        PURCHASED
    }
    public State CurrentState =  State.HIDDEN;
    
    [Header("Cost")]
    public float CostStarting;
    private float CostCurrent;
    
    [Header("Resources")]
    public float ResourceStarting;
    private float ResourceCurrent = 0;
    
    private int updateCount = 0;

    private void Awake()
    {
        CostCurrent = CostStarting;
        
        RefreshUI();
    }

    public void Purchase()
    {
        if (ResourceManager.Instance.ResourceCount < CostCurrent) return;
        
        ResourceManager.Instance.UpdateResources(-CostCurrent);
        Level++;
        
        CostCurrent = CostStarting * Mathf.Pow(CostMultiplier, Level);
        ResourceCurrent += ResourceStarting;
        
        RefreshUI();

        if (Level == 1)
        {
            BuildingList.Instance.RefreshBuildingList();
        }
    }

    private void RefreshUI()
    {
        NameText.text = Name;
        CostText.text = CostCurrent.ToString("F1");
        ResourceText.text = ResourceStarting.ToString("F1");
        LevelText.text = Level.ToString();
    }

    public void ChangeState(State newState)
    {
        CurrentState = newState;

        switch (CurrentState)
        {
            case State.HIDDEN:
                gameObject.SetActive(false);
                break;
            case State.REVEALED:
                gameObject.SetActive(true);
                break;
            case State.PURCHASED:
                gameObject.SetActive(true);
                break;
        }
    }
    
    private void FixedUpdate()
    {
        updateCount++;
        if (updateCount >= 50)
        {
            updateCount = 0;
            return;
        }
        if (updateCount > 1)
        {
            return;
        }
        
        ResourceManager.Instance.UpdateResources(ResourceCurrent);
    }
}
