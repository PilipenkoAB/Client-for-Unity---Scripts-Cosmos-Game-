using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class LocalizedText : MonoBehaviour
{
    private static GameObject cacheData;

    public string key;
    private string missingTextString = "?????";

    private Dictionary<string, string> localizedText;
    // Use this for initialization
    void Start()
    {
        cacheData = GameObject.Find("CacheData");
        localizedText = cacheData.GetComponent<CacheData>().localizedText;

        string result = missingTextString;

        try
        {
            if (localizedText.ContainsKey(key))
            {
                result = localizedText[key];
            }
        }
        catch
        {
            Debug.LogError("GetLocalizedValue error");
        }

        Text text = GetComponent<Text>();
        text.text = result;

    }

    public static string Localize(string key) 
    {
        string missingTextString = "?????";

        cacheData = GameObject.Find("CacheData");
        Dictionary<string, string> localizedText = cacheData.GetComponent<CacheData>().localizedText;

        cacheData = GameObject.Find("CacheData");
        localizedText = cacheData.GetComponent<CacheData>().localizedText;

        string result = missingTextString;

        try
        {
            if (localizedText.ContainsKey(key))
            {
                result = localizedText[key];
            }
        }
        catch
        {
            Debug.LogError("GetLocalizedValue error");
        }

        return result;
    }

}