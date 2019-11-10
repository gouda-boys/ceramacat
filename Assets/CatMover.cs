using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMover : MonoBehaviour
{
    public GeckoController geckoController;
    public GameObject geckoCapsule;
    public GameObject geckoHip;
    public Rigidbody rb;
    public GameObject spine1;
    public Vector3 spine1Original;
    public float forwardPower;
    public float jumpPower;
    public float currentSpeed;
    public GameObject frontLeftFoot;
    public float originalZDiff;
    public jump jump;
    
    // Start is called before the first frame update
    void Start()
    {
        originalZDiff = frontLeftFoot.transform.position.z - geckoCapsule.transform.position.z;
        spine1Original = spine1.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            if (!geckoController.isJumping)
            {
                StartCoroutine(jumpHold());
                rb.velocity = Vector3.zero;
                rb.AddForce(Vector3.up * jumpPower * Time.deltaTime);
                StartCoroutine(jumpRotate());
            }
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            geckoCapsule.transform.position += geckoCapsule.transform.forward * forwardPower;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            geckoCapsule.transform.position -= geckoCapsule.transform.forward * forwardPower;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            //THIS LINE HERE 
           // Debug.Log(geckoCapsule.transform.eulerAngles.y);

            geckoCapsule.transform.localRotation = Quaternion.Euler(geckoCapsule.transform.eulerAngles.x, geckoCapsule.transform.eulerAngles.y - 2, geckoCapsule.transform.eulerAngles.z);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            geckoCapsule.transform.localRotation = Quaternion.Euler(geckoCapsule.transform.eulerAngles.x, geckoCapsule.transform.eulerAngles.y + 2, geckoCapsule.transform.eulerAngles.z);
        }


        //cat body movement
        //range .2f
        //spine1.transform.localPosition = Vector3.MoveTowards(spine1.transform.localPosition, spine1Original - .1f * new Vector3(0, 0, originalZDiff - (frontLeftFoot.transform.localPosition.z - geckoCapsule.transform.position.z)), .01f);


    }

    IEnumerator jumpHold()
    {
        geckoController.isJumping = true;
        yield return new WaitForSeconds(.02f);
        geckoController.isJumping = true;
    }

    IEnumerator jumpRotate()
    {
        yield return new WaitForSeconds(.01f);
        geckoHip.transform.localRotation = Quaternion.Euler(-30, geckoHip.transform.localRotation.y, geckoHip.transform.localRotation.z);
        yield return new WaitForSeconds(.1f);
        geckoHip.transform.localRotation = Quaternion.Euler(-30, geckoHip.transform.localRotation.y, geckoHip.transform.localRotation.z);
        float rotationX = -30;
        while (rotationX < 30)
        {
            if (jump.isColliding)
            {
                break;
            }

            rotationX += .5f;
            geckoHip.transform.localRotation = Quaternion.Euler(rotationX, geckoHip.transform.localRotation.y, geckoHip.transform.localRotation.z);
            yield return new WaitForSeconds(.01f);
        }
    }
}