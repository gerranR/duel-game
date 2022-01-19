using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    public bool canShoot = true, reloading, swordUsed, canAttack = false, burst, isShooting, poison, fire, hasShotgun, bombOnHit, trampolineOnhit, slowzoneOnHit;
    public float fireRate, reloadTime, chargeTime, SwordDur, bulletDmg, swordDmg, poisonDmg, poisonTime, shotgunShots, shotgunSpread, numOfBulletBounce, fireDmg, fireTime;
    public GameObject gun, bullet, swordCol, ammoPanels, ammoSprites, muzzleflash;
    public int ammo, maxAmmo, burstNum, burstMax;
    public bool hasChargeBullet, ischarging, hasReverseControles;
    private Vector3 bulletScale;
    public float reverseControleTime;

    public AudioSource ShootAudio, shotgunAudio, meleeAudio, reloadAudio;
    public Slider ammoSlider;
    public Animator playerAim, armAnim;


    private void Update()
    {
        if(hasChargeBullet && ischarging)
        {
            chargeTime += Time.deltaTime;
        }
    }

    public void GunInput(InputAction.CallbackContext callback)
    {
        if(callback.performed && isShooting == false && canAttack)
        {
            isShooting = true;
            print("Shoot");
            Gun();
        }

        if(callback.canceled && hasChargeBullet)
        {

            if (hasShotgun && canShoot & ammo > 0 && reloading == false)
            {
                ischarging = false;
                armAnim.SetTrigger("Shooting");
                shotgunAudio.Play();
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
                    bulletInstance.GetComponent<Bullet>().fire = fire;
                    bulletInstance.GetComponent<Bullet>().fireDmg = fireDmg;
                    bulletInstance.GetComponent<Bullet>().fireTime = fireTime;
                    bulletInstance.GetComponent<Bullet>().player = this.gameObject;
                    bulletInstance.GetComponent<Bullet>().hasReverseControles = hasReverseControles;
                    bulletInstance.GetComponent<Bullet>().reverseControleTime = reverseControleTime;
                    if (chargeTime >= 5)
                    {
                        bulletScale = new Vector3(0.2f, 0.2f, 0.2f);
                        bulletInstance.GetComponent<Bullet>().dmg = bulletDmg * 2f;
                    }
                    else if (chargeTime >= 3)
                    {
                        bulletScale = new Vector3(0.15f, 0.15f, 0.15f);
                        bulletInstance.GetComponent<Bullet>().dmg = bulletDmg * 1.5f;
                    }
                    else if (chargeTime >= 1)
                    {
                        bulletScale = new Vector3(0.12f, 0.12f, 0.12f);
                        bulletInstance.GetComponent<Bullet>().dmg = bulletDmg * 1.2f;
                    }
                    bulletInstance.transform.localScale = bulletScale;

                }
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
                bulletInstance.GetComponent<Bullet>().fire = fire;
                bulletInstance.GetComponent<Bullet>().fireDmg = fireDmg;
                bulletInstance.GetComponent<Bullet>().fireTime = fireTime;
                bulletInstance.GetComponent<Bullet>().player = this.gameObject;
                bulletInstance.GetComponent<Bullet>().numOfBounces = numOfBulletBounce;
                bulletInstance.GetComponent<Bullet>().hasReverseControles = hasReverseControles;
                bulletInstance.GetComponent<Bullet>().reverseControleTime = reverseControleTime;
                if (chargeTime >= 5)
                {
                    bulletScale = new Vector3(0.3f, 0.3f, 0.3f);
                    bulletInstance.GetComponent<Bullet>().dmg = bulletDmg * 2f;
                }
                else if (chargeTime >= 3)
                {
                    bulletScale = new Vector3(0.2f, 0.2f, 0.2f);
                    bulletInstance.GetComponent<Bullet>().dmg = bulletDmg * 1.5f;
                }
                else if (chargeTime >= 1)
                {
                    bulletScale = new Vector3(0.15f, 0.15f, 0.15f);
                    bulletInstance.GetComponent<Bullet>().dmg = bulletDmg * 1.2f;
                }
                bulletInstance.transform.localScale = bulletScale;

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

    public void Gun()
    {
        if (canAttack)
        {
            if(hasChargeBullet)
            {
                bulletScale = new Vector3(.1f, .1f, 1);
                chargeTime = 0f;
                ischarging = true;
            }
            else if(hasShotgun && canShoot & ammo > 0 && reloading == false)
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
                    bulletInstance.GetComponent<Bullet>().fire = fire;
                    bulletInstance.GetComponent<Bullet>().fireDmg = fireDmg;
                    bulletInstance.GetComponent<Bullet>().fireTime = fireTime;
                    bulletInstance.GetComponent<Bullet>().player = this.gameObject;
                    bulletInstance.GetComponent<Bullet>().hasReverseControles = hasReverseControles;
                    bulletInstance.GetComponent<Bullet>().reverseControleTime = reverseControleTime;

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
                bulletInstance.GetComponent<Bullet>().fire = fire;
                bulletInstance.GetComponent<Bullet>().fireDmg = fireDmg;
                bulletInstance.GetComponent<Bullet>().fireTime = fireTime;
                bulletInstance.GetComponent<Bullet>().player = this.gameObject;
                bulletInstance.GetComponent<Bullet>().numOfBounces = numOfBulletBounce;
                bulletInstance.GetComponent<Bullet>().hasReverseControles = hasReverseControles;
                bulletInstance.GetComponent<Bullet>().reverseControleTime = reverseControleTime;

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
