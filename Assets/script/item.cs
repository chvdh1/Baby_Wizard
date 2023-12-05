using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour
{
    public float length;
    public SpriteRenderer sp;
    public int coinnum;
    public player pl;
    
    Rigidbody2D rg;

    void Awake()
    {
        rg = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "getitempoint" && gameObject.activeSelf)
            StartCoroutine(target());
    }

    public  IEnumerator target()
    {
        while(gameObject.activeSelf)
        {
            float pllength = Vector2.Distance(pl.rg.position, rg.position);

            if (pllength < length)
            {
                Vector2 dirvec = pl.rg.position - rg.position;
                Vector2 nextvec = dirvec.normalized * pl.speed*2 * Time.deltaTime;


                rg.MovePosition(rg.position + nextvec);
                rg.velocity = Vector2.zero;
            }
            yield return new WaitForFixedUpdate();
        }
        
    }
}
