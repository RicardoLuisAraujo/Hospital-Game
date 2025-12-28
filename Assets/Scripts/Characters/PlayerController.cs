using UnityEngine;
using UnityEngine.InputSystem;
using HauntedHospital.Core;

namespace HauntedHospital.Characters
{
    /// <summary>
    /// Handles player movement, gravity, and rotation.
    /// Designed for a 3rd-person, fixed-camera social RPG style.
    /// </summary>
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private float moveSpeed = 5.0f;
        [SerializeField] private float rotationSpeed = 10.0f;
        [SerializeField] private float gravity = -9.81f;

        [Header("References")]
        [Tooltip("Direct reference to the camera is needed to ensure 'forward' is relative to what the player sees.")]
        [SerializeField] private Transform cameraTransform;

        private CharacterController controller;
        private Vector2 moveInput;
        private Vector3 velocity;
        private bool isGrounded;

        private void Awake()
        {
            controller = GetComponent<CharacterController>();
            
            // Auto-find the main camera if one isn't assigned
            if (cameraTransform == null && Camera.main != null)
                cameraTransform = Camera.main.transform;
        }

        private void Update()
        {
            // Stop movement if the game is in Dialogue mode
            if (GameManager.Instance != null && GameManager.Instance.GetCurrentState() == GameState.Dialogue)
            {
                moveInput = Vector2.zero;
            }
            else
            {
                // Read keyboard input every frame for immediate responsiveness
                ReadKeyboardInput();
            }

            ApplyMovement();
            ApplyGravity();
        }

        /// <summary>
        /// Manually reads WASD input from the Keyboard.
        /// This avoids the need for a complex Input Actions asset during setup.
        /// </summary>
        private void ReadKeyboardInput()
        {
            if (Keyboard.current == null) return;

            Vector2 input = Vector2.zero;
            
            if (Keyboard.current.wKey.isPressed) input.y += 1; // North
            if (Keyboard.current.sKey.isPressed) input.y -= 1; // South
            if (Keyboard.current.aKey.isPressed) input.x -= 1; // West
            if (Keyboard.current.dKey.isPressed) input.x += 1; // East

            // Normalize prevents moving faster diagonally
            moveInput = input.normalized;
        }

        private void ApplyMovement()
        {
            isGrounded = controller.isGrounded;
            if (isGrounded && velocity.y < 0)
                velocity.y = -2f; // Keep the player stuck to the floor

            if (moveInput.magnitude < 0.1f) return;

            // 1. Get camera direction but ignore its vertical tilt (Y)
            Vector3 forward = cameraTransform.forward;
            Vector3 right = cameraTransform.right;
            forward.y = 0;
            right.y = 0;
            forward.Normalize();
            right.Normalize();

            // 2. Combine camera directions with player input
            Vector3 moveDirection = (forward * moveInput.y + right * moveInput.x).normalized;

            // 3. Smoothly rotate character to face the direction they are moving
            if (moveDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }

            // 4. Actually move the character
            controller.Move(moveDirection * moveSpeed * Time.deltaTime);
        }

        private void ApplyGravity()
        {
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
    }
}
