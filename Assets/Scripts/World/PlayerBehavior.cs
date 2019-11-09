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
    KeyCode rightKey = KeyCode.D;

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
            rigidbody3d.velocity += Vector3.up * jumpHeight;
        }
        float horizontalMove = 0;
        if(Input.GetKey(leftKey))
        {
            horizontalMove = -moveSpeed;
        }
        else if(Input.GetKey(rightKey))
        {
            horizontalMove = moveSpeed;
        }
        rigidbody3d.velocity = new Vector3(horizontalMove, rigidbody3d.velocity.y, rigidbody3d.velocity.z);
    }
}
