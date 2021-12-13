using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    public bool canShoot = true, reloading, swordUsed, canAttack = false;
    public float fireRate, reloadTime, SwordDur, bulletDmg, swordDmg;
    public GameObject gun, bullet, swordCol, ammoPanels, ammoSprites;
    public int ammo, maxAmmo;

    public AudioSource ShootAudio, meleeAudio, reloadAudio;
    public Slider ammoSlider;
    public Animator playerAim;

    public void Gun(InputAction.CallbackContext callback)
    {
        if (canAttack)
        {
            if (callback.performed && canShoot & ammo > 0 && reloading == false)
            {
                ShootAudio.Play();
                ammo--;
                ammoSlider.maxValue = maxAmmo;
                ammoSlider.value = ammo;
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
                playerAim.SetTrigger("Melee");
                meleeAudio.Play();
                swordCol.GetComponent<Collider2D>().enabled = true;
                swordUsed = true;
                print("test1");
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
        reloadAudio.Play();
        yield return new WaitForSeconds(reloadTime);
        ammo = maxAmmo;
        ammoSlider.value = ammo;
        reloading = false;
    }

     public IEnumerator ShootTimer()
    {
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }
    IEnumerator SwordTimer()
    {
        print("test2");
        yield return new WaitForSeconds(fireRate);
        print("test3");
        swordCol.GetComponent<Collider2D>().enabled = false;
        swordUsed = false;
    }

    public void CanAttack(bool attackBool)
    {
        canAttack = attackBool;
    }
}
