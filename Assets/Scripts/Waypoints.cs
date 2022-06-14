
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    [Header("Red")]
    public static Transform[] redPoints;
    public bool red;
    [Header("Yellow")]
    public static Transform[] yellowPoints;
    public bool yellow;
    [Header("Grey")]
    public static Transform[] greyPoints;
    public bool grey;
    [Header("Green")]
    public static Transform[] greenPoints;
    public bool green;

    private void Awake()
    {
        if (red)
        {
            redPoints = new Transform[transform.childCount];

            for (int i = 0; i < redPoints.Length; i++)
            {
                redPoints[i] = transform.GetChild(i);
            }
            return;
        }
        if (yellow)
        {
            yellowPoints = new Transform[transform.childCount];

            for (int i = 0; i < yellowPoints.Length; i++)
            {
                yellowPoints[i] = transform.GetChild(i);
            }
            return;
        }
        if (grey)
        {
            greyPoints = new Transform[transform.childCount];

            for (int i = 0; i < greyPoints.Length; i++)
            {
                greyPoints[i] = transform.GetChild(i);
            }
            return;
        }

        if (green)
        {
            greenPoints = new Transform[transform.childCount];

            for (int i = 0; i < greenPoints.Length; i++)
            {
                greenPoints[i] = transform.GetChild(i);
            }
            return;
        }

    }


}
