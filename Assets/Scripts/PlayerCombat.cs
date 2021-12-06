using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    public bool canShoot = true, reloading, swordUsed, canAttack = false;
    public float fireRate, reloadTime, SwordDur, bulletDmg, swordDmg;
    public GameObject gun, bullet, swordCol, ammoPanels, ammoSprites;
    public int ammo, maxAmmo;

    public List<GameObject> ammoList = new List<GameObject>();

    // Update is called once per frame
    void Update()
    {
        if (ammoList.Count != maxAmmo)
        {
            ammoList.Add(Instantiate(ammoSprites, ammoPanels.transform));
        }
    }

    public void Gun(InputAction.CallbackContext callback)
    {
        if (canAttack)
        {
            if (callback.performed && canShoot & ammo > 0 && reloading == false)
            {
                ammo--;
                ammoList[ammo].SetActive(false);
                GameObject bulletInstance = Instantiate(bullet, gun.transform.position, gun.transform.rotation);
                bulletInstance.GetComponent<Bullet>().dmg = bulletDmg;
                bulletInstance.GetComponent<Bullet>().player = this.gameObject;
                canShoot = false;
                StartCoroutine(ShootTimer());
            }

            else if(callback.performed && canShoot && ammo == 0 && reloading == false)
            {
                print("Reload");
                reloading = true;
                StartCoroutine(ReloadTimer());
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
        for (int i = 0; i < ammoList.Count; i++)
        {
            ammoList[i].SetActive(true);
        }
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
