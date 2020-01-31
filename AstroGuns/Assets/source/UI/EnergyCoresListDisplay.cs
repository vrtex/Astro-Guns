using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class EnergyCoresListDisplay : MonoBehaviour
{
    public List<Image> CoreIcons = new List<Image>();
    public EnergyCoreImageMapping IconsMapping;
    public List<EnergyCoreButton> Buttons;
    public CoresContainer Container;

    public EnergyCore.EnergyCoreType EnergyCoresType;

    private void Start()
    {
        if(CoreIcons.Count != 5)
            throw new System.Exception("This is dumb");

        foreach(Transform t in transform)
            Buttons.Add(t.GetComponent<EnergyCoreButton>());

        for(int i = 0; i < CoreIcons.Count; ++i)
            CoreIcons[i].sprite = IconsMapping.sprites[i];

        foreach(EnergyCoreButton b in Buttons)
        {
            b.Type = EnergyCoresType;
            b.OnEquipButtonPressed.AddListener((int l, EnergyCore.EnergyCoreType t) => EquipCore(l, t));
        }
    }

    public void EquipCore(int level, EnergyCore.EnergyCoreType type)
    {
        EnergyCore toEquip = Container.pollCore(level, type);
        if(toEquip == null)
            return;
        Slot slot = MenuManager.Instance.GetPanel(Panels.ItemInfo).GetComponent<ItemInfoController>().currentSlot.managedSlot;
    }

}
