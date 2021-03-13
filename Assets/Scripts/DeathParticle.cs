using UnityEngine;

public class DeathParticle : MonoBehaviour
{
    public float time = 0.5f;

    void Start()
    {
        Destroy(gameObject, time);
    }
}
