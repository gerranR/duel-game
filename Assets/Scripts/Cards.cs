using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "ScriptableObjects/CardsScriptableObject", order = 1)]
public class Cards : ScriptableObject
{
    public GameObject cardImage;
    public float fireRate, maxHP, meleeDmg, rangeDmg, speed, meleeRange, meleeResistance, rangeResistance, knaockback, poisonTime, poisonDmg, shotgunShots;
    public int maxAmmo, maxJump, burstNum;
    public bool halfHPDubbelDmg, burst, poison, shotgun;
    public string titel, discription;
}
