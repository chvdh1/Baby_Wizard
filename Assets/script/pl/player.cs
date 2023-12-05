using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class player : MonoBehaviour
{
    public float HP, maxHP,mana,maxmana, speed,dmg;//대쉬 정보는 plskill에서
    public int coin,dashcount;


    public bool stopt, stopb, stopl, stopr, isdead,isdash, ishit;

    public GameObject plmesh,eyes, pltxt;
    public radar rd;
    public Rigidbody2D rg;
    public Collider2D box;
    public Animator anim;
    public Vector2 inputvec;
    public Gamemanager gm;
    public joystick joy;
    public Vector2 dirvec;

   
    void Update()
    {
        if (isdead)
            return;
        if (!isdead)
        {
            inputvec = joy.joyvec;

            if (mana < maxmana)
                mana += Time.deltaTime * gm.plstat[14];
            else
                mana = maxmana;
            if (HP < maxHP)
                HP += Time.deltaTime * gm.plstat[13];
            else
                HP = maxmana;


            // inputvec.x = Input.GetAxisRaw("Horizontal");
            // inputvec.y = Input.GetAxisRaw("Vertical");

            if ((stopr && inputvec.x > 0) || (stopl && inputvec.x < 0))
                inputvec.x = 0;
            if ((stopt && inputvec.y > 0) || (stopb && inputvec.y < 0))
                inputvec.y = 0;
            if (inputvec.x != 0 || inputvec.y != 0)
                dirvec = inputvec.normalized;

            if (!isdash)
                move();

            if ((Input.GetKeyDown(KeyCode.Z)))
                Time.timeScale++;
            if ((Input.GetKeyDown(KeyCode.C)))
                Time.timeScale = 1;
        }
    }

    void move()
    {
        if (inputvec.x > 0)
            plmesh.transform.localScale = new Vector2(-1, 1);
        else if (inputvec.x < 0)
            plmesh.transform.localScale = new Vector2(1, 1);

        float spup = gm.plstat[11] * 0.2f > 2 ? 2 : gm.plstat[11] * 0.2f;
        float move = (inputvec.y == 0 && inputvec.x == 0) ? 0 : speed+ spup;
        
        anim.SetFloat("speed", move);

        Vector2 pos = transform.position;

        rg.transform.position = pos+inputvec.normalized * speed * Time.deltaTime;
    }
    public IEnumerator dash()
    {
        dashcount--;
        isdash = true;
       
        float time = 0;

        anim.SetTrigger("dash");
        float spup = gm.plstat[11] * 0.2f > 2 ? 2 : gm.plstat[11] * 0.2f;
        while (time<0.45f)
        {
            time += Time.deltaTime;
            Vector2 pos = transform.position;
            if ((stopr || stopl))
                dirvec.x = 0;
            if ((stopt||stopb))
                dirvec.y = 0;
            transform.position = pos + dirvec * (5+ spup) * Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForFixedUpdate();
        isdash = false;
        dashcount=2;
        gm.PS.timer[0] = gm.PS.cooltime[0];
        gm.PS.StartCoroutine(gm.PS.dashcoll());

    }

    public void dead()
    {
        Time.timeScale = 0;
        isdead = true;
        anim.SetTrigger("dead");
        gm.sfanim.SetTrigger("sfout");
        gm.bt.startbt.SetActive(true);
    }

   

}
