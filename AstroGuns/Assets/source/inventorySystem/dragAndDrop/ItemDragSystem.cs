using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDragSystem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public void OnBeginDrag(PointerEventData eventData)
    {
        GetComponent<Image>().enabled = false;

        InventorySystem.Instance.movableWeapon.gameObject.SetActive(true);
        InventorySystem.Instance.movableWeapon.GetComponent<Image>().sprite = this.GetComponent<Image>().sprite;        
    }

    public void OnDrag(PointerEventData eventData)
    {
        InventorySystem.Instance.movableWeapon.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        InventorySystem.Instance.movableWeapon.gameObject.SetActive(false);
        GetComponent<Image>().enabled = true;
    }
}
