using UnityEngine;

public enum GameSoundEffects
{
    ON_TICK,
    ON_BUILD
}

public class SoundEffectManager : MonoBehaviour
{
    public static SoundEffectManager current;

    [SerializeField]
    private AudioClip onTickSound;

    [SerializeField]
    private AudioClip onBuildSound;

    private AudioSource audioSource;

    private void Awake()
    {
        if (current != null)
        {
            Destroy(gameObject);
            return;
        }
        current = this;

        this.audioSource = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        this.audioSource.volume = 0.75f;
    }

    public void triggerSoundEffect(GameSoundEffects soundEffect)
    {
        switch (soundEffect)
        {
            case GameSoundEffects.ON_TICK:
                audioSource.PlayOneShot(onTickSound);
                return;
            case GameSoundEffects.ON_BUILD:
                audioSource.PlayOneShot(onBuildSound);
                return;
        }
    }
}
