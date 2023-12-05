using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class radar1 : MonoBehaviour
{
    public Vector3 point;
    public float scanrange;
    public LayerMask targetlayer;
    public RaycastHit2D[] targets;
    public Vector3 nearestTarget;

    void FixedUpdate()
    {

        targets = Physics2D.CircleCastAll(point, scanrange
            , Vector2.zero, 0, targetlayer);
        nearestTarget = getnearest();
    }

    Vector3 getnearest()
    {
        Vector3 result = Vector3.zero;
        float diff = 100;

        foreach (RaycastHit2D target in targets)
        {

            Vector3 targetpos = target.transform.position;
            float curdiff = (point - targetpos).magnitude;

            if (curdiff < diff && curdiff > 1)
            {
                diff = curdiff;
                result = target.transform.position;
            }
        }


        return result;
    }

}