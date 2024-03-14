using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orientator : MonoBehaviour
{
    private void FixedUpdate()
    {
        DeviceOrientation orientation = Input.deviceOrientation;
        if (DeviceOrientation.Portrait != orientation)
        {
            Screen.orientation = ScreenOrientation.AutoRotation;
        }
    }
}
