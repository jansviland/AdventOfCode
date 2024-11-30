using UnityEngine;

namespace AdventOfCode._2023.Day22.Unity.Common
{
    public class CubeGravity : MonoBehaviour
    {
        private Rigidbody rb;
        private bool isFalling = true;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            rb.useGravity = true; // Or apply a custom gravity force in Update if needed

            // add custom gravity force that only allow the the cube to fall down, not change direction, rotate etc
            rb.freezeRotation = true;
            rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;
        }

        void OnCollisionEnter(Collision collision)
        {
            // Check if the collision object is a cube
            if (collision.gameObject.CompareTag("Cube"))
            {
                isFalling = false;
                rb.isKinematic = true; // Stop the cube from being affected by physics
            }
        }

        void Update()
        {
            if (isFalling)
            {
                // Apply custom gravity force if needed
                // rb.AddForce(Physics.gravity * rb.mass);
            }
        }
    }
}