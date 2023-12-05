using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiinformation : MonoBehaviour
{
    public Gamemanager gm;

    //받을 값!
    public int element;
    public int skillnum;
    public int skillpos;



    public Transform setpos;
    public GameObject EFobj;
    public Text skillEF;
    public Text skillEF1;
    public Text skillstatint;

    // 각 스킬 정보
    public uiinformation[] skills;
    public Image[] skillnotzero;
    public Color[] imcolor;
    public GameObject requirements;
    public int skillstat;
    public int needLv;
    public float[] multiple;
    public float[] ManaConsumption;
    public float[] cooltime;
    public int[] count;
    public bool canup;
    //슬롯 관리자
    public Image[] Slotim;


    void Awake()
    {
        gm = transform.root.GetComponent<joystick>().gm;

        if (EFobj != null)
        {
            skillEF = EFobj.GetComponent<Text>();
            skillEF1 = EFobj.transform.GetChild(0).gameObject.GetComponent<Text>();
            skillstatint = EFobj.transform.GetChild(5).gameObject.GetComponent<Text>();
        }

    }
    public void skillinformation()
    {
        for (int i = 0; i < skills.Length; i++)
        {
            if(i >= 1)
            {
                if(skills[i-1].skillstat < 3 || gm.plstat[0] < skills[i].needLv)
                {
                    skills[i].skillnotzero[0].color = imcolor[0];
                    skills[i].skillnotzero[1].color = new Color(1,1,1,0.5f);
                    skills[i].requirements.SetActive(true);
                    skills[i]. canup = false;
                }
                else if (skills[i].skillstat < 1)
                {
                    skills[i].skillnotzero[0].color = imcolor[0];
                    skills[i].skillnotzero[1].color = new Color(1, 1, 1, 0.5f);
                    skills[i].requirements.SetActive(false);
                    skills[i].canup = true;
                }
                else
                {
                    skills[i].skillnotzero[0].color = imcolor[1];
                    skills[i].skillnotzero[1].color = new Color(1, 1, 1, 1f);
                    skills[i].requirements.SetActive(false);
                    skills[i]. canup = true;
                }
            }
            skills[i].skillEF.text = 
                skills[i].multiple[skills[i].skillstat]
                + "\n" + skills[i].cooltime[skills[i].skillstat];
            skills[i].skillEF1.text = skills[i].ManaConsumption[skills[i].skillstat]
                + "\n"+ skills[i].count[skills[i].skillstat];
            skills[i].skillstatint.text = "+" + skills[i].skillstat;
        }
    }
}
