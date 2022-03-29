using System;
using System.Collections;
using UnityEngine;

namespace Code.GamePlay.Camera
{
    public class CameraView : MonoBehaviour
    {
        public UnityEngine.Camera camera;

        public float speed = 2f;


        public event Action OnCameraEndMovement;  

        public void MoveCamera(Vector3 newPosition)
        {
            StartCoroutine(CMoveCamera(newPosition));
        }


        private IEnumerator CMoveCamera(Vector3 newPosition)
        {
            while (camera.transform.position != newPosition)
            {
                camera.transform.position =
                    Vector3.MoveTowards(camera.transform.position, 
                        newPosition, speed * Time.deltaTime);
                yield return null;
            }
            OnCameraEndMovement?.Invoke();
        }
    }
}