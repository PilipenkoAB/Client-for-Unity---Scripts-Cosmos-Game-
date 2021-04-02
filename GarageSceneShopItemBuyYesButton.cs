using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GarageSceneShopItemBuyYesButton : MonoBehaviour
{

    private GameObject myCarrierObject; //cache

    private GameObject shopBuyDialog;
    private GameObject shopPanel;

    // Start is called before the first frame update
    void Start()
    {
        shopBuyDialog = GameObject.Find("ShopBuyDialog");
        shopPanel = GameObject.Find("ShopPanel");


        this.GetComponent<Button>().onClick.AddListener(BuyAttempt);
    }

    // Update is called once per frame
    private void BuyAttempt()
    {
        shopBuyDialog.transform.position = new Vector2((Screen.width) * 2, (Screen.height) * 2);
        shopPanel.transform.position = new Vector2((Screen.width) * 2, (Screen.height) * 2);


        myCarrierObject = GameObject.Find("CacheData");
        string sessionToken = myCarrierObject.GetComponent<CacheData>().sessionToken;
        string playerId = myCarrierObject.GetComponent<CacheData>().playerId;


        string message = "2;" + playerId + ";" + sessionToken + ";" + "4" + ";" + GameObject.Find("ShopBuyDialogItemId").GetComponent<Text>().text;

        string answer = TcpConnection.Connect(message);

        if (answer != "" || answer != "000")
        {
            Debug.Log("2 - 4 (buy item) informtion was sended and successeful");

        }
        else
        {
            // show message that can't load shop!
            Debug.Log("2 - 4 (buy item) Fail ");
        }
    }
}
