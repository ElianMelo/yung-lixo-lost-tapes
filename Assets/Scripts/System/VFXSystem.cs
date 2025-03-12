using UnityEngine;

public class VFXSystem : MonoBehaviour
{
    public GameObject cdCollectVFX;

    public static VFXSystem Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayCDCollectVFX(Vector3 position)
    {
        var instance = Instantiate(cdCollectVFX, position, Quaternion.Euler(-90f, 0f, 0f));
        instance.GetComponent<ParticleSystem>().Play();
        Destroy(instance, 5f);
    }
}
