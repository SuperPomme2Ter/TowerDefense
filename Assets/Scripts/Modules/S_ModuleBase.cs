using System;
using System.Collections.Generic;
using UnityEngine;

public class S_ModuleBase : IModule<Stats>
{
    
    public float percentage=0;
    public Dictionary<ResourceType, int> cost = new();
    private Action<S_TowerBase> ApplyStatFunction;
    public Stats modifierType { get; set; }

    internal S_ModuleBase(Stats modifier,float percentageToAdd)
    {
         percentage=percentageToAdd;
         Debug.Log(percentage);
         if (modifier == Stats.DamageMax)
         {
             ApplyStatFunction = ApplyDamageStat;
             modifierType = Stats.DamageMax;
             return;
         }

         ApplyStatFunction = ApplyStat;
         modifierType=modifier;
    }

    public void ApplyDamageStat(S_TowerBase tower)
    {
        tower.stats[Stats.DamageMin]+=MathMethods.CrossProduct(percentage,tower.stats[Stats.DamageMin]);
        tower.stats[Stats.DamageMax]+=MathMethods.CrossProduct(percentage,tower.stats[Stats.DamageMax]);
        Debug.Log(tower.stats[Stats.DamageMax]);
    }

    public void ApplyStat(S_TowerBase tower)
    {
        tower.stats[modifierType] -=MathMethods.CrossProduct(percentage,tower.stats[modifierType]);
        Debug.Log(tower.stats[Stats.atkSpeed]);
    }

    public void ApplyModifier(S_TowerBase tower)
    {
        ApplyStatFunction?.Invoke(tower);
    }
    
}
