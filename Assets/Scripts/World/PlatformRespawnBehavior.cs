using UnityEngine;

public class PlatformRespawnBehavior : MonoBehaviour
{
    EnvironmentController env;

    // Start is called before the first frame update
    void Start()
    {
        env = GetComponentInParent<EnvironmentController>();
    }


    private void OnTriggerEnter(Collider collider)
    {
        PlatformBehavior plat;
        if(plat = collider.GetComponent<PlatformBehavior>())
        {
            env.FlagForRespawn(plat);
        }
    }
}
