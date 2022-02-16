using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private bool isUsed = false;
    [SerializeField] private int size = 0;
    [SerializeField] private BoxCollider collider;
    [SerializeField] private GameObject mesh;

    [SerializeField] private SimpleMove move;

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
        //collider.size = new Vector3(size, 1, 1.2f);
        //mesh.transform.localScale = new Vector3(size - 0.2f, 1, 1.2f);      
    }

    public void Enable(bool value)
    {
        collider.isTrigger = !value;
        mesh.gameObject.SetActive(value);
    }
}
