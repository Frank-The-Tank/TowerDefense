using UnityEngine;

public class Waypoints : MonoBehaviour {

    public static Transform[] wayPoints;

    private void Awake()
    {
        wayPoints = new Transform[transform.childCount];
        for (int i = 0; i < wayPoints.Length; i++)
        {
           wayPoints[i] = transform.GetChild(i);
        }
    }

}
