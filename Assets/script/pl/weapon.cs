using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    public poolmanager weaponpool;
    public poolmanager EFpool;
    player pl;
    public int id;
    public int elementnum;
    public int prefebid;
    public float balance;
    public float mana;
    public float percentage;
    public int count;
    public float speed;
    public float length;
    public float coll;


    public bool spin;
    public float mindmg, maxdmg;

    float timer;
    float atkspeed;
    Transform dirtrans;

    private void Awake()
    {
        pl = GetComponentInParent<player>();
    }
    void Start()
    {
        init();
    }
    void Update()
    {
        switch (id)
        {
            case 1:
                if (spin)
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;
            case 2:
                
                timer += Time.deltaTime*(1+ atkspeed);

                if(timer >speed)
                    fire();
                break;
        }

    }
    void elementEF()
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
        }
        else if (bulletlo.elementnum == 1)//얼음
        {
            bulletlo.mindmg = ((dmg - (dmg * 0.1f)) - ((dmg - (dmg * 0.1f)) * 0.1f))
                + ((dmg - (dmg * 0.1f)) * 0.2f * (balance / 100));
            bulletlo.maxdmg = (dmg - (dmg * 0.1f)) + ((dmg - (dmg * 0.1f)) * 0.1f);
        }
        else if (bulletlo.elementnum == 2)//전기
        {
            bulletlo.mindmg = (dmg - (dmg * 0.5f)) + (dmg * (balance / 100));
            bulletlo.maxdmg = dmg + (dmg * 0.5f);
        }
        atkspeed = pl.gm.plstat[10] * 0.1f > 1 ? 1 : pl.gm.plstat[10] * 0.1f;
    }
    void init()
    {
        switch(id)
        {
            case 0:
                speed = 150;
                batch();
                break;
            case 1: // 기본공격
                speed = 0.3f;
                break;
        }
    }
    void batch()
    {
        for (int i = 0; i < count; i++)
        {
            Transform bullet =
                weaponpool.Get(prefebid).transform;

            dirtrans = bullet;
            bullet.parent = transform;

            Vector3 rotvec = Vector3.forward * 360 * i / count;
            bullet.Rotate(rotvec);
            bullet.Translate(bullet.up * 1.5f, Space.World);
           

            bullet.GetComponent<bullet>().init(mindmg,maxdmg, -1,Vector3.zero);//-1은 무한관통.
            bullet bulletlo = bullet.GetComponent<bullet>();
            bulletlo.elementnum = 2;
            bulletlo.multiple = 0.8f;
            elementEF();

        }
    }
    void fire()
    {
        // 나가는 총알 나중에 확인하고 넣기

        if (pl.rd.nearestTarget == null )
            return;
        int ran = Random.Range(0, 3);
        Vector3 targetpos = pl.rd.nearestTarget.position;
        Vector3 dir = targetpos - transform.position;
        dir = dir.normalized;

        if((transform.position - targetpos).magnitude < length)
        {
            timer = 0;
            Transform bullet = weaponpool.Get(ran).transform;
            dirtrans = bullet;
            bullet.parent = weaponpool.transform;
            bullet.position = transform.position;
            bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
            bullet bulletlo = bullet.GetComponent<bullet>();
            bulletlo.init(mindmg, maxdmg, count, dir);
            bulletlo.elementnum = ran;
            bulletlo.multiple = 1;
            elementEF();
        }
    }
    IEnumerator colldown()
    {
        timer = coll;
        while(timer>0)
        {
            timer += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
    }
}
