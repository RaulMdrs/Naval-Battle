using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyShooterScript : MonoBehaviour
{
    public Sprite[] stateShip;
    public int Life;
    public bool Alive, ILoot, CountedPoint;
    public float DistancePlayer;
    public float Velocity, SpeedRotation;
    Transform Target;
    GameObject Player;
    Quaternion Rotation;
    float Angle;
    public int State;
    Vector2 Direction;


    public GameObject[] CannonPositions;
    public GameObject Bullet, Explosion;
    public GameObject Loot;

    public float LoadLeftCannons;
    public float AimToShoot = 1.5f;

    public Transform HealthBar;
    public GameObject HealthBarObj;

    Vector2 HealthBarScale;
    float HealthPercent;
    void Start()
    {
        HealthBarScale = HealthBar.localScale;
        HealthPercent = HealthBarScale.x / Life;
        State = 1;
        Player = GameObject.FindGameObjectWithTag("Player");
        Life = 100;
        Alive = true;
        ILoot = true;
        CountedPoint = false;
    }

    void Update()
    {
        if (Alive)
        {
            Target = Player.transform;
            DistancePlayer = Vector2.Distance(transform.position, Target.position);
            LoadLeftCannons -= 1 * Time.deltaTime;
            CurrentState();

            switch (State)
            {
                case 1:
                    AimToShoot = 1.5f;

                    Direction = Player.transform.position - transform.position;

                    Angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;

                    Rotation = Quaternion.AngleAxis(Angle + 90, Vector3.forward);

                    transform.rotation = Quaternion.Slerp(transform.rotation, Rotation, SpeedRotation * Time.deltaTime);

                    transform.position = Vector2.MoveTowards(transform.position, Target.position, Velocity * Time.deltaTime);


                    if (DistancePlayer <= 3)
                    {
                        State = 2;
                    }
                    break;

                case 2:
                    AimToShoot -= 1 * Time.deltaTime;

                    Direction = Player.transform.position - transform.position;

                    Angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;

                    Rotation = Quaternion.AngleAxis(Angle, Vector3.forward);

                    transform.rotation = Quaternion.Slerp(transform.rotation, Rotation, (SpeedRotation / 2) * Time.deltaTime);

                    if (LoadLeftCannons <= 0 && AimToShoot <= 0)
                    {
                        LeftFire();
                    }

                    if (DistancePlayer >= 5)
                    {
                        State = 1;
                    }
                    break;
            }

        }
    }

    void CurrentState()
    {

        if (Life <= 85 && Life > 30)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = stateShip[0];
        }

        else if (Life <= 30 && Life > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = stateShip[1];
        }

        else if (Life <= 0)
        {
            if (!CountedPoint)
            {
                Controller.Instance.Points += 1;
                CountedPoint = true;
            }
            gameObject.GetComponent<SpriteRenderer>().sprite = stateShip[2];
            Alive = false;
            GameObject Boom = Instantiate(Explosion, transform.position, transform.rotation);
            Controller.Instance.PlaySFX(Controller.Instance.Explosion, 1f);
            Destroy(Boom, 5);
            Destroy(gameObject, 1);

            int Sort = Random.RandomRange(0, 100);

            if (Sort > 80 && ILoot)
            {
                GameObject LootObj = Instantiate(Loot, transform.position, Loot.transform.rotation);
                ILoot = false;
            }
        }
    }

    public void IWasHit(int damage)
    {
        Life -= damage;
        UpdateHealthBar();
    }

    void LeftFire()
    {
        for (int i = 1; i < 3; i++)
        {
            GameObject FirstRightBullet = Instantiate(Bullet, CannonPositions[i].transform.position, CannonPositions[i].transform.rotation);
            Controller.Instance.PlaySFX(Controller.Instance.ShootCannons[Random.RandomRange(0, 3)], 1f);
        }
        LoadLeftCannons = 2;
    }


    void UpdateHealthBar()
    {
        HealthBarScale.x = HealthPercent * Life;
        HealthBar.localScale = HealthBarScale;
    }
}
