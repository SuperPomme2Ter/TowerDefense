
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class S_SurchargeInventory : MonoBehaviour,IModuleInteraction
{
    S_ModuleBase[] modules=new S_ModuleBase[2];
    [SerializeField] S_ModuleInventory moduleInventory;
    [SerializeField] S_RessourceManager resourceManager;
    [SerializeField] private GameObject compatibilityWarning;
    [SerializeField] private GameObject spaceWarning;
    [SerializeField] private GameObject rsltDisplay;
    private int mergedModuleCost = 0;
    private int mergedPercentage = 0;

    

    internal void GetModulesFromSlots(int index, S_ModuleBase module,Color moduleColor)
    {
        compatibilityWarning.SetActive(false);
        spaceWarning.SetActive(false);
        modules[index] = module;
        if ((modules[0] != null) && (modules[1] != null))
        {
            mergedModuleCost=(int)MathMethods.CrossProduct(modules[0].cost[ResourceType.Base] + modules[1].cost[ResourceType.Base], 125);
            mergedPercentage=modules[0].percentage + modules[1].percentage + 5;
            rsltDisplay.SetActive(true);
            rsltDisplay.transform.GetChild(0).GetComponent<Image>().color = moduleColor;
            rsltDisplay.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text=$"{mergedPercentage}%";
            rsltDisplay.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text=mergedModuleCost.ToString();
        }
        else
        {
            rsltDisplay.SetActive(false);
        }

    }

    private bool VerifyModulesCompatibility()
    {
        if (modules[0]==null || modules[1]==null || modules[0].modifierType != modules[1].modifierType )
        {
            compatibilityWarning.SetActive(true);
            return false;
        }
        return true;
    }


    private void CreateMergedModule()
    {
        if (!VerifyModulesCompatibility())
        {
            return;
        }
        if (resourceManager.TryPayEnoughResources(ResourceType.Base, mergedModuleCost))
        {
            S_ModuleBase newModule = new S_ModuleBase(modules[0].modifierType,
                mergedPercentage, mergedModuleCost);

            if (!moduleInventory.AddModuleToSlot(newModule))
            {
                spaceWarning.SetActive(true);

            }
            rsltDisplay.SetActive(false);
            modules=new S_ModuleBase[2];
        }
    }

    public void ModuleInteraction(S_ModuleHUDInteraction moduleHUD)
    {
        CreateMergedModule();
    }

    public void DisableSurchargeMenu()
    {
        compatibilityWarning.SetActive(false);
        spaceWarning.SetActive(false);
    }
}
