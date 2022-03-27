using System.Runtime.Serialization;
using UnityEngine;

namespace CommonBaseUI.Data
{
    public interface IJsonUtil
    {
        public void SaveToJson(string name, ISerializable data);

        public void LoadFromJson(string name, ISerializable data);
    }
}
