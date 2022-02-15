using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    //public GameObject pointsContanier;
    public List<Point> points = new List<Point>();

    private Point Selected;

    //[ContextMenu("Find")]
    //public void Find()
    //{
    //    Selected = FindPoint(comparator.transform.position);
    //}

    public Point FindPoint(Vector3 pos)
    {
        points.ForEach(i =>
        {
            i.Distance = Vector3.Distance(i.transform.position, pos);
        });

        points.Sort((x, y) => x.Distance.CompareTo(y.Distance));

        return points[0].IsUsed ? null : points[0].Distance < 2 ? points[0] : null;
    }
}