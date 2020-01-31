using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponButton : MonoBehaviour
{
    public Button interactionButton;

    private SlotController owningSlot;
    private int weaponIndex;

    // Start is called before the first frame update
    void Start()
    {
        owningSlot = transform.parent.GetComponent<SlotController>();

        weaponIndex = transform.parent.GetSiblingIndex();
        interactionButton.onClick.AddListener(() => owningSlot.OpenInfoPanel() );
    }
    
}
