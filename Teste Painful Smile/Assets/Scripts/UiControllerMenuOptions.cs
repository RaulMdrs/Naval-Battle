using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiControllerMenuOptions : MonoBehaviour
{
    public Text Time, Spawn;

    void Start()
    {

    }

    void Update()
    {
        Time.text = "" + ((int)Controller.Instance.TimeToEnd).ToString() + "s";

        Spawn.text = "" + ((int)Controller.Instance.SpawnTimeOfEnemys).ToString() + "s";
    }
}