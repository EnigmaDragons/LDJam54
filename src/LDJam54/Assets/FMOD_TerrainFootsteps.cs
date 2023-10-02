using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMOD_TerrainFootsteps : MonoBehaviour
{
    [SerializeField] FMOD_FootStepManager footstepManager;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            footstepManager.SetTerrainMetal(); 
        }
    }
}
