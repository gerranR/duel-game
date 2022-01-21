using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevTools : MonoBehaviour
{
    public int player;
    public GameObject player1, player2;
    public Cards[] card;

    public void ChangePlayer(int newPlayer)
    {
        player = newPlayer;
    }

    public void addCard(int cardNum)
    {
        if(player == 1)
        {

            player1.GetComponent<PlayerCombat>().fireRate += card[cardNum].fireRate;
            if (player1.GetComponent<PlayerCombat>().fireRate <= 0)
            {
                player1.GetComponent<PlayerCombat>().fireRate = 0.1f;
            }
            player1.GetComponent<PlayerCombat>().maxAmmo += card[cardNum].maxAmmo;
            if (player1.GetComponent<PlayerCombat>().maxAmmo <= 0)
            {
                player1.GetComponent<PlayerCombat>().maxAmmo = 1;
            }
            player1.GetComponent<PlayerHealth>().maxHealth += card[cardNum].maxHP;
            if (player1.GetComponent<PlayerHealth>().maxHealth <= 0)
            {
                player1.GetComponent<PlayerHealth>().maxHealth = 0.1f;
            }
            player1.GetComponent<PlayerHealth>().meleeResist += card[cardNum].meleeResistance;
            if (player1.GetComponent<PlayerHealth>().meleeResist <= 0)
            {
                player1.GetComponent<PlayerHealth>().meleeResist = 0.1f;
            }
            player1.GetComponent<PlayerHealth>().rangeResist += card[cardNum].rangeResistance;
            if (player1.GetComponent<PlayerHealth>().rangeResist <= 0)
            {
                player1.GetComponent<PlayerHealth>().rangeResist = 0.1f;
            }
            player1.GetComponent<PlayerCombat>().swordDmg += card[cardNum].meleeDmg;
            if (player1.GetComponent<PlayerCombat>().swordDmg <= 0)
            {
                player1.GetComponent<PlayerCombat>().swordDmg = 0.1f;
            }
            player1.GetComponent<PlayerCombat>().bulletDmg += card[cardNum].rangeDmg;
            if (player1.GetComponent<PlayerCombat>().bulletDmg <= 0)
            {
                player1.GetComponent<PlayerCombat>().bulletDmg = 0.1f;
            }
            player1.GetComponent<PlayerCombat>().numOfBulletBounce += card[cardNum].bulletBounces;
            if (player1.GetComponent<PlayerCombat>().numOfBulletBounce <= 0)
            {
                player1.GetComponent<PlayerCombat>().numOfBulletBounce = 0f;
            }
            player1.GetComponent<PlayerMovement>().speed += card[cardNum].speed;
            if (player1.GetComponent<PlayerMovement>().speed <= 0)
            {
                player1.GetComponent<PlayerMovement>().speed = 0.1f;
            }
            player1.GetComponent<PlayerMovement>().jumpsMax += card[cardNum].maxJump;
            if (player1.GetComponent<PlayerMovement>().jumpsMax <= 0)
            {
                player1.GetComponent<PlayerMovement>().jumpsMax = 1;
            }

            if (card[cardNum].burst)
            {
                player1.GetComponent<PlayerCombat>().burst = true;
                player1.GetComponent<PlayerCombat>().burstMax += card[cardNum].burstNum;
                if (player1.GetComponent<PlayerCombat>().burstMax <= 0)
                {
                    player1.GetComponent<PlayerCombat>().burstMax = 0;
                }
            }
            if (card[cardNum].poison)
            {
                player1.GetComponent<PlayerCombat>().poison = true;
                player1.GetComponent<PlayerCombat>().poisonDmg += card[cardNum].poisonDmg;
                if (player1.GetComponent<PlayerCombat>().poisonDmg <= 0)
                {
                    player1.GetComponent<PlayerCombat>().poisonDmg = 0f;
                }
                player1.GetComponent<PlayerCombat>().poisonTime += card[cardNum].poisonTime;
                if (player1.GetComponent<PlayerCombat>().poisonTime <= 0)
                {
                    player1.GetComponent<PlayerCombat>().poisonTime = 0f;
                }
            }
            if (card[cardNum].fire)
            {
                player1.GetComponent<PlayerCombat>().fire = true;
                player1.GetComponent<PlayerCombat>().fireDmg += card[cardNum].fireDmg;
                if (player1.GetComponent<PlayerCombat>().fireDmg <= 0)
                {
                    player1.GetComponent<PlayerCombat>().fireDmg = 0f;
                }
                player1.GetComponent<PlayerCombat>().fireTime += card[cardNum].fireTime;
                if (player1.GetComponent<PlayerCombat>().fireTime <= 0)
                {
                    player1.GetComponent<PlayerCombat>().fireTime = 0f;
                }
            }
            if (card[cardNum].shotgun)
            {
                player1.GetComponent<PlayerCombat>().hasShotgun = true;
                player1.GetComponent<PlayerCombat>().shotgunShots += card[cardNum].shotgunShots;
                if (player1.GetComponent<PlayerCombat>().shotgunShots <= 0)
                {
                    player1.GetComponent<PlayerCombat>().shotgunShots = 0f;
                }
            }
            if (card[cardNum].halfHPDubbelDmg)
            {
                player1.GetComponent<PlayerHealth>().maxHealth = player1.GetComponent<PlayerHealth>().maxHealth / 2;
                if (player1.GetComponent<PlayerHealth>().maxHealth <= 0)
                {
                    player1.GetComponent<PlayerHealth>().maxHealth = 1f;
                }
                player1.GetComponent<PlayerCombat>().swordDmg += player1.GetComponent<PlayerCombat>().swordDmg;
                if (player1.GetComponent<PlayerCombat>().swordDmg <= 0)
                {
                    player1.GetComponent<PlayerCombat>().swordDmg = 0.1f;
                }
                player1.GetComponent<PlayerCombat>().bulletDmg += player1.GetComponent<PlayerCombat>().bulletDmg;
                if (player1.GetComponent<PlayerCombat>().bulletDmg <= 0)
                {
                    player1.GetComponent<PlayerCombat>().bulletDmg = 0.1f;
                }
            }
            if (card[cardNum].lifeSteal)
            {
                player1.GetComponent<PlayerHealth>().hasLifeSteal = true;
                player1.GetComponent<PlayerHealth>().lifeStealAmount += card[cardNum].lifeStealAmount;
                if (player1.GetComponent<PlayerHealth>().lifeStealAmount <= 0)
                {
                    player1.GetComponent<PlayerHealth>().lifeStealAmount = 0f;
                }
            }
            if (card[cardNum].bomb)
            {
                player1.GetComponent<PlayerCombat>().bombOnHit = true;
            }
            if (card[cardNum].reverseControle)
            {
                player1.GetComponent<PlayerCombat>().hasReverseControles = true;
                player1.GetComponent<PlayerCombat>().reverseControleTime += card[cardNum].reverseControleTime;
                if (player1.GetComponent<PlayerCombat>().reverseControleTime <= 0)
                {
                    player1.GetComponent<PlayerCombat>().reverseControleTime = 0f;
                }
            }
            if (card[cardNum].slowzone)
            {
                player1.GetComponent<PlayerCombat>().slowzoneOnHit = true;
            }
            if (card[cardNum].trampoline)
            {
                player1.GetComponent<PlayerCombat>().trampolineOnhit = true;
            }
            if (card[cardNum].reflect)
            {
                player1.GetComponent<PlayerHealth>().bulletReflect = true;
                player1.GetComponent<PlayerHealth>().bulletReturnSpeed += card[cardNum].bulletReflectSpeed;
                if (player1.GetComponent<PlayerHealth>().bulletReturnSpeed <= 0)
                {
                    player1.GetComponent<PlayerHealth>().bulletReturnSpeed = 0.1f;
                }
            }

            player2.GetComponent<PlayerHealth>().knockbackAdd += card[cardNum].knaockback;
            if (player1.GetComponent<PlayerHealth>().knockbackAdd <= 0)
            {
                player1.GetComponent<PlayerHealth>().knockbackAdd = 1;
            }

            else
            {
                player1.GetComponent<PlayerHealth>().knockbackAdd += card[cardNum].knaockback;
                if (player1.GetComponent<PlayerHealth>().knockbackAdd <= 0)
                {
                    player1.GetComponent<PlayerHealth>().knockbackAdd = 1f;
                }
            }
        }
        else if (player == 2)
        {

            player2.GetComponent<PlayerCombat>().fireRate += card[cardNum].fireRate;
            if (player2.GetComponent<PlayerCombat>().fireRate <= 0)
            {
                player2.GetComponent<PlayerCombat>().fireRate = 0.1f;
            }
            player2.GetComponent<PlayerCombat>().maxAmmo += card[cardNum].maxAmmo;
            if (player2.GetComponent<PlayerCombat>().maxAmmo <= 0)
            {
                player2.GetComponent<PlayerCombat>().maxAmmo = 1;
            }
            player2.GetComponent<PlayerHealth>().maxHealth += card[cardNum].maxHP;
            if (player2.GetComponent<PlayerHealth>().maxHealth <= 0)
            {
                player2.GetComponent<PlayerHealth>().maxHealth = 0.1f;
            }
            player2.GetComponent<PlayerHealth>().meleeResist += card[cardNum].meleeResistance;
            if (player2.GetComponent<PlayerHealth>().meleeResist <= 0)
            {
                player2.GetComponent<PlayerHealth>().meleeResist = 0.1f;
            }
            player2.GetComponent<PlayerHealth>().rangeResist += card[cardNum].rangeResistance;
            if (player2.GetComponent<PlayerHealth>().rangeResist <= 0)
            {
                player2.GetComponent<PlayerHealth>().rangeResist = 0.1f;
            }
            player2.GetComponent<PlayerCombat>().swordDmg += card[cardNum].meleeDmg;
            if (player2.GetComponent<PlayerCombat>().swordDmg <= 0)
            {
                player2.GetComponent<PlayerCombat>().swordDmg = 0.1f;
            }
            player2.GetComponent<PlayerCombat>().bulletDmg += card[cardNum].rangeDmg;
            if (player2.GetComponent<PlayerCombat>().bulletDmg <= 0)
            {
                player2.GetComponent<PlayerCombat>().bulletDmg = 0.1f;
            }
            player2.GetComponent<PlayerCombat>().numOfBulletBounce += card[cardNum].bulletBounces;
            if (player2.GetComponent<PlayerCombat>().numOfBulletBounce <= 0)
            {
                player2.GetComponent<PlayerCombat>().numOfBulletBounce = 0f;
            }
            player2.GetComponent<PlayerMovement>().speed += card[cardNum].speed;
            if (player2.GetComponent<PlayerMovement>().speed <= 0)
            {
                player2.GetComponent<PlayerMovement>().speed = 0.1f;
            }
            player2.GetComponent<PlayerMovement>().jumpsMax += card[cardNum].maxJump;
            if (player2.GetComponent<PlayerMovement>().jumpsMax <= 0)
            {
                player2.GetComponent<PlayerMovement>().jumpsMax = 1;
            }
            if (card[cardNum].burst)
            {
                player2.GetComponent<PlayerCombat>().burst = true;
                player2.GetComponent<PlayerCombat>().burstMax += card[cardNum].burstNum;
                if (player2.GetComponent<PlayerCombat>().burstMax <= 0)
                {
                    player2.GetComponent<PlayerCombat>().burstMax = 0;
                }
            }
            if (card[cardNum].poison)
            {
                player2.GetComponent<PlayerCombat>().poison = true;
                player2.GetComponent<PlayerCombat>().poisonDmg += card[cardNum].poisonDmg;
                if (player2.GetComponent<PlayerCombat>().poisonDmg <= 0)
                {
                    player2.GetComponent<PlayerCombat>().poisonDmg = 0f;
                }
                player2.GetComponent<PlayerCombat>().poisonTime += card[cardNum].poisonTime;
                if (player2.GetComponent<PlayerCombat>().poisonTime <= 0)
                {
                    player2.GetComponent<PlayerCombat>().poisonTime = 0f;
                }
            }
            if (card[cardNum].fire)
            {
                player2.GetComponent<PlayerCombat>().fire = true;
                player2.GetComponent<PlayerCombat>().fireDmg += card[cardNum].fireDmg;
                if (player2.GetComponent<PlayerCombat>().fireDmg <= 0)
                {
                    player2.GetComponent<PlayerCombat>().fireDmg = 0f;
                }
                player2.GetComponent<PlayerCombat>().fireTime += card[cardNum].fireTime;
                if (player2.GetComponent<PlayerCombat>().fireTime <= 0)
                {
                    player2.GetComponent<PlayerCombat>().fireTime = 0f;
                }
            }
            if (card[cardNum].shotgun)
            {
                player2.GetComponent<PlayerCombat>().hasShotgun = true;
                player2.GetComponent<PlayerCombat>().shotgunShots += card[cardNum].shotgunShots;
                if (player2.GetComponent<PlayerCombat>().shotgunShots <= 0)
                {
                    player2.GetComponent<PlayerCombat>().shotgunShots = 0f;
                }
            }
            if (card[cardNum].halfHPDubbelDmg)
            {
                player2.GetComponent<PlayerHealth>().maxHealth = player2.GetComponent<PlayerHealth>().maxHealth / 2;
                if (player2.GetComponent<PlayerHealth>().maxHealth <= 0)
                {
                    player2.GetComponent<PlayerHealth>().maxHealth = 1f;
                }
                player2.GetComponent<PlayerCombat>().swordDmg += player2.GetComponent<PlayerCombat>().swordDmg;
                if (player2.GetComponent<PlayerCombat>().swordDmg <= 0)
                {
                    player2.GetComponent<PlayerCombat>().swordDmg = 0.1f;
                }
                player2.GetComponent<PlayerCombat>().bulletDmg += player2.GetComponent<PlayerCombat>().bulletDmg;
                if (player2.GetComponent<PlayerCombat>().bulletDmg <= 0)
                {
                    player2.GetComponent<PlayerCombat>().bulletDmg = 0.1f;
                }
            }
            if (card[cardNum].lifeSteal)
            {
                player2.GetComponent<PlayerHealth>().hasLifeSteal = true;
                player2.GetComponent<PlayerHealth>().lifeStealAmount += card[cardNum].lifeStealAmount;
                if (player2.GetComponent<PlayerHealth>().lifeStealAmount <= 0)
                {
                    player2.GetComponent<PlayerHealth>().lifeStealAmount = 0f;
                }
            }
            if (card[cardNum].bomb)
            {
                player2.GetComponent<PlayerCombat>().bombOnHit = true;
            }
            if (card[cardNum].reverseControle)
            {
                player2.GetComponent<PlayerCombat>().hasReverseControles = true;
                player2.GetComponent<PlayerCombat>().reverseControleTime += card[cardNum].reverseControleTime;
                if (player2.GetComponent<PlayerCombat>().reverseControleTime <= 0)
                {
                    player2.GetComponent<PlayerCombat>().reverseControleTime = 0f;
                }
            }
            if (card[cardNum].slowzone)
            {
                player2.GetComponent<PlayerCombat>().slowzoneOnHit = true;
            }
            if (card[cardNum].trampoline)
            {
                player2.GetComponent<PlayerCombat>().trampolineOnhit = true;
            }
            if (card[cardNum].reflect)
            {
                player2.GetComponent<PlayerHealth>().bulletReflect = true;
                player2.GetComponent<PlayerHealth>().bulletReturnSpeed += card[cardNum].bulletReflectSpeed;
                if (player2.GetComponent<PlayerHealth>().bulletReturnSpeed <= 0)
                {
                    player2.GetComponent<PlayerHealth>().bulletReturnSpeed = 0.1f;
                }
            }

            player1.GetComponent<PlayerHealth>().knockbackAdd += card[cardNum].knaockback;
            if (player1.GetComponent<PlayerHealth>().knockbackAdd <= 0)
            {
                player1.GetComponent<PlayerHealth>().knockbackAdd = 1;
            }

            else
            {
                player2.GetComponent<PlayerHealth>().knockbackAdd += card[cardNum].knaockback;
                if (player2.GetComponent<PlayerHealth>().knockbackAdd <= 0)
                {
                    player2.GetComponent<PlayerHealth>().knockbackAdd = 1f;
                }
            }
        }
    }
}
