using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Ennemy_Data", menuName = "ScriptableObjects/Ennemy_Data", order = 1)]
public class SO_Ennemy : ScriptableObject
{
    [Header("Base Value")]
    public int pv;
    public int ressourceGiven;
    public float Speed;

    [Header("Sprite")]
    public Color Variant;


}