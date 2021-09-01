using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BunnyTask/Settings")]
public class PlayerSettings : ScriptableObject
{
    public bool isPlaying;
    public bool isMuscle;
    public bool isDeath;
    public float ForwardSpeed;
    public float sensitivity;
    public float carrotNum;
    public float carrotScore;
    public float muscleTimer;
}
