using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckpointSystem : MonoBehaviour
{
    [SerializeField] private Transform LastCheckpoint;

    public Transform GetLastCheckpoint()
    {
        return LastCheckpoint;

    }
    public void SetLastCheckpoint(Transform Data)
    {
        LastCheckpoint = Data;
    }
}
