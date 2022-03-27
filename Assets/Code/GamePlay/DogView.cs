using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Code.GamePlay
{
    public class DogView : MonoBehaviour
    {
        public float dogSpeed = 10f;
        public Transform[] groundChecker;
        public float groundCheckerRadius = 0.1f;
        public LayerMask groundLayer;
        public float gravityForce = 9.8f;
        public float gravityScaler = 2f;
        public float dogHigh = 2f;
        public float jumpHigh = 2f;
        public GameObject gameObject;
        public Transform transform;
        public Rigidbody rig;
    }

}