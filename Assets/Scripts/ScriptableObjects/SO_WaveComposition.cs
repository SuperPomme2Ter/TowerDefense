using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave_Data", menuName = "ScriptableObjects/Wave_Data", order = 3)]
public class SO_WaveComposition : ScriptableObject
{
    public int enemyQuantity = 0;
    public float spawnRate = 1;
    public List<SO_Ennemy> allEnemiesType = new List<SO_Ennemy>();
    
}