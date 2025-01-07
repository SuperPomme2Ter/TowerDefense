using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Towers_Data", menuName = "ScriptableObjects/Towers_Data", order = 0)]
public class SO_Towers : ScriptableObject
{
    [Header("value corresponding : \n[0]==DamageMin\n[1]==DamageMax\n[2]==Range\n[3]==atkSpeed")]
    public float[] statValue= new float[4];
    public Dictionary<Stats, float> stats = new();

    [Header("value corresponding : \n[0]==Base\n[1]==Special")]
    public int[] costValue = new int[2];
    public Dictionary<ResourceType, float> cost = new();

    [Header("Corresponding to tower projectile behaviour")]
    public bool Hitscan;
    public bool inflictStatus;
    public bool arching;
    public int archingPower;
    public float AOERange;
    public float velocity;
    

    [Header("Sprite")]
    public Sprite baseSprite;

    public GameObject canonCharacteristics;
    public Sprite projectileSprite;



#if UNITY_EDITOR
    private void OnValidate()
    {
        stats.Clear();
        stats.Add(Stats.DamageMin, statValue[0]);
        stats.Add(Stats.DamageMax, statValue[1]);
        stats.Add(Stats.Range, statValue[2]);
        stats.Add(Stats.atkSpeed, statValue[3]);

        cost.Clear();
        cost.Add(ResourceType.Base,costValue[0]);
        cost.Add(ResourceType.Special, costValue[1]);

    }
#endif


}