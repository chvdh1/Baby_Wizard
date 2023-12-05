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
            informationtext = informationtext + "데미지 + " + statup[2] + "\n";
        if (statup[3] != 0)
            informationtext = informationtext + "사거리 + " + statup[3] + "\n";
        if (statup[4] != 0)
            informationtext = informationtext + "밸런스 + " + statup[4] + "\n";
        if (statup[5] != 0)
            informationtext = informationtext + "얼음속성 + " + statup[5] + "\n";
        if (statup[6] != 0)
            informationtext = informationtext + "불속성 + " + statup[6] + "\n";
        if (statup[7] != 0)
            informationtext = informationtext + "전기속성 + " + statup[7] + "\n";
        if (statup[8] != 0)
            informationtext = informationtext + "지력 + " + statup[8] + "\n";
        if (statup[9] != 0)
            informationtext = informationtext + "집중 + " + statup[9] + "\n";
        if (statup[10] != 0)
            informationtext = informationtext + "체력 + " + statup[10] + "\n";
        if (statup[11] != 0)
            informationtext = informationtext + "회복 + " + statup[11] + "\n";
        if (resilience[0] != 0)
            informationtext = informationtext + "HP회복량 + " + resilience[0] + "\n";
        if (resilience[1] != 0)
            informationtext = informationtext + "MP회복량 + " + resilience[1] + "\n";

    }

}
