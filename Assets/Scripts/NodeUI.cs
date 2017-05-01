using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour {

    public GameObject ui;
    public GameObject upgradeSelector;

    public Text upgradeCost;
    public Button upgradeButton;

    public Text sellAmount;

    [Header("Description Hover Fields")]
    public GameObject redDescription;
    public GameObject greenDescription;
    public GameObject yellowDescription;
    public GameObject orangeDescription;
    public GameObject blueDescription;
    public GameObject purpleDescription;

    private Node target; 

    public void SetTarget (Node _target)
    {
        target = _target;

        transform.position = target.GetBuildPosition();

        if (!target.isUpgraded)
        {
            // 0 is a placeholder for now
            upgradeCost.text = "$" + target.turretBlueprint.upgradeCostsTier1[0];
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


    // START: Description Hover Fields
    
    public void showRedDescription()
    {
        redDescription.SetActive(true);
    }

    public void showGreenDescription()
    {
        greenDescription.SetActive(true);
    }

    public void showYellowDescription()
    {
        yellowDescription.SetActive(true);
    }

    public void showOrangeDescription()
    {
        orangeDescription.SetActive(true);
    }

    public void showBlueDescription()
    {
        blueDescription.SetActive(true);
    }

    public void showPurpleDescription()
    {
        purpleDescription.SetActive(true);
    }

    public void hideDescriptions()
    {
        redDescription.SetActive(false);
        greenDescription.SetActive(false);
        yellowDescription.SetActive(false);
        orangeDescription.SetActive(false);
        blueDescription.SetActive(false);
        purpleDescription.SetActive(false);
    }

    // END

}
