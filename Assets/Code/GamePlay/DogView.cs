using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Code.GamePlay
{
    public class DogView : MonoBehaviour
    {
        [Header("Movement")] 
        public float airSpeed = 1.5f;
        public float dogSpeed = 10f;
        [Header("Ground Check")]
        public Transform[] groundChecker;
        public float groundCheckerRadius = 0.1f;
        public LayerMask groundLayer;
        [Header("Jump param")]
        public float gravityScaler = 2f;
        public float fallingGravityScaler = 6f;
        public float jumpHigh = 2f;
        public float jumpingCup = 0.3f;
        [Header("Public Data")]
        public GameObject gameObject;
        public Transform transform;
        public Rigidbody rig;
    }

}