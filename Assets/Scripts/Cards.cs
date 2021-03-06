using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "ScriptableObjects/CardsScriptableObject", order = 1)]
public class Cards : ScriptableObject
{
    public GameObject cardImage;
    public float fireRate, maxHP, meleeDmg, rangeDmg, speed, meleeRange, meleeResistance, rangeResistance, knaockback, poisonTime, poisonDmg, shotgunShots, fireDmg, fireTime, bulletBounces;
    public int maxAmmo, maxJump, burstNum;
    public bool halfHPDubbelDmg, burst, poison, shotgun, lifeSteal, bomb, trampoline, reflect, fire, slowzone, reverseControle;
    public string titel, discription;
    public float lifeStealAmount, bulletReflectSpeed, reverseControleTime;
}
