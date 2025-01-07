
using UnityEngine;

public class S_ConstructTower : S_ClickableObject
{
    [SerializeField] private S_TowerSpawner towerPool;
    [SerializeField] private SO_Towers towerChoice;
    private S_SelectConstructionPoint constructionPointScript;

    private void Start()
    {
        constructionPointScript = transform.parent.parent.GetComponent<S_SelectConstructionPoint>();
    }
    private void GetNewTower()
    {

        S_TowerBase newTower = towerPool.SpawnTower(towerChoice);
        newTower.SetSO(towerChoice);
        ConstructNewTower(newTower);
    }

    private void ConstructNewTower(S_TowerBase newTower)
    {
        newTower.transform.position = constructionPointScript.gameObject.transform.position;
        constructionPointScript.Unselected();
        constructionPointScript.gameObject.SetActive(false);
    }
    public override void Selected()
    {
        GetNewTower();
    }

    public override void Unselected()
    {
        return;
    }
}
