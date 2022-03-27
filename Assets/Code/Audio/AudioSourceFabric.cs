using UnityEngine;

namespace Code
{
    public interface IAudioSourceFabric
    {
        public AudioSource CreateSource();
    }
    public class AudioSourceFabric : MonoBehaviour, IAudioSourceFabric
    {
        [Header("Prefab with Audio Source")]
        [SerializeField] private AudioSource prefab;
        
        public AudioSource CreateSource()
        {
            var instance = Instantiate(prefab);
            return instance.GetComponent<AudioSource>();
        }
    }
}