using System;
using UnityEngine;

namespace LiteNinja.SOVariable
{
    [CreateAssetMenu(menuName = "LiteNinja/Variables/Int Var", fileName = "IntVar")]
    [Serializable]
    public class IntVar : ASOVar<int>
    {
    }
}