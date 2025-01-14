using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class S_Tooltip : MonoBehaviour
{
    [SerializeField] SO_BasePassiveModifiers moduleCharacteristics;
    List<string> tooltipText=new List<string>();
    


    private void Start()
    {
        string type = $"{moduleCharacteristics.statToChange}";
        if (moduleCharacteristics.statToChange==Stats.DamageMax)
        {
            type = "Damage";
        }
        tooltipText.Add($"{type} module");
        
        GetComponentInChildren<TextMeshProUGUI>().text =
            tooltipText[0];
    }

    public void ShowTooltip()
    {

        gameObject.SetActive(true);
    }
    public void HideTooltip()
    {
         gameObject.SetActive(false);
    }
}
