using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public GameObject[] testobj;
    public Vector3[] testvec;

    private void Update()
    {
        testvec[0] = testobj[0].transform.position;
        testvec[1] = testobj[1].transform.position;

        float ang = Mathf.Atan2(testvec[1].y, testvec[1].x) * Mathf.Rad2Deg - 90;
        testobj[0].transform.rotation = Quaternion.Euler(0, 0, ang);

        // testobj[0].transform.rotation = Quaternion.FromToRotation(Vector3.up, testvec[1]);

        Debug.Log(testobj[0].transform.rotation);
    }

}
