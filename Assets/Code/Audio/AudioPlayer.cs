using System;
using UnityEngine;
using VContainer.Unity;

namespace Code
{
    public interface IEnvironmentPlayer : IAudioPlayer
    {
        
    }
    
    public interface IMusicAudioPlayer : IAudioPlayer
    {
        
    }
    
    public interface IAudioPlayer
    {
        
    }
    
    public class AudioPlayer : ITickable, IAudioPlayer
    {
        private readonly AudioSourceFabric fabric;
        private readonly AudioDB audioDB;
        
        private readonly AudioSource currentTrack;
        private readonly AudioSource nextTrack;

        private AudioClip[] clips;
        private float currentLength;

        public AudioPlayer(AudioSourceFabric fabric, AudioDB audioDB)
        {
            this.fabric = fabric;
            this.audioDB = audioDB;

            currentTrack = fabric.CreateSource();
            nextTrack = fabric.CreateSource();
            
        }

        public void Tick()
        {
            currentLength -= Time.deltaTime;
        }


        public void PlayPlaylist(EAudioPlaylist playlistName)
        {
            clips = audioDB.GetPlaylist(playlistName);

            if (clips.Length == 0)
                throw new Exception($"There no {playlistName} clips in Audio Data Base");
            
            currentTrack.clip = clips[0];
            currentTrack.Play();

            currentLength = currentTrack.clip.length;
        }
    }
}