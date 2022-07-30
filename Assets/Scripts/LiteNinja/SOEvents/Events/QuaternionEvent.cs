using System;
using UnityEngine;

namespace LiteNinja.SOEvents
{
    [CreateAssetMenu(menuName = "LiteNinja/Events/Quaternion Event")]
    [Serializable]
    public class QuaternionEvent : ASOEvent<Quaternion>
    {
    }
}