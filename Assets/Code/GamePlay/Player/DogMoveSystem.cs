using UnityEngine;
using UnityEngine.InputSystem;
using VContainer.Unity;

namespace Code.GamePlay
{
    public class DogMoveSystem : ITickable, IFixedTickable
    {
        private readonly DogView dogView;
        private readonly IPlayerInput playerInput;
        private readonly IAudioCenter audioCenter;

        private bool jumping;
        private float jumpingTimer;
        private bool inAir;
        
        private int jumpCount;
        private const int JumpMax = 1;
        private float addSpeed = 1f;

        private float gravityScaler;

        private Vector3 platformMove;
        private static readonly int Run = Animator.StringToHash("Run");
        private static readonly int Jump1 = Animator.StringToHash("Jump");


        public DogMoveSystem(DogView dogView, IPlayerInput playerInput, IAudioCenter audioCenter)
        {
            this.dogView = dogView;
            this.playerInput = playerInput;
            this.audioCenter = audioCenter;
            
            playerInput.Actions.Player.Jump.started += Jump;
            playerInput.Actions.Player.Jump.canceled += Jump;
        }

        public void Tick()
        {
            var velocity = playerInput.Actions.Player.Move.ReadValue<Vector2>();
            var jump = playerInput.Actions.Player.Jump.triggered;
            
            MoveDog(velocity);
            CheckIsGrounded();
            Jump(jump);
            CalculateJumpHigh();
        }

        public void FixedTick()
        {
            dogView.rig.AddForce(Physics.gravity * (gravityScaler - 1) * dogView.rig.mass);

            dogView.rig.velocity += platformMove;
        }

        private void CalculateJumpHigh()
        {
            if (jumpingTimer > dogView.jumpingCup)
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

        private void Jump(InputAction.CallbackContext obj)
        {
            if (obj.started && jumpCount < JumpMax)
            {
                jumpCount++;
                inAir = true;
                jumping = true;
                dogView.Animator.SetBool(Jump1, true);
                var random = Random.Range(0, 5);
                if(random == 0) audioCenter.PlaySound(EAudioClips.Jump);
            }
            else if (obj.canceled)
            {
                jumping = false;
                jumpingTimer = 0;
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

        private void MoveDog(Vector2 input)
        {
            if (input == Vector2.zero)
            {
                dogView.Animator.SetBool(Run, false);
                return;
            }
            addSpeed = 1;
            if (!IsGrounded(dogView.groundChecker[1]))
                addSpeed *= dogView.airSpeed;
            dogView.Animator.SetBool(Run, true);

            dogView.SpriteRenderer.flipX =  input.x < 0;
            var newPosition = new Vector3(input.y, 0, input.x);
            dogView.transform.position += newPosition * dogView.dogSpeed * addSpeed * Time.deltaTime;
        }

        private void CheckIsGrounded()
        {
            if (IsGrounded(dogView.groundChecker[0]) && inAir && !jumping)
            {
                jumpCount = 0;
                inAir = false;
                dogView.Animator.SetBool(Jump1, false);
                var random = Random.Range(0, 5);
                if(random == 0) audioCenter.PlaySound(EAudioClips.Ouu);
            }
            //platformMove = IsOnPlatform(dogView.groundChecker[0]);
        }

        private Vector3 IsOnPlatform(Transform checker)
        {
            var colliders =
                Physics.OverlapSphere(checker.position, dogView.groundCheckerRadius, dogView.movPlatformLayer);
            foreach (var collider in colliders)
            {
                collider.gameObject.TryGetComponent(out Rigidbody rig);
                Debug.Log($"Moving {rig.velocity}");
                return rig.velocity;
            }
            return Vector3.zero;
        }


        private bool IsGrounded(Transform checker)
        {
            return Physics.CheckSphere(checker.position, dogView.groundCheckerRadius, dogView.groundLayer);
        }
    }
}