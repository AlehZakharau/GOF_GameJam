using System.Collections;
using UnityEngine;
using VContainer;

namespace Code.GamePlay
{
    public class Flashing : MonoBehaviour
    {
        [SerializeField] private Color showUpColor;
        [SerializeField] private Color hideColor;
        [SerializeField] private Material shader;

        private IAudioAnalyzer audioAnalyzer;
        
        private int activationIndex;
        private bool isActivation = true;
        private static readonly int FlashingColor = Shader.PropertyToID("_FlashColor");
        private static readonly int FlashingAmount = Shader.PropertyToID("_FlashAmount");

        [Inject]
        public void Construct(IAudioAnalyzer audioAnalyzer)
        {
            this.audioAnalyzer = audioAnalyzer;
            audioAnalyzer.GetSignal += AudioAnalyzerOnGetSignal;
        }

        private void AudioAnalyzerOnGetSignal()
        {
            var color = isActivation ? showUpColor : hideColor;

            StartCoroutine(FlashingShader(color));

            activationIndex++;
            if (activationIndex >= 4)
            {
                isActivation = !isActivation;
                activationIndex = 0;
            }
        }


        private IEnumerator FlashingShader(Color flashingColor)
        {
            shader.SetColor(FlashingColor, flashingColor);

            var a = Mathf.Lerp(0, 1, 1);
            shader.SetFloat(FlashingAmount, a);
            yield return new WaitForSeconds(0.01f);
            a = Mathf.Lerp(1, 0, 1);
            shader.SetFloat(FlashingAmount, a);
            yield return new WaitForSeconds(0.01f);
            a = Mathf.Lerp(0, 1, 1);
            shader.SetFloat(FlashingAmount, a);
            yield return new WaitForSeconds(0.02f);
            a = Mathf.Lerp(1, 0, 1);
            shader.SetFloat(FlashingAmount, a);
            yield return new WaitForSeconds(0.02f);
            a = Mathf.Lerp(0, 1, 1);
            shader.SetFloat(FlashingAmount, a);
            yield return new WaitForSeconds(0.01f);
            a = Mathf.Lerp(1, 0, 1f);
            shader.SetFloat(FlashingAmount, a);
            yield return new WaitForSeconds(0.01f);
        }
        
        private void OnDestroy()
        {
            audioAnalyzer.GetSignal -= AudioAnalyzerOnGetSignal;
        }
    }
}