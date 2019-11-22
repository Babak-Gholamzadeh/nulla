using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ballController : MonoBehaviour
{

	gameController GM;
	AudioSource ballHit;

	void Start ()
	{
		GM = GetComponentInParent <gameController> ();
		ballHit = GetComponent <AudioSource> ();
	}

	void OnCollisionEnter2D (Collision2D other)
	{
		if (!ballHit.isPlaying)
			ballHit.Play ();
		
		if (other.transform.CompareTag ("Result"))
		{
			GM.endLevel = true;
			PlayerPrefs.SetInt ("Save", 0);
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.CompareTag ("Box"))
		{
			GM.levelName = other.GetComponent <Text> ().text;
		}
	}
}
