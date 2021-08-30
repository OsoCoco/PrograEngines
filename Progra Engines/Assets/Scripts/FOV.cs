using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOV : MonoBehaviour
{
    public float radius;

    [Range(0,360)]
    public float angle;

    public GameObject targetRef;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeeTraget;

    private void Start()
    {
        targetRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRutine());
    }

    IEnumerator FOVRutine()
    {
        float delay = 0.2f;

        WaitForSeconds wait = new WaitForSeconds(delay);
        while (true)
        {
            yield return wait;
            FOVCheck();
        }
    }
    void FOVCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position,radius,targetMask);
        
        if (rangeChecks.Length != 0 )
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if(Vector3.Angle(transform.forward,directionToTarget) < angle/2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if(!Physics.Raycast(transform.position,directionToTarget,distanceToTarget,obstructionMask))
                {
                    canSeeTraget = true;
                }
                else
                {
                    canSeeTraget = false;
                }
            }
            else
            {
                canSeeTraget = false;
            }
        }
        else if(canSeeTraget)
        {
            canSeeTraget = false;
        }
    }
}
