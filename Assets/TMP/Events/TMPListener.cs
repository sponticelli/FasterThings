using UnityEngine;

public class TMPListener : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    public void OnPlay(AudioClip clip)
    {
        Debug.Log("Play");
        if (_audioSource.isPlaying) _audioSource.Stop();
        _audioSource.clip = clip;
        _audioSource.Play();
    }

    private void Update()
    {

    }
}
