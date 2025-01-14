
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class S_ConstructTower : S_ClickableObject
{

    [SerializeField] private SO_Towers towerChoice;
    [SerializeField] private List<Sprite> numbers=new List<Sprite>();
    [SerializeField] private List<SpriteRenderer> numbersRenderer=new List<SpriteRenderer>();
    private S_SelectConstructionPoint constructionPointScript;
    internal S_RessourceManager ressourceManager;
    internal S_TowerSpawner towerPool;

    private void Start()
    {
        constructionPointScript = transform.parent.parent.GetComponent<S_SelectConstructionPoint>();
        SetCostDisplay();
    }

    internal void SetScriptsVariables(S_RessourceManager ressourceManager,S_TowerSpawner towerPool)
    {
        this.ressourceManager = ressourceManager;
        this.towerPool = towerPool;
    }
    private void GetNewTower()
    {

        S_TowerBase newTower = towerPool.SpawnTower(towerChoice);
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
        if (ressourceManager.TryPayEnoughResources(ResourceType.Base, towerChoice.cost[ResourceType.Base]))
        {
            ressourceManager.PayResources(ResourceType.Base, towerChoice.cost[ResourceType.Base]);
            GetNewTower();
        }

    }
    public override void Unselected()
    {
        return;
    }

    public void SetCostDisplay()
    {
        int cost=towerChoice.cost[ResourceType.Base];
        int firstDigit=cost/10;
        int secondDigit=cost%10;
        SpriteRenderer fisrtDigitDisplay = numbersRenderer[0];
        SpriteRenderer secondDigitDisplay = numbersRenderer[1];
        fisrtDigitDisplay.sprite = numbers[firstDigit];
        secondDigitDisplay.sprite = numbers[secondDigit];
    }
}
