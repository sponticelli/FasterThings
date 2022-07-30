using System;
using UnityEngine;

namespace LiteNinja.SOVariable
{
    [CreateAssetMenu(menuName = "LiteNinja/Variables/Quaternion Var", fileName = "QuaternionVar")]
    [Serializable]
    public class QuaternionVar : ASOVar<Quaternion>
    {
    }
}