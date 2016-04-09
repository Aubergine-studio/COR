using UnityEngine;

public class CameraTracking : MonoBehaviour 
{
	public GameObject player;
	private Rigidbody2D playerRB;

	// Use this for initialization
	void Start () 
	{
		playerRB = player.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.localPosition = new Vector3 (playerRB.transform.localPosition.x, playerRB.transform.localPosition.y, transform.localPosition.z);
	}
}
