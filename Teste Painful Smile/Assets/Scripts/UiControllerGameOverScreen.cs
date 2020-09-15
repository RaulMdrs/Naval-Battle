using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiControllerGameOverScreen : MonoBehaviour
{
    public Text PointsTxt;

    void Start()
    {

    }

    void Update()
    {
         PointsTxt.text = "Points: " + Controller.Instance.Points.ToString();
    }
}
