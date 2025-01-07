using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class S_RessourceManager : MonoBehaviour
{
    private TextMeshProUGUI basicResourceDisplay;
    private TextMeshProUGUI SpecialResourceDisplay;

    // [HideInInspector]
    // public int basicResourceCount=0;
    // public int specialRessourceCount=0;
    

    public Dictionary<ResourceType, int> resourcesQuantity = new();
    private void Start()
    {
        basicResourceDisplay=transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        SpecialResourceDisplay=transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        resourcesQuantity.Add(ResourceType.Base,0);
        resourcesQuantity.Add(ResourceType.Special,0);
        UpdateResourceDisplay();
    }

    public void UpdateResourceDisplay()
    {
        basicResourceDisplay.text = $"Basic\n{resourcesQuantity[ResourceType.Base]}";
        SpecialResourceDisplay.text = $"Special\n{resourcesQuantity[ResourceType.Special]}";
    }

    public bool TryPayEnoughResources(ResourceType resource, int quantity)
    {
        if (resourcesQuantity[resource] < quantity)
        {
            return false;
        }
        resourcesQuantity[resource] -= quantity;
        UpdateResourceDisplay();
        return true;
    }

}
