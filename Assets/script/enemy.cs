using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class enemy : MonoBehaviour
{
   
    [Space(20)]
    public int enemynum, dmg,bulletnum, exp, eleresistance, stageint;
    public float HP, speed,hitdmg,bulletspeed,shotdis, slow;
    public Rigidbody2D target;
    public Collider2D box;
    public Animator anim;



    float mindotdmg, maxdotdmg, bulleteleper, lightningEF;
    float[] elesec = new float[4];
    int ele, maxhp;
    Gamemanager gm;
    Rigidbody2D rg;
    Transform abnormal;
    public poolmanager itempool;
   public poolmanager enemybulletpool;
    [HideInInspector] public bool isstop, ispattern, isonable, ishit, isdotdmg;
    public bool isdead;
    [HideInInspector] public bool[] eleEf = new bool[3];
    [HideInInspector] public GameObject[] eleEFobj;
    [HideInInspector] public ieceleEf SK;
  public poolmanager EF;


    void Awake()
    {
        eleEFobj = new GameObject[4];
        abnormal = transform.GetChild(0).gameObject.transform;
        rg = GetComponent<Rigidbody2D>();
        gm = Gamemanager.gamemng;
    }
    public void pattern()
    {
        for (int i = 0; i < elesec.Length; i++)
            elesec[i] = 0;
        for (int i = 0; i < eleEf.Length; i++)
            eleEf[i] = false;
        lightningEF = 1;

        if(gameObject.activeSelf)
            StartCoroutine(enemypattern());
    }
    void setactru()
    {
        isdead = false;
        isonable = true;
        slow = 1;
        StopCoroutine(eleEF());

        switch (enemynum)
        {
            case 1:
                maxhp = 50;
                HP = maxhp + maxhp * (stageint * 0.1f) + maxhp * (gm.difficultyint * 1.5f);
                speed = 0.5f + (stageint * 0.1f);
                dmg = 20 + stageint + (int)(gm.difficultyint * 1.5f);
                break;
            case 2:
                maxhp = 30;
                HP = maxhp + maxhp * (stageint * 0.1f) + maxhp * (gm.difficultyint * 1.5f);
                speed = 1f + (stageint * 0.1f);
                dmg = 30 + stageint + (int)(gm.difficultyint * 1.5f);
                break;
            case 3:
                maxhp = 10;
                HP = maxhp + maxhp * (stageint * 0.1f) + maxhp * (gm.difficultyint * 1.5f);
                speed = 5 + (stageint * 0.1f);
                dmg = 15 + stageint + (int)(gm.difficultyint * 1.5f);
                break;
            case 4:
                maxhp = 30;
                HP = maxhp + maxhp * (stageint * 0.1f) + maxhp * (gm.difficultyint * 1.5f);
                speed = 2 + (stageint * 0.1f);
                dmg = 30 + stageint + (int)(gm.difficultyint * 1.5f);
                break;
            case 5:
                maxhp = 50;
                HP = maxhp + maxhp * (stageint * 0.1f) + maxhp * (gm.difficultyint * 1.5f);
                speed = 1 + (stageint * 0.1f);
                dmg = 40 + stageint + (int)(gm.difficultyint * 1.5f);
                break;
        }
    }
    public IEnumerator enemypattern()
    {
        setactru();
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(moveCK());
        isonable = false;
        switch(enemynum)
        {
            case 2:
                bulletnum = 1;
                while (!isstop && HP > 0)
                {
                    float distarget = (rg.position - target.position).magnitude;

                    if (distarget < shotdis)
                    {
                        ispattern = true;
                        yield return new WaitForSeconds(1.5f);
                        GameObject bullet = enemybulletpool.Get(bulletnum).GetComponent<GameObject>();
                        enemybullet eb = bullet.GetComponent<enemybullet>();
                        eb.dmg = dmg / 2;
                        Vector3 targetpos = target.position;
                        Vector3 dir = targetpos - transform.position;
                        bullet.transform.rotation = Quaternion.FromToRotation(Vector3.right, dir.normalized);
                        yield return new WaitForSeconds(0.5f);
                    }
                    yield return new WaitForSeconds(3f);
                }
                break;
        
        }
         
    }
    IEnumerator moveCK()
    {
        yield return new WaitForFixedUpdate();
        while(HP > 0)
        {
            move();
            yield return new WaitForSeconds(0.3f);
        }
    }
    void move()
    {
        if (isstop || ispattern || isonable || eleEf[1] || eleEf[2])
            rg.velocity = Vector2.zero;
        else
        {
            Vector2 dirvec = target.position - rg.position;
            rg.velocity = dirvec.normalized * speed * slow;

            if (dirvec.x > 0)
                transform.localScale = new Vector2(1, 1);
            else if (dirvec.x < 0)
                transform.localScale = new Vector2(-1, 1);
        }
    }


    public void onhit()
    {
        if (isdead || ishit)
            return;
        ishit = true;
        float dmg = ele == 2 ? hitdmg * lightningEF : hitdmg;
        HP -= dmg;
        Transform text = gm.TEXTEF.Get(0).transform;
        textEF txtstring = text.GetComponent<textEF>();
        text.position = transform.position;
        txtstring.txt = string.Format("{0:n0}", dmg);
        float ran = Random.Range(0, 100);
        if(ran< bulleteleper- eleresistance)
            StartCoroutine(eleEF());
        StartCoroutine(deadbool());
       
    }
    IEnumerator eleEF()
    {
        yield return new WaitForFixedUpdate();
        if(ele == 1)
        {
            elesec[1] += 2;
            if (eleEFobj[1] == null)
            {
                eleEFobj[1] = gm.eleEf.Get(1);
                eleEFobj[1].transform.parent = abnormal;
                eleEFobj[1].transform.position = abnormal.position;
            }
            float ran = Random.Range(0, 100);
            if (ran < bulleteleper - eleresistance)
            {
                if (eleEFobj[3] == null)
                {
                    eleEFobj[3] = gm.eleEf.Get(3);
                    eleEFobj[3].transform.position = transform.position;
                    eleEFobj[3].transform.localScale = transform.localScale * 1.5f;
                   SK = eleEFobj[3].GetComponent<ieceleEf>();
                    SK.StartCoroutine(SK.ing());
                }
                elesec[3] += 2;
                SK.ingtime = elesec[3];
            }
            while (elesec[3] > 0)
            {
                eleEf[1] = true;
               
                slow = 0.5f;
                yield return new WaitForSeconds(1);
                elesec[3] -= 1;
                SK.ingtime = elesec[3];
            }
            eleEf[1] = false;
            while (elesec[1] > 0)
            {
                slow = 0.5f;
                yield return new WaitForSeconds(1);
                elesec[1]-=1;
            }
            yield return new WaitForFixedUpdate();
            eleEFobj[1].transform.parent = gm.eleEfobj;
            eleEFobj[1].SetActive(false);
            slow = 1f;
        }
        else if (ele == 0)
        {
            if (eleEFobj[0] == null)
            {
                eleEFobj[0] = gm.eleEf.Get(0);
                eleEFobj[0].transform.parent = abnormal;
                eleEFobj[0].transform.position = abnormal.position;
            }
            elesec[0] += Random.Range(3, 10);
            while (elesec[0] > 0 || HP > 0)
            {
                float ran = Random.Range(5, 10);
                float eledmg = hitdmg / ran < 1 ? 1 : hitdmg / ran;
                Transform text = gm.TEXTEF.Get(0).transform;
                textEF txtstring = text.GetComponent<textEF>();
                text.position = transform.position;
                txtstring.txt = string.Format("{0:n0}", eledmg);
                HP -= eledmg;
                if(HP <= 0)
                    anim.SetTrigger("dead");
                yield return new WaitForSeconds(1);
                elesec[0] -= 1;
            }
            eleEFobj[0].transform.parent = gm.eleEfobj;
            eleEFobj[0].SetActive(false);
        }
        else if (ele == 2)
        {
            elesec[2] += 2;
            if (eleEFobj[2] == null)
            {
                eleEFobj[2] = gm.eleEf.Get(2);
                eleEFobj[2].transform.parent = abnormal;
                eleEFobj[2].transform.position = abnormal.position;
            }
            float ran = Random.Range(0, 100);
            int sec = 2;
            Transform text = gm.TEXTEF.Get(0).transform;
            textEF txtstring = text.GetComponent<textEF>();
            text.position = transform.position;
            txtstring.txt = "기절!";
            while (elesec[2] > 0)
            {
                if (ran < bulleteleper - eleresistance && sec>0)
                    eleEf[2] = true;
                else if(sec <= 0)
                    eleEf[2] = false;
                lightningEF = 1.3f;
                yield return new WaitForSeconds(1);
                elesec[2] -= 1;
                sec--;
            }
            yield return new WaitForFixedUpdate();
            eleEFobj[2].transform.parent = gm.eleEfobj;
            eleEFobj[2].SetActive(false);
            lightningEF = 1;
        }
    }
    IEnumerator deadbool()
    {
        yield return new WaitForFixedUpdate();
        if(HP < 0 && !isdead)
        {
            int i = Random.Range(0, 10);
            int s = stageint > 10 ? 3 : stageint > 5 ? 2 : 1;
            int r = Random.Range(0, s);
            if (i < 5)
            {
                item item = itempool.Get(r).GetComponent<item>();
                item.transform.position =
                    new Vector2(transform.position.x + Random.Range(-0.5f, 0.5f)
                    , transform.position.y + Random.Range(-0.5f, 0.5f));
                item.pl = gm.pl;
            }
            yield return new WaitForFixedUpdate();
            for (int a = 0; a < 3; a++)
                if (eleEFobj[a] != null)
                {
                    eleEFobj[a].transform.parent = gm.eleEfobj;
                    eleEFobj[a].SetActive(false);
                }
            maxdotdmg = 0;
            mindotdmg = 0;
            gm.exp += exp;
            isdead = true;
            anim.SetTrigger("dead");
        }
        else
            anim.SetTrigger("hit");

        ishit = false;
        gm.txt();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            isstop = true;
        if (collision.gameObject.tag == "plbullet" )
        {
            bullet bolldmg = collision.gameObject.GetComponent<bullet>();
            hitdmg = Random.Range(bolldmg.mindmg, bolldmg.maxdmg);
            ele = bolldmg.elementnum;
            bulleteleper = bolldmg.percent;
           
            if (bolldmg.notfalse)
            {
                SpriteRenderer SR = bolldmg.EF.Get(0).GetComponent<SpriteRenderer>();
                if (bolldmg.elementnum == 0)
                    SR.color = new Color(1, 0.5f, 0, 1);
                else if (bolldmg.elementnum == 1)
                    SR.color = new Color(0, 0.85f, 1, 1);
                else if (bolldmg.elementnum == 2)
                    SR.color = new Color(0.95f, 0.95f, 0.3f, 1);
                SR.transform.GetComponent<Transform>().position = transform.position;

                if(bolldmg.staydmg)
                {
                    mindotdmg = bolldmg.mindmg > mindotdmg ? bolldmg.mindmg : mindotdmg;
                    maxdotdmg = bolldmg.maxdmg > maxdotdmg ? bolldmg.maxdmg : maxdotdmg;
                    EF = bolldmg.EF;
                    StartCoroutine(dotdmg());
                }
            }
            if(bolldmg.count == 100)//레이저 특성 발동
            {
                radar1 RD = bolldmg.GetComponent<radar1>();
                RD.point = new Vector3(transform.position.x, transform.position.y, 0);

                if (RD.nearestTarget != Vector3.zero)
                {
                    Vector3 targetpos = RD.nearestTarget;
                    Vector3 enemyvec = targetpos - RD.point;
                    float curdiff = enemyvec.magnitude;
                    if ((RD.point - targetpos).magnitude < RD.scanrange)
                    {
                        GameObject bulletobj = bolldmg.plsk.weaponpool.Get(13);
                        Transform bullettrans = bulletobj.GetComponent<Transform>();
                        bullet bullet = bullettrans.GetComponent<bullet>();
                        bullet.elementnum = 2;
                        bullet.multiple = 1.2f;
                        bullet.plsk = bolldmg.plsk;
                        bullet.EF = bolldmg.EF;
                        float dmg = bolldmg.plsk.pl.dmg;
                        bullet.mindmg = (dmg - (dmg * 0.5f)) + (dmg * (bolldmg.plsk.balance / 100));
                        bullet.maxdmg = dmg + (dmg * 0.5f);
                        
                        bolldmg.plsk.elementEF();
                        bullettrans.parent = bolldmg.plsk.weaponpool.transform;
                        bullettrans.position = (targetpos + RD.point) / 2;
                        bulletobj.GetComponent<SpriteRenderer>().size = new Vector2(1, curdiff / 3);
                        bullet.child.GetComponent<SpriteRenderer>().size = new Vector2(1, curdiff / 3);
                        bulletobj.GetComponent<CapsuleCollider2D>().size = new Vector2(0.5f, curdiff / 3f);

                        float ang = Mathf.Atan2(enemyvec.y, enemyvec.x) * Mathf.Rad2Deg - 90;
                        bullettrans.Rotate(new Vector3(0, 0, ang));

                        bullet.init(bullet.mindmg, bullet.maxdmg, -1, Vector3.zero);
                        bullet.StartCoroutine(bullet.lightningEF2child());
                    }
                }

            }
            onhit();
        }
    }
    IEnumerator dotdmg()
    {
        isdotdmg = true;
        yield return new WaitForFixedUpdate();
        while(isdotdmg)
        {
            hitdmg = Random.Range(mindotdmg, maxdotdmg);
            SpriteRenderer SR = EF.Get(0).GetComponent<SpriteRenderer>();
            if (ele == 0)
                SR.color = new Color(1, 0.5f, 0, 1);
            else if (ele == 1)
                SR.color = new Color(0, 0.85f, 1, 1);
            else if (ele == 2)
                SR.color = new Color(0.95f, 0.95f, 0.3f, 1);
            SR.transform.GetComponent<Transform>().position = transform.position;
            onhit();
            yield return new WaitForSeconds(0.5f);
        }

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            isstop = false;
        if (collision.gameObject.tag == "plbullet" && collision.gameObject.GetComponent<bullet>().staydmg)
            isdotdmg = false; 
    }
}
