using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class radar : MonoBehaviour
{
    public Gamemanager gm;
    public float scanrange;
    public LayerMask targetlayer;
    public RaycastHit2D[] targets;
    public Transform nearestTarget;
    public GameObject mark;

    public IEnumerator rader()
    {
        yield return new WaitForFixedUpdate();
        while (!gm.isclear)
        {
            targets = Physics2D.CircleCastAll(transform.position, scanrange
            , Vector2.zero, 0, targetlayer);

            nearestTarget = getnearest();
            if (nearestTarget)
            {
                mark.SetActive(true);
                mark.transform.position = nearestTarget.position;
            }
            else
                mark.SetActive(false);
            yield return new WaitForSeconds(0.15f);
        }
      
    }

    Transform getnearest()
    {
        Transform result = null;
        float diff = 100;

        foreach(RaycastHit2D target in targets)
        {
            Vector2 mypos = transform.position;
            Vector2 targetpos = target.transform.position;
            float curdiff = (mypos - targetpos).magnitude;
           
            if(curdiff < diff)
            {
                diff = curdiff;
                result = target.transform;
            }
        }


        return result;
    }
}
