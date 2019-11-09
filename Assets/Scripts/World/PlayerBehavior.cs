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

    Rigidbody rigidbody3d;
    bool touchingGround;
     
    // Start is called before the first frame update
    void Start()
    {
        rigidbody3d = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(touchingGround && Input.GetKeyDown(jumpKey))
        {
            Jump();
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

    void Jump()
    {
        rigidbody3d.velocity += Vector3.up * jumpHeight;
        touchingGround = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.GetComponent<PlatformBehavior>())
        {
            touchingGround = true;
        }
    }
}
