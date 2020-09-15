using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class UIController : MonoBehaviour
{
    public Text Timer, PointsTxt;

    void Start()
    {

    }

    void Update()
    {
        Timer.text = "" + ((int)Controller.Instance.TimeToEnd).ToString() + "s";

        PointsTxt.text = "Points: " + Controller.Instance.Points.ToString();
    }
}