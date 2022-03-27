using CommonBaseUI.Data;
using UnityEngine;
using VContainer.Unity;

namespace Code.GamePlay
{
    public class PlatformController : ITickable, IStartable
    {
        private readonly IGameConfig gameConfig;
        private float[] chordSteps;

        private int stepIndex;
        private float currentStep;

        private int StepIndex
        {
            get => stepIndex;
            set => stepIndex = stepIndex >= chordSteps.Length - 1 ? 0 : value;
        }

        public PlatformController(IGameConfig gameConfig)
        {
            this.gameConfig = gameConfig;

            chordSteps = new float[gameConfig.CommonData.chordSteps.Length + 1];
            chordSteps[0] = gameConfig.CommonData.pauseNightDay;
            for (int i = 0; i < gameConfig.CommonData.chordSteps.Length; i++)
            {
                chordSteps[i + 1] = gameConfig.CommonData.chordSteps[i];
            }
            currentStep = chordSteps[0];
        }
        public void Tick()
        {
            currentStep -= Time.deltaTime;
            if (currentStep < 0)
            {
                StepIndex++;
                currentStep = chordSteps[StepIndex];
                //Debug.Log($"Step {StepIndex}");
            }
        }

        public void Start()
        {
            Debug.Log("Start");
        }
    }
}