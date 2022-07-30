using System;
using UnityEngine;


namespace LiteNinja.Actions
{
    
    [AddComponentMenu("LiteNinja/Actions/AutoRotate")]
    public class AutoRotate : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _rotation;
        
        [SerializeField]
        private Space _space;
        
        [SerializeField]
        private float _speed;

        private void Start()
        {
            _rotation = _rotation.normalized;
        }

        // Update is called once per frame
        private void Update()
        {
            transform.Rotate(_rotation * (_speed * Time.deltaTime), _space);
        }
        
        
    }
}