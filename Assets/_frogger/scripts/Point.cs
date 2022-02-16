using UnityEngine;

public class Point: MonoBehaviour
{
    [SerializeField] private bool isUsed = false;
    [SerializeField] private float distance = 0;

    //private Frog frog;

    public bool IsUsed { get => isUsed;  }
    public float Distance { get => distance; set => distance = value; }

    public void UsePoint(bool value)
    {
        this.isUsed = value;
    }

    //public void ResetValues()
    //{
    //    if (frog == null)
    //        isUsed = false;
    //}
}
