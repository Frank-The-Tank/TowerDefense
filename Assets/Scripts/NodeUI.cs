using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour {

    public GameObject ui;
    public GameObject upgradeSelector;

    public Text upgradeCost;
    public Button upgradeButton;

    public Text sellAmount;

    private Node target; 

    public void SetTarget (Node _target)
    {
        target = _target;

        transform.position = target.GetBuildPosition();

        if (!target.isUpgraded)
        {
            // 0 is a placeholder for now
            upgradeCost.text = "$" + target.turretBlueprint.upgradeCosts[0];
            upgradeButton.interactable = true;
        } else
        {
            upgradeCost.text = "Max Level";
            upgradeButton.interactable = false;
        }

        sellAmount.text = "$" + target.turretBlueprint.GetSellAmount();

        ui.SetActive(true);
    }

    public void Hide ()
    {
        ui.SetActive(false);
        upgradeSelector.SetActive(false);
    }

    public void Upgrade ()
    {
        upgradeSelector.SetActive(true);
    }

    public void upgradeSelected (int upgradeIndex)
    {
        target.UpgradeTurret(upgradeIndex);
        BuildManager.instance.DeselectNode();
    }

    public void Sell ()
    {
        target.SellTurret();
        BuildManager.instance.DeselectNode();
    }

}
