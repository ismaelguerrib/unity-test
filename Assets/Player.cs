using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ig_Player : MonoBehaviour
{
  // accélération / décélération
  readonly float speed = 10f;
  readonly float drag = 1; // résistance
  float thrust; // poussée 

  // rotation
  readonly float rotationSpeed = 150f;
  float rotation;

  // pouvoir tirer
  public GameObject projectile;
  readonly float projectileSpeed = 4f;

  // controler la fréquence de tir
  readonly float fireRate = .25f;
  float nextFire;


  Rigidbody2D rb;

  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    rb.drag = drag;
  }

  void Update()
  {
    if (GameManager.state == GameManager.States.play)
    {
      Move();
      ig_Fire();
    }
  }

  void ig_Fire()
  {
    nextFire += Time.deltaTime;
    if (Input.GetButton("Fire1") && nextFire > fireRate)
    {
      ig_Shoot();
      nextFire = 0;
    }
  }

  void ig_Shoot()
  {
    GameObject bulletPlayer = Instantiate(projectile, transform.position, transform.rotation);
    bulletPlayer.GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(0, projectileSpeed, 0);
  }


  void Move()
  {
    thrust = Input.GetAxisRaw("Horizontal");
    if (thrust < 0)
    {
      thrust = 0; // rb.drag += Mathf.Abs(thrust);
    }
  }

  private void FixedUpdate()
  {
    Vector3 force = transform.TransformDirection(0, thrust * speed, 0);
    rb.AddForce(force);
  }


}
