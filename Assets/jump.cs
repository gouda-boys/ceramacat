using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jump : MonoBehaviour
{
    public Rigidbody durr;
    public GeckoController geckoController;
    public GameObject geckoHip;
    public bool isColliding = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
          
        }
    }

    protected void LateUpdate()
    {
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        isColliding = true;
        Debug.Log("collision Enter");
        if (geckoController.isJumping)
        {
            geckoController.isJumping = false;

        }
        geckoHip.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    private void OnCollisionExit(Collision collision)
    {
        isColliding = false;
    }
}
