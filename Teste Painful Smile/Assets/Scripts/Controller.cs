using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System;


public class Controller : MonoBehaviour
{
    public static Controller Instance;

    [Header("Audio")]
    public AudioSource sfxSource;
    public AudioSource musicSource;

    public AudioClip[] ShootCannons;
    public AudioClip Explosion, Beat;

    public float TimeToEnd = 100, SpawnTimeOfEnemys;

    public int Points;

    public bool CanCout;

    void Start()
    {
        Points = 0;
        CanCout = false;
    }

    void Update()
    {
   
        if (SceneManager.GetActiveScene().name ==  "GameOverScreen")
        {
            CanCout = false;
        }
        if (SceneManager.GetActiveScene().name == "GameScreen")
        {
            CanCout = true;
        }
        if (CanCout)
        {
            TimeToEnd -= 1 * Time.deltaTime;
        }
        if (TimeToEnd <= 0)
        {
            LoadScene("GameOverScreen");
        }
    }

    public void PlaySFX(AudioClip sfxClip, float volume)
    {
        sfxSource.PlayOneShot(sfxClip, volume);
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void SetTimeToEnd(float value)
    {
        TimeToEnd = value;
    }

    public void SetSpawnTimeOfEnemys(float value)
    {
        SpawnTimeOfEnemys = value;
    }

    public void SetCanCout(bool value)
    {
        CanCout = value;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Reset()
    {
        SpawnTimeOfEnemys = 9;
        TimeToEnd = 120;
        CanCout = false;
        Points = 0;
    }
}