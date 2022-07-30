using System;
using UnityEngine;

namespace LiteNinja.SOVariable
{
    [CreateAssetMenu(menuName = "LiteNinja/Variables/AudioClip Var", fileName = "AudioClipVar")]
    [Serializable]
    public class AudioClipVar : ASOVar<AudioClip>
    {
    }
}