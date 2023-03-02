using System.Collections;
using UnityEngine;

public class OnBuildParticleHandler : MonoBehaviour
{
    private ParticleSystem particle;

    [SerializeField]
    private Variables variables;

    private void Awake()
    {
        particle = GetComponent(typeof(ParticleSystem)) as ParticleSystem;
    }

    public void startParticle(Color particleColor, int goldPerClickDifference)
    {
        var main = particle.main;
        var limitVelocityOverLifetime = particle.limitVelocityOverLifetime;
        main.startColor = particleColor;

        float percentage =
            (float)goldPerClickDifference / (float)variables.maxFoldPerClickDifference;
        if (percentage > 1f)
        {
            percentage = 1f;
        }
        float effectiveDrag =
            variables.minDrag + ((variables.minDrag - variables.maxDrag) * percentage);
        float effectiveSpeed =
            variables.minSpeed + ((variables.minSpeed - variables.maxSpeed) * percentage);

        limitVelocityOverLifetime.drag = effectiveDrag;
        main.startSpeed = effectiveSpeed;

        particle.Play();

        StartCoroutine(cleanUp(particle.main.duration));
    }

    IEnumerator cleanUp(float durationSecond)
    {
        yield return new WaitForSeconds(durationSecond);
        Destroy(gameObject);
    }
}
