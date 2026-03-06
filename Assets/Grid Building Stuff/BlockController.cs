using UnityEngine;
using UnityEngine.UI;

public class BlockController : MonoBehaviour
{
    public Color validPlacementColour;
    public Color invalidPlacementColour;

    public bool hasValidPlacement;

    void Awake()
    {
        hasValidPlacement = true;
    }

    public void SetValidity(bool validity)
    {
        if (validity == true) hasValidPlacement = true;
        else hasValidPlacement = false;
        UpdateColour();
    }

    public void UpdateColour()
    {
        if (hasValidPlacement)
        {
            GetComponent<Image>().color = validPlacementColour;
        }
        else
        {
            GetComponent<Image>().color = invalidPlacementColour;
        }
    }
}
