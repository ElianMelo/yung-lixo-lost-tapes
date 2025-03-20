using UnityEngine;

public class VFXSystem : MonoBehaviour
{
    public GameObject cdCollectVFX;
    public GameObject starGenericVFX;

    public static VFXSystem Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayCDCollectVFX(Vector3 position)
    {
        PlayGenericParticle(cdCollectVFX, position);
    }

    public void PlayStarGenericVFX(Vector3 position)
    {
        PlayGenericParticle(starGenericVFX, position);
    }

    private void PlayGenericParticle(GameObject particle, Vector3 position)
    {
        var instance = Instantiate(particle, position, Quaternion.Euler(-90f, 0f, 0f));
        instance.GetComponent<ParticleSystem>().Play();
        Destroy(instance, 5f);
    }
}
