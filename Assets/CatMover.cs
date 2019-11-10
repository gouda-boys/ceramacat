using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMover : MonoBehaviour
{
    public GeckoController geckoController;
    public GameObject geckoCapsule;
    public GameObject spine1;
    public Vector3 spine1Original;
    public float forwardPower;
    public float jumpPower;
    public float currentSpeed;
    public GameObject frontLeftFoot;
    public float originalZDiff;
    public IEnumerator currentJump;
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
            StartCoroutine(jumpHold());
            currentJump = jumpArc();
            StartCoroutine(currentJump);

         
}
        if (Input.GetKey(KeyCode.RightArrow))
        {
            geckoCapsule.transform.position += transform.forward * forwardPower;
}
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            geckoCapsule.transform.position -= transform.forward * forwardPower / 2;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentJump != null)
            {
                StopCoroutine(currentJump);
}

        }

        //cat body movement
        //range .2f
        spine1.transform.localPosition = spine1Original - .05f * new Vector3(0, 0, frontLeftFoot.transform.localPosition.z - geckoCapsule.transform.position.z);


    }

    IEnumerator jumpHold()
    {
        geckoController.isJumping = true;
        yield return new WaitForSeconds(.1f);
        geckoController.isJumping = false;
    }

    IEnumerator jumpArc()
    {
        float jumpStartTime = Time.time;
        float jumpStartY = geckoCapsule.transform.position.y;
        while(true)
        {
            Debug.Log(Time.time - jumpStartTime);
            geckoCapsule.transform.position = new Vector3(geckoCapsule.transform.position.x, jumpStartY + (-Mathf.Pow(5 * (Time.time - jumpStartTime), 2) + (10 * (Time.time - jumpStartTime))), geckoCapsule.transform.position.z);
            yield return new WaitForSeconds(.01f);

}

        //−0.2000𝑥2+2.000𝑥

    }


}
