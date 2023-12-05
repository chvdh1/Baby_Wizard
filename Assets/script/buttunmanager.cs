using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class buttunmanager : MonoBehaviour
{
    public Gamemanager gm;
    public GameObject startbt;
    public Text HPtxt, timetxt, enemytxt, cointxt, stageint, MPtxt , needmana;
    public bool test;

    public int skillnum, skillcoolnum;
    public Image[] skillbtSP;
    public Sprite[] skillbtSPchange;

    public GameObject dirbt, beforebt;
    [Header("�Ͻ�����")]
    public GameObject[] Pause; // ����â, ����, �ڵ�â
    public Text[] code;

   [Header("��ųƮ��")]

    public uiinformation[] skills;
    public int[] skillint;
    public int ClickSkillNum,ClickSlotNum;
    public GameObject[] SKelement;
    public GameObject choicebt,statbt, registrationbt,chickicon;
    public GameObject[] numchoicebt;

    [Header("statâ")]
    public GameObject[] statobj;
    public GameObject[] questionobj;
    public Text[] stat; //��,HP,MP, dmg, ��Ÿ�, �뷱��, ��,��,��, ��,��,ü,ȸ
    int questionnum;
    invenpos inpos;
    [Space(10f)]

    [Header("npc")]
    public GameObject[] chapterobj;
    public Text[] Specificityuitext1;
    public Text[] Specificityuitext2;
    public Text[] Specificityuitext3;
    /**[TextArea]
    public string[] nextTolk;
    public GameObject Tolkingfade, nextTolkim;
    public Text tolktext;
    public Image NpcImg;**/


    //�����ư�� ���� ����
    public GameObject[] uiobj;

    public void stagestart() // ���۹�ư
    {
        gm.playerobj.transform.GetChild(0).gameObject.SetActive(false);
        gm.playerobj.transform.GetChild(1).gameObject.SetActive(false);
        for (int i =0;i< gm.enemypool.transform.childCount;i++)
            gm.enemypool.transform.GetChild(i).gameObject.SetActive(false);        
        gm.Startstage();
    }


    //�����ư
    public void exitbt()
    {
        beforebt = null;
        dirbt = null;
        Time.timeScale = 1;
        for (int i = 0; i < uiobj.Length; i++)
        {
            uiobj[i].SetActive(false);
        }
        for (int i = 0; i < gm.inven.Length; i++)
        {
            if (gm.invenitemnum[i] != -1)
            {
                gm.items[gm.invenitemnum[i]].SetActive(false);
            }
        }

    }

    //�Ͻ����� ��ư
    public void menubt()
    {
        Time.timeScale = 0;
        uiobj[0].SetActive(true);
        uiobj[1].SetActive(true);
        Pause[0].SetActive(true);
        Pause[1].SetActive(false);
        Pause[2].SetActive(false);
    }
    public void gameoverbt()//���� ��ư
    {
        Time.timeScale = 1;
        Application.Quit();
    }
    public void lobbybt()//�κ�
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("lobby");
    }
    public void settingbt()//����
    {
        Pause[0].SetActive(false);
        Pause[1].SetActive(true);
        Pause[2].SetActive(false);
    }
    public void codebt()//�ڵ��Է�
    {
        Pause[0].SetActive(false);
        Pause[1].SetActive(false);
        Pause[2].SetActive(true);
    }
    public void codeck()//�ڵ�Ȯ�� (�̿ϼ�)
    {
        if (code[1].text.ToString().Equals("�ȳ��ϼ���?"))
        {
            //  pl.soul += 2000;
            code[0].text = "�ҿ� 2000�� �߰�! �Է¿Ϸ�!";
        }
        else if (code[1].text.ToString().Equals("�̰Եǳ�"))
        {
           // pl.soul += 4000;
            code[0].text = "�ҿ� 4000�� �߰�!�Է¿Ϸ�!";
        }
        else
        {
            code[0].text = "�ٽ� �� �� Ȯ���ϼ���!";
        }
        StartCoroutine(txtf());
        // data.jsondata.Save();
    }
    IEnumerator txtf()
    {
        menubt();
        yield return new WaitForSecondsRealtime(2);
        code[0].text = " ";
    }

    //��ųƮ��
    public void skilltreebt()
    {
        uiobj[0].SetActive(true);
        uiobj[1].SetActive(true);
        uiobj[2].SetActive(true);
        uiinformation uiinformation = 
            EventSystem.current.currentSelectedGameObject.GetComponent<uiinformation>();
        uiinformation iecinformation = uiinformation.skills[0];
        iecinformation.skillinformation();
        for (int i = 0; i < SKelement.Length; i++)
        {
            if (i == 1)
                SKelement[i].SetActive(true);
            else
                SKelement[i].SetActive(false);
        }
        dirbt = null;
    }
    public void skillelementnum() //�Ӽ�����
    {
        uiinformation uiinformation = 
            EventSystem.current.currentSelectedGameObject.GetComponent<uiinformation>();
       
        for (int i = 0; i < SKelement.Length; i++)
        {
            if (uiinformation.element == i)
                SKelement[i].SetActive(true);
            else
                SKelement[i].SetActive(false);
        }
        uiinformation.skillinformation();
    }
    public void skillnumbt() //��ų����
    {
        if (dirbt != null)
            beforebt = dirbt;
        dirbt = EventSystem.current.currentSelectedGameObject;
        if (dirbt == beforebt)
        {
            choicebt.SetActive(false);

            uiinformation beforeui = beforebt.GetComponent<uiinformation>();
            beforeui.skillEF.text = beforeui.multiple[beforeui.skillstat]
                + "\n" + beforeui.cooltime[beforeui.skillstat];
            beforeui.skillEF1.text = beforeui.ManaConsumption[beforeui.skillstat]
                + "\n" + beforeui.count[beforeui.skillstat];
            beforeui.skillstatint.text = "+" + (beforeui.skillstat);
            beforebt = null;
            dirbt = null;
        }
        else
        {
            if ((beforebt != null))
            {
                uiinformation beforeui = beforebt.GetComponent<uiinformation>();
                if (beforeui.skillEF != null)
                    beforeui.skillEF.text = beforeui.multiple[beforeui.skillstat]
                        + "\n" + beforeui.cooltime[beforeui.skillstat];
                if (beforeui.skillEF1 != null)
                    beforeui.skillEF1.text = beforeui.ManaConsumption[beforeui.skillstat]
                        + "\n" + beforeui.count[beforeui.skillstat];
                if (beforeui.skillstatint != null)
                    beforeui.skillstatint.text = "+" + (beforeui.skillstat);
            }
            uiinformation uiinformation = dirbt.GetComponent<uiinformation>();
            if (uiinformation.canup)
            {
                choicebt.SetActive(true);
                Image im = dirbt.GetComponent<Image>();
                choicebt.GetComponent<Image>().color = im.color;
                choicebt.transform.SetParent(dirbt.transform);
                choicebt.transform.position = uiinformation.setpos.position;
                skillexplanation();
            }
        }
    }
    public void skillexplanation()//����
    {
        uiinformation uiinformation = dirbt.GetComponent<uiinformation>();
        if (gm.PS.skillpoint <= 0 || gm.PS.skillstat[uiinformation.skillnum] >= 5)
            statbt.SetActive(false);
        else
        {
            statbt.SetActive(true);
            uiinformation.skillEF.text = uiinformation.multiple[uiinformation.skillstat+1]+ "��"
                + "\n" + uiinformation.cooltime[uiinformation.skillstat+1]+ "��";
            uiinformation.skillEF1.text = uiinformation.ManaConsumption[uiinformation.skillstat+1]+ "��"
                + "\n" + uiinformation.count[uiinformation.skillstat+1]+ "��";
            uiinformation.skillstatint.text = "+" + (uiinformation.skillstat+1)+ "��";
        }
        if (gm.PS.skillstat[uiinformation.skillnum] <= 0)
            registrationbt.SetActive(false);
        else
        {
            registrationbt.SetActive(true);
        }
    }
    public void StatPlus() //����+
    {
        if (gm.PS.skillpoint <= 0)
            return;
        uiinformation uiinformation = dirbt.GetComponent<uiinformation>();
        uiinformation.skillstat++;
        gm.skilleleint[uiinformation.element]++;
        gm.PS.skillpoint--; 
        gm.PS.skillstat[uiinformation.skillnum] = uiinformation.skillstat;
        choicebt.SetActive(false);
        uiinformation.skillEF.text = uiinformation.multiple[uiinformation.skillstat]
            + "\n" + uiinformation.cooltime[uiinformation.skillstat];
        uiinformation.skillEF1.text = uiinformation.ManaConsumption[uiinformation.skillstat]
            + "\n" + uiinformation.count[uiinformation.skillstat];
        uiinformation.skillstatint.text = "+" + (uiinformation.skillstat);
        dirbt = null;
        gm.txt();
        StartCoroutine(skillupdate());
    }
    public IEnumerator skillupdate()
    {
        for (int i = 0; i < skills.Length; i++)
        {
            gm.PS.multiple[skills[i].skillnum] = skills[i].multiple[skills[i].skillstat];
            gm.PS.cooltime[skills[i].skillnum] = skills[i].cooltime[skills[i].skillstat];
            gm.PS.mana[skills[i].skillnum] = skills[i].ManaConsumption[skills[i].skillstat];
            gm.PS.count[skills[i].skillnum] = skills[i].count[skills[i].skillstat];
        }
        yield return new WaitForFixedUpdate();
    }
    public void SkillRegistrationbt() //��ų��Ϲ�ư
    {
        uiinformation uiinformation = dirbt.GetComponent<uiinformation>();
        ClickSkillNum = uiinformation.skillnum;
        dirbt = EventSystem.current.currentSelectedGameObject;
        uiinformation Slotmanager = dirbt.GetComponent<uiinformation>();
        for (int i = 0; i < Slotmanager.Slotim.Length; i++)
        {
            if (skillint[i+1] == 0)
            {
                Slotmanager.Slotim[i].sprite = skillbtSPchange[15];
                Slotmanager.Slotim[i].color = new Color(0, 0, 0, 0.5f);
            }
            else
            {
                int num = skillint[i+1] > 10 ? skillint[i+1] - 5 : skillint[i+1];
                int num1 = skillint[i+1] > 20 ? num - 5 : num;
                Slotmanager.Slotim[i].sprite = skillbtSPchange[num1 - 1];
                Slotmanager.Slotim[i].color = new Color(1, 1, 1, 1);
            }
        }
        uiobj[4].SetActive(false);
        chickicon.SetActive(false);
        numchoicebt[0].SetActive(true);
    }
    public void SkillSlotbt() //��ų���Գѹ���
    {
        dirbt = EventSystem.current.currentSelectedGameObject;
        uiinformation skillpos = dirbt.GetComponent<uiinformation>();
        ClickSlotNum = skillpos.skillpos;
        chickicon.transform.position = dirbt.transform.position;
        chickicon.SetActive(true);
    }
    public void SkillRegistration() //��ų���
    {
        skillint[ClickSlotNum] = ClickSkillNum;
       
        numchoicebt[0].SetActive(false);
    }
    public void SkillDelet() //��ų����
    {
        skillint[ClickSlotNum] = 0;
        numchoicebt[0].SetActive(false);
    }

    // statâ
    public void statbtclick() // Ŭ�� and �κ�â
    {
        gm.txt();
       
        statobj[0].SetActive(true);
        uiobj[0].SetActive(true);
        gm.information[0].SetActive(false);
        gm.checkbt[0].SetActive(false);
        for (int i = 0; i < gm.inven.Length; i++)
        {
            if (gm.invenitemnum[i] != -1)
            {
                gm.items[gm.invenitemnum[i]].SetActive(true);
                gm.items[gm.invenitemnum[i]].transform.position
                    = gm.inven[i].transform.position;
            }
        }
        for (int i = 0; i < gm.spownitemnum.Length; i++)
        {
            if(gm.spownitemnum[i] != -1)
            gm.items[gm.spownitemnum[i]].SetActive(false);
        }
           
    }
    public void questionclick() // ?Ŭ�� ???�����ư�� ����
    {
        if(!statobj[1].activeSelf)
        {
            statobj[1].SetActive(true);
            questionobj[0].SetActive(true);
            for (int i = 1; i < 3; i++)
                questionobj[i].SetActive(false);
            questionnum = 0;
            questionobj[3].GetComponent<Image>().color = new Color(0.85f, 0.85f, 0.85f, 0.5f);
            questionobj[4].GetComponent<Image>().color = new Color(0.85f, 0.85f, 0.85f, 1f);
        }
        else
            statobj[1].SetActive(false);
    }
    public void questionRmove() // > Ŭ��
    {
        if (questionnum == 2)
            return;
        questionnum ++;
        for (int i = 0; i < 3; i++)
        {
            if(i == questionnum)
                questionobj[i].SetActive(true);
            else
                questionobj[i].SetActive(false);
        }
        if(questionnum == 2)
            questionobj[4].GetComponent<Image>().color = new Color(0.85f, 0.85f, 0.85f, 0.5f);
        questionobj[3].GetComponent<Image>().color = new Color(0.85f, 0.85f, 0.85f, 1f);
        
    }
    public void questionLmove() // < Ŭ��
    {
        if (questionnum == 0)
            return;
        questionnum--;
        for (int i = 0; i < 3; i++)
        {
            if (i == questionnum)
                questionobj[i].SetActive(true);
            else
                questionobj[i].SetActive(false);
        }
        if (questionnum == 0)
            questionobj[3].GetComponent<Image>().color = new Color(0.85f, 0.85f, 0.85f, 0.5f);
        questionobj[4].GetComponent<Image>().color = new Color(0.85f, 0.85f, 0.85f, 1f);

    }
    public void statupbt()//���Ⱦ�!
    {
        if (gm.plstatint <= 0)
            return;
        gm.plstatint--;
        uiinformation statint = EventSystem.current.currentSelectedGameObject.GetComponent<uiinformation>();
        if(statint.element == 1)
            gm.LvStat[0]++;
        else if (statint.element == 2)
            gm.LvStat[1]++;
        else if (statint.element == 3)
            gm.LvStat[2]++;
        else if (statint.element == 4)
            gm.LvStat[3]++;

        gm.txt();
    }

    //������ ��ư
    public void information()//����â
    {
        if (dirbt != null)
            beforebt = dirbt;
        dirbt = EventSystem.current.currentSelectedGameObject;
        if(dirbt == beforebt)
        {
            gm.information[0].SetActive(false);
            dirbt = null;
            beforebt = null;
        }
        else
        {
            invenitem bt = dirbt.GetComponent<invenitem>();
            gm.innametxt.text = bt.itemsubname + bt.itemname;
            gm.informationtxt.text = bt.informationtext;
            gm.information[0].SetActive(true);
            gm.informationim.sprite = dirbt.GetComponent<Image>().sprite;
            gm.informationim.color = dirbt.GetComponent<Image>().color;
            if (statobj[0].activeSelf && (bt.itemnum== gm.invenitemnum[0]
                || bt.itemnum == gm.invenitemnum[1] || bt.itemnum == gm.invenitemnum[2]
                || bt.itemnum == gm.invenitemnum[3] || bt.itemnum == gm.invenitemnum[4]
                || bt.itemnum == gm.invenitemnum[5] || bt.itemnum == gm.invenitemnum[6]
                || bt.itemnum == gm.invenitemnum[7] || bt.itemnum == gm.invenitemnum[8]
                ))
            {
                gm.information[1].SetActive(false); 
                gm.information[2].SetActive(true);
            }
            else
            {
                gm.information[1].SetActive(true);
                gm.information[2].SetActive(false);
            }
        }
    }
    public void informationF()//����â����
    {
        gm.information[0].SetActive(false);
    }
    public void declinebt()// ������ư
    {
        gm.items[gm.spownitemnum[0]].SetActive(false);
        gm.items[gm.spownitemnum[1]].SetActive(false);
        gm.items[gm.spownitemnum[2]].SetActive(false);
        gm.clearitem[0].SetActive(false);
        gm.StartCoroutine(gm.nextstage());
    }
    public void deletbt()// �ı��ư
    {
        invenitem invenitem = dirbt.GetComponent<invenitem>();
        for (int i = 0; i < gm.invenitemnum.Length; i++)
            if (gm.invenitemnum[i] == invenitem.itemnum)
            {
                invenpos invenpos = gm.inven[i].GetComponent<invenpos>();
                for (int r = 0; r < invenpos.upstat.Length; r++)
                    invenpos.upstat[r] = 0;
                gm.invenitemnum[i] = -1;
                StartCoroutine(itemEF());
            }
        gm.information[0].SetActive(false);
        dirbt.SetActive(false);
        dirbt = null;
        beforebt = null;
    }
    public void getitembt()// ȹ���ư
    {
        statbtclick();
        for (int i = 0; i < gm.inven.Length; i++)
        {
            Animator inanim = gm.inven[i].GetComponent<Animator>();
            inanim.SetTrigger("select");
        }
    }
    public void socketbt()// ���Ϲ�ư
    {
        gm.checkbt[0].SetActive(true);
        gm.checkbt[0].transform.position =
            Input.mousePosition;
        inpos = EventSystem.current.currentSelectedGameObject.GetComponent<invenpos>();
        int num = inpos.posint;
        gm.checkbt[0].transform.GetChild(0).gameObject.GetComponent<Text>().text
            = num + " ����";
        gm.checkbt[0].transform.GetChild(1).gameObject.GetComponent<Text>().text
                = inpos.informationtext;
        if(dirbt != null)
        {
            gm.checkbt[1].SetActive(true);
            gm.checkbt[1].transform.GetChild(0).gameObject.GetComponent<Text>().text
                = "����";
        }
        else
            gm.checkbt[1].SetActive(false);

    }
    public void socketdelbt()// �������� ������ ��ư
    {
        gm.checkbt[0].SetActive(false);
    }
    public void checkbt1()// 1��ư
    {
        dirbt.transform.position = gm.inven[inpos.posint - 1].transform.position;
        invenitem invenitem = dirbt.GetComponent<invenitem>();
        gm.invenitemnum[inpos.posint - 1] = invenitem.itemnum;
        for (int i = 0; i < gm.spownitemnum.Length; i++)
            if (gm.spownitemnum[i] == invenitem.itemnum)
                gm.spownitemnum[i] = -1;

        for (int i = 0; i < inpos.upstat.Length-2; i++)
            inpos.upstat[i] = invenitem.statup[i];
        inpos.upstat[12] = invenitem.resilience[0];
        inpos.upstat[13] = invenitem.resilience[1];
        exitbt();
        for (int i = 0; i < gm.inven.Length; i++)
        {
            Animator inanim = gm.inven[i].GetComponent<Animator>();
            inanim.SetTrigger("end");
        }
        gm.StartCoroutine(gm.nextstage());
        StartCoroutine(itemEF()); //������ ���� �ֽ�ȭ
    }

    IEnumerator itemEF() //������ ���� �ֽ�ȭ
    {
        yield return new WaitForFixedUpdate();
        for (int i = 0; i < gm.invenitemstat.Length; i++)
            gm.invenitemstat[i] = 0;
        yield return new WaitForFixedUpdate();
        for (int r = 0; r < gm.invenitemstat.Length; r++)
        {
            for (int i = 0; i < gm.inven.Length; i++)
            {
                invenpos invenpos = gm.inven[i].GetComponent<invenpos>();
                gm.invenitemstat[r] = gm.invenitemstat[r] +
                    (invenpos.upstat[r] * invenpos.EFstat[r]);
                yield return new WaitForFixedUpdate();
            }
            yield return new WaitForFixedUpdate();
        }
        
        gm.txt();
    }

    //��ų��ư
    public void PLdash() // �÷��̾� �뽬
    {
        if (gm.pl.isdead || gm.pl.dashcount <= 0 || gm.PS.timer[0] > 0)
            return;
        gm.pl.StartCoroutine(gm.pl.dash());
    }
    public IEnumerator mananull()
    {
        gm.pl.pltxt.SetActive(true);
        Text txt = gm.pl.pltxt.GetComponent<Text>();
        txt.text = "������ ������...";
        yield return new WaitForSecondsRealtime(2);
        gm.pl.pltxt.SetActive(false);
    }
    public void skillchange() // ��������
    {
        if (skillint[0] == 5 || skillint[skillint[0] + 1] == 0)
            skillint[0] = 1;
        else
            skillint[0]++;
        skillbtim();
    }
    public void skillbtclick()
    {
        switch (skillnum)
        {
            case 1:
                PLiceskill1();
                break;
            case 2:
                PLiceskill2();
                break;
            case 3:
                PLiceskill3();
                break;
            case 4:
                PLiceskill4();
                break;
            case 5:
                PLiceskill5();
                break;


            case 11:
                PLFireskill1();
                break;
            case 12:
                PLFireskill2();
                break;
            case 13:
                PLFireskill3();
                break;
            case 14:
                PLFireskill4();
                break;
            case 15:
                PLFireskill5();
                break;

            case 21:
                PLlightningskill1();
                break;
            case 22:
                PLlightningskill2();
                break;
            case 23:
                PLlightningskill3();
                break;
            case 24:
                PLlightningskill4();
                break;
            case 25:
                PLlightningskill5();
                break;
        }
        StartCoroutine(skillcoolim());
    }
    public void skillbtim()
    {
        skillnum = skillint[skillint[0]];
        switch (skillnum)
        {
            case 1:
                skillbtSP[0].sprite = skillbtSPchange[0];
                skillcoolnum = 1;
                break;
            case 2:
                skillbtSP[0].sprite = skillbtSPchange[1];
                skillcoolnum = 2;
                break;
            case 3:
                skillbtSP[0].sprite = skillbtSPchange[2];
                skillcoolnum = 3;
                break;
            case 4:
                skillbtSP[0].sprite = skillbtSPchange[3];
                skillcoolnum = 4;
                break;
            case 5:
                skillbtSP[0].sprite = skillbtSPchange[4];
                skillcoolnum = 5;
                break;


            case 11:
                skillbtSP[0].sprite = skillbtSPchange[5];
                skillcoolnum = 11;
                break;
            case 12:
                skillbtSP[0].sprite = skillbtSPchange[6];
                skillcoolnum = 12;
                break;
            case 13:
                skillbtSP[0].sprite = skillbtSPchange[7];
                skillcoolnum = 13;
                break;
            case 14:
                skillbtSP[0].sprite = skillbtSPchange[8];
                skillcoolnum = 14;
                break;
            case 15:
                skillbtSP[0].sprite = skillbtSPchange[9];
                skillcoolnum = 15;
                break;



            case 21:
                skillbtSP[0].sprite = skillbtSPchange[10];
                skillcoolnum = 21;
                break;
            case 22:
                skillbtSP[0].sprite = skillbtSPchange[11];
                skillcoolnum = 22;
                break;
            case 23:
                skillbtSP[0].sprite = skillbtSPchange[12];
                skillcoolnum = 23;
                break;
            case 24:
                skillbtSP[0].sprite = skillbtSPchange[13];
                skillcoolnum = 24;
                break;
            case 25:
                skillbtSP[0].sprite = skillbtSPchange[14];
                skillcoolnum = 25;
                break;
        }
        needmana.text = string.Format("{0:n0}", gm.PS.mana[skillnum]);
        StartCoroutine(skillcoolim());

    }
    IEnumerator skillcoolim()
    {
        skillbtSP[1].fillAmount = 0;
        while (gm.PS.timer[skillcoolnum] > 0)
        {
            skillbtSP[1].fillAmount = (gm.PS.timer[skillcoolnum] / gm.PS.cooltime[skillcoolnum]);
            yield return new WaitForFixedUpdate();
        }
    }


    //��ų��
    public void PLiceskill1() // ���̽����Ǿ�?
    {
        if (gm.pl.isdead || gm.pl.rd.nearestTarget == null || gm.PS.timer[1] > 0)
            return;
        if (gm.PS.mana[1] > gm.pl.mana)
            StartCoroutine(mananull());
        else
        {
            gm.pl.mana -= gm.PS.mana[1];
            gm.PS.icespear();
        }
            
    }
    public void PLiceskill2() // ���̽����?
    {
        if (gm.pl.isdead  || gm.PS.timer[2] > 0)
            return;
        if (gm.PS.mana[2] > gm.pl.mana)
            StartCoroutine(mananull());
        else
        {
            gm.pl.mana -= gm.PS.mana[2];
            gm.PS.iceskill2();
        }
    }
    public void PLiceskill3() // ������(?)
    {
        if (gm.pl.isdead || gm.pl.rd.nearestTarget == null || gm.PS.timer[3] > 0)
            return;
        if (gm.PS.mana[3] > gm.pl.mana)
            StartCoroutine(mananull());
        else
        {
            gm.PS.iceskill3();
            gm.pl.mana -= gm.PS.mana[3];
        }
    }
    public void PLiceskill4() // ��������(?)
    {
        if (gm.pl.isdead || gm.pl.rd.nearestTarget == null || gm.PS.timer[4] > 0)
            return;
        if (gm.PS.mana[4] > gm.pl.mana)
            StartCoroutine(mananull());
        else
        {
            gm.pl.mana -= gm.PS.mana[4];
            gm.PS.iceskill4();
        }
    }
    public void PLiceskill5() // �������Ǳñ�(?)
    {
        if (gm.pl.isdead || gm.PS.timer[5] > 0)
            return;
        if (gm.PS.mana[5] > gm.pl.mana)
            StartCoroutine(mananull());
        else
        {
            gm.PS.iceskill5();
            gm.pl.mana -= gm.PS.mana[5];
        }
    }
    public void PLFireskill1() // ���̾
    {
        if (gm.pl.isdead || gm.pl.rd.nearestTarget == null || gm.PS.timer[11] > 0)
            return;
        if (gm.PS.mana[11] > gm.pl.mana)
            StartCoroutine(mananull());
        else
        {
            gm.pl.mana -= gm.PS.mana[11];
            gm.PS.fireskill1();
        }
    }
    public void PLFireskill2() // �ұ�
    {
        if (gm.pl.isdead || gm.PS.timer[12] > 0)
            return;
        if (gm.PS.mana[12] > gm.pl.mana)
            StartCoroutine(mananull());
        else
        {
            gm.pl.mana -= gm.PS.mana[12];
            gm.PS.fireskill2();
        }
    }
    public void PLFireskill3() // mateo
    {
        if (gm.pl.isdead || gm.pl.rd.nearestTarget == null || gm.PS.timer[13] > 0)
            return;
        if (gm.PS.mana[13] > gm.pl.mana)
            StartCoroutine(mananull());
        else
        {
            gm.pl.mana -= gm.PS.mana[13];
            gm.PS.fireskill3();
        }
    }
    public void PLFireskill4() // ������?
    {
        if (gm.pl.isdead || gm.PS.timer[14] > 0)
            return;
        if (gm.PS.mana[14] > gm.pl.mana)
            StartCoroutine(mananull());
        else
        {
            gm.pl.mana -= 30;
            gm.PS.fireskill4();
        }
    }
    public void PLFireskill5() //�ҹٴ�?
    {
        if (gm.pl.isdead || gm.PS.timer[15] > 0)
            return;
        if (gm.PS.mana[15] > gm.pl.mana)
            StartCoroutine(mananull());
        else
        {
            gm.pl.mana -= gm.PS.mana[15];
            gm.PS.fireskill5();
        }
    }
    public void PLlightningskill1()// ������
    {
        if (gm.pl.isdead || gm.pl.rd.nearestTarget == null || gm.PS.timer[21] > 0)
            return;
        if (gm.PS.mana[21] > gm.pl.mana)
            StartCoroutine(mananull());
        else
        {
            gm.pl.mana -= gm.PS.mana[21];
            gm.PS.lightningskill1();
        }
    }
    public void PLlightningskill2()// ����
    {
        if (gm.pl.isdead || gm.pl.rd.nearestTarget == null || gm.PS.timer[22] > 0)
            return;
        if (gm.PS.mana[22] > gm.pl.mana)
            StartCoroutine(mananull());
        else
        {
            gm.pl.mana -= gm.PS.mana[22];
            gm.PS.lightningskill2();
        }
    }
    public void PLlightningskill3()// ������
    {
        if (gm.pl.isdead || gm.pl.rd.nearestTarget == null || gm.PS.timer[23] > 0)
            return;
        if (gm.PS.mana[23] > gm.pl.mana)
            StartCoroutine(mananull());
        else
        {
            gm.pl.mana -= gm.PS.mana[23];
            gm.PS.lightningskill3();
        }
    }
    public void PLlightningskill4()// ����
    {
        if (gm.pl.isdead || gm.pl.rd.nearestTarget == null || gm.PS.timer[24] > 0)
            return;
        if (gm.PS.mana[24] > gm.pl.mana)
            StartCoroutine(mananull());
        else
        {
            gm.pl.mana -= gm.PS.mana[24];
            gm.PS.lightningskill4();
        }
    }
    public void PLlightningskill5()// �������
    {
        if (gm.pl.isdead || gm.pl.rd.nearestTarget == null || gm.PS.timer[24] > 0)
            return;
        if (gm.PS.mana[24] > gm.pl.mana)
            StartCoroutine(mananull());
        else
        {
            gm.pl.mana -= gm.PS.mana[25];
            gm.PS.lightningskill5();
        }
    }

    //����ǥ!
    public void ReactionBt()// ���� ��ư
    {
        switch(gm.npcnum)
        {
            case 1: // �������� �̵� ������ �Ӱ����� Ȯ���Ұ�!
                chapterobj[0].SetActive(true);
                break;
            case 2:// ����(Ư��, ��ų, ����)
                break;
            case 3:// ����
                break;
            case 4:// ���� ������
                break;
            case 5:// ������
                break;
        }
    }
    public void lobbyExitBt() // ���������� ���� ��ư
    {
        for (int i = 0; i < chapterobj.Length; i++)
        {
            chapterobj[i].SetActive(false);
        }
        gm.stagemodeint = 0;
    }
    public void ModeSelectBt()//chapterobj[0]
    {
        uiinformation ModeSelect = EventSystem.current.currentSelectedGameObject.GetComponent<uiinformation>();
        gm.stagemodeint = ModeSelect.element;
        chapterobj[0].SetActive(false);
        chapterobj[1].SetActive(true);
    }

    public void Specificitybt()//chapterobj[1]
    {
        if (gm.MaxPlSpecificityindex <= gm.PlSpecificityindex)
            return;
        uiinformation Specificity = EventSystem.current.currentSelectedGameObject.GetComponent<uiinformation>();
        if (gm.SpecificityStack[Specificity.skillpos - 1] <= 20)
        {
            gm.SpecificityStack[Specificity.skillpos - 1]++;
            gm.PlSpecificityindex++;
        }
        StartCoroutine(Specificityuiupdate());
    }
    IEnumerator Specificityuiupdate()
    {
        yield return new WaitForFixedUpdate();
        Specificityuitext1[0].text = "����+" + gm.SpecificityStack[0];
        Specificityuitext2[0].text = "�ູ+" + gm.SpecificityStack[1];
        Specificityuitext3[0].text = "Ž��+" + gm.SpecificityStack[2];
        if (gm.SpecificityStack[0] < 20)
            Specificityuitext1[1].text = 
                "������ +" + gm.SpecificityStack[0] + "��" + (gm.SpecificityStack[0] + 1);
        else
            Specificityuitext1[1].text = "������ +" + gm.SpecificityStack[0];


        if (gm.SpecificityStack[1] < 20)
            Specificityuitext2[1].text =
                "HP, MP +" + (gm.SpecificityStack[1] * 5) + "��" + ((gm.SpecificityStack[1] * 5) + 5);
        else
            Specificityuitext2[1].text = "HP, MP +" + (gm.SpecificityStack[1] * 5);


        if (gm.SpecificityStack[2] < 20)
            Specificityuitext3[1].text =
                "���� ȹ�淮\n+" + gm.SpecificityStack[2] * 5 + "��" + ((gm.SpecificityStack[2] * 5) + 5);
        else
            Specificityuitext3[1].text = "���� ȹ�淮\n+" + gm.SpecificityStack[2] * 5;

        if (gm.SpecificityStack[0] >= 5)
            Specificityuitext1[2].color = new Color(1, 1, 1, 1);
        else
            Specificityuitext1[2].color = new Color(1, 1, 1, 0.5f);

        if (gm.SpecificityStack[0] >= 10)
            Specificityuitext1[3].color = new Color(1, 1, 1, 1);
        else
            Specificityuitext1[3].color = new Color(1, 1, 1, 0.5f);

        if (gm.SpecificityStack[0] >= 20)
            Specificityuitext1[4].color = new Color(1, 1, 1, 1);
        else
            Specificityuitext1[4].color = new Color(1, 1, 1, 0.5f);

        if (gm.SpecificityStack[1] >= 5)
            Specificityuitext2[2].color = new Color(1, 1, 1, 1);
        else
            Specificityuitext2[2].color = new Color(1, 1, 1, 0.5f);

        if (gm.SpecificityStack[1] >= 10)
            Specificityuitext2[3].color = new Color(1, 1, 1, 1);
        else
            Specificityuitext2[3].color = new Color(1, 1, 1, 0.5f);

        if (gm.SpecificityStack[1] >= 20)
            Specificityuitext2[4].color = new Color(1, 1, 1, 1);
        else
            Specificityuitext2[4].color = new Color(1, 1, 1, 0.5f);

        if (gm.SpecificityStack[2] >= 5)
            Specificityuitext3[2].color = new Color(1, 1, 1, 1);
        else
            Specificityuitext3[2].color = new Color(1, 1, 1, 0.5f);

        if (gm.SpecificityStack[2] >= 10)
            Specificityuitext3[3].color = new Color(1, 1, 1, 1);
        else
            Specificityuitext3[3].color = new Color(1, 1, 1, 0.5f);

        if (gm.SpecificityStack[2] >= 20)
            Specificityuitext3[4].color = new Color(1, 1, 1, 1);
        else
            Specificityuitext3[4].color = new Color(1, 1, 1, 0.5f);




    }
    public void SpecificityResetbt()
    {
        gm.PlSpecificityindex = 0;
        for (int i = 0; i < gm.SpecificityStack.Length; i++)
        {
            gm.SpecificityStack[i] = 0;
        }
        StartCoroutine(Specificityuiupdate());
    }
    public void SpecificityEnterbt() // ���� ����
    {
        chapterobj[1].SetActive(false);
        chapterobj[2].SetActive(true);
        chapterobj[2].GetComponent<MapController>().Start();
    }
    // SceneManager.LoadScene("1");


}
