using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    
    public GameObject Player;
    public GameObject TouchPanel;
    public GameObject[] Floors = new GameObject[3];
    public GameObject[] Roads = new GameObject[2];
    public GameObject[] Houses = new GameObject[2];
    public GameObject[] Grasses = new GameObject[2];
    public GameObject[] House_Blurrys = new GameObject[4];
    public GameObject[] Men = new GameObject[2];

    public Sprite[] FloorSprite = new Sprite[3];

    private float[] bgSpeed = { 0.12f, 0.06f, 0.03f };

    private float skyWidth = 9f;
    private float roadWidth = 9.68f;
    private float grassWidth = 9.7f;
    private float floorWidth = 7.98f;
    
    private float houseWidth = 6.58f;
    private float houseInternal = 1.4f;
    private float houseBlrWidth = 3.3f;
    private float houseBlrInternal = 0.2f;

    private float manWidth = 1.15f;
    private bool[] hasMen = {false, false};
    private bool[] isKicking = { false, false };

    private Vector2 jumpForce = new Vector2(0, 8);

    private Animator AnimatorPlayer;
    private Animator AnimatorMan1;
    private Animator AnimatorMan2;

    public bool isGameStart = true;
    public bool isDeath = false;

    void Start()
    {
        AnimatorPlayer = Player.GetComponent<Animator>();
        AnimatorPlayer.SetBool("isRunning", true);
        AnimatorMan1 = Men[0].GetComponent<Animator>();
        AnimatorMan2 = Men[1].GetComponent<Animator>();
    }

    void Update()
    {
        if (isGameStart && !isDeath)
            Background_Moving();
    }

    public void Jump()
    {
        if (Player.GetComponent<Player>().isJumping == false) {
            Player.GetComponent<Player>().isJumping = true;
            AnimatorPlayer.SetBool("isJumping", true);
            Player.GetComponent<Rigidbody2D>().velocity = jumpForce;
        }
    }

    void Background_Moving()
    {
        foreach (GameObject Road in Roads)
        {
            Road.GetComponent<Transform>().localPosition += Vector3.left * bgSpeed[0];
            if (Road.GetComponent<Transform>().localPosition.x <= -skyWidth / 2 - roadWidth / 2)
                Road.GetComponent<Transform>().localPosition += new Vector3(roadWidth * 2, 0, 0);
        }
        foreach (GameObject Floor in Floors)
        {
            Floor.GetComponent<Transform>().localPosition += Vector3.left * bgSpeed[0];
            if (Floor.GetComponent<Transform>().localPosition.x <= -skyWidth / 2 - floorWidth / 2)
            {
                Floor.GetComponent<Transform>().localPosition += new Vector3(floorWidth * 3, 0, 0);
                int floorType = Random.Range(0, 3);
                switch (floorType)
                {
                    case 0:
                        Floor.GetComponent<SpriteRenderer>().sprite = FloorSprite[0];
                        Floor.transform.GetChild(0).gameObject.SetActive(false);
                        Floor.transform.GetChild(1).gameObject.SetActive(false);
                        Floor.transform.GetChild(2).gameObject.SetActive(true);
                        break;
                    case 1:
                        Floor.GetComponent<SpriteRenderer>().sprite = FloorSprite[1];
                        Floor.transform.GetChild(0).gameObject.SetActive(false);
                        Floor.transform.GetChild(1).gameObject.SetActive(false);
                        Floor.transform.GetChild(2).gameObject.SetActive(true);
                        break;
                    case 2:
                        Floor.GetComponent<SpriteRenderer>().sprite = FloorSprite[2];
                        Floor.transform.GetChild(0).gameObject.SetActive(true);
                        Floor.transform.GetChild(1).gameObject.SetActive(true);
                        Floor.transform.GetChild(2).gameObject.SetActive(false);
                        break;
                }
            }
        }
        foreach (GameObject House in Houses)
        {
            House.GetComponent<Transform>().localPosition += Vector3.left * bgSpeed[0];
            if (House.GetComponent<Transform>().localPosition.x <= -skyWidth / 2 - houseWidth / 2)
            {
                House.GetComponent<Transform>().localPosition += new Vector3(houseWidth * 2 + houseInternal * 2, 0, 0);
                if (Random.Range(0, 2) == 1)
                {
                    if (!hasMen[0])
                    {
                        hasMen[0] = true;
                        float x = House.GetComponent<Transform>().localPosition.x;
                        Men[0].GetComponent<Transform>().localPosition = new Vector3(x + 1.5f, -3.6f, 0);
                    }
                    else if (!hasMen[1])
                    {
                        hasMen[1] = true;
                        float x = House.GetComponent<Transform>().localPosition.x;
                        Men[1].GetComponent<Transform>().localPosition = new Vector3(x + 1.5f, -3.6f, 0);
                    }
                }
            }
        }

        if (hasMen[0])
        {
            Men[0].GetComponent<Transform>().localPosition += Vector3.left * bgSpeed[0];
            if (Men[0].GetComponent<Transform>().localPosition.x <= 4f && isKicking[0] == false)
            {
                AnimatorMan1.SetBool("isKicking", true);
                isKicking[0] = true;
            }
            if (Men[0].GetComponent<Transform>().localPosition.x <= -skyWidth / 2 - manWidth / 2)
            {
                hasMen[0] = false;
                AnimatorMan1.SetBool("isKicking", false);
                isKicking[0] = false;
            }

        }
        if (hasMen[1])
        {
            Men[1].GetComponent<Transform>().localPosition += Vector3.left * bgSpeed[0];
            if (Men[1].GetComponent<Transform>().localPosition.x <= 4f && isKicking[1] == false)
            {
                AnimatorMan2.SetBool("isKicking", true);
                isKicking[1] = true;
            }
            if (Men[1].GetComponent<Transform>().localPosition.x <= -skyWidth / 2 - manWidth / 2)
            {
                hasMen[1] = false;
                AnimatorMan2.SetBool("isKicking", false);
                isKicking[1] = false;
            }
        }

        foreach (GameObject Grass in Grasses)
        {
            Grass.GetComponent<Transform>().localPosition += Vector3.left * bgSpeed[1];
            if (Grass.GetComponent<Transform>().localPosition.x <= -skyWidth / 2 - grassWidth / 2)
            {
                Grass.GetComponent<Transform>().localPosition += new Vector3(grassWidth * 2, 0, 0);
            }
        }
        foreach (GameObject House_B in House_Blurrys)
        {
            House_B.GetComponent<Transform>().localPosition += Vector3.left * bgSpeed[2];
            if (House_B.GetComponent<Transform>().localPosition.x <= -skyWidth / 2 - houseBlrWidth / 2)
            {
                House_B.GetComponent<Transform>().localPosition += new Vector3(houseBlrWidth * 4 + houseBlrInternal * 4, 0, 0);
            }
        }
    }
}

public class GM
{
    private static GameManager _gameManager = null;
    public static GameManager GameManager
    {
        get
        {
            if (_gameManager == null)
                _gameManager = GameObject.FindObjectOfType<GameManager>();
            return _gameManager;
        }
    }
}