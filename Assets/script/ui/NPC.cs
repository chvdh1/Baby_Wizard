using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NPC : MonoBehaviour
{
    [SerializeField] int npcnum;
    [SerializeField] string npctolk;
    Rigidbody2D rg;
   public  Gamemanager gm;

    public GameObject tolk;
    public Text tolktext;



    private void Awake()
    {
        rg = GetComponent<Rigidbody2D>();
        switch(npcnum)
        {
            case 1:
                npctolk = "스테이지 이동";
                break;
            case 2:
                npctolk = "은인";
                break;
            case 3:
                npctolk = "상점";
                break;
            case 4:
                npctolk = "보석 생성기";
                break;
            case 5:
                npctolk = "우편함";
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10 && collision.gameObject.tag == "Player")
        {
            tolk.SetActive(true);
            tolktext.text = npctolk;
            gm.changebt.SetTrigger("in");
            StartCoroutine(tolkfollow());
            StartCoroutine(changebtin());
            gm.npcnum = npcnum;
        }
    }
    IEnumerator tolkfollow()
    {
        while(tolk.activeSelf)
        {
            tolk.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 1.5f, 0));
            yield return new WaitForFixedUpdate();
        }
    }
    IEnumerator changebtin()
    {
        yield return new WaitForSeconds(0.5f);
        gm.startuidelobj[1].SetActive(false);
        gm.startuidelobj[2].SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10 && collision.gameObject.tag == "Player")
        {
            tolk.SetActive(false);
            gm.changebt.SetTrigger("out");
            StartCoroutine(changebtout());
            gm.npcnum = -1;
        }
    }
    IEnumerator changebtout()
    {
        yield return new WaitForSeconds(0.5f);
        gm.startuidelobj[2].SetActive(false);
        gm.startuidelobj[1].SetActive(true);
    }
}
