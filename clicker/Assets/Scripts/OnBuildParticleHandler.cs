using System.Collections;
using UnityEngine;

public class OnBuildParticleHandler : MonoBehaviour
{
    private ParticleSystem particle;

    private void Awake()
    {
        particle = GetComponent(typeof(ParticleSystem)) as ParticleSystem;
    }

    void Start()
    {
        var main = particle.main;
        main.startColor = Color.red;

        particle.Play();

        StartCoroutine(cleanUp(particle.main.duration));
    }

    IEnumerator cleanUp(float durationSecond)
    {
        yield return new WaitForSeconds(durationSecond);
        Destroy(gameObject);
    }
}
