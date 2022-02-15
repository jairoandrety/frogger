using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trunk : MonoBehaviour
{
    [SerializeField] private bool isUsed = false;
    [SerializeField] private int size = 0;
    [SerializeField] private BoxCollider collider;
    [SerializeField] private GameObject mesh;

    [SerializeField] private SimpleMove move;
    [SerializeField] private List<Point> basePoints = new List<Point>();
    [SerializeField] private List<Point> points = new List<Point>();

    public List<Point> Points { get => points; set => points = value; }
    public bool IsUsed { get => isUsed; }
    public int Size { get => size; }

    public void StartMove()
    {
        move.StartMove();
    }

    public void SetupTrunk(int size, Vector3 direction, int speed)
    {
        isUsed = true;
        this.size = size;
        move.SetSpeed(direction, speed);
        collider.size = new Vector3(size, 1, 1.2f);
        mesh.transform.localScale = new Vector3(size - 0.2f, 1, 1.2f);

        basePoints.ForEach(i => i.gameObject.SetActive(false));
        points.Clear();
        points.AddRange(basePoints.GetRange(0, size /2));
        points.ForEach(i => i.gameObject.SetActive(true));
    }

    public void Enable(bool value)
    {
        collider.isTrigger = !value;
        mesh.gameObject.SetActive(value);
    }
}