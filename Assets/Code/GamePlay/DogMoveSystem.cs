using System;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer.Unity;

namespace Code.GamePlay
{
    public class DogMoveSystem : ITickable, IFixedTickable
    {
        private readonly DogView dogView;
        private readonly IPlayerInput playerInput;

        private bool jumping;
        private float jumpingCup = 0.3f;
        private float jumpingTimer = 0f;
        private float jumpingCof = 1f;
        private int jumpCount;
        private const int JumpMax = 1;

        private Vector3 force;
        private float gravityScaler;
        

        public DogMoveSystem(DogView dogView, IPlayerInput playerInput)
        {
            this.dogView = dogView;
            this.playerInput = playerInput;
            
            playerInput.Actions.Player.Jump.started += JumpOnstarted;
            playerInput.Actions.Player.Jump.canceled += JumpOnstarted;
        }

        public void Tick()
        {
            var velocity = playerInput.Actions.Player.Move.ReadValue<Vector2>();
            var jump = playerInput.Actions.Player.Jump.triggered;
            
            MoveDog(velocity);
            Jump(jump);
            CalculateJumpHigh();
        }

        public void FixedTick()
        {
            dogView.rig.AddForce(Physics.gravity * (gravityScaler - 1) * dogView.rig.mass);
        }

        private void CalculateJumpHigh()
        {
            if (jumpingTimer > jumpingCup)
            {
                jumping = false;
                jumpingTimer = 0;
            }
            if (jumping)
            {
                dogView.rig.velocity = new Vector2(dogView.rig.velocity.x, dogView.jumpHigh);
                jumpingTimer += Time.deltaTime;
            }
        }

        private void JumpOnstarted(InputAction.CallbackContext obj)
        {
            if (obj.started)
            {
                jumping = true;
            }
            else if (obj.canceled)
            {
                jumping = false;
                jumpingTimer = 0;
                jumpingCof = 1f;
            }
        }

        private void Jump(bool jump)
        {
            // if (jump)
            // {
            //     var jumpForce = Mathf.Sqrt(dogView.jumpHigh * -2 * (Physics2D.gravity.y * gravityScaler - 1));
            //     dogView.rig.AddForce(new Vector3(0, jumpForce * jumpingCof, 0), (ForceMode)ForceMode2D.Impulse);
            // }

            if (dogView.rig.velocity.y >= 0)
            {
                gravityScaler = dogView.gravityScaler;
            }
            else if(dogView.rig.velocity.y < 0)
            {
                gravityScaler = dogView.fallingGravityScaler;
            }
        }

        private void JumpPushing()
        {
            
        }


        private void MoveDog(Vector2 input)
        {
            if(input == Vector2.zero) return;

            var addSpeed = 1f;
            if (!IsGrounded(dogView.groundChecker[1]))
                addSpeed *= 1.5f; 
            var newPosition = new Vector3(input.y, 0, input.x);
            dogView.transform.position += newPosition * dogView.dogSpeed * addSpeed * Time.deltaTime;
        }


        private bool IsGrounded(Transform checker)
        {
            return Physics.CheckSphere(checker.position, dogView.groundCheckerRadius, dogView.groundLayer);
        }

        private Vector3 FindSurface()
        {
            var a = Array.Empty<Collider>();
            var size = Physics.OverlapSphereNonAlloc(dogView.groundChecker[0].position, dogView.groundCheckerRadius, a, dogView.groundLayer);
            return a.Length > 0 ? a[0].transform.position : Vector3.zero;
        }
    }
}