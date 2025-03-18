using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public Transform target;
    public Vector3 axisOffset = Vector3.zero;

    void Update()
    {
        if (target != null)
        {
            Vector3 directionToTarget = target.position - transform.position;

            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

            targetRotation *= Quaternion.Euler(axisOffset);

            transform.rotation = targetRotation;
        }
    }
}
