using System.IO;
using System.Runtime.Serialization;
using UnityEngine;

namespace CommonBaseUI.Data
{
    public sealed class JsonUtil : IJsonUtil
    {
        public void SaveToJson(string name, ISerializable data)
        {
            // путь к файлу
            string filePath = Path.Combine(Application.dataPath, name + ".json"); // это то же самое, что Application.dataPath+"\SaveData.json"

            // переносим все переменные класса в формат json
            string jsonData = JsonUtility.ToJson(data);
            // записываем данные в файл
            File.WriteAllText(filePath, jsonData);
            //Debug.Log("Game saved to: " + filePath);
        }

        public void LoadFromJson(string name, ISerializable data)
        {
            //string filePath = Path.Combine(Application.dataPath, "SaveData.json");
            string filePath = Path.Combine(Application.dataPath, name + ".json");
            // если файл существует
            if (File.Exists(filePath))
            {
                // вытаскиваем их файла все данные в формате json
                string jsonData = File.ReadAllText(filePath);
                // переносим данные в класс
                JsonUtility.FromJsonOverwrite(jsonData, data);
                //Debug.Log("Game loaded from: " + filePath);
            }
            else
            {
                SaveToJson(name, data);
            }
        }
    }
}