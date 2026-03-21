using System;
using TMPro;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;
    
    public float ResourceCount = 0;
    public int ClickValue = 1;
    
    public TextMeshProUGUI ResourceCountText;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateResources(float value)
    {
        ResourceCount += value;
        ResourceCountText.text = ResourceCount.ToString("F1");
    }
    
    public void RegisterClick()
    {
        UpdateResources(ClickValue);
    }
}
