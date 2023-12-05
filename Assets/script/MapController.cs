using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MapController : MonoBehaviour
{
    int[] mapint = new int[32]; //0~2선택/3~6/7~9/10~13/14~16/17~20/21~23/24~26/27~30/31
    public Image[] mapimg;
    int[] choicemapint = new int[2];
    public Gamemanager gm;

    public void Start()
    {
        for (int i = 0; i < mapint.Length; i++)
        {
            if (i == mapint.Length - 1)
            {
                mapint[i] = 5;
            }
               
            else
            {
                int ran = Random.Range(0, 100);
                if (ran > 90)
                    mapint[i] = 2;
                else if (ran > 70)
                    mapint[i] = 3;
                else if (ran > 50)
                    mapint[i] = 4;
                else
                    mapint[i] = 1;
            }
        }
    }

    public void fastSelect()
    {
        Map Mapinfor = EventSystem.current.currentSelectedGameObject.GetComponent<Map>();
        if (Mapinfor.mapnum > 2)
            return;

        maptype(Mapinfor.maptypeint);

        //map 선택 번호 주기
        choicemapint[0] = Mapinfor.mapnum + 3;
        choicemapint[1] = Mapinfor.mapnum + 4;
    }
    public void anotherfloorSelect()
    {
        Map Mapinfor = EventSystem.current.currentSelectedGameObject.GetComponent<Map>();
        if (Mapinfor.mapnum != choicemapint[0] && Mapinfor.mapnum != choicemapint[1])// 예외 번호 확인
            return;


        maptype(Mapinfor.maptypeint);

        //map 선택 번호 주기
        if (Mapinfor.mapnum == 3 || Mapinfor.mapnum == 10 || Mapinfor.mapnum == 17 || Mapinfor.mapnum == 24)
        {
            choicemapint[0] = -1;
            choicemapint[1] = Mapinfor.mapnum + 4;
        }
        else if (Mapinfor.mapnum == 6 || Mapinfor.mapnum == 13 || Mapinfor.mapnum == 20 || Mapinfor.mapnum == 27)
        {
            choicemapint[0] = Mapinfor.mapnum + 3;
            choicemapint[1] = -1;
        }
        else if (Mapinfor.mapnum == 28 || Mapinfor.mapnum == 29 || Mapinfor.mapnum == 30)
        {
            choicemapint[0] = 31;
            choicemapint[1] = -1;
        }
        else
        {
            choicemapint[0] = Mapinfor.mapnum + 3;
            choicemapint[1] = Mapinfor.mapnum + 4;
        }
    }

    public void maptype(int maptyep)
    {
        switch(maptyep)
        {
            case 1://일반
                gm.Startstage();
                break;
            case 2://엘리트
                gm.Startstage();// 수정해서 엘리트면 엘리트 몹 나오게!!
                break;
            case 3: //상점
                gm.shop();
                break;
            case 4: //???
                gm.unknown();
                break;
            case 5: //보스
                gm.boss();
                break;
        }
    }

}
