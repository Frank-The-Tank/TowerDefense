using System.Collections;
using UnityEngine;

[System.Serializable]
public class TurretBlueprint {

    public GameObject prefab;
    public int cost;

    public GameObject[] upgradedPrefabs;
    public int[] upgradeCosts;

    public int GetSellAmount ()
    {
        return cost / 2;
    }
}

