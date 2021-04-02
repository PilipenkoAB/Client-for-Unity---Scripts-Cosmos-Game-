using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CacheData : MonoBehaviour
{

    public static CacheData instance;

    public string sessionToken = "";
    public string playerId = "";
    public int battleSessionId = 0;
    public string language = "languageEn.json";
    public Dictionary<string, string> localizedText;
    void Awake()
    {
        this.InstantiateController();
    }

    private void InstantiateController()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if (this != instance)
        {
            Destroy(this.gameObject);
        }
    }
}
