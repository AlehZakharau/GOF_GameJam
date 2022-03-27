using System;
using UnityEngine;

namespace Code
{
    [CreateAssetMenu(fileName = "Audio", menuName = "DataBase/Audio", order = 0)]
    public class AudioDB : ScriptableObject
    {
        [SerializeField] private AudioBase[] audioBases;
        [SerializeField] private AudioPlaylist[] audioPlaylists;


        public AudioClip GetClip(EAudioClips name)
        {
            foreach (var audioBase in audioBases)
            {
                if (audioBase.name == name)
                {
                    return audioBase.clip;
                }
            }
            throw new Exception($"doesn't find Clip with name: {name}");
        }
        
        public AudioClip[] GetPlaylist(EAudioPlaylist name)
        {
            foreach (var audioPlaylist in audioPlaylists)
            {
                if (audioPlaylist.name == name)
                {
                    return audioPlaylist.clips;
                }
            }
            throw new Exception($"doesn't find Clip with name: {name}");
        }

    }

    [Serializable]
    public class AudioBase
    {
        public EAudioClips name;
        public AudioClip clip;
    }

    [Serializable]
    public class AudioPlaylist
    {
        public EAudioPlaylist name;
        public AudioClip[] clips;
    }

    public enum EAudioPlaylist
    {
        Music,
        Environment
    }

    public enum EAudioClips
    {
        MainTheme,
        Environment,
        CatScare,
        CatMeow,
        CatPure,
        Jump,
        Button,
        Splash,
        Fireworks,
        CatMeow2,
        CatEating
    }
}