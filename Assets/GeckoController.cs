using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeckoController : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Transform headBone;
    [SerializeField] float headMaxTurnAngle;
    [SerializeField] float headTrackingSpeed;

    [SerializeField] LegStepper frontLeftLegStepper;
    [SerializeField] LegStepper frontRightLegStepper;
    [SerializeField] LegStepper backLeftLegStepper;
    [SerializeField] LegStepper backRightLegStepper;
    public Vector3 tailTargetPosition;
    public GameObject tailAimObject;
    public GameObject gecko;
    public float tailMoveSpeed;
    public float tailInbetween = 1.5f;
    public bool isJumping;

    void Awake()
    {
        StartCoroutine(LegUpdateCoroutine());
        StartCoroutine(TailMove());
    }

    void LateUpdate()
    {
        if (isJumping)
        {
            tailInbetween = .1f;
            tailMoveSpeed = .3f;
        }
        else
        {
            tailInbetween = 1.5f;
            tailMoveSpeed = .01f;
        }

        tailAimObject.transform.position = Vector3.MoveTowards(tailAimObject.transform.position, tailTargetPosition, tailMoveSpeed);

        // Store the current head rotation since we will be resetting it
        Quaternion currentLocalRotation = headBone.localRotation;
        // Reset the head rotation so our world to local space transformation will use the head's zero rotation. 
        // Note: Quaternion.Identity is the quaternion equivalent of "zero"
        headBone.localRotation = Quaternion.identity;

        Vector3 targetWorldLookDir = target.position - headBone.position;
        Vector3 targetLocalLookDir = headBone.InverseTransformDirection(targetWorldLookDir);

        // Apply angle limit
        targetLocalLookDir = Vector3.RotateTowards(
            Vector3.forward,
            targetLocalLookDir,
            Mathf.Deg2Rad * headMaxTurnAngle, // Note we multiply by Mathf.Deg2Rad here to convert degrees to radians
            0 // We don't care about the length here, so we leave it at zero
        );

        // Get the local rotation by using LookRotation on a local directional vector
        Quaternion targetLocalRotation = Quaternion.LookRotation(targetLocalLookDir, Vector3.up);

        // Apply smoothing
        headBone.localRotation = Quaternion.Slerp(
            currentLocalRotation,
            targetLocalRotation,
            1 - Mathf.Exp(-headTrackingSpeed * Time.deltaTime)
        );
    }



    IEnumerator LegUpdateCoroutine()
    {
        // Run forever
        while (true)
        {
            if (!isJumping)
            {
                // Try moving one diagonal pair of legs
                do
                {
                    frontLeftLegStepper.TryMove();
                    backRightLegStepper.TryMove();
                    // Wait a frame
                    yield return null;

                    // Stay in this loop while either leg is moving.
                    // If only one leg in the pair is moving, the calls to TryMove() will let
                    // the other leg move if it wants to.
                } while (backRightLegStepper.Moving || frontLeftLegStepper.Moving);

                // Do the same thing for the other pair
                do
                {
                    frontRightLegStepper.TryMove();
                    backLeftLegStepper.TryMove();
                    yield return null;
                } while (backLeftLegStepper.Moving || frontRightLegStepper.Moving);
            }
            else
            {
                frontRightLegStepper.Jump();
                frontLeftLegStepper.Jump();
                backLeftLegStepper.Jump();
                backRightLegStepper.Jump();
                yield return null;
                //each leg goes to its jump position
            }
        }
    }

    IEnumerator TailMove()
    {
        while (true)
        {
            yield return new WaitForSeconds(tailInbetween);
            tailTargetPosition = gecko.transform.position + gecko.transform.forward * -.1f + gecko.transform.right * .3f + gecko.transform.up * .01f;
            yield return new WaitForSeconds(tailInbetween);
            tailTargetPosition = gecko.transform.position + gecko.transform.forward * -.3f + gecko.transform.right * -.2f + gecko.transform.up * .2f;
            yield return new WaitForSeconds(tailInbetween);
            tailTargetPosition = gecko.transform.position + gecko.transform.forward * -.2f + gecko.transform.right * .1f + gecko.transform.up * .3f;
            yield return new WaitForSeconds(tailInbetween);
            tailTargetPosition = gecko.transform.position + gecko.transform.forward * -.5f + gecko.transform.right * -.2f + gecko.transform.up * .15f;
            yield return new WaitForSeconds(tailInbetween);
            tailTargetPosition = gecko.transform.position + gecko.transform.forward * -0f + gecko.transform.right * .2f + gecko.transform.up * .2f;
            yield return new WaitForSeconds(tailInbetween);
            tailTargetPosition = gecko.transform.position + gecko.transform.forward * -.23f + gecko.transform.right * -.3f + gecko.transform.up * .1f;
        }
    }
}