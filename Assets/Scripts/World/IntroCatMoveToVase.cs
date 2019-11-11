using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCatMoveToVase : MonoBehaviour
{
    [SerializeField]
    GameObject vase;

    [SerializeField]
    float distanceFromVase = 0.1f;

    [SerializeField]
    float moveSpeed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, vase.transform.position) > distanceFromVase)
        {
            transform.position = Vector3.MoveTowards(transform.position, vase.transform.position, moveSpeed);
        }
    }
}
