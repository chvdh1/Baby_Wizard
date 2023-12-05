using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class textEF : MonoBehaviour
{
    public TextMeshPro text;
    public Color alpha;
    public string txt;

    public void getstring()
    {
        text.text = txt;
        text.color = alpha;
    }

    public void setF()
    {
        gameObject.SetActive(false);
    }
}
