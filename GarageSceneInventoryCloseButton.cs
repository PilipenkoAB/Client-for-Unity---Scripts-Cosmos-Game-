using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GarageSceneInventoryCloseButton : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject inventoryPanel;

    // Start is called before the first frame update
    void Start()
    {
        inventoryPanel = GameObject.Find("InventoryPanel");

        Button buttonOpenInventory = this.GetComponent<Button>();
        buttonOpenInventory.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        inventoryPanel.transform.position = new Vector2((Screen.width) * 2, (Screen.height) * 2);
       // inventory.SetActive(false);
    }
}
