using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Code.GamePlay
{
    public class MovingPlatform : MonoBehaviour
    {
        [SerializeField] private Transform start;
        [SerializeField] private Transform end;
        [SerializeField] private float speed;
        [SerializeField] private Rigidbody rigidbody;
        
        private Vector3 startPosition;
        private Vector3 endPosition;

        private void Start()
        {
            startPosition = start.position;
            endPosition = end.position;
        }

        private void Update()
        {
            var timeScale = 1 / (Vector3.Distance(startPosition, endPosition) / speed);
            transform.position = Vector3.Lerp(startPosition, endPosition, Mathf.Abs(Time.time * timeScale % 2 - 1));
        }

        // private void FixedUpdate()
        // {
        //     var timeScale = 1 / (Vector3.Distance(startPosition, endPosition) / speed);
        //     rigidbody.MovePosition(Vector3.Lerp(startPosition, endPosition, Mathf.Abs(Time.time * timeScale % 2 - 1)));
        // }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                other.transform.SetParent(transform);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                other.transform.SetParent(null);
            }
        }
    }
}