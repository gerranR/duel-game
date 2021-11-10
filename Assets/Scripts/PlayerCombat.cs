using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private bool canShoot = true, reloading;
    public float fireRate, reloadTime;
    public GameObject gun, bullet;
    public int ammo, maxAmmo;

    // Update is called once per frame
    void Update()
    {
        Combat();
    }

    void Combat()
    {
        if (Input.GetButtonDown("Fire1") && canShoot & ammo > 0 && reloading == false)
        {
            ammo--;
            Instantiate(bullet, gun.transform.position, gun.transform.rotation);
            canShoot = false;
            StartCoroutine(ShootTimer());
        }
        if (Input.GetButtonDown("Reload") && reloading == false)
        {
            print("Reload");
            reloading = true;
            StartCoroutine(ReloadTimer());
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
}
