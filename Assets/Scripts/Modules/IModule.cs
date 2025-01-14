using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IModule<T>
{
    public T modifierType {get; set;}
    public Dictionary<Stats, float> ApplyModifier(Dictionary<Stats, float> statToChange);

}
