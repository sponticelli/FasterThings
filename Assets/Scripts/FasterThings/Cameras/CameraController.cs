using UnityEngine;

namespace FasterThings.Cameras
{
    public abstract class CameraController : MonoBehaviour
    {

        public enum UpdateType
        {
            FixedUpdate,
            LateUpdate,
            Update
        }

        public UpdateType UpdateMode;
        
        public Transform[] HumanPlayers;
        public Transform[] BotPlayers;

        protected Camera _camera;


        protected virtual void Awake()
        {
            _camera = GetComponentInChildren<Camera>();
        }


        protected abstract void CameraUpdate();

        protected virtual void Update()
        {
            if (UpdateMode == UpdateType.Update)
            {
                CameraUpdate();
            }
        }

        protected virtual void LateUpdate()
        {
            if (UpdateMode == UpdateType.LateUpdate)
            {
                CameraUpdate();
            }
        }

        protected virtual void FixedUpdate()
        {
            if (UpdateMode == UpdateType.FixedUpdate)
            {
                CameraUpdate();
            }
        }
    }
}