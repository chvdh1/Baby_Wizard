using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercoll : MonoBehaviour
{
    public player pl;
    public float indmg;
    Gamemanager gm;
    CircleCollider2D box;
    void Awake()
    {
        box = GetComponent<CircleCollider2D>();
        gm = Gamemanager.gamemng;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //행동제어
        if (collision.gameObject.layer == 6)
            pl.stopt = true;
        if (collision.gameObject.layer ==7)
            pl.stopb = true;
        if (collision.gameObject.layer == 8)
           pl. stopl = true;
        if (collision.gameObject.layer == 9)
            pl.stopr = true;
        //데미지계산
        if (collision.gameObject.layer == 3 && !pl.isdash && !pl.ishit)
        {
            enemy enemydmg = collision.GetComponentInParent<enemy>();
            pl.HP -= enemydmg.dmg;
            StartCoroutine(onhit());
        }
        //픽업아이템
        if (collision.gameObject.tag == "coin")
        {
            item coinnum = collision.gameObject.GetComponent<item>();
            
            pl.coin += (Random.Range(2+ coinnum.coinnum, 5) + gm.stageint+coinnum.coinnum) * Random.Range(1, 2);
            pl.mana++;
            Debug.Log(pl.coin);
            gm.txt();
            collision.gameObject.SetActive(false);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
            pl.stopt = false;
        if (collision.gameObject.layer == 7)
            pl.stopb = false;
        if (collision.gameObject.layer == 8)
            pl.stopl = false;
        if (collision.gameObject.layer == 9)
            pl.stopr = false;
    }

    IEnumerator onhit()
    {
        yield return new WaitForFixedUpdate();
        gm.txt();
        if (pl.HP > 0)
        {
            pl.ishit = true;
            pl.anim.SetTrigger("hit");
            yield return new WaitForSeconds(1f);
            pl.ishit = false;
        }
        else
            pl.dead();
        gm.txt();
    }
}
