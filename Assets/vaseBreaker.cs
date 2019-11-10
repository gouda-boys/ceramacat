using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vaseBreaker : MonoBehaviour
{
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > 10)
        {
            audioSource.Play();
        }
        //break vase
        Debug.Log("VASE BROKEN");
    }

}
