using UnityEngine;
using System.Collections;

public class WeaponsController : MonoBehaviour 
{
    public GameObject explosion;
    CarController carController;

    public GameObject spikeBall;
    public GameObject bomb;
    public GameObject splitMetalBall;
    private GameObject currentWeapon;

    private bool hasWeapon = false;

	// Use this for initialization
    IEnumerator Start() 
    {
        yield return new WaitForSeconds(1);
        carController = GetComponent<CarController>();
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(Random.Range(0, 2));
	}

    void OnTriggerEnter(Collider other)
    {
        //Picked up weapon box
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            SetRandomWeapon();
        }
        //Hit by a weapon
        else if (other.gameObject.CompareTag("weapon"))
        {
            //Remove weapon from game
            other.gameObject.SetActive(false);

            //Instantiate explosion particles
            GameObject newExplosion = (GameObject)Instantiate(explosion, other.gameObject.transform.position, Quaternion.identity);

            //Delay user from moving 
            StartCoroutine(wait(newExplosion));
        }
    }

    IEnumerator wait(GameObject other)
    {
        carController.canMove = false;
        carController.SetKinematic(true);
        carController.SetRigidbodyVelocity(0f);

        yield return new WaitForSeconds(0.5f);

        carController.SetKinematic(false);
        carController.canMove = true;
        other.SetActive(false);

    }

    public void ShootWeapon()
    {
        if (!hasWeapon)
            return;

        hasWeapon = false;
        if (currentWeapon == spikeBall || currentWeapon == bomb)
        {
            Vector3 pos = transform.position;
            pos.y = pos.y + 1;

            Instantiate(currentWeapon, transform.position - transform.forward + Vector3.up, transform.rotation);
        }
        else
        {
            GameObject weapon = (GameObject)Instantiate(currentWeapon, transform.position + transform.forward + Vector3.up, transform.rotation);
            weapon.GetComponent<Rigidbody>().AddForce(weapon.transform.forward*50,ForceMode.Impulse);
        }
    }

    public void SetRandomWeapon()
    {
        hasWeapon = true;
        int rand = Random.Range(0, 2);
        Debug.Log(rand);
        if (rand == 0)
            currentWeapon = spikeBall;
        if (rand == 1)
            currentWeapon = bomb;
        if (rand == 2)
            currentWeapon = splitMetalBall;
        else
            currentWeapon = spikeBall;
    }
}
