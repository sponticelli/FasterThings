using System;
using UnityEngine;

namespace LiteNinja.SOEvents
{
    [CreateAssetMenu(menuName = "LiteNinja/Events/GameObject Event")]
    [Serializable]
    public class GameObjectEvent : ASOEvent<GameObject>
    {
    }
}