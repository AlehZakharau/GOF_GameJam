using System;
using System.Runtime.Serialization;
using UnityEngine;

namespace CommonBaseUI.Data
{
    public interface IGameConfig
    {
        public CommonData CommonData { get;}
    }
    [CreateAssetMenu(fileName = "GameConfig", menuName = "DataBase/GameConfig", order = 0)]
    public class GameConfig : ScriptableObject, IGameConfig
    {
        [SerializeField] private CommonData commonData;
        public CommonData CommonData => commonData;
    }

    [Serializable]
    public class CommonData : ISerializable
    {
        public float dogSpeed = 2f;
        [Header("Rhythm settings")]
        public float[] chordSteps;
        public float pauseNightDay;
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            
        }
    }
}