using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    [SerializeField] private Transform camTransform;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(camTransform.position);
    }
}
