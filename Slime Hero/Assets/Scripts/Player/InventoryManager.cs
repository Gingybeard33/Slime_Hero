using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
   public List<AbilityController> abilitySlots = new List<AbilityController>(5);
   public List<Image> abilityUISlots = new List<Image>(5);
   public List<PassiveItem> passiveItemSlots = new List<PassiveItem>(3);
   public List<Image> passiveItemUISlots = new List<Image>(3);

   public void AddAbility(int slotIndex, AbilityController ability)
   {
        abilitySlots[slotIndex] = ability;
        abilityUISlots[slotIndex].enabled = true;
        abilityUISlots[slotIndex].sprite = ability.abilityData.Icon;
   }

   public void AddPassiveItem(int slotIndex, PassiveItem passiveItem)
   {
        passiveItemSlots[slotIndex] = passiveItem;
        passiveItemUISlots[slotIndex].enabled = true;
        passiveItemUISlots[slotIndex].sprite = passiveItem.passiveItemData.Icon;
   }

   
}
