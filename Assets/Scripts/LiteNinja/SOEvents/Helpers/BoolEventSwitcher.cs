using LiteNinja.SOEvents;
using UnityEngine;


namespace LiteNinja.Events.Helpers
{
    /// <summary>
    /// An helper to emit BoolEvent events alternating between true and false.
    /// </summary>
    [AddComponentMenu("LiteNinja/Components/Bool Event Switcher")]
    public class BoolEventSwitcher : MonoBehaviour
    {
        [SerializeField] private BoolEvent _boolEvent;
        [SerializeField] private bool _boolValue;
        [SerializeField] private bool _emitOnStart;

        private void Start()
        {
            if (_emitOnStart)
            {
                _boolEvent.Raise(_boolValue);
            }
        }

        public void SwitchBool()
        {
            _boolValue = !_boolValue;
            _boolEvent.Raise(_boolValue);
        }
    }
    
    
}