using UnityEngine;
using VContainer;

namespace Code.GamePlay.Triggers.Code.GamePlay.Triggers
{
    public class FinishLevelDetector : MonoBehaviour
    {
        [SerializeField] private Transform finishPosition;
        private ITriggersDetector triggersDetector;
        private IAudioCenter audioCenter;

        private LayerMask layerMask;
        private bool isPlayed;

        [Inject]
        public void Construct(ITriggersDetector triggersDetector, IAudioCenter audioCenter)
        {
            this.triggersDetector = triggersDetector;
            this.audioCenter = audioCenter;
        }

        private void Start()
        {
            layerMask = LayerMask.NameToLayer("Player");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == layerMask)
            {
                triggersDetector.DetectTriggerSetPosition(ETriggers.FinishLevel, finishPosition.position);
                if (!isPlayed)
                {
                    audioCenter.PlaySound(EAudioClips.CheckPoint);
                    isPlayed = true;
                }
            }
        }
    }
}