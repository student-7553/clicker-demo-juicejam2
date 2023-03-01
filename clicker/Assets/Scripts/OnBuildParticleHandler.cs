using System.Collections;
using UnityEngine;

public class OnBuildParticleHandler : MonoBehaviour
{
    private ParticleSystem particle;

    private void Awake()
    {
        particle = GetComponent(typeof(ParticleSystem)) as ParticleSystem;
    }

    public void startParticle(Color particleColor)
    {
        var main = particle.main;
        main.startColor = particleColor;

        particle.Play();

        StartCoroutine(cleanUp(particle.main.duration));
    }

    IEnumerator cleanUp(float durationSecond)
    {
        yield return new WaitForSeconds(durationSecond);
        Destroy(gameObject);
    }
}
