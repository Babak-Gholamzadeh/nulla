using UnityEngine;
using System.Collections;

public class audioBG : MonoBehaviour
{

	void Awake ()
	{
		DontDestroyOnLoad (this.gameObject);
	}


}
