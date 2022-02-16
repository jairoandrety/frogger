using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Disabler : MonoBehaviour
{
    public Action<Trunk> OnDisablerTrunk;
    public Action<Obstacle> OnDisablerObstacle;

    private void OnTriggerEnter(Collider other)
    {
        Trunk platformSelected = other.gameObject.GetComponent<Trunk>();

        if (platformSelected != null)
        {
            other.gameObject.SetActive(false);
            OnDisablerTrunk?.Invoke(platformSelected);
        }

        Obstacle obstacleSelected = other.gameObject.GetComponent<Obstacle>();

        if (obstacleSelected != null)
        {
            other.gameObject.SetActive(false);
            OnDisablerObstacle?.Invoke(obstacleSelected);
        }
    }
}
