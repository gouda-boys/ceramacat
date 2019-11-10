using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 1f;

    [SerializeField]
    float jumpHeight = 2f;

    [SerializeField]
    KeyCode jumpKey = KeyCode.Space;

    [SerializeField]
    KeyCode leftKey = KeyCode.W;

    [SerializeField]
    KeyCode altLeftKey = KeyCode.LeftArrow;

    [SerializeField]
    KeyCode rightKey = KeyCode.D;

    [SerializeField]
    KeyCode altRightKey = KeyCode.RightArrow;

    [SerializeField]
    float maxDistanceToTouchSurface = 10f;

    Rigidbody rigidbody3d;
     
    // Start is called before the first frame update
    void Start()
    {
        rigidbody3d = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(jumpKey))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, maxDistanceToTouchSurface))
            {
                rigidbody3d.velocity += Vector3.up * jumpHeight;
            } else if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, maxDistanceToTouchSurface))
            {
                rigidbody3d.velocity += Vector3.up * jumpHeight;
            }
        }
        MoveHorizontal();
    }

    void MoveHorizontal()
    {
        float horizontalMove = 0;
        if(Input.GetKey(leftKey) || Input.GetKey(altLeftKey))
        {
            horizontalMove = -moveSpeed;
        }
        else if(Input.GetKey(rightKey) || Input.GetKey(altRightKey))
        {
            horizontalMove = moveSpeed;
        }
        rigidbody3d.velocity = new Vector3(horizontalMove, rigidbody3d.velocity.y, rigidbody3d.velocity.z);
    }
}
