using System;
using UnityEngine;

namespace LiteNinja.SOVariable
{
    [CreateAssetMenu(menuName = "LiteNinja/Variables/Bool Var", fileName = "BoolVar")]
    [Serializable]
    public class BoolVar : ASOVar<bool>
    {
    }
}