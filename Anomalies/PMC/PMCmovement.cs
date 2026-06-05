using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class PMCmovement : MonoBehaviour
{
    [SerializeField] float sprintSpeed = 8f;
    [SerializeField] float walkSpeed = 4f; 
    Transform lookTarget;
    Transform destinationTarget;
    NavMeshAgent _Navmesh;

    Animator pmcAnimator;
    PMCweapon weaponsController;
    Animator weaponsAnimator;

    private void Start()
    {
        _Navmesh = GetComponent<NavMeshAgent>();
        pmcAnimator = GetComponent<Animator>();
        weaponsController = GetComponent<PMCrunner>()._Weapon;
        weaponsAnimator = weaponsController.GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if(destinationTarget != null)
            {
                Move();
            }
            else
            {
                _Navmesh.SetDestination(transform.position);
                pmcAnimator.SetBool("isWalking", false); 
                weaponsAnimator.SetBool("isWalking", false); 
            }

            if (lookTarget != null)
            {
                Look();
            }
        }
    }
    
    void Move()
    {
        _Navmesh.SetDestination(destinationTarget.position);
        pmcAnimator.SetBool("isWalking", true);
        weaponsAnimator.SetBool("isWalking", true);
    }
    void Look()
    {
        transform.LookAt(lookTarget);
    }

    public void LookAt(Transform lookTar)
    {
        lookTarget = lookTar;
    }
    public void MoveTo(Transform destination, bool sprint)
    {
        destinationTarget = destination;
        if(sprint)
        {
            _Navmesh.speed = sprintSpeed;
            pmcAnimator.SetBool("isSprinting", true);
            weaponsAnimator.SetBool("isSprinting", true);
        }
        else
        {
            _Navmesh.speed = walkSpeed;
            pmcAnimator.SetBool("isSprinting", false);
            weaponsAnimator.SetBool("isSprinting", false);
        }
    }
}
