using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCoreView : MonoBehaviour
{
    public Text textPoints;
    public Text textLifes;

    public Slider sliderTime;

    public List<GameObject> panels;

    public void SetUIValues(int points, int lifes)
    {
        textPoints.text = points.ToString();
        textLifes.text = lifes.ToString();
    }

    public void SetTimeValue(float time)
    {
        sliderTime.value = time;
    }

    public void ActivePanels(int value)
    {
        panels.ForEach(i => i.SetActive(false));
        panels[value].SetActive(true);
    }
}