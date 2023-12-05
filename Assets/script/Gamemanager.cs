using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum gmstate
{
    Lobby = 0,
    Battle = 1,
    Shop = 2,
}

public class Gamemanager : MonoBehaviour
{
    public gmstate gmstate;
    public static Gamemanager gamemng;
    [HideInInspector] public enemypool enemypool;
    [HideInInspector] public poolmanager itempool;
    [HideInInspector] public poolmanager weaponpool;
    [HideInInspector] public poolmanager EFpool;
    [HideInInspector] public poolmanager TEXTEF;
    [HideInInspector] public poolmanager eleEf;
    [HideInInspector] public Transform eleEfobj;
    public poolmanager enemyspawnEF;
    [HideInInspector] public buttunmanager bt;
    [HideInInspector] public plskill PS;
    [HideInInspector] public player pl;
    [SerializeField] Transform[] stageonepoint;
    public Transform[] playerpos;
    public GameObject stagefade,playerobj;
    [HideInInspector] public Animator sfanim;
    [Space(10f)]
    [Header("player")]

    public int exp;
    public int  plstatint;
    public float[] plstat;  //랩,HP,MP, dmg, 사거리, 밸런스, 얼,불,전, 지,집,체,회
    public int[] skilleleint;
    public int[] LvStat;
    public int[] maxexp;
    public Animator changebt;
    [HideInInspector] public Slider expui;
    [HideInInspector] public RectTransform[] HMP;
    [HideInInspector] public Text[] backui;
    [Space(10f)]
    [Header("invenitems")]
    [HideInInspector] public Image informationim;
    [HideInInspector] public Text innametxt, informationtxt;
    [HideInInspector] public GameObject[] information;
    [HideInInspector] public GameObject[] clearitem;
    Animator clearanim;
    [SerializeField] List<int> item1;
    [SerializeField] List<int> item2;
    [SerializeField] List<int> item3;
    [SerializeField] List<int> item4;
    public GameObject[] items;
    [HideInInspector] public GameObject[] inven;
    [HideInInspector] public GameObject[] checkbt;
    public int[] invenitemnum;
    [HideInInspector] public int[] spownitemnum;
    public float[] invenitemstat;
    [Space(10f)]

    [Header("stage")]
    public GameObject[] startuidelobj;
    [HideInInspector] public int difficultyint,stageenemyint, stageint;
    [HideInInspector] public bool isclear;
    int spawnlength;
    [HideInInspector] public float spawntime;
    [Space(10f)]

    [Header("lobby")]
    public GameObject lbfade;
    public int npcnum, stagemodeint;
    public int[] SpecificityStack;
    public int PlSpecificityindex, MaxPlSpecificityindex;
    Animator lbfanim;

    [Header("Shop or Unknown")]
    public GameObject[] commonuiboj;
    public GameObject[] Shopuiboj;


