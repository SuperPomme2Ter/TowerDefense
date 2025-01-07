using UnityEngine;

public class S_MainModulePart : MonoBehaviour
{
    [SerializeField] private S_RessourceManager resourceManager;
    [SerializeField] S_ModuleInventory inventory;
    
    public void TryBuyModule(SO_BasePassiveModifiers module)
    {
        if (resourceManager.TryPayEnoughResources(ResourceType.Base, module.cost))
        {
            
        }
    }
}
