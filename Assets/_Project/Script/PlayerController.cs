using UnityEngine;
using UnityEngine.InputSystem;

namespace Nexuris.Gameplay
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;
        private Rigidbody2D rb;
        private Vector2 moveInput;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        void FixedUpdate()
        {
            rb.linearVelocity = moveInput * moveSpeed;
        }

        public void Move(InputAction.CallbackContext context)
        {
            moveInput = context.ReadValue<Vector2>();
        }
    }
}