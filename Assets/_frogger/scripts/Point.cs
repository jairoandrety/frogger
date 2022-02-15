using UnityEngine;

public class Point: MonoBehaviour
{
    [SerializeField] private bool isUsed = false;
    [SerializeField] private float distance = 0;

    public bool IsUsed { get => isUsed;  }
    public float Distance { get => distance; set => distance = value; }

    public void UsePoint(bool value)
    {
        this.isUsed = value;
    }
}