    void Awake()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
        sfanim = stagefade.GetComponent<Animator>();
        clearanim = clearitem[0].GetComponent<Animator>();
        lbfanim = lbfade.GetComponent<Animator>();
        gamemng = this;
        maxexp = new int[70];
        for (int i = 0; i < 70; i++)
        {
            if (i == 0)
                maxexp[i] = 20;
            else if (i == 1)
                maxexp[i] = 30;
            else if (i == 2)
                maxexp[i] = 40;
            else
                maxexp[i] = maxexp[i-3] + maxexp[i - 1];
        }
        for (int i = 0; i < items.Length; i++)
        {
            items[i].SetActive(false);
        }
        for (int i = 8; i < startuidelobj.Length; i++)
        {
            startuidelobj[i].SetActive(false);
        }
      
     }

    
    public void txt()
    {
        //pl ui
        bt.HPtxt.text = string.Format("{0:n0}", pl.HP);
        HMP[0].sizeDelta = new Vector2(pl.HP / pl.maxHP * 45.5f, 40.6f);
        bt.MPtxt.text = string.Format("{0:n0}", pl.mana);
        HMP[1].sizeDelta = new Vector2(pl.mana / pl.maxmana * 45.5f, 40.6f);
        bt.cointxt.text = string.Format("{0:n0}", pl.coin);
        //랩,sp,ap
        backui[0].text = "LV " + plstat[0];
        backui[1].text = plstatint != 0 ? "AP " + plstatint : "";
        backui[2].text = PS.skillpoint != 0 ? "SP " + PS.skillpoint : "";


        pl.maxHP = plstat[1] = 50 + (plstat[11] * 5) + (plstat[0] * 10)+ invenitemstat[0];//  * 셋트;
        pl.maxmana = plstat[2] = 50+ plstat[9] + (plstat[11] * 3) + (plstat[0] * 5)+invenitemstat[1];//  * 셋트;
        pl.dmg = plstat[3] = 10 + (plstat[9] * 5) + (plstat[0] * 5)+ invenitemstat[2];//  셋트;
        pl.rd.scanrange = plstat[4] = 5 + (plstat[10] * 0.1f) + invenitemstat[3];//* 셋트;
        plstat[5] = (plstat[10] * 3)+ invenitemstat[4];// 셋트;
        plstat[6] = skilleleint[0] + invenitemstat[5];//  셋트;
        plstat[7] = skilleleint[1] + invenitemstat[6];//  * 셋트;
        plstat[8] = skilleleint[2] + invenitemstat[7];//  셋트;
        plstat[9] = LvStat[0] + invenitemstat[8];
        plstat[10] = LvStat[1] + invenitemstat[9];
        plstat[11] = LvStat[2] + invenitemstat[10];
        plstat[12] = LvStat[3] + invenitemstat[11];
        //회복량
        plstat[13] = (1 + plstat[12]+ invenitemstat[12]) * 0.2f;
        plstat[14] = (1 + plstat[12]+invenitemstat[13]) * 0.5f;
        //plstat
        //렙,HP,MP, dmg, 사거리, 밸런스, 얼,불,전, 지,집,체,회
        bt.stat[0].text = string.Format("{0:n0}", plstat[0]+"  AP:" + plstatint);
        bt.stat[1].text = string.Format("{0:n0}", plstat[1]);
        bt.stat[2].text = string.Format("{0:n0}", plstat[2]);
        bt.stat[3].text = string.Format("{0:n0}", plstat[3]);
        bt.stat[4].text = string.Format("{0:n0}", plstat[4]);
        bt.stat[5].text = string.Format("{0:n0}", plstat[5]);
        bt.stat[6].text = string.Format("{0:n0}", plstat[6]);
        bt.stat[7].text = string.Format("{0:n0}", plstat[7]);
        bt.stat[8].text = string.Format("{0:n0}", plstat[8]);
        bt.stat[9].text = "지력:\n" + plstat[9];
        bt.stat[10].text = "집중:\n" + plstat[10];
        bt.stat[11].text = "체력:\n" + plstat[11];
        bt.stat[12].text = "회복:\n" + plstat[12];
        bt.stat[13].text = "SP : " + PS.skillpoint;

        float curexp = exp;
        float max = maxexp[(int)plstat[0]];
        if (curexp / max < 1)
            expui.value = curexp / max;
        else
        {
            exp = 0;
            plstat[0]++;
            plstatint += 3;
            PS.skillpoint++;
            txt();
        }
          
        //ui변경
        bt.skillbtim();

    }
    void Start()
    {
        lbfanim.SetTrigger("lbin");
        Time.timeScale = 0;
        stagefade.SetActive(true);
        bt.startbt.SetActive(true);
    }

    public void Startstage()
    {
        gmstate = gmstate.Battle;
        StopCoroutine(Spawntart());
        bt.startbt.SetActive(false);
        pl.anim.SetTrigger("start");
        Time.timeScale = 1;
        txt();
        isclear = false;
        pl.rd.StartCoroutine(pl.rd.rader());
        pl.HP = pl.maxHP;
        pl.isdead = false;
        spawntime = 120;
        StartCoroutine(time());
        stagefade.SetActive(true);
        sfanim.SetTrigger("sfin");
    }
    public IEnumerator nextstage()
    {
        clearitem[0].SetActive(false);
        stageint++;
        sfanim.SetTrigger("sfout");
        yield return new WaitForSeconds(1);
        Time.timeScale = 0;
        bt.startbt.SetActive(true);
    }


    public IEnumerator time()
    {
        yield return new WaitForFixedUpdate();
        while (spawntime > 0)
        {
            spawntime -= 1;
            int min = Mathf.FloorToInt(spawntime / 60);
            int sec = Mathf.FloorToInt(spawntime % 60);
            yield return new WaitForSeconds(1);
            bt.timetxt.text = string.Format("{0:D2}:{1:D2}", min, sec);
        }

    }
    public IEnumerator Spawntart()
    {
        //스테이지 시작 모션(애니)
        bt.stageint.text = string.Format("{0:n0}", "STAGE " + stageint);
        Transform plspawnEF = enemyspawnEF.Get(0).transform;
        plspawnEF.position = new Vector3(0, 0);
        yield return new WaitForSeconds(0.5f);
        playerobj.transform.position = playerpos[0].position;
        playerobj.transform.GetChild(0).gameObject.SetActive(true);
        playerobj.transform.GetChild(1).gameObject.SetActive(true);
        for (int i = 0; i < startuidelobj.Length; i++)
            startuidelobj[i].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        //스폰 시작
        while (spawntime>0)
        {
            float min = stageenemyint < 5 ? 0.1f : spawntime > 100 ? 3 :
                spawntime > 80 ? 2 : spawntime > 30 ? 1 : 0.5f;
            int enemymin = stageint > 5 ? 2 : 1;
            int enemymax = stageint > 10 ? 5 : 3;
            int enemyran = Random.Range(enemymin, enemymax);
            yield return new WaitForSeconds(Random.Range(min, min+1));
            
            int ranpoint = Random.Range(0, stageonepoint.Length);
            for (int i = 0; i < enemyran; i++)
            {
                int maxint = stageint > 15 ? 4 : stageint > 12 ? 3 : 
                    stageint > 9 ? 2 : stageint > 6 ? 1 : 0;
                int ran = Random.Range(0, maxint);
                Vector2 ranpos = 
                    new Vector2(stageonepoint[ranpoint].position.x+Random.Range(-2f,2f),
                    stageonepoint[ranpoint].position.y + Random.Range(-2f, 2f));
                Transform spawnEF = enemyspawnEF.Get(1).transform;
                spawnEF.position = ranpos;
                yield return new WaitForSeconds(0.5f);
                enemy enemyl = enemypool.Get(ran).GetComponent<enemy>();
                enemyl.transform.position = ranpos;
                enemyl.target = pl.rg;
                enemyl.stageint = stageint;
                enemyl.itempool = itempool;
                enemyl.Invoke("pattern",1);
                //stageenemyint는 스크립트 enemycount에서 관리.
            }
        }
    }
    public IEnumerator clearstage() // 수정 필요함
    {
        isclear = true;
        yield return new WaitForFixedUpdate();
        clearitem[0].SetActive(true);
        clearanim.SetTrigger("clear");
        spawnlength = stageint > 15 ? 4 : stageint > 10 ? 3 : stageint > 5 ? 2 : 1;
        for (int i = 0; i < spownitemnum.Length; i++)
            spownitemnum[i] = -1;
        yield return new WaitForSeconds(2);
        for (int i = 0; i < 3; i++)
        {
            int y = Random.Range(0, spawnlength);
            if (y == 0)
            {
                int ran = Random.Range(0, item1.Count);
                spownitemnum[i] = item1[ran];
                items[item1[ran]].SetActive(true);
                items[item1[ran]].transform.position
                    = clearitem[i + 1].transform.position;
                item1.RemoveAt(ran);
            }
            else if (y == 1)
            {
                int ran = Random.Range(0, item2.Count);
                spownitemnum[i] = item2[ran];
                items[item2[ran]].SetActive(true);
                items[item2[ran]].transform.position
                    = clearitem[i + 1].transform.position;
                item2.RemoveAt(ran);
            }
            else if (y == 2)
            {
                int ran = Random.Range(0, item3.Count);
                spownitemnum[i] = item3[ran];
                items[item3[ran]].SetActive(true);
                items[item3[ran]].transform.position
                    = clearitem[i + 1].transform.position;
                item3.RemoveAt(ran);
            }
            else if (y == 3)
            {
                int ran = Random.Range(0, item4.Count);
                spownitemnum[i] = item4[ran];
                items[item4[ran]].SetActive(true);
                items[item4[ran]].transform.position
                    = clearitem[i + 1].transform.position;
                item4.RemoveAt(ran);
            }
        }

    }


    public void shop() //상점 씬
    {
        gmstate = gmstate.Shop;
        commonuiboj[0].SetActive(true);
        commonuiboj[1].SetActive(true);
        Shopuiboj[0].SetActive(true);
        for (int i = 0; i < 3; i++)
        {
            int y = Random.Range(0, spawnlength);
            if (y == 0)
            {
                int ran = Random.Range(0, item1.Count);
                spownitemnum[i] = item1[ran];
                items[item1[ran]].SetActive(true);
                items[item1[ran]].transform.position
                    = clearitem[i + 1].transform.position;
                item1.RemoveAt(ran);
            }
            else if (y == 1)
            {
                int ran = Random.Range(0, item2.Count);
                spownitemnum[i] = item2[ran];
                items[item2[ran]].SetActive(true);
                items[item2[ran]].transform.position
                    = clearitem[i + 1].transform.position;
                item2.RemoveAt(ran);
            }
            else if (y == 2)
            {
                int ran = Random.Range(0, item3.Count);
                spownitemnum[i] = item3[ran];
                items[item3[ran]].SetActive(true);
                items[item3[ran]].transform.position
                    = clearitem[i + 1].transform.position;
                item3.RemoveAt(ran);
            }
            else if (y == 3)
            {
                int ran = Random.Range(0, item4.Count);
                spownitemnum[i] = item4[ran];
                items[item4[ran]].SetActive(true);
                items[item4[ran]].transform.position
                    = clearitem[i + 1].transform.position;
                item4.RemoveAt(ran);
            }
        }

    }
    public void unknown()
    {
        gmstate = gmstate.Shop;
        //미지 씬
    }
    public void boss()
    {
        //보스 씬
    }


}
