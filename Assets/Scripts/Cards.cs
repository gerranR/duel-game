using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "ScriptableObjects/CardsScriptableObject", order = 1)]
public class Cards : ScriptableObject
{
    public Sprite cardImage;
    public float fireRate, maxHP, meleeDmg, rangeDmg, speed, meleeRange;
    public int maxAmmo;
}
