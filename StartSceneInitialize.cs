using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneInitialize : MonoBehaviour
{

    private GameObject cacheData;


    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(3840, 2160, true); // set default resolution

        cacheData = GameObject.Find("CacheData");
        string language = cacheData.GetComponent<CacheData>().language;

        LocalizationManager.LoadLocalizedText(language);
        
        // put loading language in coroutine in loading Login scene

        StartCoroutine(LoadSceneAsync.LoadYourAsyncScene("Login"));
    }

}
