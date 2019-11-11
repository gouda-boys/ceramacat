using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vaseBreaker : MonoBehaviour
{
    AudioSource audioSource;
    public GameObject vaseShard;
    private bool vaseBroken;
    public GameObject vasePartsParent;
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
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (!vaseBroken)
            {
            //    Debug.Log(collision.gameObject.tag);

//               Debug.Log(collision.relativeVelocity.magnitude);
                if (collision.relativeVelocity.magnitude > 5)
                {
                    //   audioSource.Play();
                }
                //break vase
                //GameObject vs1 = Instantiate(vaseShard, vasePartsParent.transform);
                //vs1.AddComponent<Rigidbody>();
                //vs1.GetComponent<Rigidbody>().AddForce(new Vector3(1, 1, 1) * 5);

                //GameObject vs2 = Instantiate(vaseShard, vasePartsParent.transform);
                //vs2.AddComponent<Rigidbody>();
                //vs2.GetComponent<Rigidbody>().AddForce(new Vector3(-1, 1, 1) * 5);

                //GameObject vs3 = Instantiate(vaseShard, vasePartsParent.transform);
                //vs3.AddComponent<Rigidbody>();
                //vs3.GetComponent<Rigidbody>().AddForce(new Vector3(1, 1, -1) * 5);

                gameObject.AddComponent<TriangleExplosion>();
                StartCoroutine(gameObject.GetComponent<TriangleExplosion>().SplitMesh(true));
                vaseBroken = true;
            }
            else
            {
                //vase is broken
                Destroy(this.gameObject);
            }
            IntroVaseTriggerScene triggerScene = GetComponent<IntroVaseTriggerScene>();
            if (triggerScene)
            {
                triggerScene.LoadMainScene();
            }

        }
    }
}

public static class MathUtilities
{
    public static void Random(this ref Vector3 myVector, Vector3 min, Vector3 max)
    {
        myVector = new Vector3(UnityEngine.Random.Range(min.x, max.x), UnityEngine.Random.Range(min.y, max.y), UnityEngine.Random.Range(min.z, max.z));
    }
}
