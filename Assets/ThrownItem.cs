using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownItem : MonoBehaviour
{
    // Start is called before the first frame update
    public Throwable gunShotFrom;
    public GameObject burst;
    public int force;
    public float timer;
    void Start()
    {
        timer = 0;  
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += 0.01f;
	if(timer >= 1.5f) {
	    Boom();
	}
    }
    void Boom() {
	GameObject newBurst = Instantiate(burst, transform.position, transform.rotation);
	newBurst.SetActive(true);
	newBurst.GetComponent<ParticleSystem>().Play();
	Collider[] things = Physics.OverlapSphere(transform.position, 3);
	HashSet<UnityEngine.GameObject> test = new HashSet<UnityEngine.GameObject>();
	foreach(Collider c in things) {
		GameObject g = c.transform.root.gameObject;
		test.Add(g);
	}
	
	foreach(GameObject c in test) {
		
		//GameObject g = c.transform.root.gameObject;

		Debug.Log(c);
		c.GetComponent<IDamageable>()?.TakeDamage(gunShotFrom.damage);
		if(c.GetComponent<Rigidbody>() != null) {
			c.GetComponent<Rigidbody>().AddExplosionForce(800, transform.position, 150, 3);
		}
	}
	Destroy(gameObject);
	//Debug.Log("Ping!");
	
	//Vector3 v = collision.contacts[0].point;
	//gunShotFrom.bulletHit(v, collision.GetContact(0).normal);

    }
    public static string[] RemoveDuplicates(string[] s)  {
   	 HashSet<string> set = new HashSet<string>(s);
   	 string[] result = new string[set.Count];
    	 set.CopyTo(result);
   	 return result;
    }
   

    
}
