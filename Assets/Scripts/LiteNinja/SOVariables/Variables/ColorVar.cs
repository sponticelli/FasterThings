using System;
using UnityEngine;

namespace LiteNinja.SOVariable
{
    [CreateAssetMenu(menuName = "LiteNinja/Variables/Color Var", fileName = "ColorVar")]
    [Serializable]
    public class ColorVar : ASOVar<Color>
    {
    }
}