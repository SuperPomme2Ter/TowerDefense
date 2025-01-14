using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class S_ModuleInventory : MonoBehaviour
{
    [SerializeField] private S_RessourceManager resourceManager;
    
    [SerializeField] TextMeshProUGUI totalAtkSpeedModuleQuantityDisplay;
    [SerializeField] private TextMeshProUGUI totalAtkModuleQuantityDisplay;
    [SerializeField] private List<S_ModuleSlot> atkSpeedModulesSlot = new List<S_ModuleSlot>();
    [SerializeField] private List<S_ModuleSlot> atkModulesSlot = new List<S_ModuleSlot>();
    Dictionary<Stats,List<S_ModuleSlot>> allSlots = new Dictionary<Stats, List<S_ModuleSlot>>();
    Dictionary<Stats,int> modulesQuantity = new Dictionary<Stats,int>();
    

    private void Start()
    {
        gameObject.SetActive(false);
        allSlots.Add(Stats.atkSpeed,atkSpeedModulesSlot);
        allSlots.Add(Stats.DamageMax,atkModulesSlot);
        
        modulesQuantity.Add(Stats.atkSpeed,0);
        modulesQuantity.Add(Stats.DamageMax,0);
        
        gameObject.SetActive(false);
    }
    private void UpdateModulesDisplay()
    {
        
        totalAtkSpeedModuleQuantityDisplay.text = $"X {modulesQuantity[Stats.atkSpeed]}";
        totalAtkModuleQuantityDisplay.text = $"X {modulesQuantity[Stats.DamageMax]}";
    }

    //Fonction provisoire
    public bool AddModuleToSlot(Stats statToChange,int percentage,int cost)
    {
        foreach (var slots in allSlots[statToChange])
        {
            if (slots.TryAddModuleToStock(statToChange, percentage, cost))
            {
                UpdateModulesDisplay();
                return true;
            }
        }
        return false;

    }

    public bool AddModuleToSlot(S_ModuleBase module)
    {
        foreach (var slots in allSlots[module.modifierType])
        {
            if (slots.TryAddModuleToStock(module))
            {
                UpdateModulesDisplay();
                return true;
            }
        }
        return false;
    }
    public void TryBuyModule(SO_BasePassiveModifiers module)
    {
        if (resourceManager.TryPayEnoughResources(ResourceType.Base, module.cost))
        {
            if(AddModuleToSlot(module.statToChange,module.percentage,module.cost))
            {
                resourceManager.PayResources(ResourceType.Base, module.cost);
                return;
            }
        }
        return;
    }
    
}
