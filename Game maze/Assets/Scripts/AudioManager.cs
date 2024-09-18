using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Audio sources for background music, movement sounds and sound effects
    [Header("-------- Audio Source -------")]
    public AudioSource musicSource;
    public AudioSource soundMoveSource;
    [SerializeField] private AudioSource _SFXSource;

    [Header("-------- AudioClip -------")]
    public AudioClip background;
    public AudioClip buttonClick;
    public AudioClip running;
    public AudioClip walking;
    public AudioClip getKey;
    public AudioClip damage;
    public AudioClip pause;
    public AudioClip playerDeath;
    public AudioClip finish;

    private void Start()
    {
        // Start playing background music
        PlayMusic(background);
    }

    // Play background music
    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    // Pause the background music
    public void Mute()
    {
        musicSource.Pause();
    }

    // Resume playing background music
    public void Resume()
    {
        PlayMusic(background);
    }

    // Play a sound effect clip
    public void PlaySFX(AudioClip clip)
    {
        _SFXSource.PlayOneShot(clip);
    }

    // Play a sound effect for movement
    public void PlayMoveSource(AudioClip clip)
    {
        soundMoveSource.clip = clip;
        soundMoveSource.Play();
    }

    // Play the button click sound effect
    public void PlayButton()
    {
        _SFXSource.PlayOneShot(buttonClick);
    }
}
