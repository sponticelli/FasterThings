using UnityEngine;

namespace FasterThings.AI
{
    public abstract class AVehicleAI : MonoBehaviour
    {
        [SerializeField]
        protected bool _active;
        
        public bool Active
        {
            get => _active;
            set => _active = value;
        }
    }
}