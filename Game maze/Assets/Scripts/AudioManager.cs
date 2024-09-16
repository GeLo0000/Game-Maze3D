using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("-------- Audio Source -------")]
    public AudioSource musicSource;
    public AudioSource soundMoveSource;
    [SerializeField] private AudioSource SFXSource;

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
    public AudioClip destroyStone;
    public AudioClip flyStone;

    private void Start()
    {
        PlayMusic(background);
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void PlayMoveSource(AudioClip clip)
    {
        soundMoveSource.clip = clip;
        soundMoveSource.Play();
    }

    public void PlayButton()
    {
        SFXSource.PlayOneShot(buttonClick);
    }
}
