using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class S_ModuleSlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI quantityDisplay;
    [SerializeField] TextMeshProUGUI percentageDisplay;
    internal int percentage;
    Stack<S_ModuleBase> modulesStock = new Stack<S_ModuleBase>();
    
    void Start()
    {
        ActualizeDisplay();
    }
    

    internal bool VerifyQuantity()
    {
        if (modulesStock.Count >= 5)
        {
            return false;
        }
        return true;
    }
    
    internal bool VerifySlotCompatibility(int percentageToCompare)
    {
        if (modulesStock.Count <= 0)
        {
            return true;
        }
        if (percentage!=percentageToCompare)
        {
            return false;
        }
        return true;

    }

    public bool TryAddModuleToStock(Stats modifier, int percentageToAdd, int cost)
    {

        if (!VerifyQuantity() || !VerifySlotCompatibility(percentageToAdd))
        {
            return false;
        }
        if (modulesStock.Count <= 0)
        {
            SetSlotCharacteristics(percentageToAdd);
        }
        modulesStock.Push(new S_ModuleBase(modifier, percentageToAdd, cost));
        ActualizeDisplay();
        return true;
    }

    public bool TryAddModuleToStock(S_ModuleBase module)
    {
        if (!VerifyQuantity() || !VerifySlotCompatibility(module.percentage))
        {
            return false;
        }
        if (modulesStock.Count <= 0)
        {
            SetSlotCharacteristics(module.percentage);
        }
        modulesStock.Push(module);
        ActualizeDisplay();
        return true;
    }

    public void RecoverModule(S_ModuleBase module)
    {
        modulesStock.Push(module);
        ActualizeDisplay();
    }

    public bool TryGetModuleFromStock(out S_ModuleBase module)
    {
        module = null;
        if (modulesStock.Count > 0)
        {
            module= modulesStock.Pop();

            if (modulesStock.Count <= 0)
            {
                ResetSlot();
            }
            ActualizeDisplay();
            return true;

        }
        return false;

    }

    private void SetSlotCharacteristics(int slotPercentage)
    {
        percentage=slotPercentage;
        ActualizeDisplay();
    }

    private void ResetSlot()
    {
        percentage=0;
    }
    
    
    internal void ActualizeDisplay()
    {
        quantityDisplay.text =$"X {modulesStock.Count}/5";
        percentageDisplay.text =$"{percentage}%";
    }
    
}
