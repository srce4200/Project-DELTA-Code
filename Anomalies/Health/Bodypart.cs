using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bodypart : MonoBehaviour
{
    public float damageMultiplier = 1f;
    public void TakeDamage(float damage)
    {
        transform.root.GetComponent<Health>().TakeDamage(damage * damageMultiplier);
    }
}
