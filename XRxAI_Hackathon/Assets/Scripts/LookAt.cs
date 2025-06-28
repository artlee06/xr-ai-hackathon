using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    
    [SerializeField]
    private Transform target;

    public Boolean shouldInvert = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldInvert) {
            transform.rotation = Quaternion.LookRotation(-target.position + transform.position, Vector3.up);
        } else {
            transform.rotation = Quaternion.LookRotation(target.position - transform.position, Vector3.up);
        }
    }
}
