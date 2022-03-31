using System;
using CommonBaseUI.Data;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Code.GamePlay
{
    public class PlatformController : MonoBehaviour
    {
        public GameObject[] platforms;

        
        private IAudioAnalyzer audioAnalyzer;

        private int activationIndex;
        private bool isActivation = true;

        [Inject]
        public void Construct(IAudioAnalyzer audioAnalyzer)
        {
            this.audioAnalyzer = audioAnalyzer;
            audioAnalyzer.GetSignal += AudioAnalyzerOnGetSignal;
        }

        private void Start()
        {
            foreach (var platform in platforms)
            {
                platform.SetActive(false);
            }
        }

        private void AudioAnalyzerOnGetSignal()
        {
            platforms[activationIndex++].SetActive(isActivation);

            if (activationIndex >= platforms.Length)
            {
                isActivation = !isActivation;
                activationIndex = 0;
            }
        }

        private void OnDestroy()
        {
            audioAnalyzer.GetSignal -= AudioAnalyzerOnGetSignal;
        }
    }
}