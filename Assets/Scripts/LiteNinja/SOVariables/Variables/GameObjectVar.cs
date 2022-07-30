using System;
using UnityEngine;

namespace LiteNinja.SOVariable
{
    [CreateAssetMenu(menuName = "LiteNinja/Variables/GameObject Var", fileName = "GameObjectVar")]
    [Serializable]
    public class GameObjectVar : ASOVar<AudioClip>
    {
    }
}