using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Disabler : MonoBehaviour
{
    public Action<Trunk> OnDisabler;

    private void OnTriggerEnter(Collider other)
    {
        Trunk platformSelected = other.gameObject.GetComponent<Trunk>();

        if (platformSelected != null)
        {
            other.gameObject.SetActive(false);
            OnDisabler?.Invoke(platformSelected);
        }
    }
}
