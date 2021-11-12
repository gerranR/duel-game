using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    private bool canShoot = true, reloading, swordUsed, canAttack = true;
    public float fireRate, reloadTime, SwordDur, bulletDmg, swordDmg;
    public GameObject gun, bullet, swordCol;
    public int ammo, maxAmmo;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Gun(InputAction.CallbackContext callback)
    {
        if (canAttack)
        {
            if (callback.performed && canShoot & ammo > 0 && reloading == false)
            {
                ammo--;
                GameObject bulletInstance = Instantiate(bullet, gun.transform.position, gun.transform.rotation);
                bulletInstance.GetComponent<Bullet>().dmg = bulletDmg;
                canShoot = false;
                StartCoroutine(ShootTimer());
            }

        }
    }


    public void Sword(InputAction.CallbackContext callback)
    {
        if(canAttack)
        {
            if (callback.performed && swordUsed == false)
            {
                swordCol.GetComponent<Collider2D>().enabled = true;
                swordUsed = true;
                StartCoroutine(SwordTimer());
            }
        }
    }

    public void Reload(InputAction.CallbackContext callback)
    {
        if (canAttack)
        {
            if (callback.performed && reloading == false)
            {
                print("Reload");
                reloading = true;
                StartCoroutine(ReloadTimer());
            }
        }
    }



    IEnumerator ReloadTimer()
    {
        yield return new WaitForSeconds(reloadTime);
        ammo = maxAmmo;
        reloading = false;
    }

    IEnumerator ShootTimer()
    {
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }
    IEnumerator SwordTimer()
    {
        yield return new WaitForSeconds(fireRate);
        swordCol.GetComponent<Collider2D>().enabled = false;
        swordUsed = false;
    }

    public void CanAttack(bool attackBool)
    {
        canAttack = attackBool;
    }
}
