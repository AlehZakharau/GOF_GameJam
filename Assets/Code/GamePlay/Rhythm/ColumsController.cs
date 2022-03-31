using System;
using System.Collections;
using UnityEngine;
using VContainer;

namespace Code.GamePlay
{
    public class ColumsController : MonoBehaviour
    {
        public GameObject[] columns;
        public Vector3 target;
        public float speedForward;
        public float speedBack;

        
        private IAudioAnalyzer audioAnalyzer;

        private int activationIndex;

        [Inject]
        public void Construct(IAudioAnalyzer audioAnalyzer)
        {
            this.audioAnalyzer = audioAnalyzer;
            audioAnalyzer.GetSignal += AudioAnalyzerOnGetSignal;
        }
        


        private void AudioAnalyzerOnGetSignal()
        {
            StartCoroutine(MoveColumn(columns[activationIndex].transform, columns[activationIndex].transform.position + target,
                speedForward, activationIndex++));

            if (activationIndex >= columns.Length)
            {
                activationIndex = 0;
            }
        }

        private IEnumerator MoveColumn(Transform column, Vector3 target, float speed, int index)
        {
            var startPosition = column.position;
            var time = 0f;

            while (column.position != target)
            {
                column.position = Vector3.Lerp(column.position, target,
                    (time/Vector2.Distance(startPosition, target))*speed);
                time += Time.deltaTime;
                yield return null;
            }
            
            if (columns[index].transform.position == target)
            {
                StartCoroutine(MoveBack(columns[index].transform, startPosition,
                    speedBack, index));
                yield break;
            }
            
            //float timeScale = 1 / (Vector3.Distance(startPosition, endPosition) / speed);
            //obj.transform.position = Vector3.Lerp(startPosition, endPosition, Mathf.Abs(Time.time * timeScale % 2 – 1));
        }

        private IEnumerator MoveBack(Transform column, Vector3 target, float speed, int index)
        {
            yield return new WaitForSeconds(0.3f);
            
            var startPosition = column.position;
            var time = 0f;

            while (column.position != target)
            {
                column.position = Vector3.Lerp(column.position, target,
                    (time/Vector2.Distance(startPosition, target))*speed);
                time += Time.deltaTime;
                yield return null;
            }
        }

        private void OnDestroy()
        {
            audioAnalyzer.GetSignal -= AudioAnalyzerOnGetSignal;
        }
    }
}