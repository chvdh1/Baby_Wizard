using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class joystick : MonoBehaviour
{
    // public datamanager data;
    public Gamemanager gm;
    public GameObject bigsmall;
    public GameObject small;
    public GameObject big;
    Vector3 stickfirstpos;
    public Vector3 joyvec;
    float stickradius;
    public Vector3 depos;

    void Start()
    {
        if(bigsmall != null)
        stickradius = big.gameObject.GetComponent<RectTransform>().sizeDelta.y / 2;

    }

    public void pointdown()
    {
        big.SetActive(true);
        small.SetActive(true);
        big.transform.position = Input.mousePosition;
        small.transform.position = Input.mousePosition;
        stickfirstpos = Input.mousePosition;
    }
    public void drag(BaseEventData baseEventData)
    {
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        Vector3 dragpos = pointerEventData.position;
        joyvec = (dragpos - stickfirstpos).normalized;

        float stickdis = Vector3.Distance(dragpos, stickfirstpos);

        if (stickdis < stickradius)
        {
            small.transform.position = stickfirstpos + joyvec * stickdis;
        }
        else
            small.transform.position = stickfirstpos + joyvec * stickradius;
    }
    public void drop()
    {
        big.transform.position = depos;
        small.transform.position = depos;
        stickfirstpos = depos;
        joyvec = Vector3.zero;
        big.SetActive(false);
        small.SetActive(false);
    }

}
