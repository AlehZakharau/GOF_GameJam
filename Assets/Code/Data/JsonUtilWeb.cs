using System.Collections;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.Networking;

namespace CommonBaseUI.Data
{
    public sealed class JsonUtilWeb : MonoBehaviour, IJsonUtil
    {
        public void SaveToJson(string name, ISerializable data)
        {
            StartCoroutine(SaveToJsonWeb(name, data));
        }

        private IEnumerator SaveToJsonWeb(string name, ISerializable data)
        {
            string filePath = Path.Combine(Path.Combine
                (Application.streamingAssetsPath, name + ".json"));
            // переносим все переменные класса в формат json
            string jsonData = JsonUtility.ToJson(data);

            using (UnityWebRequest www = UnityWebRequest.Put(filePath, jsonData))
            {
                yield return www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    Debug.Log("Upload complete!");
                }
            }
        }

        public void LoadFromJson(string name, ISerializable data)
        {
            StartCoroutine(LoadDataFromJsonWeb(name, data));
        }

        private IEnumerator LoadDataFromJsonWeb(string name, ISerializable data)
        {
            //string filePath = Path.Combine(Application.dataPath, "SaveData.json");
            string filePath = Path.Combine(Path.Combine
                (Application.streamingAssetsPath, name + ".json"));
            UnityWebRequest www = new UnityWebRequest(filePath);
            www.downloadHandler = new DownloadHandlerBuffer();

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                JsonUtility.FromJsonOverwrite(www.downloadHandler.text, data);
                Debug.Log($"<color=green>Game data downloaded");
            }
        }
    }
}
