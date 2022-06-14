using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TurretBluePrint
{
    public TextMeshProUGUI gunPrice;
    public TextMeshProUGUI upgradePriceTxt;
    public TextMeshProUGUI sellPriceTxt;
    public GameObject preFab;
    public int cost;
    public int upgradePrice;
    public int sellPrice;


    public void GunCostInfo()
    {
        gunPrice.text = "$" + cost.ToString();

    }

    
}
