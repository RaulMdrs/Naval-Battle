using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NavalBattle
{
    public class BulletScript : MonoBehaviour
    {
        public float Velocity;
        public int Damage;

        public Vector3 Movement;

        void Start()
        {
            Destroy(gameObject, 5);
        }

        void Update()
        {
            Movement.y = -Velocity * Time.deltaTime;
            transform.Translate(Movement);
        }


        private void OnCollisionEnter2D(Collision2D collision)
        {

            Debug.Log("bati em " + collision.gameObject.name);
            if (collision.gameObject.tag == "EnemyChaser")
            {
                collision.gameObject.GetComponent<EnemyChaserScript>().IWasHit(20);
            }

            if (collision.gameObject.tag == "EnemyShooter")
            {
                collision.gameObject.GetComponent<EnemyShooterScript>().IWasHit(20);
            }

            if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<PlayerScript>().IWasHit(20);
            }
            Destroy(gameObject);
        }
    }
}