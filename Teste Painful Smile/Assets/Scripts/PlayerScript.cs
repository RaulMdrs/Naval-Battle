using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public int Life;
    public float Velocity;

    public Sprite[] stateShip;
    public Vector3 Movement;

    public GameObject[] CannonPositions;
    public GameObject Bullet, Explosion;

    public float LoadLeftCannons, LoadRightCannons, LoadFrontCannon;

    public Transform HealthBar;
    public GameObject HealthBarObj;

    Vector2 HealthBarScale;
    float HealthPercent;

    void Start()
    {
        HealthBarScale = HealthBar.localScale;
        HealthPercent = HealthBarScale.x / Life;
        LoadLeftCannons = 1;
        LoadRightCannons = 1;
        LoadFrontCannon = 0.3f;
    }

    void Update()
    {
        Movement = Vector3.zero;
        LoadLeftCannons -= 1 * Time.deltaTime;
        LoadRightCannons -= 1 * Time.deltaTime;
        LoadFrontCannon -= 1 * Time.deltaTime;

        if (Input.GetKey("w"))
        {
            Movement.y = -Velocity * Time.deltaTime;
        }

        if (Input.GetKey("s"))
        {
            Movement.y = (Velocity / 2) * Time.deltaTime;
        }

        if (Input.GetKey("a"))
        {
            transform.Rotate(0, 0, 90 * Time.deltaTime);
        }
        if (Input.GetKey("d"))
        {
            transform.Rotate(0, 0, -90 * Time.deltaTime);
        }

        transform.Translate(Movement);

        if (Input.GetMouseButtonDown(0) && LoadFrontCannon <= 0)
        {
            FrontFire();
        }


        if (Input.GetKey("e") && LoadRightCannons <= 0)
        {
            RightFire();
        }

        if (Input.GetKey("q") && LoadLeftCannons <= 0)
        {
            LeftFire();
        }


        CurrentState();
    }


    void FrontFire()
    {
        GameObject FrontBullet = Instantiate(Bullet, CannonPositions[0].transform.position, transform.rotation);
        Controller.Instance.PlaySFX(Controller.Instance.ShootCannons[Random.RandomRange(0, 3)], 1f);
        LoadFrontCannon = 1;
    }

    void RightFire()
    {
        for (int i = 1; i < 4; i++)
        {
            GameObject FirstRightBullet = Instantiate(Bullet, CannonPositions[i].transform.position, CannonPositions[i].transform.rotation);
            Controller.Instance.PlaySFX(Controller.Instance.ShootCannons[Random.RandomRange(0, 3)], 1f);
        }
        LoadRightCannons = 2;
    }

    void LeftFire()
    {
        for (int i = 4; i < 7; i++)
        {
            GameObject FirstRightBullet = Instantiate(Bullet, CannonPositions[i].transform.position, CannonPositions[i].transform.rotation);
            Controller.Instance.PlaySFX(Controller.Instance.ShootCannons[Random.RandomRange(0, 3)], 1f);
        }
        LoadLeftCannons = 2;
    }

    void CurrentState()
    {
        if (Life > 60)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = stateShip[0];
        }
        if (Life <= 60 && Life > 30)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = stateShip[1];
        }

        else if (Life <= 30 && Life > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = stateShip[2];
        }

        else if (Life <= 0)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = stateShip[3];
            GameObject Boom = Instantiate(Explosion, transform.position, transform.rotation);
            Controller.Instance.PlaySFX(Controller.Instance.Explosion, 1f);
            Destroy(gameObject);
            Controller.Instance.LoadScene("GameOverScreen");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EnemyChaser")
        {
            IWasHit(20);
            Controller.Instance.PlaySFX(Controller.Instance.Beat, 1f);
            collision.gameObject.GetComponent<EnemyChaserScript>().IWasHit(20);
            collision.gameObject.GetComponent<EnemyChaserScript>().ICanFollow = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Repair")
        {
            if (Life < 70)
            {
                Life += 30;
            }
            else if (Life > 70)
            {
                Life = 100;
            }
            UpdateHealthBar();
        }

        Destroy(collision.gameObject);
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
