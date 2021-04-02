using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageBox : MonoBehaviour
{

    public Button closeMessageBox;
    public GameObject messageBoxBody; //Text box if server status if offline
    // Start is called before the first frame update
    void Start()
    {

        closeMessageBox.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        messageBoxBody.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
