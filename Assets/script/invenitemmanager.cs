using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class invenitemmanager : MonoBehaviour
{
    public invenitem[] itme;

    private void Awake()
    {
        for (int i = 0; i < itme.Length; i++)
        {
            itme[i].itemnum = i;
            switch (i)
            {
                case < 8:
                    itme[i].itemsubname = "���ϱ� ";
                    itme[i].Rating = 1;
                    break;
                case <16:
                    itme[i].itemsubname = "�ϱ� ";
                    itme[i].Rating = 2;
                    break;
                case <24:
                    itme[i].itemsubname = "����� ";
                    itme[i].Rating = 3;
                    break;
                case < 32:
                    itme[i].itemsubname = "��� ";
                    itme[i].Rating = 4;
                    break;
                case < 40:
                    itme[i].itemsubname = "�ֻ�� ";
                    itme[i].Rating = 5;
                    break;
                case < 48:
                    itme[i].itemsubname = "������ ";
                    itme[i].Rating = 6;
                    break;
                case < 56:
                    itme[i].itemsubname = "������ ";
                    itme[i].Rating = 7;
                    break;
            }
        }
    }
}
