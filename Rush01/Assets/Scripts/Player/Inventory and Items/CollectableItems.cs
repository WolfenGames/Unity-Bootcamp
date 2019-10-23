using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class CollectableItems : Interactable, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public int itemID;
    public string itemName;
    public GameObject prefab;
    public Sprite icon;
    public int stackAmount;
    public bool consumable = false;
    public int maxStackAmt = 10;
    private Image img;
    [HideInInspector] public RectTransform rect;
    [HideInInspector] public TMP_Text stackAmt;
    private Transform previousEquippedSlot;
    private GameObject inventoryPanel;
    private GameObject draggingLayer;
	[TextArea]
	public	string		description;

    public void Awake()
    {
        rect = GetComponent<RectTransform>();
        img = GetComponent<Image>();
        draggingLayer = GameObject.Find("UI Drag Layer Canvas");

        if (img != null && icon != null)
            img.sprite = icon;

        if (transform.childCount > 0 && transform.GetChild(0).GetComponent<TMP_Text>() != null)
        {
            stackAmt = transform.GetChild(0).GetComponent<TMP_Text>();
            stackAmt.text = stackAmount.ToString();
        }

        inventoryPanel = GameObject.Find("Inventory Panel");
    }

    public override void Interact()
    {
        Player.p.RemoveFocus();
        InventorySystem.Instance.AddToInventory(this);
        Player.p.animationManager.animator.SetBool("pickup", true);
        ResetInteracted();
    }

    public void CopyContents(CollectableItems worldObjectToUIObject)
    {
        itemID = worldObjectToUIObject.itemID;
        stackAmount = worldObjectToUIObject.stackAmount;
        consumable = worldObjectToUIObject.consumable;
        maxStackAmt = worldObjectToUIObject.maxStackAmt;
        stackAmt.text = stackAmount.ToString();
        description = worldObjectToUIObject.description;
        img.sprite = icon;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (transform.parent.GetComponent<EquipSlot>() != null)
            Player.p.GetComponent<CharacterCombat>().UnequipWeapon();

        previousEquippedSlot = transform.parent;
        img.raycastTarget = false;
        rect.pivot = new Vector2(0, 1);
        transform.SetParent(draggingLayer.transform);
        stackAmt.enabled = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject != null)
            PlaceIntoSlot(eventData.pointerCurrentRaycast.gameObject.transform);
        else
            DropItemIntoWorld();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!string.IsNullOrEmpty(description))
            UI_Tips.Instance.ShowTooltop(description);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UI_Tips.Instance.HideTooltip();
    }

    public void ResetRectInSlot(RectTransform _rect)
    {
        _rect.pivot = new Vector2(0.5f, 0.5f);
        _rect.anchorMin = new Vector2(0f, 0f);
        _rect.anchorMax = new Vector2(1f, 1f);
        _rect.anchoredPosition = new Vector2(0, 0);
        _rect.sizeDelta = new Vector2(-15, -15);
        _rect.GetComponent<Image>().raycastTarget = true;
        _rect.transform.GetChild(0).GetComponent<TMP_Text>().enabled = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right && consumable == true)
        {
            stackAmount--;
            stackAmt.text = stackAmount.ToString();
			Player.p.Heal();
            if (stackAmount <= 0)
                GameObject.Destroy(gameObject);
        }
    }

    private void PlaceIntoSlot(Transform slot)
    {
        if (slot.GetComponent<InventorySlot>() != null)
        {
            if (slot.childCount <= 0)
                transform.SetParent(slot);
            else
            {
                Debug.Log("Here! 0");
                SwapItemPositions(slot.GetChild(0));
            }
        }
        else if (slot.GetComponent<EquipSlot>() != null && consumable == false)
        {
            if (slot.childCount <= 0)
                EquipItem(slot);
            else
                SwapItemPositions(slot.GetChild(0));
        }
        else if (slot.GetComponent<CollectableItems>() != null)
        {
            SwapItemPositions(slot);
        }
        else
        {
            if (slot.name == "Bin Slot")
                DestroyItem();
            else
                ResetToPreviousSlot();
        }

        ResetRectInSlot(rect);
    }

    private void EquipItem(Transform item)
    {
        transform.SetParent(item.transform);
        Player.p.GetComponent<CharacterCombat>().EquipWeapon(prefab);
    }

    private void SwapItemPositions(Transform item)
    {
        CollectableItems swapingItemInfo = item.GetComponent<CollectableItems>();

        //If we are swapping the equiped item, just requip the enw weapon
        if (item.parent.GetComponent<EquipSlot>() != null)
        {
            if (consumable == true)
            {
                ResetToPreviousSlot();
                return;
            }

            Player.p.GetComponent<CharacterCombat>().UnequipWeapon();
            Player.p.GetComponent<CharacterCombat>().EquipWeapon(prefab);
        }

        if (itemID == swapingItemInfo.itemID && stackAmount < maxStackAmt)
        {
            int openSpace = (swapingItemInfo.maxStackAmt - swapingItemInfo.stackAmount);

            if (stackAmount <= openSpace)
            {
                swapingItemInfo.stackAmount += stackAmount;
                swapingItemInfo.stackAmt.text = swapingItemInfo.stackAmount.ToString();
                GameObject.Destroy(gameObject);
            }
            else
            {
                swapingItemInfo.stackAmount += openSpace;
                swapingItemInfo.stackAmt.text = swapingItemInfo.stackAmount.ToString();
                stackAmount -= openSpace;
                stackAmt.text = stackAmount.ToString();

                for (int i = 0; i < inventoryPanel.transform.childCount; i++)
                {
                    if (inventoryPanel.transform.GetChild(i).childCount == 0)
                    {
                        transform.SetParent(inventoryPanel.transform.GetChild(i));
                        ResetRectInSlot(rect);
                        break;
                    }
                }
            }
        }
        else
        {
            var swappingParent = item.parent;
            item.transform.SetParent(previousEquippedSlot);
            transform.SetParent(swappingParent);
            ResetRectInSlot(item.GetComponent<RectTransform>());
        }
    }

    private void DestroyItem()
    {
        GameObject.Destroy(gameObject);
    }

    private void ResetToPreviousSlot()
    {
        transform.SetParent(previousEquippedSlot);
    }

    private void DropItemIntoWorld()
    {
        GameObject.Destroy(gameObject);
        GameObject.Instantiate(prefab, Player.p.transform.position + new Vector3(0, Player.p.transform.localScale.y / 2, 0) + Player.p.transform.forward, Random.rotation);
    }

    public bool StackItem()
    {
        bool succeededStack = false;
        if (stackAmount < maxStackAmt)
        {
            stackAmount++;
            succeededStack = true;
        }
        return succeededStack;
    }
}
