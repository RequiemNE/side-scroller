using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ledge : MonoBehaviour
{
    [SerializeField] private GameObject handPos, standPos;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ledgeGrab"))
        {
            var player = other.transform.parent.GetComponent<RewiredIM>();
            if (player != null)
            {
                player.GrabLedge(handPos, this);
            }
        }
    }

    public Vector3 GetStandPos()
    {
        return standPos.transform.position;
    }
}
