using System;
using UnityEngine;
using VContainer.Unity;

namespace Code.GamePlay
{
    public interface IAudioAnalyzer
    {
        public event Action GetSignal;
    }
    public class AudioAnalyzer : IAudioAnalyzer, IStartable, ITickable
    {
        private readonly IAudioSourceFabric fabric;
        private readonly AudioDB audioDB;

        private readonly AudioSource audioSource;

        private float pitchValue;
        
        private const int QSamples = 1024;
        private const float Threshold = 0.02f;
        
        float[] samples;
        private float[] spectrum;
        private float fSample;

        private bool detected;
        private float timer;
        private float pitchCup = 0.4f;
        private int pitchCounter;

        private float delayTimer;
        private float delayStart = 2f;
        private bool delay;

        public event Action GetSignal;

        public AudioAnalyzer(IAudioSourceFabric fabric, AudioDB audioDB)
        {
            this.fabric = fabric;
            this.audioDB = audioDB; 

            audioSource = fabric.CreateSource();
        }

        public void Start()
        {
            audioSource.clip = audioDB.GetClip(EAudioClips.TestBit);
            audioSource.loop = true;
            audioSource.pitch = 1.2f;
            samples = new float[QSamples];
            spectrum = new float[QSamples];
            fSample = AudioSettings.outputSampleRate;
        }

        public void Tick()
        {
            audioSource.GetOutputData(samples, 0);

            audioSource.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);
            var maxN = 0;
            float maxV = 0;
            for (var i = 0; i < QSamples; i++)
            { // find max 
                if (!(spectrum[i] > maxV) || !(spectrum[i] > Threshold))
                    continue;
 
                maxV = spectrum[i];
                maxN = i; // maxN is the index of max
            }
            
            float freqN = maxN; 
            if (maxN > 0 && maxN < QSamples - 1)
            { // interpolate index using neighbours
                var dL = spectrum[maxN - 1] / spectrum[maxN];
                var dR = spectrum[maxN + 1] / spectrum[maxN];
                freqN += 0.5f * (dR * dR - dL * dL);
            }
            pitchValue = freqN * (fSample / 2) / QSamples;

            SendSignal(pitchCup);
            DelayStart();
        }

        private void SendSignal(float pitch)
        {
            if (detected)
            {
                timer += Time.deltaTime;
            }
            if (pitchValue > 0 && !detected)
            {
                detected = true;
                Debug.Log($"Pitch {pitchCounter}");
                pitchCounter++;
                GetSignal?.Invoke();
            }
            if (timer > pitchCup)
            {
                detected = false;
                timer = 0;
            }
        }

        private void DelayStart()
        {
            if (delay) return;
            delayTimer += Time.deltaTime;

            if (delayTimer> delayStart)
            {
                timer = 0;
                delay = true;
                audioSource.Play();
            }
        }
    }
}