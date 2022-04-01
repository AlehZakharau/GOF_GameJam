using System.Collections;
using UnityEngine;
using VContainer;

namespace Code.GamePlay.Triggers
{
    public class EndDetector : MonoBehaviour
    {
        [SerializeField] private ParticleSystem[] particles;
        [SerializeField] private Canvas end;

        private IAudioCenter audioCenter;

        [Inject]
        public void Construct(IAudioCenter audioCenter)
        {
            this.audioCenter = audioCenter;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                StartCoroutine(StartFireworks());
                audioCenter.PlaySound(EAudioClips.Fireworks, new SourceConfig(true));
                end.enabled = true;
            }
        }


        private IEnumerator StartFireworks()
        {
            foreach (var particle in particles)
            {
                particle.Play();
                yield return new WaitForSeconds(Random.Range(0, 0.03f));
            }
        }
    }
}