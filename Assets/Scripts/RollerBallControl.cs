using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Ball
{
    public class RollerBallControl : MonoBehaviour
    {
        int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
        float camRayLength = 100f;
        public bool useMouse = false;
        private Ball ball; // Reference to the ball controller.

        private Vector3 move;
        // the world-relative desired move direction, calculated from the camForward and user input.

        private Transform cam; // A reference to the main camera in the scenes transform
        private Vector3 camForward; // The current forward direction of the camera
        private bool jump; // whether the jump button is currently pressed
        private Rigidbody rb;

        public Vector3 GetMove(){
            return move;
        }

        private void Awake()
        {
            // Set up the reference.
            ball = GetComponent<Ball>();
            rb = ball.GetComponent<Rigidbody>();
            floorMask = LayerMask.GetMask("Floor");

            // get the transform of the main camera
            if (Camera.main != null)
            {
                cam = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Ball needs a Camera tagged \"MainCamera\", for camera-relative controls.");
                // we use world-relative controls in this case, which may not be what the user wants, but hey, we warned them!
            }
        }


        private void Update()
        {
            // Get the axis and jump input.
            Vector3 calcForward;
            Vector3 calcRight;
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
            jump = CrossPlatformInputManager.GetButton("Jump");

            // calculate move direction
            if (cam != null)
            {
                if (useMouse)
                {
                    // Create a ray from the mouse cursor on screen in the direction of the camera.
                    Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

                    // Create a RaycastHit variable to store information about what was hit by the ray.
                    RaycastHit floorHit;
                    // Perform the raycast and if it hits something on the floor layer...
                    if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
                    {
                        // Create a vector from the player to the point on the floor the raycast from the mouse hit.
                        calcForward = floorHit.point - transform.position;
                        calcRight = new Vector3(calcForward.z, calcForward.y, calcForward.x);
                        // Ensure the vector is entirely along the floor plane.
                        calcForward.y = 0f;
                    }
                    else
                    {
                        calcForward = cam.forward;
                        calcRight = cam.right;
                    }
                }
                else
                {
                    calcForward = cam.forward;
                    calcRight = cam.right;
                }

                move = (v * calcForward + h * calcRight).normalized;

            }
            else
            {
                // we use world-relative directions in the case of no main camera
                move = (v * Vector3.forward + h * Vector3.right).normalized;
            }
        }


        private void FixedUpdate()
        {
            // Call the Move function of the ball controller
            ball.Move(move, jump);
            jump = false;

        }
    }
}
