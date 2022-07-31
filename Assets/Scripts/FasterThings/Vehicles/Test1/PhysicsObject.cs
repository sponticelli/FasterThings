using UnityEngine;

namespace FasterThings.Vehicles.Test1
{
    public class PhysicsObject : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<MeshCollider>().convex = true;

            var rigidbody = gameObject.AddComponent<Rigidbody>();
            rigidbody.interpolation = RigidbodyInterpolation.Extrapolate;
        }

        public void Hit(Vector3 velocity)
        {
            velocity = Quaternion.AngleAxis(Random.Range(-15, 15), Vector3.up) * velocity;
            velocity = Quaternion.AngleAxis(Random.Range(-15, 15), Vector3.right) * velocity;

            GetComponent<Rigidbody>().AddForce(velocity * Random.Range(30, 60));
            GetComponent<Rigidbody>().AddTorque(velocity * Random.Range(30, 60));

            gameObject.layer = 2;
        }
    }
}