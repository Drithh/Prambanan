

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item {

    public enum ItemType {
        Sword,
        HealthPotion,
        ManaPotion,
        Coin,
        Medkit,
        ArmorNone,
        Armor_1,
        Armor_2,
        HelmetNone,
        Helmet,
        SwordNone,
        Sword_1,
        Sword_2
    }

    public ItemType itemType;
    public int amount = 1;

    
    public Sprite GetSprite() {
        return GetSprite(itemType);
    }

    public static Sprite GetSprite(ItemType itemType) {
        switch (itemType) {
        default:
            case ItemType.Sword:        return ItemAssets.Instance.s_Sword;
            case ItemType.HealthPotion: return ItemAssets.Instance.s_HealthPotion;
            case ItemType.ManaPotion:   return ItemAssets.Instance.s_ManaPotion;
            case ItemType.Coin:         return ItemAssets.Instance.s_Coin;
            case ItemType.Medkit:       return ItemAssets.Instance.s_Medkit;

            case ItemType.ArmorNone:    return ItemAssets.Instance.s_ArmorNone;
            case ItemType.Armor_1:      return ItemAssets.Instance.s_Armor_1;
            case ItemType.Armor_2:      return ItemAssets.Instance.s_Armor_2;
            case ItemType.HelmetNone:   return ItemAssets.Instance.s_HelmetNone;
            case ItemType.Helmet:       return ItemAssets.Instance.s_Helmet;
            case ItemType.Sword_1:      return ItemAssets.Instance.s_Sword_1;
            case ItemType.Sword_2:      return ItemAssets.Instance.s_Sword_2;
        }
    }

    public override string ToString() {
        return itemType.ToString();
    }



}
