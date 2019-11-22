using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameController : MonoBehaviour
{
	public bool endLevel = false;
	public bool startLevel = true;
	public int levelNumber;
	public string levelName = "s";
	public GameObject darkImg;
	public Text levelNameTxt;
	public float angleBoard;

	float alpha = 1f;
	int levelsOpen;

	void Awake ()
	{
		levelName = PlayerPrefs.GetString ("CurrentLevelName", "nulla");
		//PlayerPrefs.SetInt ("LevelNumber", levelNumber);
		if (levelNumber > -1)
		{
			if (PlayerPrefs.GetInt ("LevelNumber", 0) <= levelNumber)
				PlayerPrefs.SetInt ("LevelNumber", levelNumber);
		}
		else
		{
			angleBoard = 360f - (NameToNum () * 10f);
			levelNameTxt.text = levelName;
			levelsOpen = PlayerPrefs.GetInt ("LevelNumber", 0);
		}

		darkImg.SetActive (true);
	}

	void Update ()
	{
		if (startLevel)
		{
			if (alpha > 0f)
				alpha -= Time.deltaTime;
			else
			{
				darkImg.SetActive (false);
				startLevel = false;
			}
			darkImg.GetComponent <Image> ().color = new Color (0f, 0f, 0f, alpha);
		}
		if (endLevel)
		{
			darkImg.SetActive (true);
			if (alpha < 1f)
				alpha += Time.deltaTime;
			else
				NextLevel ();
			darkImg.GetComponent <Image> ().color = new Color (0f, 0f, 0f, alpha);
		}
		if (levelNumber < 0)
		{
			if ((int) angleBoard / 10 <= levelsOpen || (int) angleBoard / 10 == 35)
				AngleToName ();
			if (levelNameTxt.text == "∞")
				levelNameTxt.fontSize = 130;
			else
				levelNameTxt.fontSize = 63;
		}
	}

	void NextLevel ()
	{
		if (levelName != "")
			SceneManager.LoadScene ("Level" + levelName);
		else
			Invoke ("Exit", 1f);
	}



	public void SetBackBtn ()
	{
		if (SceneManager.GetActiveScene ().name == "Levels")
		{
			levelName = PlayerPrefs.GetString ("CurrentLevelName", "nulla");
		}
		else
		{
			levelName = "s";
			PlayerPrefs.SetString ("CurrentLevelName", levelNameTxt.text);
		}
		PlayerPrefs.SetInt ("Save", 1);
		endLevel = true;
	}

	public void SetRunBtn ()
	{
		if (levelNumber < 0)
		{
			transform.FindChild ("Box").FindChild ("ControlBox").GetComponent <Text> ().text = levelNameTxt.text;
		}
	}

	public void AngleToName ()
	{
		int intAngle = (int) angleBoard / 10;
		switch (intAngle)
		{
			case 0:
				levelNameTxt.text = "nulla";
				break;
			case 1:
				levelNameTxt.text = "I";
				break;
			case 2:
				levelNameTxt.text = "II";
				break;
			case 3:
				levelNameTxt.text = "III";
				break;
			case 4:
				levelNameTxt.text = "IV";
				break;
			case 5:
				levelNameTxt.text = "V";
				break;
			case 6:
				levelNameTxt.text = "VI";
				break;
			case 7:
				levelNameTxt.text = "VII";
				break;
			case 8:
				levelNameTxt.text = "VIII";
				break;
			case 9:
				levelNameTxt.text = "IX";
				break;
			case 10:
				levelNameTxt.text = "X";
				break;
			case 11:
				levelNameTxt.text = "XI";
				break;
			case 12:
				levelNameTxt.text = "XII";
				break;
			case 13:
				levelNameTxt.text = "XIII";
				break;
			case 14:
				levelNameTxt.text = "XIV";
				break;
			case 15:
				levelNameTxt.text = "XV";
				break;
			case 16:
				levelNameTxt.text = "∞" ;//"XVI";
				break;
			case 17:
				levelNameTxt.text = "XVII";
				break;
			case 18:
				levelNameTxt.text = "XVIII";
				break;
			case 19:
				levelNameTxt.text = "XIX";
				break;
			case 20:
				levelNameTxt.text = "XX";
				break;
			default:
				levelNameTxt.text = "";
				break;
		}
	}

	public int NameToNum ()
	{
		switch (levelName)
		{
			case "nulla":
				return 0;
			case "I":
				return 1; 
			case "II":
				return 2; 
			case "III":
				return 3; 
			case "IV":
				return 4; 
			case "V":
				return 5; 
			case "VI":
				return 6; 
			case "VII":
				return 7; 
			case "VIII":
				return 8; 
			case "IX":
				return 9; 
			case "X":
				return 10;
			case "XI":
				return 11;
			case "XII":
				return 12; 
			case "XIII":
				return 13; 
			case "XIV":
				return 14; 
			case "XV":
				return 15;
			case "∞":
				return 16;
			default:
					return -1; 
		}
	}

	void Exit ()
	{
		Application.Quit ();
	}
}

