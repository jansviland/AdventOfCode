using UnityEngine;

namespace AdventOfCode._2023.Day22.Unity.Common
{
    public class CameraController : MonoBehaviour
    {
        public Transform target; // The tower or object you want to rotate around
        public float distance = 15.0f; // Distance from the target
        public float speed = 5.0f; // Speed of the rotation

        private float angle = 0;

        void Update()
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                angle += speed * Time.deltaTime; // Rotate right
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                angle -= speed * Time.deltaTime; // Rotate left
            }

            // Calculating position based on angle and distance
            // Vector3 offset = Quaternion.Euler(angle,90, 0) * new Vector3(distance, 0, 0);
            // transform.position = target.position + offset;

            // turn the camera so z is up
            // transform.rotation = Quaternion.Euler(angle, 90, 0);
            

            Debug.Log("Camera position: " + transform.position);
            Debug.DrawLine(target.position, transform.position, Color.red); // Draw a line in the Scene window to better see the position of the camera

            // Always look at the target
            // transform.LookAt(target);
        }
    }
}