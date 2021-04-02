using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GarageSceneShopOpenButton : MonoBehaviour
{
    private GameObject myCarrierObject; //cache

    private GameObject shopPanel;

    private GameObject shopContent;

    private GameObject shopBuyDialog;


    // Start is called before the first frame update
    void Start()
    {
        shopPanel = GameObject.Find("ShopPanel");
        shopContent = GameObject.Find("ShopContent");
        shopBuyDialog = GameObject.Find("ShopBuyDialog");

        GameObject.Find("ShopBuyDialogNoButton").GetComponent<Button>().onClick.AddListener(DialogNoButton);

      //  shopPanel.SetActive(false);
      // shopBuyDialog.SetActive(false);

        this.GetComponent<Button>().onClick.AddListener(OpenShop);


    }

    private void OpenShop()
    {
        myCarrierObject = GameObject.Find("CacheData");
        string sessionToken = myCarrierObject.GetComponent<CacheData>().sessionToken;
        string playerId = myCarrierObject.GetComponent<CacheData>().playerId;


        string message = "2;" + playerId + ";" + sessionToken + ";" + ";3";

        string answer = TcpConnection.Connect(message);

        if (answer != "" || answer != "000")
        {
            Debug.Log("2 - 3 (shop info) informtion was sended and successeful");


            shopPanel.transform.position = new Vector2((Screen.width) / 2, (Screen.height) / 2);
          // shopPanel.SetActive(true);
            PopulateShopScrollView(answer);
        }
        else
        {
            // show message that can't load shop!
            Debug.Log("2 - 3 (shop info) Fail ");
        }
    }


    private void PopulateShopScrollView(string serverAnswer) { 

        foreach (Transform child in shopContent.transform)
	    {
			Destroy(child.gameObject);
        }

        // send itemId to buy, price, typeId, typeItemId

        string[] answerObjects = serverAnswer.Split(';');

        for (int i = 0; i < answerObjects.Length; i+=4)
        {
            GameObject item = (Instantiate(GameObject.Find("ShopItemDefault")) as GameObject);
            item.transform.parent = shopContent.transform;
            item.name = "ShopItem" + answerObjects[i];
            item.transform.Find("ShopItemDefaultBuyButton").name = answerObjects[i];
            
            //---
            //    GameObject.Find("ShopItemBuyButton" + answerObjects[i]).GetComponent<Button>().onClick.RemoveListener(OpenBuyDialog);
            //   GameObject.Find("ShopItemBuyButton" + answerObjects[i]).GetComponent<Button>().onClick.AddListener(OpenBuyDialog);

            item.transform.Find("ShopItemDefaultPrice").name = "ShopItemPrice" + answerObjects[i];
            item.transform.Find("ShopItemPrice" + answerObjects[i]).GetComponent<Text>().text = answerObjects[i+1];

            item.transform.Find("ShopItemDefaultImage").name = "ShopItemImage" + answerObjects[i];
            item.transform.Find("ShopItemDefaultName").name = "ShopItemName" + answerObjects[i];

            if (answerObjects[i + 2] == "0") // cockpit
            {
                item.transform.Find("ShopItemImage" + answerObjects[i]).GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/ItemsIcons/CockpitSlot/" + answerObjects[i + 3]);
                item.transform.Find("ShopItemName" + answerObjects[i]).GetComponent<Text>().text = LocalizedText.Localize("DB_Cockpit_"+ answerObjects[i+3] + "_Name");
            }
            else if (answerObjects[i + 2] == "1") // engine
            {
                item.transform.Find("ShopItemImage" + answerObjects[i]).GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/ItemsIcons/EngineSlot/" + answerObjects[i + 3]);
                item.transform.Find("ShopItemName" + answerObjects[i]).GetComponent<Text>().text = LocalizedText.Localize("DB_Engine_" + answerObjects[i + 3] + "_Name");
            }
            else if (answerObjects[i + 2] == "2") // weapon
            {
                item.transform.Find("ShopItemImage" + answerObjects[i]).GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/ItemsIcons/WeaponSlot/" + answerObjects[i + 3]);
                item.transform.Find("ShopItemName" + answerObjects[i]).GetComponent<Text>().text = LocalizedText.Localize("DB_Weapon_" + answerObjects[i + 3] + "_Name");
            }
            else if (answerObjects[i + 2] == "3") // bigslot
            {
                item.transform.Find("ShopItemImage" + answerObjects[i]).GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/ItemsIcons/BigSlot/" + answerObjects[i + 3]);
                item.transform.Find("ShopItemName" + answerObjects[i]).GetComponent<Text>().text = LocalizedText.Localize("DB_BigSlot_" + answerObjects[i + 3] + "_Name");
            }
            else if (answerObjects[i + 2] == "4") // mediumslot
            {
                item.transform.Find("ShopItemImage" + answerObjects[i]).GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/ItemsIcons/MediumSlot/" + answerObjects[i + 3]);
                item.transform.Find("ShopItemName" + answerObjects[i]).GetComponent<Text>().text = LocalizedText.Localize("DB_MediumSlot_" + answerObjects[i + 3] + "_Name");
            }
            else if (answerObjects[i + 2] == "5") // smallslot
            {
                item.transform.Find("ShopItemImage" + answerObjects[i]).GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/ItemsIcons/SmallSlot/" + answerObjects[i + 3]);
                item.transform.Find("ShopItemName" + answerObjects[i]).GetComponent<Text>().text = LocalizedText.Localize("DB_SmallSlot_" + answerObjects[i + 3] + "_Name");
            }

            //depends on the localization



        }


            Debug.Log("2 - 3 (shop info) message -  " + serverAnswer);
    }


    private void DialogNoButton() 
    {
        // shopBuyDialog.SetActive(false);
        shopBuyDialog.transform.position = new Vector2((Screen.width) * 2, (Screen.height) * 2);
    }

}
