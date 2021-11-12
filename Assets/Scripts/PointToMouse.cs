using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PointToMouse : MonoBehaviour
{
    public GameObject player;
    private Vector2 mousePos;

    private void FixedUpdate()
    {
        Vector3 diffrence = Camera.main.ScreenToWorldPoint(mousePos) - transform.position;
        diffrence.Normalize();

        float rotationZ = Mathf.Atan2(diffrence.y, diffrence.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);

        //if(rotationZ < -90 || rotationZ > 90)
        //{
        //    if(player.transform.eulerAngles.y == 0)
        //    {
        //        transform.localRotation = Quaternion.Euler(180, 0, -rotationZ);
        //    }
        //    else if(player.transform.eulerAngles.y == 180)
        //    {
        //        transform.localRotation = Quaternion.Euler(180, 180, -rotationZ);
        //    }
        //}
    }

    public void MousePos(InputAction.CallbackContext context)
    {
        mousePos = context.ReadValue<Vector2>();
    }
}
