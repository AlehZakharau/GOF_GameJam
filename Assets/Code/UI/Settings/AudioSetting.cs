using CommonBaseUI.Data;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using VContainer;

namespace Code.UI
{
    public class AudioSetting : MonoBehaviour
    {
        [SerializeField] private AudioMixer mixer;
        [SerializeField] private Slider sliderSound;
        [SerializeField] private Slider sliderMusic;

        private GameSettingsDataManager data;
        private IAudioCenter audioCenter;

        [Inject]
        public void Construct(GameSettingsDataManager data, IAudioCenter audioCenter)
        {
            this.data = data;
            this.audioCenter = audioCenter;
        }

        private void Start()
        {
            sliderSound.value = data.SoundVolume;
            sliderMusic.value = data.MusicVolume;
            mixer.SetFloat("SoundVolume", Mathf.Log10(sliderSound.value) * 20);
            mixer.SetFloat("MusicVolume", Mathf.Log10(sliderMusic.value) * 20);
            sliderSound.onValueChanged.AddListener(delegate { ChangeSoundVolume(); });
            sliderMusic.onValueChanged.AddListener(delegate { ChangeMusicVolume(); });
        }

        public void ChangeSoundVolume()
        {
            var volume = Mathf.Log10(sliderSound.value) * 20;
            mixer.SetFloat("SoundVolume", volume);
            data.SoundVolume = sliderSound.value;
            audioCenter.PlaySound(EAudioClips.Button);
        }

        public void ChangeMusicVolume()
        {
            var volume = Mathf.Log10(sliderMusic.value) * 20;
            mixer.SetFloat("MusicVolume", volume);
            data.MusicVolume = sliderMusic.value;
        }
    }
}