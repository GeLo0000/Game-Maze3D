using UnityEngine;

public class SpikeController : MonoBehaviour
{
    // Reference to the AudioSource for playing sound
    [SerializeField] private AudioSource _spikeAudioSource;

    // Audio clips for the spike's up and down movements
    [SerializeField] private AudioClip _spikeUpSound;
    [SerializeField] private AudioClip _spikeDownSound;

    // Method to play the spike-up sound, triggered by animation event
    public void PlaySpikeUPSound()
    {
        if (_spikeAudioSource != null && _spikeUpSound != null)
        {
            _spikeAudioSource.PlayOneShot(_spikeUpSound);
        }
    }

    // Method to play the spike-down sound, triggered by animation event
    public void PlaySpikeDownSound()
    {
        if (_spikeAudioSource != null && _spikeDownSound != null)
        {
            _spikeAudioSource.PlayOneShot(_spikeDownSound);
        }
    }
}
