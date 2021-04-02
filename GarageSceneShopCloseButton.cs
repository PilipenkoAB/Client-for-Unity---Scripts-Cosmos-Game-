using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GarageSceneShopCloseButton : MonoBehaviour
{
    private GameObject shopPanel;

    // Start is called before the first frame update
    void Start()
    {
        shopPanel = GameObject.Find("ShopPanel");

        this.GetComponent<Button>().onClick.AddListener(CloseShop);
    }


    private void CloseShop()
    {
        shopPanel.transform.position = new Vector2((Screen.width) * 2, (Screen.height) * 2);

    }
}
