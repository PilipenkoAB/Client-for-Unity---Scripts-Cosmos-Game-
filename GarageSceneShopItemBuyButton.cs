using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GarageSceneShopItemBuyButton : MonoBehaviour
{

    private GameObject shopBuyDialog;
    // Start is called before the first frame update
    void Start()
    {
        shopBuyDialog = GameObject.Find("ShopBuyDialog");

          this.GetComponent<Button>().onClick.AddListener(OpenBuyDialog);
    }

    private void OpenBuyDialog()
    {
        // kostil'
        GameObject.Find("ShopBuyDialogItemId").GetComponent<Text>().text = this.name;

        //------------------
        shopBuyDialog.transform.position = new Vector2((Screen.width) / 2, (Screen.height) / 2);

        shopBuyDialog = GameObject.Find("ShopBuyDialog");
      //  shopBuyDialog.SetActive(true);
        //   shopBuyDialog.SetActive(true);

        Debug.Log("OpenBuyDialog - " + this.gameObject.name);
    }
}
