using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaserScript : MonoBehaviour
{
    public Sprite[] stateShip;
    public int Life;

    public bool Alive, ICanFollow, ILoot, CountedPoint;

    public float Velocity, SpeedRotation, WaitingTime, DistancePlayer;

    Transform Target;

    GameObject Player;

    Vector2 Direction;

    Quaternion Rotation;

    float Angle;

    public GameObject Loot, Explosion;

    public Transform HealthBar;
    public GameObject HealthBarObj;

    Vector2 HealthBarScale;
    float HealthPercent;

    void Start()
    {
        HealthBarScale = HealthBar.localScale;
        HealthPercent = HealthBarScale.x / Life;
        Player = GameObject.FindGameObjectWithTag("Player");
        Alive = true;
        ICanFollow = true;
        ILoot = true;
        WaitingTime = 1.5f;
        CountedPoint = false;
    }

    void Update()
    {

        if (Alive)
        {
            CurrentState();
            Target = Player.transform;
            if (WaitingTime < 0)
            {
                ICanFollow = true;
                WaitingTime = 1.5f;
            }

            if (ICanFollow)
            {
                Direction = Player.transform.position - transform.position;

                Angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;

                Rotation = Quaternion.AngleAxis(Angle + 90, Vector3.forward);

                transform.rotation = Quaternion.Slerp(transform.rotation, Rotation, SpeedRotation * Time.deltaTime);

                transform.position = Vector2.MoveTowards(transform.position, Target.position, Velocity * Time.deltaTime);
            }

            if (!ICanFollow)
            {
                WaitingTime -= 1 * Time.deltaTime;

                Direction = Player.transform.position - transform.position;

                Angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;

                Rotation = Quaternion.AngleAxis(Angle + 90, Vector3.forward);

                transform.rotation = Quaternion.Slerp(transform.rotation, Rotation, SpeedRotation * Time.deltaTime);

            }
        }
    }

    void CurrentState()
    {

        if (Life <= 60 && Life > 30)
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


    void UpdateHealthBar()
    {
        HealthBarScale.x = HealthPercent * Life;
        HealthBar.localScale = HealthBarScale;
    }
}
