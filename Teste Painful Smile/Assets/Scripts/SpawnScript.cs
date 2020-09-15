using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    public GameObject[] EnemysToSpawn;
    public Transform[] Points;

    public float TimeToNextEnemy, Timer = 10;

    void Start()
    {

    }

    void Update()
    {
        TimeToNextEnemy = Controller.Instance.SpawnTimeOfEnemys;
        Timer -= 1 * Time.deltaTime;
        Spawn();
    }

    void Spawn()
    {
        if (Timer <= 0)
        {
            Vector2 position = new Vector2(Random.RandomRange(Points[0].position.x, Points[1].position.x), Random.RandomRange(Points[0].position.y, Points[1].position.y));
            GameObject EnemySpawned = Instantiate(EnemysToSpawn[Random.RandomRange(0, EnemysToSpawn.Length)], position, gameObject.transform.rotation);
            Timer = TimeToNextEnemy;
        }
    }
}