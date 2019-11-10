﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WallBreakable : MonoBehaviour
{

    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < 1f)
        {

            GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
