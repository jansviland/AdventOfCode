using UnityEngine;

namespace AdventOfCode._2023.Day22.Unity.Common
{
    public class CameraController : MonoBehaviour
    {
        public Transform target; // The tower or object you want to rotate around
        public float distance = 15.0f; // Distance from the target
        public float speed = 50.0f; // Speed of the rotation
        public float height = 5.0f; // Height above the target

        private float angle = 0;

        void Update()
        {
            // rotate
            if (Input.GetKey(KeyCode.RightArrow))
            {
                angle += speed * Time.deltaTime; // Rotate right
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                angle -= speed * Time.deltaTime; // Rotate left
            }

            // move up and down
            if (Input.GetKey(KeyCode.UpArrow))
            {
                height += speed * Time.deltaTime; // Move up
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                height -= speed * Time.deltaTime; // Move down
            }

            Debug.Log("Target position: " + target.position);
            Debug.Log("Angle: " + angle);

            // Calculating position based on angle and distance
            Vector3 offset = Quaternion.Euler(0, angle, 0) * new Vector3(0, 0, distance);
            transform.position = target.position - offset;

            transform.position = new Vector3(target.position.x + offset.x, height, target.position.z + offset.z);

            Debug.Log("Updated position: " + transform.position);
            Debug.DrawLine(target.position, transform.position, Color.red); // Draw a line in the Scene window to better see the position of the camera

            // Always look at the target
            transform.LookAt(target);
        }
    }
}