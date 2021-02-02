using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ig_BulletPlayer : MonoBehaviour
{
  float life = 4f;

  void Update()
  {
    life -= Time.deltaTime;
    if (life <= 0)
    {
      Destroy(gameObject);
    }
  }
}
