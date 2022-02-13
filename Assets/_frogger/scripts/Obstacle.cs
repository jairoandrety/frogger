using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float speed = 0;
    [SerializeField] private Vector3 direction = Vector3.zero;

    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

    }
}
