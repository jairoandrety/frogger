using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMove : MonoBehaviour
{
    [SerializeField] private float speed = 0;
    [SerializeField] private Vector3 direction = Vector3.zero;

    private bool isMove = false;

    public void StartMove()
    {
        isMove = true;
    }

    public void SetSpeed(Vector3 direction, int speed)
    {
        this.direction = direction;
        this.speed = speed;
    }

    void Update()
    {
        if (!isMove)
            return;

        transform.Translate(direction * speed * Time.deltaTime);
    }
}
