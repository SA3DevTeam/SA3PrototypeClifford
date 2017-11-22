using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterBaseInput : MonoBehaviour {
    public float Acceleration;
    public float AirAcceleration;
    public float MaxSpeed;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (GetComponent<CharacterStats>().PlayerState == CharacterState.Running || GetComponent<CharacterStats>().PlayerState == CharacterState.Still)
        {
            StandardMovement();
        }
	}
    void StandardMovement()
    {
        if (GetComponent<CharacterStats>().Grounded) {
            if (GetComponent<CharacterStats>().RelativeNewVelocity.x * Mathf.Sign(GetComponent<CharacterStats>().RelativeNewVelocity.x) < MaxSpeed) { GetComponent<CharacterStats>().RelativeNewVelocity.x = Input.GetAxis("Movement X") * Acceleration * Time.fixedDeltaTime * 100; Debug.Log(Input.GetAxis("Movement X")); }
            if (GetComponent<CharacterStats>().RelativeNewVelocity.z * Mathf.Sign(GetComponent<CharacterStats>().RelativeNewVelocity.z) < MaxSpeed) { GetComponent<CharacterStats>().RelativeNewVelocity.z = Input.GetAxis("Movement Y") * Acceleration * Time.fixedDeltaTime * 100; Debug.Log(Input.GetAxis("Movement Y")); }
            
        }
        else
        {
            if (GetComponent<CharacterStats>().RelativeNewVelocity.x * Mathf.Sign(GetComponent<CharacterStats>().RelativeNewVelocity.x) < MaxSpeed) { GetComponent<CharacterStats>().RelativeNewVelocity.x = Input.GetAxis("Movement X") * AirAcceleration * Time.fixedDeltaTime * 100; }
            if (GetComponent<CharacterStats>().RelativeNewVelocity.z * Mathf.Sign(GetComponent<CharacterStats>().RelativeNewVelocity.z) < MaxSpeed) { GetComponent<CharacterStats>().RelativeNewVelocity.z = Input.GetAxis("Movement Y") * AirAcceleration * Time.fixedDeltaTime * 100; }
        }
        if (Vector3.Distance(Vector3.zero, GetComponent<CharacterStats>().RelativeNewVelocity) != 0)
        {
            if (GetComponent<CharacterStats>().PlayerState == CharacterState.Still)
            {
                GetComponent<CharacterStats>().PlayerState = CharacterState.Running;
            }
        }
    }
}
