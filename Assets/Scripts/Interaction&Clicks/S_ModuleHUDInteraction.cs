using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_ModuleHUDInteraction : MonoBehaviour
{
    private S_ModuleBase selectedModule=null;
    public S_ModuleSlot moduleSlot;
    

    public void SelectModule(S_ModuleSlot selectedModuleSlot)
    {
        if (selectedModule != null)
        {
            UnselectModule();
        }

        moduleSlot = selectedModuleSlot;
        moduleSlot.TryGetModuleFromStock(out selectedModule);
        return;
    }

    public bool GiveModule(out S_ModuleBase moduleToGive)
    {
        moduleToGive = null;
        if (selectedModule == null)
        {
            return false;
        }
        moduleToGive = selectedModule;
        moduleSlot = null;
        selectedModule = null;
        return true;
        
    }

    private void UnselectModule()
    {
        if (selectedModule == null)
        {
            return;
        }
        moduleSlot.RecoverModule(selectedModule);
        moduleSlot = null;
        selectedModule = null;
    }
    public void DisplayToolTip(GameObject tooltip)
    {
        tooltip.SetActive(true);
    }

    public void HideToolTip(GameObject tooltip)
    {
        tooltip.SetActive(false);
    }
    
    public void GameObjectActivation(GameObject objectToActivate)
    {
        objectToActivate.SetActive(!objectToActivate.activeSelf);

    }
}
