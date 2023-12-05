using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uianim : MonoBehaviour
{
    public GameObject[] obj;
    Gamemanager gm;
    private void Awake()
    {
        gm = Gamemanager.gamemng;
    }
    public void objin()
    {
        for(int  i = 0;i< obj.Length;i++)
            obj[i].SetActive(true);
    }
    public void objout()
    {
        for (int i = 0; i < obj.Length; i++)
            obj[i].SetActive(false);
    }
    public void battelstart()
    {
        gm.StartCoroutine(gm.Spawntart());
    }


}
