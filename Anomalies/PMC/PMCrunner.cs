using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PMCrunner : MonoBehaviour
{
    float detectDistance;
    float distanceToEnemy;
    Transform target;
    bool isAlerted;
    string enemyTag = "ermacore faction"; 
    PhotonView _pv;
    PMCmovement _Movement;
    public PMCweapon _Weapon; 
    [SerializeField] Transform enemyDetectorAimer;
    // Start is called before the first frame update
    void Start()
    {
        _pv = GetComponent<PhotonView>(); 
        _Movement = GetComponent<PMCmovement>();
        if (PhotonNetwork.IsMasterClient)
            TeleportToNavMesh();
    }

    void TeleportToNavMesh()
    {
        NavMeshHit myNavHit;
        if (NavMesh.SamplePosition(transform.position, out myNavHit, 100, -1))
        {
            transform.position = myNavHit.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

        target = GetClosestEnemy();

        if (target == null)
            return;

        distanceToEnemy = Vector3.Distance(transform.position, target.position);
        if(distanceToEnemy > 200)
        {
            _Movement.MoveTo(target, true);
        }
        else if(distanceToEnemy < 200 && distanceToEnemy > 100)
        {
            _Movement.MoveTo(target, false); 
        }
        else
        {
            
            RaycastHit hit; //check if you see the player
             
            if (Physics.Raycast(enemyDetectorAimer.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity) && hit.transform.tag == enemyTag)
            { 
                _Weapon.Semi(target);
                _Movement.LookAt(target);
                _Movement.MoveTo(null, false);
            }
            else
            {
                _Movement.MoveTo(target, false);
                _Movement.LookAt(target);
            } 
        }
    }

    #region findTarget
    Transform GetClosestEnemy()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 100f);
        Collider nearestCollider = null;
        float minSqrDistance = Mathf.Infinity;
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].tag == enemyTag)
            {
                float sqrDistanceToCenter = (transform.position - colliders[i].transform.position).sqrMagnitude;
                if (sqrDistanceToCenter < minSqrDistance)
                {
                    minSqrDistance = sqrDistanceToCenter;
                    nearestCollider = colliders[i];
                }
            }
        }
        if (nearestCollider != null)
            return nearestCollider.transform;
        else
            return null;
    }
    #endregion 
}
