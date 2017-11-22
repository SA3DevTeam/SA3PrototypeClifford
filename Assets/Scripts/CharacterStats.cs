using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CharacterState
{
    Still,
    Idle,
    Running,
    SpinAttack,
    SpinDashPrep,
    SpinDashReving,
    WallRun,
    WallSliding,
}
public class CharacterStats : MonoBehaviour {
    public CharacterState PlayerState;
    public Vector3 Velocity;
    public Vector3 RelativeVelocity;
    public Vector3 RelativeNewVelocity;
    public bool Grounded;
    public int Score;
    public int RingCount;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
