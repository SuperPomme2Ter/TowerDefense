using System;
using System.Collections.Generic;
using UnityEngine;

public class S_ModuleBase : IModule<Stats>
{
    
    public int percentage=0;
    public Dictionary<ResourceType, int> cost = new();
    private Func<Dictionary<Stats, float>,Dictionary<Stats, float>> ApplyStatFunction;
    public Stats modifierType { get; set; }

    internal S_ModuleBase(Stats modifier,int percentageToAdd, int moduleCost)
    {
        cost.Add(ResourceType.Base,0);
        cost.Add(ResourceType.Special,0); 
        cost[ResourceType.Base]=moduleCost;
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

    private Dictionary<Stats, float> ApplyDamageStat(Dictionary<Stats, float> stats)
    {
        stats[Stats.DamageMin]+=MathMethods.CrossProduct(percentage,stats[Stats.DamageMin]);
        stats[Stats.DamageMax]+=MathMethods.CrossProduct(percentage,stats[Stats.DamageMax]);
        Debug.Log(stats[Stats.DamageMax]);
        return stats;
    }

    private Dictionary<Stats, float> ApplyStat(Dictionary<Stats, float> stats)
    {
        stats[modifierType] -=MathMethods.CrossProduct(percentage,stats[modifierType]);
        Debug.Log(stats[modifierType]);
        return stats;
    }

    public Dictionary<Stats, float> ApplyModifier(Dictionary<Stats, float> statToChange)
    {
        return ApplyStatFunction?.Invoke(statToChange);
    }
    
}
