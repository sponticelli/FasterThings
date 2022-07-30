using LiteNinja.SOEvents;
using UnityEngine;

public class TMPRaiser : MonoBehaviour
{
    [SerializeField] private AudioClipEvent _audioClipEvent;
    [SerializeField] private AudioClip _audioClip;
    
    private void Start()
    {
        _audioClipEvent?.Raise(_audioClip);
    }

    
}
