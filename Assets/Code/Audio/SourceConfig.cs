using UnityEngine.Audio;

namespace Code
{
    public struct SourceConfig
    {
        public bool Loop;
        public float PitchRange;
        public EAudioMixerGroupNames MixerGroup;

        public SourceConfig(bool loop = false, float pitchRange = 0, 
            EAudioMixerGroupNames audioMixerGroup = EAudioMixerGroupNames.Master )
        {
            Loop = loop;
            PitchRange = pitchRange;
            MixerGroup = audioMixerGroup;
        }
    }
}