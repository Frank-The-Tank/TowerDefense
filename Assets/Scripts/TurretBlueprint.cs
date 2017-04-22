using System.Collections;
using UnityEngine;

[System.Serializable]
public class TurretBlueprint {

    public GameObject prefab;
    public int cost;

    public GameObject[] upgradedPrefabsTier1;
    public int[] upgradeCostsTier1;

    public int GetSellAmount ()
    {
        return cost / 2;
    }
}

