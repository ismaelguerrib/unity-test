using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ig_Ennemies : MonoBehaviour
{
  readonly float ig_initialSpeed = 3.0f;
  Vector2 speed;

  public int points = 3;

  public GameObject projectile;
  readonly float projectileSpeed = 2f;

  // controler la fréquence de tir
  readonly float fireRate = .15f;
  float nextFire;


  Rigidbody2D rb;

  GameManager gameManager;


  void Start()
  {
    gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    // déterminer la vitesse x / y
    float x = Random.Range(-ig_initialSpeed, ig_initialSpeed);
    float y = Random.Range(-ig_initialSpeed, ig_initialSpeed);
    speed = new Vector2(x, y);

    // appliquer la vélocité
    rb = GetComponent<Rigidbody2D>();
    rb.velocity = speed;
  }

  void Update()
  {
    rb.velocity = speed;
  }

  void OnTriggerEnter2D(Collider2D collision)
  {
    // Bullet  Player  Enemy

    if (collision.tag == "Player")
    {
      gameManager.ig_KillPlayer();
    }
    else if (collision.tag == "Bullet-Player")
    {
      // détruire la bullet
      Destroy(collision.gameObject);
      // destruction = ennemy initial
      Destroy(gameObject);

      // score
      gameManager.ig_AddScore(points);
    }



  }


}
