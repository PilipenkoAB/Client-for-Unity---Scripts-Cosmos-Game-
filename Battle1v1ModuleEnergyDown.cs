using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Battle1v1ModuleEnergyDown : MonoBehaviour
{
    private GameObject myCarrierObject; //cache

    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(DownModuleEnergy);
    }

    void DownModuleEnergy()
    {
        //Load cache 
        myCarrierObject = GameObject.Find("CacheData");
        string sessionToken = myCarrierObject.GetComponent<CacheData>().sessionToken;
        string playerId = myCarrierObject.GetComponent<CacheData>().playerId;
        int battleSessionId = myCarrierObject.GetComponent<CacheData>().battleSessionId;

        string message = "3;" + playerId + ";" + sessionToken + ";" + battleSessionId + ";3" + ";2" + ";" + this.name;

        string answer = TcpConnection.Connect(message);
        if (answer != "" || answer != "000")
        {
            Debug.Log("3 - 3 - 2 informtion was sended and successeful");
        }
        else
        {
            Debug.Log("3 - 3 - 2  Fail ");
        }
    }
}
