using UnityEngine;

public class S_ConstructionPointParent : MonoBehaviour
{
    [SerializeField] S_RessourceManager ressourceManager;
    [SerializeField] S_TowerSpawner towerSpawner;

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            foreach (BoxCollider2D towerButtons in transform.GetChild(i).GetComponent<S_SelectConstructionPoint>().choiceButtons)
            {
                towerButtons.GetComponent<S_ConstructTower>().SetScriptsVariables(ressourceManager,towerSpawner);
            }
        }

    }
}
