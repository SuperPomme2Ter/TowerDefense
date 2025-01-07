using System.Linq;
using TMPro;
using UnityEngine;

public class S_Tooltip : MonoBehaviour
{
    [SerializeField] S_ModuleBase module;


    private void Start()
    {
        //GetComponentInChildren<TextMeshProUGUI>().text = "Cost : " +module.bonus.First().Value+" "+module.bonus.First().Key + "resources.";
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
