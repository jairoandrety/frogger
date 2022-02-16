using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private bool isReached = false;

    //public GameObject pointsContanier;
    public List<Point> points = new List<Point>();
    private Point selected;

    public bool IsReached { get => isReached; }

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

        selected = points[0];
        return selected;
    }

    public void SetReached(bool value)
    {
        isReached = value;
    }

    public void ResetValues()
    {
        isReached = false;
        points.ForEach(i => i.UsePoint(false));
    }
}