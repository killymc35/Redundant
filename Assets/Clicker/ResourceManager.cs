using TMPro;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static int ResourceCount = 0;
    public static int ClickValue = 1;
    public TextMeshProUGUI ResourceCountText;
    
    public void UpdateResources(int value)
    {
        ResourceCount += value;
        ResourceCountText.text = ResourceCount.ToString();
    }
    
    public void RegisterClick()
    {
        UpdateResources(ClickValue);
    }
}
