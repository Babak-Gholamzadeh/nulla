using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class cameraController : MonoBehaviour
{
	public GameObject BG;
	public Text levelNumber;
	public GameObject startPoint;
	public GameObject ball;
	public Button runBtn;
	public Sprite imgRunBtn1;
	public Sprite imgRunBtn2;
	public Sprite imgBoard11;
	public Sprite imgBoard12;
	public Sprite imgBoard21;
	public Sprite imgBoard22;

	float tsClick = 0.5f;
	float tsClickCounter;
	bool boardSelected = false;
	bool ballRun = false;

	Transform currentBoard;
	Vector3 startPos;
	Vector3 firstPos;
	Vector3 levelNumPos;

	float xR = 0f, yR = 0f, tR = 0f;

	void Start ()
	{
		levelNumPos = levelNumber.transform.position - BG.transform.position;
	}

	void Update ()
	{
		levelNumber.transform.position = BG.transform.position + levelNumPos;
		if (tR > 0f)
		{
			tR -= Time.deltaTime;
			BG.transform.position = Vector3.Lerp (BG.transform.position, new Vector3 (xR, yR, BG.transform.position.z), 0.5f * Time.deltaTime);
		}
		else
		{
			xR = BG.transform.position.x + Random.Range (-0.4f, 0.4f);
			yR = BG.transform.position.y + Random.Range (-0.4f, 0.4f);
			if (BG.transform.position.x < -0.3f || BG.transform.position.x > 0.5f)
				xR = 0.2f;
			if (BG.transform.position.y < 1.1f || BG.transform.position.y > 2.5f)
				yR = 1.8f;
			tR = Random.Range (0.5f, 1.5f);
		}

		/////Touch///////
		///*
		if (Input.touchCount == 1)
		{
			Vector3 touchPos;
			if (Input.GetTouch (0).phase == TouchPhase.Began)
			{
				touchPos = new Vector3 (Input.GetTouch (0).position.x, Input.GetTouch (0).position.y, 0f);
				startPos = touchPos;
				firstPos = startPos;
				tsClickCounter = tsClick;
			}
			else if (Input.GetTouch (0).phase == TouchPhase.Moved)
			{
				touchPos = new Vector3 (Input.GetTouch (0).position.x, Input.GetTouch (0).position.y, 0f);
				if (boardSelected)
				{
					float zRotate = 0f;
					if (Camera.main.ScreenPointToRay (touchPos).origin.y < currentBoard.position.y)
						zRotate = -((startPos.x - touchPos.x) / 6f);
					else
						zRotate = ((startPos.x - touchPos.x) / 6f);
					//if (Camera.main.ScreenPointToRay (touchPos).origin.x < currentBoard.position.x)
					//	zRotate = (startPos.y - touchPos.y);
					//else
					//	zRotate = -(startPos.y - touchPos.y);
					currentBoard.Rotate (0f, 0f, zRotate);
				}
				startPos = touchPos;
				tsClickCounter -= Time.deltaTime;			
			}
			else if (Input.GetTouch (0).phase == TouchPhase.Canceled || Input.GetTouch (0).phase == TouchPhase.Ended)
			{
				touchPos = new Vector3 (Input.GetTouch (0).position.x, Input.GetTouch (0).position.y, 0f);
				Vector3 secondPos = touchPos;
				if (Mathf.Abs (firstPos.x - secondPos.x) < 1f && Mathf.Abs (firstPos.y - secondPos.y) < 1f)
				{
					if (!ballRun)
					{
						Ray ray = Camera.main.ScreenPointToRay (touchPos);
						RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, 100f);
						if (boardSelected)
						{
							if (tsClickCounter > 0f)
							{
								if (currentBoard.CompareTag ("Board1"))
								{
									currentBoard.GetComponent <SpriteRenderer> ().color = Color.black;
									currentBoard.GetComponent <SpriteRenderer> ().sprite = imgBoard11;
								}
								else
								{
									currentBoard.GetComponent <SpriteRenderer> ().sprite = imgBoard21;
								}
								currentBoard.GetComponent <boardSave> ().Save ();
								boardSelected = false;
							}
						}
						if (hit.collider)
						{
							if (hit.transform.CompareTag ("Board1"))
							{
								boardSelected = true;
								hit.transform.GetComponent <SpriteRenderer> ().color = Color.white;
								hit.transform.GetComponent <SpriteRenderer> ().sprite = imgBoard12;
								currentBoard = hit.transform;
							}
							else if (hit.transform.CompareTag ("Board2")) {
								boardSelected = true;
								hit.transform.GetComponent <SpriteRenderer> ().sprite = imgBoard22;
								currentBoard = hit.transform;
							}
						}
					}
				}
			}
		}
		//*/
		/////Mouse/////////
		/*
		if (Input.GetMouseButtonDown (0))
		{
			startPos = Input.mousePosition;
			firstPos = startPos;
			tsClickCounter = tsClick;
		}

		if (Input.GetMouseButton (0))
		{
			if (boardSelected)
			{
				float zRotate = 0f;
				if (Camera.main.ScreenPointToRay (Input.mousePosition).origin.y < currentBoard.position.y)
					zRotate = -((startPos.x - Input.mousePosition.x) / 6f);
				else
					zRotate = ((startPos.x - Input.mousePosition.x) / 6f);
				//if (Camera.main.ScreenPointToRay (Input.mousePosition).origin.x < currentBoard.position.x)
				//	zRotate = (startPos.y - Input.mousePosition.y);
				//else
				//	zRotate = -(startPos.y - Input.mousePosition.y);
				currentBoard.Rotate (0f, 0f, zRotate);
			}
			startPos = Input.mousePosition;
			tsClickCounter -= Time.deltaTime;
		}

		if (Input.GetMouseButtonUp (0))
		{
			Vector3 secondPos = Input.mousePosition;
			if (Mathf.Abs (firstPos.x - secondPos.x) < 1f && Mathf.Abs (firstPos.y - secondPos.y) < 1f)
			{
				if (!ballRun)
				{
					Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
					RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, 100f);
					if (boardSelected)
					{
						if (tsClickCounter > 0f)
						{
							if (currentBoard.CompareTag ("Board1"))
							{
								currentBoard.GetComponent <SpriteRenderer> ().color = Color.black;
								currentBoard.GetComponent <SpriteRenderer> ().sprite = imgBoard11;
							}
							else
							{
								currentBoard.GetComponent <SpriteRenderer> ().sprite = imgBoard21;
							}
							currentBoard.GetComponent <boardSave> ().Save ();
							boardSelected = false;
						}
					}
					if (hit.collider)
					{
						if (hit.transform.CompareTag ("Board1"))
						{
							boardSelected = true;
							hit.transform.GetComponent <SpriteRenderer> ().color = Color.white;
							hit.transform.GetComponent <SpriteRenderer> ().sprite = imgBoard12;
							currentBoard = hit.transform;
						}
						else if (hit.transform.CompareTag ("Board2")) {
							boardSelected = true;
							hit.transform.GetComponent <SpriteRenderer> ().sprite = imgBoard22;
							currentBoard = hit.transform;
						}
					}
				}
			}
		}
		//*/
	}

	public void SetRunBtn ()
	{
		ballRun = !ballRun;
		if (ball.GetComponent <Rigidbody2D> ().isKinematic == true)
		{
			if (boardSelected)
			{
				if (currentBoard.CompareTag ("Board1"))
				{
					currentBoard.GetComponent <SpriteRenderer> ().color = Color.black;
					currentBoard.GetComponent <SpriteRenderer> ().sprite = imgBoard11;
				}
				else
				{
					currentBoard.GetComponent <SpriteRenderer> ().sprite = imgBoard21;
				}
				currentBoard.GetComponent <boardSave> ().Save ();
				boardSelected = false;
			}
			runBtn.GetComponent <Image> ().sprite = imgRunBtn2;
			ball.GetComponent <Rigidbody2D> ().isKinematic = false;
		}
		else
		{
			runBtn.GetComponent <Image> ().sprite = imgRunBtn1;
			ball.GetComponent <Rigidbody2D> ().isKinematic = true;
			ball.transform.position = startPoint.transform.position;
		}
	}
}
