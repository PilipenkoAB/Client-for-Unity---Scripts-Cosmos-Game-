using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GarageSceneCircleItemRightClickFocus : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    // right click on the item - buttons inside
    private Button ItemRighClickInfoButtonInfo;
    private Button ItemRighClickInfoButtonRemove;

    private GameObject ItemRighClickInfo;


    bool inFocus = false;
    // Start is called before the first frame update
    void Start()
    {
        // click
        ItemRighClickInfoButtonInfo = GameObject.Find("ItemRighClickInfoButtonInfo").GetComponent<Button>();
        ItemRighClickInfoButtonRemove = GameObject.Find("ItemRighClickInfoButtonRemove").GetComponent<Button>();

        ItemRighClickInfoButtonInfo.onClick.AddListener(ItemRighClickInfoButtonInfoClick);
        ItemRighClickInfoButtonRemove.onClick.AddListener(ItemRighClickInfoButtonRemoveClick);

        ItemRighClickInfo = GameObject.Find("ItemRighClickInfo");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && inFocus == false)
        {
            Debug.Log(" Update - Pressed left mouse button.");
             ItemRighClickInfo.SetActive(false);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        inFocus = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        inFocus = true;
    }


    // button clicks ItemRighClickInfo
    void ItemRighClickInfoButtonInfoClick()
    {
        Debug.Log("You have clicked the info button!");
        inFocus = false;
        ItemRighClickInfo.SetActive(false);
    }
    void ItemRighClickInfoButtonRemoveClick()
    {
        Debug.Log("You have clicked the removed button!");
        inFocus = false;
        ItemRighClickInfo.SetActive(false);
    }




}
