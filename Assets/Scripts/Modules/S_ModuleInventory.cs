using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class S_ModuleInventory : MonoBehaviour
{
    [SerializeField] private S_RessourceManager resourceManager;
    
    private TextMeshProUGUI atkSpeedModuleQuantityDisplay;
    private TextMeshProUGUI atkModuleQuantityDisplay;

    Dictionary<Stats,List<S_ModuleBase>> AllModules = new Dictionary<Stats,List<S_ModuleBase>>();
    
    public S_ModuleBase selectedModule;
    [SerializeField] private bool aaah=false;
    
    
    


    private void Start()
    {
        AllModules.Add(Stats.atkSpeed,new List<S_ModuleBase>());
        AllModules.Add(Stats.DamageMax,new List<S_ModuleBase>());
        atkSpeedModuleQuantityDisplay=transform.GetChild(0).GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        atkModuleQuantityDisplay=transform.GetChild(1).GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        
    }

    private void UpdateModulesDisplay()
    {
        atkSpeedModuleQuantityDisplay.text = $"X {AllModules[Stats.atkSpeed].Count}";
        atkModuleQuantityDisplay.text = $"X {AllModules[Stats.DamageMax].Count}";
    }

    //Fonction provisoire
    private void AddModule(SO_BasePassiveModifiers module)
    {
        AllModules[module.statToChange].Add(new S_ModuleBase(module.statToChange, module.percentage));
        UpdateModulesDisplay();
    }
    public void TryBuyModule(SO_BasePassiveModifiers module)
    {
        if (resourceManager.TryPayEnoughResources(ResourceType.Base, module.cost))
        {
            AddModule(module);
        }
    }

    public void ChooseModule(bool test)
    {
        if (selectedModule != null)
        {
            selectedModule = null;
            aaah=false;
            Debug.Log("zqefqe");
            return;
            
        }
        
        Debug.Log("bbbbbbbbbbbbbbbb");
        aaah=true;
        if (test)
        {
            Debug.Log(AllModules[Stats.atkSpeed]); 
            selectedModule=AllModules[Stats.atkSpeed][0];
            return;
        }
        selectedModule=AllModules[Stats.DamageMax][0];
    }

    public void RemoveModule()
    {
        AllModules[selectedModule.modifierType].Remove(selectedModule);
        selectedModule=null;
        UpdateModulesDisplay();
    }
}
