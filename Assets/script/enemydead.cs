using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemydead : MonoBehaviour
{
    public GameObject[] enemyobj;
    public int num;
    public Sprite[] id;
    public Sprite[] hit;
    SpriteRenderer SP;
    private void Awake()
    {
        SP = GetComponent<SpriteRenderer>();
    }
    public void idSP()
    {
        switch(num)
        {
            case 1:
                SP.sprite = id[0];
                break;
            case 2:
                SP.sprite = id[1];
                break;
            case 3:
                SP.sprite = id[2];
                break;
            case 4:
                SP.sprite = id[3];
                break;
            case 5:
                SP.sprite = id[4];
                break;
        }
    }
    public void hitSP()
    {
        switch (num)
        {
            case 1:
                SP.sprite = hit[0];
                break;
            case 2:
                SP.sprite = hit[1];
                break;
            case 3:
                SP.sprite = hit[2];
                break;
            case 4:
                SP.sprite = hit[3];
                break;
            case 5:
                SP.sprite = hit[4];
                break;
        }
    }

    public void dead()
    {
        for (int i = 0; i < enemyobj.Length; i++)
        {
            enemyobj[i].SetActive(false);
        }
    }
}
