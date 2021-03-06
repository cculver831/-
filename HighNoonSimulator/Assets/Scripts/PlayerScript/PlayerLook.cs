﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class PlayerLook : MonoBehaviourPunCallbacks
{
    [SerializeField] private string MouseXInputname = "Vertical" , MouseYInputname = "Horizontal";
    [SerializeField] private float mouseSensitivity = 100;

    [SerializeField] private Transform playerBody;

    private float xAxisClamp;
    // Start is called before the first frame update

    private void Awake()
    {
    
            LockCursor();
            xAxisClamp = 0.0f;
   
    }
    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
     
            CameraRotation();
        
        
    }
    private void CameraRotation()
    {
        float mouseX = Input.GetAxis(MouseXInputname) * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis(MouseYInputname) * mouseSensitivity * Time.deltaTime;

        xAxisClamp += mouseY;

        if(xAxisClamp > 90.0f)
        {
            xAxisClamp = 90.0f;
            mouseY = 0.0f;
            ClampXaxisRotationtoValue(270.0f);
        }
       else if (xAxisClamp < -90.0f)
        {
            xAxisClamp = -90.0f;
            mouseY = 0.0f;
            ClampXaxisRotationtoValue(90.0f);
        }

        transform.Rotate(Vector3.left * mouseY);
        playerBody.Rotate(Vector3.up * mouseX);
    }
    private void ClampXaxisRotationtoValue(float value)
    {
        Vector3 eulerRotation = transform.eulerAngles;
        eulerRotation.x = value;
        transform.eulerAngles = eulerRotation;
    }
}
