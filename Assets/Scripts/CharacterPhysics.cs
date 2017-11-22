using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPhysics : MonoBehaviour {
    public float GroundDisstanceFromRoot;
    public Vector3 Gravity;
    public float MaxFallSpeed;
    
	// Use this for initialization
	void Start () {
        //GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0,0,1), ForceMode.VelocityChange);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 RelativeGravity = transform.InverseTransformDirection(Gravity);
        
        if (GetComponent<CharacterStats>().Velocity.y > MaxFallSpeed && !GetComponent<CharacterStats>().Grounded)
        {
            GetComponent<CharacterStats>().RelativeNewVelocity.y += RelativeGravity.y;
            GetComponent<CharacterStats>().RelativeNewVelocity.z += RelativeGravity.z;
            GetComponent<CharacterStats>().RelativeNewVelocity.x += RelativeGravity.x;
            Debug.Log("Fall");
            Debug.DrawRay(transform.position, RelativeGravity);
        }else if (GetComponent<CharacterStats>().Grounded && Vector3.Distance(Vector3.zero, GetComponent<CharacterStats>().RelativeNewVelocity) != 0)
        {

        }

        if (GetComponent<CharacterStats>().Grounded)
        {
            if (GetComponent<CharacterStats>().PlayerState == CharacterState.SpinAttack)
            {
                GetComponent<CharacterStats>().RelativeNewVelocity.x += RelativeGravity.x * 2;
                GetComponent<CharacterStats>().RelativeNewVelocity.z += RelativeGravity.z * 2;
            }else if (GetComponent<CharacterStats>().PlayerState == CharacterState.Running )
            {
                GetComponent<CharacterStats>().RelativeNewVelocity.x += RelativeGravity.x;
                GetComponent<CharacterStats>().RelativeNewVelocity.z += RelativeGravity.z;
            }
        }

        GetComponent<Rigidbody>().AddRelativeForce(GetComponent<CharacterStats>().RelativeNewVelocity - GetComponent<CharacterStats>().RelativeVelocity, ForceMode.VelocityChange);
        GetComponent<CharacterStats>().Velocity = GetComponent<Rigidbody>().velocity;
        GetComponent<CharacterStats>().RelativeVelocity = transform.InverseTransformVector(GetComponent<Rigidbody>().velocity);
        GetComponent<CharacterStats>().RelativeNewVelocity = GetComponent<CharacterStats>().RelativeVelocity;

        RaycastHit Ground;
        
        Vector3 RayVector = transform.InverseTransformDirection(new Vector3(0, -GroundDisstanceFromRoot, 0));

        Debug.DrawRay(transform.position, RayVector, Color.red);
        if (Physics.Raycast(transform.position, RayVector, out Ground))
        {
            Quaternion rotCur = Quaternion.FromToRotation(transform.up, Ground.normal) * transform.rotation;
            GetComponent<CharacterStats>().Grounded = true;
            transform.rotation = Quaternion.Lerp(transform.rotation, rotCur, Time.fixedDeltaTime * 10);
        }
        else
        {
            GetComponent<CharacterStats>().Grounded = false;
        }
    }
}
