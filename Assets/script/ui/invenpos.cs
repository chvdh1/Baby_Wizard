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
        informationtext = "���� ȿ�� : ";
        if (EFstat[0] > 1)
            informationtext = informationtext + "HP " + EFstat[0] + "����\n";
        if (EFstat[1] > 1)
            informationtext = informationtext + "MP " + EFstat[1] + "����\n";
        if (EFstat[2] > 1)
            informationtext = informationtext + "������ " + EFstat[2] + "����\n";
        if (EFstat[3] > 1)
            informationtext = informationtext + "��Ÿ� " + EFstat[3] + "����\n";
        if (EFstat[4] > 1)
            informationtext = informationtext + "�뷱�� " + EFstat[4] + "����\n";
        if (EFstat[5] > 1)
            informationtext = informationtext + "�����Ӽ� " + EFstat[5] + "����\n";
        if (EFstat[6] > 1)
            informationtext = informationtext + "�ҼӼ� " + EFstat[6] + "����\n";
        if (EFstat[7] > 1)
            informationtext = informationtext + "����Ӽ� " + EFstat[7] + "����\n";
        if (EFstat[8] > 1)
            informationtext = informationtext + "���� " + EFstat[8] + "����\n";
        if (EFstat[9] > 1)
            informationtext = informationtext + "���� " + EFstat[9] + "����\n";
        if (EFstat[10] > 1)
            informationtext = informationtext + "ü�� " + EFstat[10] + "����\n";
        if (EFstat[11] > 1)
            informationtext = informationtext + "ȸ�� " + EFstat[11] + "����\n";
        if (EFstat[12] > 1)
            informationtext = informationtext + "HPȸ���� " + EFstat[0] + "����\n";
        if (EFstat[13] > 1)
            informationtext = informationtext + "MPȸ���� " + EFstat[1] + "����\n";

    }
}
