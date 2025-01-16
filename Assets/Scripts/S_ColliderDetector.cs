using System;
using UnityEngine;

public class S_ColliderDetector : MonoBehaviour
{
    [SerializeField] S_TowerBase tower;
    private void OnTriggerEnter2D(Collider2D other)
    {
        tower.AddEnemyInRange(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        tower.RemoveEnemyInRange(other);
    }
}
