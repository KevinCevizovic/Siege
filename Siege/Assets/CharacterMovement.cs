using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    float hInput, yInput;


    [SerializeField]
    float rotateSpeed;

    float allowedRotX;
    float allowedRotY;

    //public GameObject RotateObject;

    CharacterShooting shooting;

    private void Start()
    {
        shooting = GetComponent<CharacterShooting>();
    }

    void Update()
    {
        if (shooting.shot)
        {
            return;
        }
        hInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");

        //transform.Rotate(new Vector3(0f, hInput * rotateSpeed * Time.deltaTime, 0f));

        allowedRotY = Mathf.Clamp(allowedRotY + hInput, Mathf.NegativeInfinity, Mathf.Infinity);

        //RotateObject.transform.rotation = Quaternion.Euler(allowedRotX, allowedRotY, 0f);
        transform.rotation = Quaternion.Euler(0f, allowedRotY, 0f);
        //RotateObject.transform.Rotate(new Vector3(yInput * rotateSpeed * Time.deltaTime, 0f, 0f), Space.Self);

    }
}
