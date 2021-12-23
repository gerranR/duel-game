using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PointToMouse : MonoBehaviour
{
    public GameObject player;
    private Vector2 mousePos;
    public bool isFlipped;
    private float heading;

    private void FixedUpdate()
    {
        if (GetComponentInParent<PlayerInput>().currentControlScheme == "Keyboard & mouse")
        {
            Vector3 diffrence = Camera.main.ScreenToWorldPoint(mousePos) - transform.position;
            diffrence.Normalize();

            float rotationZ = Mathf.Atan2(diffrence.y, diffrence.x) * Mathf.Rad2Deg;

            if(isFlipped)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, rotationZ * -1);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
            }

            if (rotationZ < -90 || rotationZ > 90)
            {
                if (player.transform.eulerAngles.y == 0)
                {
                    transform.localRotation = Quaternion.Euler(180, 0, -rotationZ);
                }
                else if (player.transform.eulerAngles.y == 180)
                {
                    transform.localRotation = Quaternion.Euler(180, 180, -rotationZ);
                }
            }
        }
        else
        {
            if (mousePos.sqrMagnitude > 0.1f)
            {
                Vector2 aim = mousePos;
                heading = Mathf.Atan2(aim.x, aim.y) * Mathf.Rad2Deg;

                if (isFlipped)
                {
                    transform.rotation = Quaternion.Euler(0f, 0f, (-heading * -1 + 90));
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0f, 0f, (-heading + 90));
                }
                
            }

            if (-heading > 0)
            {
                print("true");
                if (transform.localScale.y == 1)
                {
                    transform.localScale = new Vector3(1, -1, 1);
                }

            }
            else if (transform.localScale.y == -1)
                {
                    print("true2");
                    transform.localScale = new Vector3(1, 1, 1);
                }
           

        }

        //if (transform.rotation.z < -90 || transform.rotation.z > 90)
        //{

        //    if (player.transform.eulerAngles.y == 0)
        //    {
        //        transform.localRotation = Quaternion.Euler(180, 0, -transform.localRotation.z);
        //    }
        //}
        //else
        //{

        //    if (player.transform.eulerAngles.y == 180)
        //    {
        //        transform.localRotation = Quaternion.Euler(180, 180, -transform.localRotation.z);
        //    }
        //}
    }

    private void Update()
    {
        print(transform.localScale.y);
    }

    public void MousePos(InputAction.CallbackContext context)
    {
            mousePos = context.ReadValue<Vector2>();

    }
}
