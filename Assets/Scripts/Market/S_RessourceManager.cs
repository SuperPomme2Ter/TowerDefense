using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class S_RessourceManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI basicResourceDisplay;
    [SerializeField] TextMeshProUGUI SpecialResourceDisplay;

    // [HideInInspector]
    // public int basicResourceCount=0;
    // public int specialRessourceCount=0;

    [SerializeField] private int basicBeginningResources=0;
    public Dictionary<ResourceType, int> resourcesQuantity = new();
    
    private void Start()
    {
        resourcesQuantity.Add(ResourceType.Base,basicBeginningResources);
        resourcesQuantity.Add(ResourceType.Special,0);
        UpdateResourceDisplay();
    }

    public void UpdateResourceDisplay()
    {
        basicResourceDisplay.text = resourcesQuantity[ResourceType.Base].ToString();
        SpecialResourceDisplay.text = resourcesQuantity[ResourceType.Special].ToString();
    }

    public bool TryPayEnoughResources(ResourceType resource, int quantity)
    {
        if (resourcesQuantity[resource] < quantity)
        {
            return false;
        }

        return true;
    }

    public void PayResources(ResourceType resource, int quantity)
    {
        resourcesQuantity[resource] -= quantity;
        UpdateResourceDisplay();
    }

}
