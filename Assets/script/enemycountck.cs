using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemycountck : MonoBehaviour
{
    List<GameObject> enemycount = new List<GameObject>();
    public int enemyint;


    public  Gamemanager gm;
    BoxCollider2D bx;
    private void Awake()
    {
        bx = GetComponent<BoxCollider2D>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 3)
        {
            enemycount.Add(collision.gameObject);
            StartCoroutine(ck());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            enemycount.Remove(collision.gameObject);
            StartCoroutine(ckout());
        }
    }

    IEnumerator ck()
    {
        for (int i = 0; i < 2; i++)
        {
            enemyint = enemycount.Count;
            gm.stageenemyint = enemyint;
            gm.bt.enemytxt.text = string.Format("{0:n0}", gm.stageenemyint);
            yield return new WaitForFixedUpdate();
        }
    }
    IEnumerator ckout()
    {
        for (int i = 0; i < 2; i++)
        {
            enemyint = enemycount.Count;
            gm.stageenemyint = enemyint;
            gm.bt.enemytxt.text = string.Format("{0:n0}", gm.stageenemyint);
            yield return new WaitForFixedUpdate();
        }
        if (gm.spawntime <= 0 && gm.stageenemyint <= 0 && !gm.isclear)
            gm.StartCoroutine(gm.clearstage());
    }
}
