using System;
using UnityEngine;

namespace Code.GamePlay
{
    public class DogMove : MonoBehaviour
    {
        [SerializeField] private DogView dogView;


        private int jumpCount;
        private const int JumpMax = 1;

        private void Update()
        {
            MoveDog(Vector2.zero);
            Jump(false);
        }

        private void Jump(bool jump)
        {
            var gravity = dogView.gravityForce * dogView.gravityScaler;
            if (IsGrounded(dogView.groundChecker[0]) || IsGrounded(dogView.groundChecker[1]))
            {
                jumpCount = 0;
                gravity = 0;
                var a = FindSurface();
                if (a != Vector3.zero)
                {
                    var newPosition = new Vector3(dogView.transform.position.x,
                        a.y + dogView.dogHigh, dogView.transform.position.z);
                    dogView.transform.position = newPosition;
                }
            }

            if (jump)
            {
                if (jumpCount >= JumpMax) return;
                jumpCount++;
                gravity = dogView.jumpHigh;
            }

            dogView.transform.position = Vector3.MoveTowards(dogView.transform.position,
                new Vector3(dogView.transform.position.x, dogView.transform.position.y + gravity,
                    dogView.transform.position.z), dogView.dogSpeed * Time.deltaTime * Time.deltaTime);
            // dogView.transform.Translate(new Vector3(0, gravity, 0) * Time.deltaTime * Time.deltaTime);
        }


        private void MoveDog(Vector2 input)
        {
            if (input == Vector2.zero) return;

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
            var size = Physics.OverlapSphereNonAlloc(dogView.groundChecker[0].position, dogView.groundCheckerRadius, a,
                dogView.groundLayer);
            return a.Length > 0 ? a[0].transform.position : Vector3.zero;
        }
    }
}