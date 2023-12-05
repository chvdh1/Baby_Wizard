using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class invenitem : MonoBehaviour
{
    public string itemname;
    public string itemsubname;
    public int itemnum;
    public int Rating;

    public int[] statup;
    public float[] resilience;
    public int[] set;
        
    [TextArea]
    public string informationtext;

    private void Awake()
    {
        if (statup[0] != 0)
            informationtext = informationtext + "HP + " + statup[0] + "\n";
        if (statup[1] != 0)
            informationtext = informationtext + "MP + " + statup[1] + "\n";
        if (statup[2] != 0)
            informationtext = informationtext + "������ + " + statup[2] + "\n";
        if (statup[3] != 0)
            informationtext = informationtext + "��Ÿ� + " + statup[3] + "\n";
        if (statup[4] != 0)
            informationtext = informationtext + "�뷱�� + " + statup[4] + "\n";
        if (statup[5] != 0)
            informationtext = informationtext + "�����Ӽ� + " + statup[5] + "\n";
        if (statup[6] != 0)
            informationtext = informationtext + "�ҼӼ� + " + statup[6] + "\n";
        if (statup[7] != 0)
            informationtext = informationtext + "����Ӽ� + " + statup[7] + "\n";
        if (statup[8] != 0)
            informationtext = informationtext + "���� + " + statup[8] + "\n";
        if (statup[9] != 0)
            informationtext = informationtext + "���� + " + statup[9] + "\n";
        if (statup[10] != 0)
            informationtext = informationtext + "ü�� + " + statup[10] + "\n";
        if (statup[11] != 0)
            informationtext = informationtext + "ȸ�� + " + statup[11] + "\n";
        if (resilience[0] != 0)
            informationtext = informationtext + "HPȸ���� + " + resilience[0] + "\n";
        if (resilience[1] != 0)
            informationtext = informationtext + "MPȸ���� + " + resilience[1] + "\n";

    }

}
