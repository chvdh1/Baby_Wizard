using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public Animator bulletmotion;
    public poolmanager EF;
    public poolmanager skillEF;
    public float mindmg, maxdmg, percent;
    public float multiple;
    public int per;
    public int elementnum;
    public int bulletint;
    public float shoyspeed, staytime, speed, dottime;
    public bool notfalse, staydmg;
    public GameObject[] childobj;

    //전기 특성
    public radar1 RD;
    public int count;
    public Sprite[] changesprite;
    public SpriteRenderer[] LT;
    public plskill plsk;
    public GameObject child;
    public GameObject mesh;
    public Vector3 dirtarget;

    Rigidbody2D rg;

    private void Awake()
    {
        rg = GetComponent<Rigidbody2D>();
    }
    public void init(float mindmg, float maxdmg, int per, Vector3 dir)
    {
        this.mindmg = mindmg * multiple;
        this.maxdmg = maxdmg * multiple;
        this.per = per;
        rg.velocity = dir * shoyspeed;
    }
    void elementEF()
    {
        if (notfalse)
            return;
        SpriteRenderer SR = EF.Get(0).GetComponent<SpriteRenderer>();
        if (elementnum == 0)
            SR.color = new Color(1, 0.5f, 0, 1);
        else if (elementnum == 1)
            SR.color = new Color(0, 0.85f, 1, 1);
        else if (elementnum == 2)
            SR.color = new Color(0.95f, 0.95f, 0.3f, 1);
        SR.transform.GetComponent<Transform>().position = transform.position;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {

            if (per != -1 && !staydmg)
            {
                elementEF();
                per--;

                if (per == -1)
                {
                    rg.velocity = Vector2.zero;
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    gameObject.SetActive(false);
                }
            }
        }
        if (collision.gameObject.tag == "wall" && !notfalse)
        {
            elementEF();
            transform.rotation = Quaternion.Euler(0, 0, 0);
            gameObject.SetActive(false);
        }

    }
    public void bulletfal()
    {
        gameObject.SetActive(false);
    }

    public IEnumerator firebollEF()
    {
        while(gameObject.activeSelf)
        {
           Transform eletran =  skillEF.Get(0).transform;
            eletran.position = this.transform.position;
            eletran.rotation = this.transform.rotation;
            yield return new WaitForSeconds(0.05f);
        }
       
    }

    public IEnumerator endtime()
    {
        while (staytime > 0)
        {
            staytime -= 1;
            yield return new WaitForSeconds(1);
        }
        bulletmotion.SetTrigger("fal");
    }
    public IEnumerator endtime1()
    {
        while (staytime > 0)
        {
            staytime -= 1;
            yield return new WaitForSeconds(1);
        }

        gameObject.SetActive(false);
    }
    public IEnumerator endtime2()//불길
    {
        while (staytime > 0)
        {
            staytime -= 1;
            yield return new WaitForSeconds(1);
        }
        gameObject.SetActive(false);
    }
    public IEnumerator spin()
    {
        yield return new WaitForFixedUpdate();
        while (gameObject.activeSelf)
        {
            transform.Rotate(Vector3.back * speed * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
    }
    public IEnumerator lightningEF1()
    {
        int SPran = Random.Range(0, 3);
        LT[0].sprite = changesprite[SPran];
        child.GetComponent<SpriteRenderer>().sprite = changesprite[SPran + 3];
        LT[0].color = new Color(1, 1, 1, 1);
        child.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        radar1 RD = GetComponent<radar1>();
        yield return new WaitForSeconds(0.1f);
        if (count != 0 && RD.nearestTarget != Vector3.zero)
        {
            Vector3 targetpos = RD.nearestTarget;
            Vector3 enemyvec = targetpos - RD.point;
            float curdiff = enemyvec.magnitude;

            if ((RD.point - targetpos).magnitude < RD.scanrange)
            {
                GameObject bulletobj = plsk.weaponpool.Get(13);
                Transform bullettrans = bulletobj.GetComponent<Transform>();
                bullet bullet = bullettrans.GetComponent<bullet>();
                radar1 RDC = bulletobj.GetComponent<radar1>();
                RDC.point = targetpos;
                bullet.elementnum = 2;
                bullet.multiple = plsk.multiple[21];
                bullet.count = count - 1;
                bullet.plsk = plsk;
                bullet.EF = EF;
                elementEF();
                bullet.mindmg = mindmg / multiple;
                bullet.maxdmg = maxdmg / multiple;
                bullettrans.parent = plsk.weaponpool.transform;
                bullettrans.position = (targetpos + transform.position) / 2;
                bulletobj.GetComponent<SpriteRenderer>().size = new Vector2(1, curdiff / 3);
                bullet.child.GetComponent<SpriteRenderer>().size = new Vector2(1, curdiff / 3);
                bulletobj.GetComponent<CapsuleCollider2D>().size = new Vector2(0.5f, curdiff / 3f);
                Debug.Log(bullet.child);
                float ang = Mathf.Atan2(enemyvec.y, enemyvec.x) * Mathf.Rad2Deg - 90;
                bullettrans.Rotate(new Vector3(0, 0, ang));

                bullet.init(bullet.mindmg, bullet.maxdmg, -1, Vector3.zero);
                bullet.StartCoroutine(bullet.lightningEF1());
            }
        }
        yield return new WaitForFixedUpdate();
        for (int i = 80; i > 0; i--)
        {
            LT[0].color = new Color(1, 1, 1, i / 80f);
            child.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, i / 80f);

            yield return new WaitForSeconds(1 / 90f);
        }
        yield return new WaitForFixedUpdate();
        transform.rotation = Quaternion.Euler(0, 0, 0);
        gameObject.SetActive(false);
    }

    public IEnumerator lightningEF2()
    {
        int SPran = Random.Range(0, 3);
        int SP1ran = Random.Range(0, 3);
        LT[0].sprite = changesprite[SPran];
        LT[1].sprite = changesprite[SP1ran + 3];
        radar1 RD = GetComponent<radar1>();
        yield return new WaitForSeconds(0.1f);
        while (gameObject.activeSelf)
        {
            if (RD.nearestTarget != Vector3.zero)
            {
                Vector3 targetpos = RD.nearestTarget;
                Vector3 enemyvec = targetpos - RD.point;
                float curdiff = enemyvec.magnitude;

                if ((RD.point - targetpos).magnitude < RD.scanrange)
                {
                    GameObject bulletobj = plsk.weaponpool.Get(13);
                    Transform bullettrans = bulletobj.GetComponent<Transform>();
                    bullet bullet = bullettrans.GetComponent<bullet>();
                    bullet.elementnum = 2;
                    bullet.multiple = plsk.multiple[22] ;
                    bullet.count = count - 1;
                    bullet.plsk = plsk;
                    bullet.EF = EF;
                    elementEF();
                    bullet.mindmg = mindmg / multiple;
                    bullet.maxdmg = maxdmg / multiple;
                    bullettrans.parent = plsk.weaponpool.transform;
                    bullettrans.position = (targetpos + transform.position) / 2;
                    bulletobj.GetComponent<SpriteRenderer>().size = new Vector2(1, curdiff / 3);
                    bullet.child.GetComponent<SpriteRenderer>().size = new Vector2(1, curdiff / 3);
                    bulletobj.GetComponent<CapsuleCollider2D>().size = new Vector2(0.5f, curdiff / 3f);
                    Debug.Log(bullet.child);
                    float ang = Mathf.Atan2(enemyvec.y, enemyvec.x) * Mathf.Rad2Deg - 90;
                    bullettrans.Rotate(new Vector3(0, 0, ang));

                    bullet.init(bullet.mindmg, bullet.maxdmg, -1, Vector3.zero);
                    bullet.StartCoroutine(bullet.lightningEF2child());


                }
            }
            yield return new WaitForSeconds(1.5f);
        }

        yield return new WaitForFixedUpdate();
    }
   public IEnumerator lightningEF2child()
    {
        yield return new WaitForFixedUpdate();
        SpriteRenderer SR = GetComponent<SpriteRenderer>();
        for (int i = 80; i > 0; i--)
        {
            SR.color = new Color(1, 1, 1, i / 80f);
            child.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, i / 80f);

            yield return new WaitForSeconds(1 / 90f);
        }
        transform.rotation = Quaternion.Euler(0, 0, 0);
        gameObject.SetActive(false);
    }
    public IEnumerator lightningEF3()
    {
        //카운트 '100'으로 특성부여(전도)
        int SPran = Random.Range(0, 3);
        LT[0].sprite = changesprite[SPran];
        yield return new WaitForFixedUpdate();
        for (int i = 20; i > 0; i--)
        {
            LT[0].color = new Color(1, 1, 1, i / 20f);
            LT[1].color = new Color(1, 1, 1, i / 20f);

            yield return new WaitForSeconds(1 / 50f);
        }
        transform.rotation = Quaternion.Euler(0, 0, 0);
        gameObject.SetActive(false);
    }
   public IEnumerator lightning4EF()
    {
        if (count >=5)
            StopCoroutine(lightning4EF());
        for (int i = 1; i < plsk.targetrader.Length; i++)
            if (!plsk.targetrader[i].activeSelf)
            {
                plsk.LTcount = i;
            }
        yield return new WaitForFixedUpdate();
        while (plsk.targetrader[count-1].activeSelf)
        {
            GameObject bulletobj = plsk.targetrader[plsk.LTcount];
            Transform bullettrans = bulletobj.GetComponent<Transform>();
            bullet bullet = bullettrans.GetComponent<bullet>();
            radar1 RDC = bullet.GetComponent<radar1>();
            bullettrans.position = RD.point;
            bullet.elementnum = 2;
            bullet.multiple = 1.3f;
            bullet.plsk = plsk;
            bullet.EF = EF;
            bullet.mindmg = mindmg / multiple;
            bullet.maxdmg = maxdmg / multiple;
            plsk.targetrader[plsk.LTcount].SetActive(true);
            bullet.init(bullet.mindmg, bullet.maxdmg, -1, Vector3.zero);
            while (RD.nearestTarget != null &&
                (RD.point - RD.nearestTarget).magnitude < RD.scanrange)
            {
                Vector3 targetpos = RD.nearestTarget;
                Vector3 enemyvec = targetpos - RD.point;
                float curdiff = enemyvec.magnitude;
                RDC.point = targetpos;
                bullettrans.position = (targetpos + RD.point) / 2;
                plsk.targetrader[plsk.LTcount].GetComponent<SpriteRenderer>().size = new Vector2(1, curdiff / 3f);
                bullet.child.GetComponent<SpriteRenderer>().size = new Vector2(1, curdiff / 3f);
                plsk.targetrader[plsk.LTcount].GetComponent<CapsuleCollider2D>().size = new Vector2(0.5f, curdiff / 3f);
                float ang = Mathf.Atan2(enemyvec.y, enemyvec.x) * Mathf.Rad2Deg - 90;
                bullettrans.rotation = Quaternion.Euler(new Vector3(0, 0, ang));

                yield return new WaitForSeconds(0.1f);
                if (plsk.LTcount < 5 && bulletobj.activeSelf
                    && gameObject.activeSelf)
                    bullet.StartCoroutine(bullet.lightning4EF());
            }
            for (int i = count; i < plsk.targetrader.Length; i++)
                plsk.targetrader[i].SetActive(false);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForFixedUpdate();
        for (int i = count; i < plsk.targetrader.Length; i++)
            plsk.targetrader[i].SetActive(false);
    }
}
