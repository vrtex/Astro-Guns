using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDragSystem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Vector3 beginPosition;
    public void OnBeginDrag(PointerEventData eventData)
    {
        // numer slota
        for (int i = 0; i < InventorySystem.Instance.weaponsInSlots.Count; i++)
        {
            if (this.gameObject.Equals(InventorySystem.Instance.weaponsInSlots[i].gameObject))
            {
                InventorySystem.lastMovedSlotNumber = i;
                break;
            }
        }

        // wyłączenie broni w slocie
        GetComponent<Image>().enabled = false;

        // włączenie przesuwanej broni i ustawienie jej grafiki
        InventorySystem.Instance.movableWeapon.gameObject.SetActive(true);
        InventorySystem.Instance.movableWeapon.GetComponent<Image>().sprite = this.GetComponent<Image>().sprite;
    }

    public void OnDrag(PointerEventData eventData)
    {
        InventorySystem.Instance.movableWeapon.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // wyłączenie przesuwanej broni
        InventorySystem.Instance.movableWeapon.gameObject.SetActive(false);

        // włączenie broni w slocie
        GetComponent<Image>().enabled = true;
    }
}
