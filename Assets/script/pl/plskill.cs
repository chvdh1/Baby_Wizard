using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class plskill : MonoBehaviour
{
    public poolmanager weaponpool;
    public poolmanager EFpool;
    public poolmanager skillEFpool;
    public player pl;

    public int prefebid, testint, skillpoint;
    public float balance;
    public int[] skillstat;
    //스킬개별로
    //public float percentage; 
    public float length;
    public float[] timer;
    public float[] multiple;
    public float[] mana;
    public float[] cooltime;
    public int[] count;

    public Image[] coolim;

    public bool spin, nullenemy;
    public GameObject[] onlyonebullet;
    public GameObject[] targetrader;
    public int LTcount;  
   Transform dirtrans;


    private void Awake()
    {
        pl = GetComponentInParent<player>();
    }
    public void elementEF()
    {
        bullet bulletlo = dirtrans.GetComponent<bullet>();
        bulletlo.EF = EFpool;
        float dmg = pl.dmg;
        float statbalance = pl.gm.plstat[5] * 3 > 60 ? 60 : pl.gm.plstat[5] * 3;
        balance = statbalance; //아이템 셋 확인해서 3항함수 ㄱ
            if (bulletlo.elementnum == 0)//불
        {
            bulletlo.mindmg = (dmg - (dmg * 0.2f)) + (dmg * 0.4f * (balance / 100));
            bulletlo.maxdmg = dmg + (dmg * 0.2f);
            bulletlo.percent = 30 + (pl.gm.plstat[7]/2);
        }
        else if (bulletlo.elementnum == 1)//얼음
        {
            bulletlo.mindmg = ((dmg - (dmg * 0.1f)) - ((dmg - (dmg * 0.1f)) * 0.1f))
                + ((dmg - (dmg * 0.1f)) * 0.2f * (balance / 100));
            bulletlo.maxdmg = (dmg - (dmg * 0.1f)) + ((dmg - (dmg * 0.1f)) * 0.1f);
            bulletlo.percent = 30 + (pl.gm.plstat[6] / 2);
        }
        else if (bulletlo.elementnum == 2)//전기
        {
            bulletlo.mindmg = (dmg - (dmg * 0.5f)) + (dmg * (balance / 100));
            bulletlo.maxdmg = dmg + (dmg * 0.5f);
            bulletlo.percent = 30 + (pl.gm.plstat[8] / 2);
        }
    }
    public IEnumerator dashcoll()// 0 
    {
        yield return new WaitForFixedUpdate();
        while (timer[0] > 0)
        {
            timer[0] -= Time.fixedDeltaTime;
            coolim[0].fillAmount = (timer[0] / cooltime[0]);
            yield return new WaitForFixedUpdate();
        }
    }

    public void icespear()
    {
        Vector3 targetpos = pl.rd.nearestTarget.position;
        Vector3 dir = targetpos - transform.position;
        dir = dir.normalized;

        if ((transform.position - targetpos).magnitude < length)
        {
            Transform bullettrans = weaponpool.Get(3).transform;
            bullet bullet = bullettrans.GetComponent<bullet>();
            dirtrans = bullettrans;
            bullet.elementnum = 1;
            bullet.multiple = pl.gm.PS.multiple[1];
            elementEF();
            bullettrans.localScale = new Vector2(pl.gm.PS.count[1],pl.gm.PS.count[1] * 2);
            bullettrans.parent = weaponpool.transform;
            bullettrans.position = transform.position;
            bullettrans.rotation = Quaternion.FromToRotation(Vector3.up, dir);
            bullet.init(bullet.mindmg, bullet.maxdmg, -1, dir);
           
            StartCoroutine(colldown1());
        }

    }
    IEnumerator colldown1()
    {
        timer[1] = cooltime[1];
        yield return new WaitForFixedUpdate();
        while (timer[1] >0)
        {
            timer[1] -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
    }
    public void iceskill2()//노바?
    {
        Vector3 dir = transform.position;
        dir = dir.normalized;

        Transform bullettrans = weaponpool.Get(4).transform;
        bullet bullet = bullettrans.GetComponent<bullet>();
        dirtrans = bullettrans;
        bullet.elementnum = 1;
        bullet.multiple = pl.gm.PS.multiple[2];
        elementEF();
        bullettrans.localScale = new Vector2(pl.gm.PS.count[2]*2, pl.gm.PS.count[2]*2);
        bullettrans.parent = weaponpool.transform;
        bullettrans.position = transform.position;
        bullet.init(bullet.mindmg, bullet.maxdmg, -1, dir);
        StartCoroutine(colldown2());
    }
    IEnumerator colldown2()
    {
        timer[2] = cooltime[2];
        yield return new WaitForFixedUpdate();
        while (timer[2] > 0)
        {
            timer[2] -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
    }
    public void iceskill3()//얼음꽃?
    {
        Vector3 targetpos = pl.rd.nearestTarget.position;
        Vector3 dir = targetpos - transform.position;
        dir = dir.normalized;

        Transform bullettrans = weaponpool.Get(5).transform;
        bullet bullet = bullettrans.GetComponent<bullet>();
        bullet bulletc = bullet.childobj[0].GetComponent<bullet>();
        bulletc.elementnum = 1;
        bulletc.multiple = pl.gm.PS.multiple[3]/2;
        dirtrans = bullettrans;
        bullet.elementnum = 1;
        bullet.multiple = pl.gm.PS.multiple[3];
        bullet.staytime = pl.gm.PS.count[3]*1.5f;
        elementEF();
        bullettrans.localScale = new Vector2(pl.gm.PS.count[3], pl.gm.PS.count[3]);
        bullettrans.parent = weaponpool.transform;
        bullettrans.position = targetpos;
        float dmg = pl.dmg;
        bulletc.mindmg = ((dmg - (dmg * 0.1f)) - ((dmg - (dmg * 0.1f)) * 0.1f))
                + ((dmg - (dmg * 0.1f)) * 0.2f * (balance / 100));
        bulletc.maxdmg = (dmg - (dmg * 0.1f)) + ((dmg - (dmg * 0.1f)) * 0.1f);
        bulletc.percent = 30 + (pl.gm.plstat[6] / 2);
        bulletc.EF = bullet.EF;
        bullet.init(bullet.mindmg, bullet.maxdmg, -1, dir);

        bullet.bulletmotion = bullet.GetComponent<Animator>();

        bulletc.init(bulletc.mindmg, bulletc.maxdmg, -1, dir);
        bullet.StartCoroutine(bullet.endtime());
        StartCoroutine(colldown3());
    }
    IEnumerator colldown3()
    {
        timer[3] = cooltime[3];
        yield return new WaitForFixedUpdate();
        while (timer[3] > 0)
        {
            timer[3] -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
    }
    public void iceskill4()//얼음장판?
    {
        Vector3 targetpos = pl.rd.nearestTarget.position;
        Vector3 dir = targetpos - transform.position;
        dir = dir.normalized;

        if (onlyonebullet[0]==null)
        {
            onlyonebullet[0] = weaponpool.Get(6);

            Transform bullettrans = onlyonebullet[0].transform;
            bullet bullet = bullettrans.GetComponent<bullet>();
            dirtrans = bullettrans;
            bullet.elementnum = 1;
            bullet.multiple = pl.gm.PS.multiple[4];
            bullet.speed = 10;
            elementEF();
            bullettrans.localScale = new Vector2(pl.gm.PS.count[4]*2, pl.gm.PS.count[4]*2);
            bullettrans.parent = weaponpool.transform;
            bullettrans.position = targetpos;
            bullet.init(bullet.mindmg, bullet.maxdmg, -1, dir);

            bullet.StartCoroutine(bullet.spin());
            StartCoroutine(mana4());
        }
        else if(onlyonebullet[0] != null)
        {
            if(!onlyonebullet[0].activeSelf)
            {
                onlyonebullet[0].SetActive(true);
                Transform bullettrans = onlyonebullet[0].GetComponent<Transform>();
                bullet bullet = bullettrans.GetComponent<bullet>();
                dirtrans = bullettrans;
                bullet.elementnum = 1;
                bullet.multiple = pl.gm.PS.multiple[4];
                bullet.speed = 10;
                elementEF();
                bullettrans.localScale = new Vector2(5 * (pl.gm.PS.count[4] * 0.1f), 5 * (pl.gm.PS.count[4] * 0.1f));
                bullettrans.parent = weaponpool.transform;
                bullettrans.position = targetpos;
                bullet.init(bullet.mindmg, bullet.maxdmg, -1, dir);
                bullet.StartCoroutine(bullet.spin());
                StartCoroutine(mana4());
            }
            else
                onlyonebullet[0].SetActive(false);
        }
        StartCoroutine(colldown4());
    }
    IEnumerator mana4()
    {
        yield return new WaitForFixedUpdate();
        while (onlyonebullet[0].activeSelf)
        {
            if (pl.mana < pl.gm.PS.mana[4])
            {
                onlyonebullet[0].SetActive(false);
                pl.gm.bt.StartCoroutine(pl.gm.bt.mananull());
            }
            else
                pl.gm.pl.mana -= pl.gm.PS.mana[4];
            yield return new WaitForSeconds(1);
        }
    }
    IEnumerator colldown4()
    {
        yield return new WaitForSeconds(1);
        while (onlyonebullet[0].activeSelf)
        {
            if(pl.mana> mana[4])
                pl.mana -= mana[4];
            else
            {
                pl.gm.bt.StartCoroutine(pl.gm.bt.mananull());
                onlyonebullet[0].SetActive(false);
            }
            yield return new WaitForSeconds(1);
        }
    }
    public void iceskill5()//얼음꽃?
    {
        Vector3 dir = transform.position;
        dir = dir.normalized;

        GameObject bullettobj = weaponpool.Get(7);
        Transform bullettrans = bullettobj.transform;
        bullet bullet = bullettrans.GetComponent<bullet>();
        dirtrans = bullettrans;
        bullet.elementnum = 1;
        bullet.multiple = pl.gm.PS.multiple[5];
        bullet.staytime = pl.gm.PS.count[5]*2;
        bullet.speed = -20;
        elementEF();
        bullettrans.localScale = new Vector2(pl.gm.PS.count[5], pl.gm.PS.count[5]);
        bullettrans.parent = transform;
        bullettrans.position = transform.position;
        bullet.init(bullet.mindmg, bullet.maxdmg, -1, dir);
        cooltime[5] = bullet.staytime;
        bullet lol = bullettrans.GetChild(2).GetComponent<bullet>();
        lol.staytime = 20;
        lol.speed = -80;
        lol.StartCoroutine(lol.spin());
        bullet.StartCoroutine(bullet.spin());

        for (int i = 0; i < 8; i++)
        {
            if (bullet.childobj[i] == null)
                bullet.childobj[i] = weaponpool.Get(3);
            
            Transform bulletC = bullet.childobj[i].transform;
            bullet bulletlc = bulletC.GetComponent<bullet>();
            bulletC.localScale = new Vector3(1.2f, 1.2f, 1);
            bulletC.parent = bullettobj.transform.GetChild(2).transform;
            bulletC.position = transform.position;
            bulletlc.elementnum = 1;
            bulletlc.multiple = pl.gm.PS.multiple[1];
            bulletlc.notfalse = true;
            float dmg = pl.dmg;
            bulletlc.mindmg = ((dmg - (dmg * 0.1f)) - ((dmg - (dmg * 0.1f)) * 0.1f))
                    + ((dmg - (dmg * 0.1f)) * 0.2f * (balance / 100));
            bulletlc.maxdmg = (dmg - (dmg * 0.1f)) + ((dmg - (dmg * 0.1f)) * 0.1f);
            bulletlc.percent = 30 + (pl.gm.plstat[7] / 2);
            bulletC.localScale = new Vector2(pl.gm.PS.count[1], pl.gm.PS.count[1] * 2); Vector3 rotvec = Vector3.forward * 360 * i / 8;
            bulletC.Rotate(rotvec);
            bulletC.Translate(bulletC.right * 2.2f, Space.World);
            bulletlc.init(bulletlc.mindmg, bulletlc.maxdmg, -1, Vector3.zero);//-1은 무한관통.
            bulletlc.EF = EFpool;
        }
        bullet.StartCoroutine(bullet.endtime1());

        StartCoroutine(colldown5());
    }
    IEnumerator colldown5()
    {
        timer[5] = cooltime[5];
        yield return new WaitForFixedUpdate();
        while (timer[5] > 0)
        {
            timer[5] -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
    }


    public void fireskill1()//파이어볼?
    {
        Vector3 targetpos = pl.rd.nearestTarget.position;
        Vector3 enemyvec = targetpos - transform.position;
        Vector3 dir = enemyvec.normalized;

        if ((transform.position - targetpos).magnitude < length)
        {
            Transform bullettrans = weaponpool.Get(8).transform;
            bullet bullet = bullettrans.GetComponent<bullet>();
            dirtrans = bullettrans;
            bullet.elementnum = 0;
            bullet.speed = 7;
            bullet.multiple = pl.gm.PS.multiple[11];
            elementEF();
            bullettrans.parent = weaponpool.transform;
            bullettrans.position = transform.position;
            float ang = Mathf.Atan2(enemyvec.y, enemyvec.x) * Mathf.Rad2Deg-90;
            bullettrans.Rotate(new Vector3(0, 0, ang));
            bullet.skillEF = skillEFpool;
            bullet.StartCoroutine(bullet.firebollEF());
            bullet.init(bullet.mindmg, bullet.maxdmg, 2, bullettrans.up);
            int r = pl.gm.PS.count[11];
            for (int i = -1*(r-1); i < r; i++)
            {
                if(i != 0)
                {
                    float ivecrot = ((45/i) + ang)%360;
                    Transform bulletC = weaponpool.Get(8).transform;
                    bullet bulletl2 = bulletC.GetComponent<bullet>();
                    dirtrans = bulletC;
                    bulletl2.elementnum = 0;
                    bulletl2.speed = 7;
                    bulletl2.multiple = pl.gm.PS.multiple[11]; ;
                    elementEF();
                    bulletC.parent = weaponpool.transform;
                    bulletC.position = transform.position;
                    bulletC.Rotate(new Vector3(0, 0, ivecrot));
                    bulletl2.skillEF = skillEFpool;
                    bulletl2.StartCoroutine(bulletl2.firebollEF());
                    bulletl2.init(bulletl2.mindmg, bulletl2.maxdmg, 2, bulletC.up);
                }
            }
            StartCoroutine(firecolldown1());
        }

    }
    IEnumerator firecolldown1()
    {
        timer[11] = cooltime[11];
        yield return new WaitForFixedUpdate();
        while (timer[11] > 0)
        {
            timer[11] -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
    }
    public void fireskill2()//불타는 발걸음?
    {
        StartCoroutine(flames());
        StartCoroutine(firecolldown2());
    }
    IEnumerator flames()
    {
        yield return new WaitForFixedUpdate();
        for (int i = 0; i < pl.gm.PS.count[12]; i++)
        {
            Transform bullettrans = weaponpool.Get(9).transform;
            bullet bullet = bullettrans.GetComponent<bullet>();
            dirtrans = bullettrans;
            bullet.elementnum = 0;
            bullet.multiple = pl.gm.PS.multiple[12];
            bullet.notfalse = true;
            bullet.staytime = 10;
            bullet.staydmg = true;
            elementEF();
            bullettrans.parent = weaponpool.transform;
            bullettrans.position = transform.position;
            bullet.init(bullet.mindmg, bullet.maxdmg, -1, Vector3.zero);
            bullet.StartCoroutine(bullet.endtime2());
            yield return new WaitForSeconds(0.5f);
        }
    }
    IEnumerator firecolldown2()
    {
        timer[12] = cooltime[12];
        yield return new WaitForFixedUpdate();
        while (timer[12] > 0)
        {
            timer[12] -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
    }
    public void fireskill3()//매ㅔㅌ오?
    {
        Vector3 targetpos = pl.rd.nearestTarget.position;
        Vector3 enemyvec = targetpos - transform.position;

        if ((transform.position - targetpos).magnitude < length)
        {
            Transform bullettrans = weaponpool.Get(10).transform;
            bullet bullet = bullettrans.GetComponent<bullet>();
            dirtrans = bullettrans;
            bullet.elementnum = 0;
            bullet.multiple = pl.gm.PS.multiple[13];
            elementEF();
            bullettrans.parent = weaponpool.transform;
            bullettrans.position = targetpos;
            bullet.init(bullet.mindmg, bullet.maxdmg, -1, targetpos);

            StartCoroutine(firecolldown3());
        }

    }
    IEnumerator firecolldown3()
    {
        timer[13] = cooltime[13];
        yield return new WaitForFixedUpdate();
        while (timer[13] > 0)
        {
            timer[13] -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
    }
    public void fireskill4()//시즈모드?
    {
        if (pl.rd.nearestTarget == null)
            return;
        int up = 5;
        length += up;
        pl.speed = 0;


        Vector3 targetpos = pl.rd.nearestTarget.position;
        Vector3 enemyvec = targetpos - transform.position;
        Vector3 dir = enemyvec.normalized;

        if ((transform.position - targetpos).magnitude < length)
        {
            if(onlyonebullet[6] == null)
            {
                onlyonebullet[6] = weaponpool.Get(11);
                Transform bullettrans = onlyonebullet[6].GetComponent<Transform>();
                bullet bullet = bullettrans.GetComponent<bullet>();
                dirtrans = bullettrans;
                bullet.elementnum = 0;
                bullet.multiple = pl.gm.PS.multiple[14]/2 ;
                elementEF();
                bullettrans.parent = weaponpool.transform;
                bullettrans.position = transform.position;
                bullet.init(bullet.mindmg, bullet.maxdmg, -1, Vector3.zero);

                StartCoroutine(firecontinuous());
                StartCoroutine(mana14());

            }
            else
            {
                if(onlyonebullet[6].activeSelf)
                {
                    onlyonebullet[6].SetActive(false);
                    StartCoroutine(firecolldown4());
                }
                    
                else
                {
                    onlyonebullet[6].SetActive(true);
                    Transform bullettrans = onlyonebullet[6].GetComponent<Transform>();
                    bullet bullet = bullettrans.GetComponent<bullet>();
                    dirtrans = bullettrans;
                    bullet.elementnum = 0;
                    bullet.multiple = pl.gm.PS.multiple[14]/2 ;
                    elementEF();
                    bullettrans.parent = weaponpool.transform;
                    bullettrans.position = transform.position;
                    bullet.init(bullet.mindmg, bullet.maxdmg, -1, Vector3.zero);

                    StartCoroutine(firecontinuous());
                    StartCoroutine(mana14());
                }
            }
        }
    }
    IEnumerator mana14()
    {
        yield return new WaitForFixedUpdate();
        while (onlyonebullet[6].activeSelf)
        {
            if (pl.mana < pl.gm.PS.mana[14])
            {
                onlyonebullet[6].SetActive(false);
                pl.gm.bt.StartCoroutine(pl.gm.bt.mananull());
            }
            else
                pl.gm.pl.mana -= pl.gm.PS.mana[14];
            yield return new WaitForSeconds(1);
        }
    }
    IEnumerator firecontinuous()
    {
        pl.dashcount = 0;
        float time = 0.2f;
        while (onlyonebullet[6].activeSelf &&
            pl.rd.nearestTarget != null
            && !nullenemy)
        {
            Vector3 targetpos = pl.rd.nearestTarget.position;
            Vector3 enemyvec = targetpos - transform.position;
            Transform bullettrans = weaponpool.Get(8).transform;
            bullet bullet = bullettrans.GetComponent<bullet>();
            dirtrans = bullettrans;
            bullet.elementnum = 0;
            bullet.speed = 10;
            bullet.multiple = pl.gm.PS.multiple[14];
            elementEF();
            bullettrans.parent = weaponpool.transform;
            bullettrans.position = transform.position;
            float ang = Mathf.Atan2(enemyvec.y, enemyvec.x) * Mathf.Rad2Deg - 90;
            bullettrans.Rotate(new Vector3(0, 0, ang));

            bullet.init(bullet.mindmg, bullet.maxdmg, 2, bullettrans.up);
            yield return new WaitForSeconds(time);
            if ((transform.position - targetpos).magnitude >= length)
                nullenemy = true;

        }
        yield return new WaitForFixedUpdate();
        onlyonebullet[6].SetActive(false);
        nullenemy = false;
        length -= 5;
        pl.speed = 2;
        pl.dashcount = 2;

    }
    IEnumerator firecolldown4()
    {
        timer[14] = cooltime[14];
        yield return new WaitForFixedUpdate();
        while (timer[14] > 0)
        {
            timer[14] -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
    }
    public void fireskill5()//불바다?
    {
        onlyonebullet[7] = weaponpool.Get(12);
        Transform bullettrans = onlyonebullet[7].GetComponent<Transform>();
        bullet bullet = bullettrans.GetComponent<bullet>();
        dirtrans = bullettrans;
        bullet.elementnum = 0;
        bullet.multiple = pl.gm.PS.multiple[15]; ;
        bullet.notfalse = true;
        bullet.staytime = 10;
        bullet.staydmg = true;
        elementEF();

        bullettrans.parent = weaponpool.transform;
        bullettrans.position = new Vector3(0, 0, 0);
        bullet.init(bullet.mindmg, bullet.maxdmg, -1, Vector3.zero);
        StartCoroutine(fire5());
        StartCoroutine(firecolldown5());
    }
    IEnumerator fire5()
    {
        yield return new WaitForSeconds(0.5f);
        while (onlyonebullet[7].activeSelf)
        {
            int ran = Random.Range(0, 20);
            if(ran<10)
            {
                Transform bullettrans = weaponpool.Get(9).transform;
                bullet bullet = bullettrans.GetComponent<bullet>();
                dirtrans = bullettrans;
                bullet.elementnum = 0;
                bullet.multiple = pl.gm.PS.multiple[12];
                bullet.notfalse = true;
                bullet.staytime = 10;
                bullet.staydmg = true;
                elementEF();
                bullettrans.parent = weaponpool.transform;
                float ranposx = Random.Range(-13.5f, 13.5f);
                float ranposy = Random.Range(-13.5f, 13.5f);
                Vector3 ranpos = new Vector3(ranposx, ranposy, 0);
                bullettrans.position = ranpos;
                bullet.init(bullet.mindmg, bullet.maxdmg, -1, Vector3.zero);
                bullet.StartCoroutine(bullet.endtime2());
            }
            else if(ran < 19)
            {
                Transform bullettrans = weaponpool.Get(8).transform;
                bullet bullet = bullettrans.GetComponent<bullet>();
                dirtrans = bullettrans;
                bullet.elementnum = 0;
                bullet.speed = 7;
                bullet.multiple = pl.gm.PS.multiple[11];

                elementEF();
                bullettrans.parent = weaponpool.transform;
                float ranposx = Random.Range(-13.5f, 13.5f);
                Vector3 ranpos = new Vector3(ranposx, 13.5f, 0);
                bullettrans.position = ranpos;
                bullettrans.Rotate(new Vector3(0, 0, 180));

                bullet.init(bullet.mindmg, bullet.maxdmg, -1, bullettrans.up);
            }
            else if (ran < 20)
            {
                Transform bullettrans = weaponpool.Get(10).transform;
                bullet bullet = bullettrans.GetComponent<bullet>();
                dirtrans = bullettrans;
                bullet.elementnum = 0;
                bullet.multiple = pl.gm.PS.multiple[13];
                elementEF();

                bullettrans.parent = weaponpool.transform;
                float ranposx = Random.Range(-13.5f, 13.5f);
                float ranposy = Random.Range(-13.5f, 13.5f);
                Vector3 ranpos = new Vector3(ranposx, ranposy, 0);
                bullettrans.position = ranpos;
                bullet.init(bullet.mindmg, bullet.maxdmg, -1, Vector3.zero);
            }
            yield return new WaitForSeconds(pl.gm.PS.count[15]*0.01f);
        }
    }
    IEnumerator firecolldown5()
    {
        timer[15] = cooltime[15];
        yield return new WaitForFixedUpdate();
        while (timer[15] > 0)
        {
            timer[15] -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
    }


    public void lightningskill1()// 정전기
    {
        Vector3 targetpos = pl.rd.nearestTarget.position;
        Vector3 enemyvec = targetpos - transform.position;
        float curdiff = enemyvec.magnitude;
        Vector3 dir = enemyvec.normalized;

        if ((transform.position - targetpos).magnitude < length)
        {
            GameObject bulletobj = weaponpool.Get(13);
            Transform bullettrans = bulletobj.GetComponent<Transform>();
            bullet bullet = bullettrans.GetComponent<bullet>();
            radar1 RD = bullet.GetComponent<radar1>();
            RD.point = targetpos;
            dirtrans = bullettrans;
            bullet.elementnum = 2;
            bullet.multiple = pl.gm.PS.multiple[21];
            bullet.count = pl.gm.PS.count[21];
            bullet.plsk = this;
            elementEF();
            bullettrans.parent = weaponpool.transform;
            bullettrans.position = (targetpos+transform.position)/2;
            bulletobj.GetComponent<SpriteRenderer>().size = new Vector2(1, curdiff / 3f);
            bullet.child.GetComponent< SpriteRenderer > ().size = new Vector2(1, curdiff / 3f);
            bulletobj.GetComponent<CapsuleCollider2D>().size = new Vector2(0.5f, curdiff / 3f);


            float ang = Mathf.Atan2(enemyvec.y, enemyvec.x) * Mathf.Rad2Deg - 90;
            bullettrans.Rotate(new Vector3(0, 0, ang));

            bullet.init(bullet.mindmg, bullet.maxdmg, -1, Vector3.zero);
            bullet.StartCoroutine(bullet.lightningEF1());
            StartCoroutine(lightningcolldown1());
        }
    }
    IEnumerator lightningcolldown1()
    {
        timer[21] = cooltime[21];
        yield return new WaitForFixedUpdate();
        while (timer[21] > 0)
        {
            timer[21] -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
    }
    public void lightningskill2()//낙뢰?
    {
        Vector3 targetpos = pl.rd.nearestTarget.position;

        if ((transform.position - targetpos).magnitude < length)
        {
            GameObject bulletobj = weaponpool.Get(14);
            Transform bullettrans = bulletobj.GetComponent<Transform>();
            bullet bullet = bullettrans.GetComponent<bullet>();
            radar1 RD = bullet.GetComponent<radar1>();
            RD.point = targetpos;
            dirtrans = bullettrans;
            bullet.elementnum = 2;
            bullet.multiple = pl.gm.PS.multiple[22];
            bullet.plsk = this;
            elementEF();
            bullet bulletC = bullet.child.GetComponent<bullet>();
            bulletC.elementnum = 2;
            bulletC.multiple = pl.gm.PS.multiple[22]; ;
            bulletC.EF = EFpool;
            float dmg = pl.dmg;
            bulletC.mindmg = (dmg - (dmg * 0.5f)) + (dmg * (balance / 100));
            bulletC.maxdmg = dmg + (dmg * 0.5f);
            bulletC.percent = 30 + (pl.gm.plstat[8] / 2);



            bullettrans.parent = weaponpool.transform;
            bullettrans.position = targetpos;
            bullet.init(bullet.mindmg, bullet.maxdmg, -1, targetpos);
            bullet.StartCoroutine(bullet.lightningEF2());
            StartCoroutine(lightningcolldown2());
        }

    }
    IEnumerator lightningcolldown2()
    {
        timer[22] = cooltime[22];
        yield return new WaitForFixedUpdate();
        while (timer[22] > 0)
        {
            timer[22] -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
    }
    public void lightningskill3()// 레이저
    {
        Vector3 targetpos = pl.rd.nearestTarget.position;
        Vector3 enemyvec = targetpos - transform.position;
        float curdiff = enemyvec.magnitude;
        Vector3 dir = enemyvec.normalized;

        if ((transform.position - targetpos).magnitude < length)
        {
            GameObject bulletobj = weaponpool.Get(15);
            Transform bullettrans = bulletobj.GetComponent<Transform>();
            bullet bullet = bullettrans.GetComponent<bullet>();
            dirtrans = bullettrans;
            bullet.elementnum = 2;
            bullet.multiple = pl.gm.PS.multiple[23]; ;
            bullet.plsk = this;
            bullet.count = 100;
            elementEF();
            bullettrans.parent = weaponpool.transform;
            bullettrans.position = transform.position;

            float ang = Mathf.Atan2(enemyvec.y, enemyvec.x) * Mathf.Rad2Deg - 90;
            bullettrans.Rotate(new Vector3(0, 0, ang));

            bullet.init(bullet.mindmg, bullet.maxdmg, -1, Vector3.zero);
            bullet.StartCoroutine(bullet.lightningEF3());
            StartCoroutine(lightningcolldown3());
        }
    }
    IEnumerator lightningcolldown3()
    {
        timer[23] = cooltime[23];
        yield return new WaitForFixedUpdate();
        while (timer[23] > 0)
        {
            timer[23] -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
    }
    public void lightningskill4()// 정전기
    {
        Vector3 targetpos = pl.rd.nearestTarget.position;
        Vector3 enemyvec = targetpos - transform.position;
        float curdiff = enemyvec.magnitude;
        Vector3 dir = enemyvec.normalized;
        if(onlyonebullet[8] == null || !onlyonebullet[8].activeSelf)
        {
            pl.dashcount = 0;
            pl.speed = 1;
            onlyonebullet[8] = weaponpool.Get(16);
            Transform bullettrans = onlyonebullet[8].GetComponent<Transform>();
            bullet bullet = bullettrans.GetComponent<bullet>();
            radar1 RD = bullet.GetComponent<radar1>();
            RD.point = targetpos;
            dirtrans = bullettrans;
            bullet.elementnum = 2;
            bullet.multiple = pl.gm.PS.multiple[24]; ;
            bullet.plsk = this;
            elementEF();
            bullettrans.parent = transform;
            bullettrans.position = transform.position;

            bullet.init(bullet.mindmg, bullet.maxdmg, -1, Vector3.zero);
            StartCoroutine(lightning4EF());
            StartCoroutine(mana24());
        }
        else
        {
            pl.dashcount = 2;
            pl.speed = 2;
            onlyonebullet[8].SetActive(false);
            StartCoroutine(lightningcolldown4());
        }
    }
    IEnumerator lightningcolldown4()
    {
        timer[24] = cooltime[24];
        yield return new WaitForFixedUpdate();
        while (timer[24] > 0)
        {
            timer[24] -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
    }
    IEnumerator mana24()
    {
        yield return new WaitForFixedUpdate();
        while (onlyonebullet[8].activeSelf)
        {
            if (pl.gm.pl.mana < pl.gm.PS.mana[24])
            {
                onlyonebullet[8].SetActive(false);
                pl.gm.bt.StartCoroutine(pl.gm.bt.mananull());
            }
           else
                pl.gm.pl.mana -= pl.gm.PS.mana[24];

            yield return new WaitForSeconds(1);
        }
    }
    IEnumerator lightning4EF()
    {
        yield return new WaitForFixedUpdate();
        while (onlyonebullet[8].activeSelf)
        {
           
            targetrader[0].SetActive(true);
            Transform bullettrans = targetrader[0].GetComponent<Transform>();
            bullet bullet = bullettrans.GetComponent<bullet>();
            radar1 RD = bullet.GetComponent<radar1>();
            bullet.elementnum = 2;
            bullet.multiple = pl.gm.PS.multiple[24]; ;
            bullet.plsk = this;
            dirtrans = bullettrans;
            elementEF();
            bullet.init(bullet.mindmg, bullet.maxdmg, -1, Vector3.zero);
            while (pl.rd.nearestTarget != null &&
                (transform.position - pl.rd.nearestTarget.position).magnitude < length)
            {
                Vector3 targetpos = pl.rd.nearestTarget.position;
                Vector3 enemyvec = targetpos - transform.position;
                float curdiff = enemyvec.magnitude;
                RD.point = targetpos;
                
                bullettrans.position = (targetpos + transform.position) / 2;
                targetrader[0].GetComponent<SpriteRenderer>().size = new Vector2(1, curdiff / 3f);
                bullet.child.GetComponent<SpriteRenderer>().size = new Vector2(1, curdiff / 3f);
                targetrader[0].GetComponent<CapsuleCollider2D>().size = new Vector2(0.5f, curdiff / 3f);
                float ang = Mathf.Atan2(enemyvec.y, enemyvec.x) * Mathf.Rad2Deg - 90;
                bullettrans.rotation = Quaternion.Euler(new Vector3(0, 0, ang));
                bullet.StartCoroutine(bullet.lightning4EF());
                yield return new WaitForSeconds(0.1f);
            }

            for (int i = 0; i < targetrader.Length; i++)
                targetrader[i].SetActive(false);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.1f);

        if (pl.gm.PS.mana[24] > pl.gm.pl.mana)
            pl.gm.bt.StartCoroutine(pl.gm.bt.mananull());

        for (int i = 0; i < targetrader.Length; i++)
            targetrader[i].SetActive(false);
    }
    public void lightningskill5()// 썬더스톰
    {
        Vector3 targetpos = pl.rd.nearestTarget.position;
        if (onlyonebullet[9] == null || !onlyonebullet[9].activeSelf)
        {
            onlyonebullet[9] = weaponpool.Get(18);
            Transform bullettrans = onlyonebullet[9].GetComponent<Transform>();
            bullet bullet = bullettrans.GetComponent<bullet>();
            radar1 RD = bullet.GetComponent<radar1>();
            RD.point = targetpos;
            dirtrans = bullettrans;
            bullet.elementnum = 2;
            bullet.multiple = pl.gm.PS.multiple[25];
            bullet.plsk = this;
            elementEF();
            bullettrans.parent = weaponpool.transform; ;
            bullettrans.position = targetpos;

            bullet.init(bullet.mindmg, bullet.maxdmg, -1, Vector3.zero);
            StartCoroutine(lightningcolldown5());
            StartCoroutine(lightning5EF());
        }
        IEnumerator lightningcolldown5()
        {
            timer[25] = cooltime[25];
            bullet bullet = onlyonebullet[9].GetComponent<bullet>();
            bullet.LT[0].color = new Color(1, 1, 1, 1);
            bullet.LT[1].color = new Color(1, 1, 1,1);
            bullet.LT[2].color = new Color(1, 1, 1, 1);
            bullet.LT[3].color = new Color(1, 0.9f, 0.8f, 0.5f);
            for (int i = 0; i < 16; i++)
            {
                Debug.Log(i);
                yield return new WaitForSeconds(1);
            }
            for (int i = 80; i > 0; i--)
            {
                bullet.LT[0].color = new Color(1, 1, 1, i / 80f);
                bullet.LT[1].color = new Color(1, 1, 1, i / 80f);
                bullet.LT[2].color = new Color(1, 1, 1, i / 80f);
                bullet.LT[3].color = new Color(1, 0.9f, 0.8f, i / 160f);
                yield return new WaitForSeconds(1 / 5f);
            }
            onlyonebullet[9].SetActive(false);
            yield return new WaitForFixedUpdate();
            while (timer[25] > 0)
            {
                timer[25] -= Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
        }
        IEnumerator lightning5EF()
        {
            yield return new WaitForFixedUpdate();
            while (onlyonebullet[9].activeSelf)
            {
                onlyonebullet[10] = weaponpool.Get(19);
                GameObject bulletobj = onlyonebullet[10];
                Transform bullettrans = bulletobj.GetComponent<Transform>();
                bullet bullet = bullettrans.GetComponent<bullet>();
                bullet.elementnum = 2;
                bullet.multiple = pl.gm.PS.multiple[25]; ;
                bullet.plsk = this;
                dirtrans = bullettrans;
                elementEF();
                bullet.init(bullet.mindmg, bullet.maxdmg, -1, Vector3.zero);
                radar1 bulletrader = onlyonebullet[9].GetComponent<radar1>();
                while (onlyonebullet[9].activeSelf&&
                    bulletrader.nearestTarget != null &&
                    (onlyonebullet[9].transform.position - bulletrader.nearestTarget).magnitude < bulletrader.scanrange)
                {
                    bullet.LT[0].color = new Color(1, 1, 1, 1);
                    bullet.LT[1].color = new Color(1, 1, 1, 1);
                    Vector3 targetpos = bulletrader.nearestTarget;
                    Vector3 enemyvec = targetpos - onlyonebullet[9].transform.position;

                    bullettrans.position = onlyonebullet[9].transform.position;
                    float ang = Mathf.Atan2(enemyvec.y, enemyvec.x) * Mathf.Rad2Deg - 90;
                    bullettrans.rotation = Quaternion.Euler(new Vector3(0, 0, ang));
                    yield return new WaitForSeconds(0.1f);
                }
                if(bullet.LT[0].color.a == 1)
                    for (int i = 80; i > 0; i--)
                    {
                        bullet.LT[0].color = new Color(1, 1, 1, i / 80f);
                        bullet.LT[1].color = new Color(1, 1, 1, i / 80f);

                        yield return new WaitForSeconds(1 / 90f);
                    }
                bulletobj.SetActive(false);
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(0.1f);
            bullet bulletf = onlyonebullet[10].GetComponent<bullet>();
            for (int i = 80; i > 0; i--)
            {
                bulletf.LT[0].color = new Color(1, 1, 1, i / 80f);
                bulletf.LT[1].color = new Color(1, 1, 1, i / 80f);

                yield return new WaitForSeconds(1 / 90f);
            }
            onlyonebullet[10].SetActive(false);
        }
    }
}

