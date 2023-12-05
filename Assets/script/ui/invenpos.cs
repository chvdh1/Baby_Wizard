using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class invenpos : MonoBehaviour
{
    public int posint;

    public float[] EFstat;
    public float[] upstat;
    [TextArea]
    public string informationtext;

    private void Awake()
    {
        upstat = new float[14];
        Efup();
    }

    public void Efup()
    {
        informationtext = "家南 瓤苞 : ";
        if (EFstat[0] > 1)
            informationtext = informationtext + "HP " + EFstat[0] + "刘气\n";
        if (EFstat[1] > 1)
            informationtext = informationtext + "MP " + EFstat[1] + "刘气\n";
        if (EFstat[2] > 1)
            informationtext = informationtext + "单固瘤 " + EFstat[2] + "刘气\n";
        if (EFstat[3] > 1)
            informationtext = informationtext + "荤芭府 " + EFstat[3] + "刘气\n";
        if (EFstat[4] > 1)
            informationtext = informationtext + "闺繁胶 " + EFstat[4] + "刘气\n";
        if (EFstat[5] > 1)
            informationtext = informationtext + "倔澜加己 " + EFstat[5] + "刘气\n";
        if (EFstat[6] > 1)
            informationtext = informationtext + "阂加己 " + EFstat[6] + "刘气\n";
        if (EFstat[7] > 1)
            informationtext = informationtext + "傈扁加己 " + EFstat[7] + "刘气\n";
        if (EFstat[8] > 1)
            informationtext = informationtext + "瘤仿 " + EFstat[8] + "刘气\n";
        if (EFstat[9] > 1)
            informationtext = informationtext + "笼吝 " + EFstat[9] + "刘气\n";
        if (EFstat[10] > 1)
            informationtext = informationtext + "眉仿 " + EFstat[10] + "刘气\n";
        if (EFstat[11] > 1)
            informationtext = informationtext + "雀汗 " + EFstat[11] + "刘气\n";
        if (EFstat[12] > 1)
            informationtext = informationtext + "HP雀汗樊 " + EFstat[0] + "刘气\n";
        if (EFstat[13] > 1)
            informationtext = informationtext + "MP雀汗樊 " + EFstat[1] + "刘气\n";

    }
}
