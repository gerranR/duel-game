using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    public bool canShoot = true, reloading, swordUsed, canAttack = false, burst, isShooting, poison, hasShotgun, bombOnHit, trampolineOnhit;
    public float fireRate, reloadTime, SwordDur, bulletDmg, swordDmg, poisonDmg, poisonTime, shotgunShots, shotgunSpread, numOfBulletBounce;
    public GameObject gun, bullet, swordCol, ammoPanels, ammoSprites, muzzleflash;
    public int ammo, maxAmmo, burstNum, burstMax;

    public AudioSource ShootAudio, meleeAudio, reloadAudio;
    public Slider ammoSlider;
    public Animator playerAim, armAnim;

    public void GunInput(InputAction.CallbackContext callback)
    {
        if(callback.performed && isShooting == false && canAttack)
        {
            isShooting = true;
            print("Shoot");
            Gun();
        }
    }

    public void Gun()
    {
        if (canAttack)
        {
            if(hasShotgun && canShoot & ammo > 0 && reloading == false)
            {
                armAnim.SetTrigger("Shooting");
                ShootAudio.Play();
                ammo--;
                ammoSlider.maxValue = maxAmmo;
                ammoSlider.value = ammo;
                for (int i = 0; i < shotgunShots; i++)
                {
                    Quaternion spread = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(Random.Range(-shotgunSpread, shotgunSpread), Random.Range(-shotgunSpread, shotgunSpread), 0f));

                    GameObject bulletInstance = Instantiate(bullet, gun.transform.position, (gun.transform.rotation * spread));
                    bulletInstance.GetComponent<Bullet>().dmg = bulletDmg;
                    bulletInstance.GetComponent<Bullet>().poison = poison;
                    bulletInstance.GetComponent<Bullet>().poisonDmg = poisonDmg;
                    bulletInstance.GetComponent<Bullet>().poisonTime = poisonTime;
                    bulletInstance.GetComponent<Bullet>().player = this.gameObject;
                    
                }
                if(burst)
                {
                    StartCoroutine(burstTimer());
                }
                else
                {
                    canShoot = false;
                    StartCoroutine(ShootTimer());
                }

            }
            else if (canShoot & ammo > 0 && reloading == false)
            {
                armAnim.SetTrigger("Shooting");
                ShootAudio.Play();
                ammo--;
                ammoSlider.maxValue = maxAmmo;
                ammoSlider.value = ammo;
                GameObject bulletInstance = Instantiate(bullet, gun.transform.position, gun.transform.rotation);
                bulletInstance.GetComponent<Bullet>().dmg = bulletDmg;
                bulletInstance.GetComponent<Bullet>().poison = poison;
                bulletInstance.GetComponent<Bullet>().poisonDmg = poisonDmg;
                bulletInstance.GetComponent<Bullet>().poisonTime = poisonTime;
                bulletInstance.GetComponent<Bullet>().player = this.gameObject;
                bulletInstance.GetComponent<Bullet>().numOfBounces = numOfBulletBounce;

                if (burst)
                {
                    StartCoroutine(burstTimer());
                }
                else
                {
                    canShoot = false;
                    StartCoroutine(ShootTimer());
                }
            }

            else if (canShoot && ammo == 0 && reloading == false)
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
        isShooting = false;
        burstNum = 0;
        reloading = false;
    }

     public IEnumerator ShootTimer()
    {
        muzzleflash.SetActive(true);
        yield return new WaitForSeconds(.1f);
        muzzleflash.SetActive(false);
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
        isShooting = false;
    }
    public IEnumerator burstTimer()
    {
        print("burts");

        muzzleflash.SetActive(true);
        yield return new WaitForSeconds(.1f);
        muzzleflash.SetActive(false);
        burstNum++;
        if(burstNum<burstMax)
        {
            Gun();
        }
        else
        {
            canShoot = false;
            burstNum = 0;
        }
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
        isShooting = false;
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
