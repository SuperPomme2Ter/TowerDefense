using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class S_SurchargeButton : MonoBehaviour, IModuleInteraction
{
    [SerializeField] private Color atkModuleColor;
    [SerializeField] private Color atkSpeedModuleColor;
    [SerializeField] private GameObject slottedModuleDisplay;
    [SerializeField] int slotIndex;
    [SerializeField] S_SurchargeInventory inventory;
    
    private S_ModuleBase SlottedModule;
    private S_ModuleSlot slotOfSlottedModule;
    private Dictionary<Stats, Color> ModuleColors = new Dictionary<Stats, Color>();
    void Start()
    {
        ModuleColors.Add(Stats.DamageMax,atkModuleColor);
        ModuleColors.Add(Stats.atkSpeed,atkSpeedModuleColor);
        gameObject.SetActive(false);
    }

    public void ModuleInteraction(S_ModuleHUDInteraction moduleHUD)
    {
        if (SlottedModule != null)
        {
            slotOfSlottedModule.RecoverModule(SlottedModule);
            SlottedModule = null;
            inventory.GetModulesFromSlots(slotIndex,SlottedModule,ModuleColors[Stats.atkSpeed]);
            slottedModuleDisplay.SetActive(false);
            return;
        }

        if (moduleHUD.moduleSlot != null)
        {
            slotOfSlottedModule = moduleHUD.moduleSlot;
            if (moduleHUD.GiveModule(out SlottedModule))
            {
                inventory.GetModulesFromSlots(slotIndex, SlottedModule, ModuleColors[SlottedModule.modifierType]);
                SetSlottedModuleDisplay();
            }
        }
    }

    private void SetSlottedModuleDisplay()
    {
        slottedModuleDisplay.SetActive(true);
        Image moduleDisplay = slottedModuleDisplay.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>();
        TextMeshProUGUI percentageDisplay = slottedModuleDisplay.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        moduleDisplay.color = ModuleColors[SlottedModule.modifierType];
        percentageDisplay.text = $"{SlottedModule.percentage}%";
    }

    public void DiscardModule()
    {
        SlottedModule = null;
        slottedModuleDisplay.SetActive(false);
    }
}
