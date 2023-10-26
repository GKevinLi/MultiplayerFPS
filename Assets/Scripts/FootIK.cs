using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootIK : MonoBehaviour
{
    Animator anim;
    public Transform thing;
    public Transform LeftFoot;
    public Transform RightFoot;
    public Rigidbody rb;
    public Vector3 temp;
    public Vector3 temp2;
    private float f;
    private float f2;

    public LayerMask layerMask; // Select all layers that foot placement applies to.


    public float DistanceToGround;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
	temp = new Vector3(anim.GetIKPosition(AvatarIKGoal.LeftFoot).x, thing.position.y - 0.88f, anim.GetIKPosition(AvatarIKGoal.LeftFoot).z);
	temp2 = new Vector3(anim.GetIKPosition(AvatarIKGoal.RightFoot).x, thing.position.y - 0.88f, anim.GetIKPosition(AvatarIKGoal.RightFoot).z);
	f = 0;
	f2 = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnAnimatorIK(int layerIndex) {
	    
	    if(anim == null) {
		return;
		}
            // Set the weights of left and right feet to the current value defined by the curve in our animations.
            anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, anim.GetFloat("IKLeftWeight"));
            anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, anim.GetFloat("IKLeftWeight"));
            anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, anim.GetFloat("IKRightWeight"));
            anim.SetIKRotationWeight(AvatarIKGoal.RightFoot, anim.GetFloat("IKRightWeight"));
		
	    
            // Left Foot
            RaycastHit hit;
	    RaycastHit hit2;
		Vector3 footPosition1 = new Vector3(0, 0, 0);
		Vector3 footPosition2 = new Vector3(0, 0, 0);
            // We cast our ray from above the foot in case the current terrain/floor is above the foot position.
            Ray ray = new Ray(new Vector3(anim.GetIKPosition(AvatarIKGoal.LeftFoot).x, temp.y, anim.GetIKPosition(AvatarIKGoal.LeftFoot).z) + new Vector3(0, 0.7f, 0), Vector3.down);
		
		Debug.DrawRay(new Vector3(anim.GetIKPosition(AvatarIKGoal.LeftFoot).x, temp.y, anim.GetIKPosition(AvatarIKGoal.LeftFoot).z) + new Vector3(0, 0.7f, 0), Vector3.down,Color.green);
            if (Physics.Raycast(ray, out hit, DistanceToGround + 5f, layerMask)) 		{

               
			  
                    footPosition1 = hit.point; 
		    if(anim.GetFloat("Velocity X") < 0.01 && anim.GetFloat("Velocity Z") < 0.01) {
			temp = new Vector3(anim.GetIKPosition(AvatarIKGoal.LeftFoot).x, thing.position.y - 1.08f, anim.GetIKPosition(AvatarIKGoal.LeftFoot).z);
			
		    }
		   
		    else {
			temp = new Vector3(temp.x, thing.position.y - 1.08f, temp.z);
			//Debug.Log("Hi");
		    }
		    f = temp.y - footPosition1.y;
		    
                    footPosition1.y += DistanceToGround; 
                    //anim.SetIKPosition(AvatarIKGoal.LeftFoot, footPosition1);
                    //anim.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.FromToRotation(Vector3.up, hit.normal) * transform.rotation);


                

            }
	    else {
	    Debug.Log("funi evh viej ");
		anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 0);
            anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 0);
            anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, 0);
            anim.SetIKRotationWeight(AvatarIKGoal.RightFoot, 0);
	}
            // Right Foot
            ray = new Ray(new Vector3(anim.GetIKPosition(AvatarIKGoal.RightFoot).x, temp2.y, anim.GetIKPosition(AvatarIKGoal.RightFoot).z) + new Vector3(0, 0.7f, 0), Vector3.down);
		Debug.DrawRay(new Vector3(anim.GetIKPosition(AvatarIKGoal.RightFoot).x, temp2.y, anim.GetIKPosition(AvatarIKGoal.RightFoot).z) + new Vector3(0, 0.7f, 0), Vector3.down, Color.green);
		
            if (Physics.Raycast(ray, out hit2, DistanceToGround + 5f, layerMask)) 			{

               
			  
                    footPosition2 = hit2.point;
		    if(anim.GetFloat("Velocity X") < 0.01 && anim.GetFloat("Velocity Z") < 0.01) {
			temp2 = new Vector3(anim.GetIKPosition(AvatarIKGoal.RightFoot).x, thing.position.y - 1.08f, anim.GetIKPosition(AvatarIKGoal.RightFoot).z);
		    }
		 
		    else {
			temp2 = new Vector3(temp2.x, thing.position.y - 1.08f, temp2.z);
			
		    }
		    f2 = temp2.y - footPosition2.y;
                    footPosition2.y += DistanceToGround;
                    //anim.SetIKPosition(AvatarIKGoal.RightFoot, footPosition2);
                    //anim.SetIKRotation(AvatarIKGoal.RightFoot, 					Quaternion.FromToRotation(Vector3.up, hit2.normal) * transform.rotation);


                

         }
	else {
	    Debug.Log("funi evh viej ");
		anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 0);
            anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 0);
            anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, 0);
            anim.SetIKRotationWeight(AvatarIKGoal.RightFoot, 0);
	}
	
	//if (f < 0.1)
        //{
            //f = 0;
        //}
        //if (f2 < 0.1)
        //{
            //f2 = 0;
        //}
	
	

	//Debug.Log(LL.distance);
	//Debug.Log(RR.distance);
	
	//Debug.Log(anim.GetIKPositionWeight(AvatarIKGoal.LeftFoot));
	//Debug.Log(anim.GetIKPositionWeight(AvatarIKGoal.RightFoot));
	
		if(f > f2) {
		    //if(anim.GetFloat("Velocity X") < 0.05 && anim.GetFloat("Velocity Z") < 0.05) {
			
		    	
			anim.SetIKPosition(AvatarIKGoal.RightFoot, new Vector3(footPosition2.x, footPosition2.y + f + 0.1f, footPosition2.z));
			anim.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.FromToRotation(Vector3.up, hit2.normal) * transform.rotation);
			anim.SetIKPosition(AvatarIKGoal.LeftFoot, footPosition1);
                    	anim.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.FromToRotation(Vector3.up, hit.normal) * transform.rotation);

			//}
		}
		else if(f < f2) {
			//if(anim.GetFloat("Velocity X") < 0.05 && anim.GetFloat("Velocity Z")< 0.05) {
				
		    		
			anim.SetIKPosition(AvatarIKGoal.LeftFoot, new Vector3(footPosition1.x, footPosition1.y + f2 + 0.1f, footPosition1.z));
			anim.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.FromToRotation(Vector3.up, hit.normal) * transform.rotation);
			anim.SetIKPosition(AvatarIKGoal.RightFoot, footPosition2);
                    	anim.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.FromToRotation(Vector3.up, hit2.normal) * transform.rotation);
				
			//}
		}
	        else {

			anim.SetIKPosition(AvatarIKGoal.LeftFoot, footPosition1);
                    	anim.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.FromToRotation(Vector3.up, hit.normal) * transform.rotation);
			anim.SetIKPosition(AvatarIKGoal.RightFoot, footPosition2);
                    	anim.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.FromToRotation(Vector3.up, hit2.normal) * transform.rotation);
			
		}
		
		
			
		
		
		
		


        
    }
}
