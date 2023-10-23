using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    float hInput, yInput;


    [SerializeField]
    float rotateSpeed;

    float allowedRot;

    [SerializeField]
    GameObject RotateObject;

    void Update()
    {
        hInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");

        transform.Rotate(new Vector3(0f, hInput * rotateSpeed * Time.deltaTime, 0f));

        allowedRot = Mathf.Clamp(allowedRot + yInput, 0f, 65f);
        
        RotateObject.transform.rotation = Quaternion.Euler(allowedRot, gameObject.transform.rotation.y, gameObject.transform.rotation.z);
        //RotateObject.transform.Rotate(new Vector3(yInput * rotateSpeed * Time.deltaTime, 0f, 0f), Space.Self);

    }
}
