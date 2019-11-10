using System.Collections.Generic;
using UnityEngine;

public class OldManBehavior : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    [SerializeField]
    float oldManSpeed = 1f;

    [SerializeField]
    int lagBehindSteps = 30;

    [SerializeField]
    string isWalkingAnimationFlag = "isWalking";

    [SerializeField]
    string bendDownAnimationTrigger = "bendDown";

    [SerializeField]
    float distanceToCatch = 1f;

    [SerializeField]
    GameOverCanvas gameOver;

    Queue<Vector3> previousPlayerPositions = new Queue<Vector3>();

    Animator anim;

    bool playerCaught = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerCaught)
        {
            return;
        }

        previousPlayerPositions.Enqueue(player.transform.position);
        if(previousPlayerPositions.Count >= lagBehindSteps)
        {
            Vector3 followPosition = previousPlayerPositions.Dequeue();
            transform.position = Vector3.MoveTowards(transform.position, followPosition, oldManSpeed);
            anim.SetBool(isWalkingAnimationFlag, true);
        }
        else
        {
            anim.SetBool(isWalkingAnimationFlag, false);
        }
        transform.LookAt(player.transform.position);

        if(Vector3.Distance(transform.position, player.transform.position) <= distanceToCatch)
        {
            anim.SetBool(isWalkingAnimationFlag, false);
            anim.SetTrigger(bendDownAnimationTrigger);
            playerCaught = true;
            gameOver.Show();
        }
    }
}
