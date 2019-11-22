using UnityEngine;
using System.Collections;

public class boardSave : MonoBehaviour
{
	gameController GM;

	void Start ()
	{
		GM = GetComponentInParent <gameController> ();
		if (GM.levelNumber < 0)
		{
			transform.rotation = Quaternion.Euler (0f, 0f, GM.angleBoard);
		}
		else
		{
			if (PlayerPrefs.GetInt ("Save") == 1)
				transform.rotation = Quaternion.Euler (0f, 0f, PlayerPrefs.GetFloat (gameObject.name, transform.rotation.eulerAngles.z));
		}
	}

	void Update ()
	{
		if (GM.levelNumber < 0)
		{
			GM.angleBoard = 360 - transform.rotation.eulerAngles.z;
		}
	}

	public void Save ()
	{
		PlayerPrefs.SetFloat (gameObject.name, transform.rotation.eulerAngles.z);
	}
}
