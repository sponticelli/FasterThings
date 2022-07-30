using System;
using UnityEngine;

namespace LiteNinja.SOVariable
{
    [CreateAssetMenu(menuName = "LiteNinja/Variables/String Var", fileName = "StringVar")]
    [Serializable]
    public class StringVar : ASOVar<string>
    {
    }
}