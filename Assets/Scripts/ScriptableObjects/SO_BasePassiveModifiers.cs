using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PassiveModules_Data", menuName = "ScriptableObjects/PassiveModules_Data", order = 2)]
public class SO_BasePassiveModifiers : ScriptableObject
{
    public Stats statToChange;

    public int percentage;
    
    public int cost;
    public ResourceType resourcesType;

}
