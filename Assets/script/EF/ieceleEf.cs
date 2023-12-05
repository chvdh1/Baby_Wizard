using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ieceleEf : MonoBehaviour
{
    SpriteRenderer SP;
    public float ingtime;
    public float endtime;
    public Sprite[] im;

    private void Awake()
    {
        SP = GetComponent<SpriteRenderer>();
    }

    public  IEnumerator ing()
    {
        yield return new WaitForFixedUpdate();
        while(ingtime > 0)
        {
            SP.sprite = im[0];
            SP.color = new Color(1, 1, 1, 0.5f);
            ingtime -= 1;
            yield return new WaitForSeconds(1);
        }
        yield return new WaitForFixedUpdate();
        SP.sprite = im[1];
        for (int i = 60; i > 0; i--)
        {
            SP.color = new Color(1, 1, 1, i/60f);
            yield return new WaitForSeconds(1/40);
        }
        gameObject.SetActive(false);
    }
}
