using UnityEngine;

public class EnvironmentController : MonoBehaviour
{
    [SerializeField]
    float scrollSpeed = 1;

    [SerializeField]
    float canvasWidth = 14;

    PlatformBehavior[] platforms;

    // Start is called before the first frame update
    void Start()
    {
        platforms = GetComponentsInChildren<PlatformBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        foreach(PlatformBehavior plat in platforms)
        {
            plat.transform.position += Vector3.left * scrollSpeed;
        }
    }

    public void FlagForRespawn(PlatformBehavior platform)
    {
        platform.transform.position += Vector3.right * canvasWidth;
    }
}
