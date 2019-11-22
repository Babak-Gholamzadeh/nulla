using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class startupCode : MonoBehaviour
{

	void Start ()
	{
		//print (PlayerPrefs.GetInt ("LevelNumber", 0).ToString ());
		SceneManager.LoadScene (PlayerPrefs.GetInt ("LevelNumber", 0) + 2);
	}


}
