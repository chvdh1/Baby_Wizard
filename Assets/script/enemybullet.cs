using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemybullet : MonoBehaviour
{
    public enemy enemy;
    public float dmg;
    public float speed;

    Rigidbody2D rg;

    private void Awake()
    {
        rg = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && collision.gameObject.tag == "wall")
        {
            rg.velocity = Vector2.zero;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            gameObject.SetActive(false);
        }
    }


}
