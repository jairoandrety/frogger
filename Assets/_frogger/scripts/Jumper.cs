using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Jumper : MonoBehaviour
{
    public Action OnGrounded;
    public Action OnDead;

    private bool isGrounded = false;
    private GameObject objectToCollision;

    public bool IsGrounded { get => isGrounded; set => isGrounded = value; }
    public GameObject ObjectToCollision { get => objectToCollision; }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        Vector3 position = contact.point;

        objectToCollision = collision.gameObject;

#if UNITY_EDITOR
        Debug.Log(ObjectToCollision.tag);
#endif
        if (ObjectToCollision != null && ObjectToCollision.tag == "obstacle")
        {
            OnDead?.Invoke();
        }

        if (ObjectToCollision != null && ObjectToCollision.tag == "ground")
        {
            isGrounded = true;
            OnGrounded?.Invoke();

            Bounce bounce = objectToCollision.GetComponent<Bounce>();

            if (bounce != null)
                bounce.SendBouncing();
        }

       
    }    
}
