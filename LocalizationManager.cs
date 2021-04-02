using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class LocalizationManager : MonoBehaviour
{
    private static GameObject cacheData;


    public static void LoadLocalizedText(string fileName)
    {
        cacheData = GameObject.Find("CacheData");
        Dictionary<string, string> localizedText = cacheData.GetComponent<CacheData>().localizedText;

        localizedText = new Dictionary<string, string>();
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath, Encoding.Default);
            LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);

            for (int i = 0; i < loadedData.items.Length; i++)
            {
                localizedText.Add(loadedData.items[i].key, loadedData.items[i].value);
            }

            cacheData.GetComponent<CacheData>().localizedText = localizedText;
            Debug.Log("Data loaded, dictionary contains: " + localizedText.Count + " entries");

        }
        else
        {
            Debug.LogError("Cannot find file!");
        }

    }

}