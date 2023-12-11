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

    LineRenderer lr;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        lr = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            firePower = Mathf.Lerp(firePower, maxPower, Time.deltaTime * maxTime);
            UpdateTrajectory(firePoint.transform.position, firePoint.transform.forward, firePower * Mathf.PI, 0.07f, 45.0f);
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
        yield return new WaitForSeconds(0.5f);
        shot = true;
        GameObject cannonBall1 = Instantiate(cannonBall, firePoint.transform.position, firePoint.transform.rotation);
        cannonBall1.GetComponent<Rigidbody>().velocity = firePoint.transform.forward * firePower;
        firePower = 1f;
    }

    void UpdateTrajectory(Vector3 startPos, Vector3 direction, float speed, float timePerSegmentInSeconds, float maxTravelDistance)
    {
        var positions = new List<Vector3>();
        var lastPos = startPos;
        var currentPos = firePoint.transform.position;
        positions.Add(startPos);

        var traveledDistance = 0.0f;
        while (traveledDistance < maxTravelDistance)
        {
            traveledDistance += speed * timePerSegmentInSeconds;
            var hasHitSomething = TravelTrajectorySegment(currentPos, direction, speed, timePerSegmentInSeconds, positions);
            if (hasHitSomething)
            {
                break;
            }
            lastPos = currentPos;
            currentPos = positions[positions.Count - 1];
            direction = currentPos - lastPos;
            direction.Normalize();
        }

        BuildTrajectoryLine(positions);
    }

    bool TravelTrajectorySegment(Vector3 startPos, Vector3 direction, float speed, float timePerSegmentInSeconds, List<Vector3> positions)
    {
        var newPos = startPos + direction * speed * timePerSegmentInSeconds + Physics.gravity * timePerSegmentInSeconds;

        RaycastHit hitInfo;
        var hasHitSomething = Physics.Linecast(startPos, newPos, out hitInfo);
        if (hasHitSomething)
        {
            newPos = hitInfo.point;
        }
        positions.Add(newPos);

        return hasHitSomething;
    }

    void BuildTrajectoryLine(List<Vector3> positions)
    {
        lr.SetVertexCount(positions.Count);
        for (var i = 0; i < positions.Count; ++i)
        {
            lr.SetPosition(i, positions[i]);
        }
    }

}
