using UnityEngine;

public class SpikeController : MonoBehaviour
{
    [SerializeField] private AudioSource spikeAudioSource;
    [SerializeField] private AudioClip spikeUpSound;
    [SerializeField] private AudioClip spikeDownSound;

    // �����, ���� ���� ���������� � ��䳿 �������
    public void PlaySpikeUPSound()
    {
        if (spikeAudioSource != null && spikeUpSound != null)
        {
            spikeAudioSource.PlayOneShot(spikeUpSound);
        }
    }
    public void PlaySpikeDownSound()
    {
        if (spikeAudioSource != null && spikeDownSound != null)
        {
            spikeAudioSource.PlayOneShot(spikeDownSound);
        }
    }
}

