using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour {

    public static ItemAssets Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }


    public Transform pfItemWorld;

    public Sprite s_Sword;
    public Sprite s_HealthPotion;
    public Sprite s_ManaPotion;
    public Sprite s_Coin;
    public Sprite s_Medkit;
    public Sprite s_Sword_1;
    public Sprite s_Sword_2;
    public Sprite s_ArmorNone;
    public Sprite s_Armor_1;
    public Sprite s_Armor_2;
    public Sprite s_HelmetNone;
    public Sprite s_Helmet;

}
