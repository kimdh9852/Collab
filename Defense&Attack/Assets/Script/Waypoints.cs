using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    //point를 저장할 배열
    public static Transform[] points;

    //point 저장 (자식에다가 다음 포인트들을 다 집어넣음)
    private void Awake()
    {
        points = new Transform[transform.childCount];

        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i);
        }

    }
}
