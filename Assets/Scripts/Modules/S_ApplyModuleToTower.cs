using System.Collections.Generic;
using UnityEngine;

public class S_ApplyModuleToTower : MonoBehaviour
{

    [SerializeField] internal S_ModuleHUDInteraction moduleHUD;
    public Dictionary<Stats, float> TryApplyModule(Dictionary<Stats, float> statToChange, S_ModuleBase moduleToApply)
    {
        statToChange=moduleToApply.ApplyModifier(statToChange);
        Debug.Log("Module applied");
        return statToChange;
        
    }
}
