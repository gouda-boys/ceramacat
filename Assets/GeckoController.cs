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


    public bool isJumping;

    void Awake()
    {
        StartCoroutine(LegUpdateCoroutine());
    }

    void LateUpdate()
    {
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
}