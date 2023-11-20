using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterShooting : MonoBehaviour
{
    float firePower;

    [SerializeField]
    float maxTime, maxPower, fireSpeed;

    public bool shot = false;
    private Animator anim;

    [SerializeField]
    GameObject firePoint, cannonBall;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            firePower = Mathf.Lerp(firePower, maxPower, Time.deltaTime * maxTime);
        }
        else
            shot = false;

        if (Input.GetButtonDown("Fire1"))
        {
            shot = false;
        }
        else if(Input.GetButtonUp("Fire1"))
        {
            shot = true;

            if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.90f && !anim.IsInTransition(0))
            {
                StartCoroutine(Shoot());
            }
        }

        if (!shot)
            anim.SetBool("Shoot", false);
        if (shot)
            anim.SetBool("Shoot", true);

        Debug.Log(shot);
    }

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(0.75f);
        shot = true;
        GameObject cannonBall1 = Instantiate(cannonBall, firePoint.transform.position, firePoint.transform.rotation);
        cannonBall1.GetComponent<Rigidbody>().velocity = firePoint.transform.forward * firePower;
        firePower = 1f;
    }
}
