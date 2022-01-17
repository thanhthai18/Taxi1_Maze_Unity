using DG.Tweening;
using GestureRecognizer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController_TaxiMinigame1 : MonoBehaviour
{
    public static GameController_TaxiMinigame1 instance;

    public Canvas canvas;
    public Taxi_TaxiMinigame1 myTaxi;
    public Camera mainCamera;
    public Vector3 pos;
    public RaycastHit2D[] hit;
    public List<SpaceLane_TaxiMinigame1> listPathMove = new List<SpaceLane_TaxiMinigame1>();
    public bool isHoldMouse;
    public Tween taxiMovement;
    private float speed = 5;
    public int level, power, life, maxPower;
    public Text txtLevel, txtPower, txtLife;
    public GameObject checkPoint, customer;
    public bool isOnMove;
    public bool isCannotMove;
    public List<Transform> listWayPoint = new List<Transform>();
    public int checkStep;
    public GameObject item1_Prefab, item2_Prefab;
    public List<GameObject> listItem1 = new List<GameObject>();
    public List<GameObject> listItem2 = new List<GameObject>();
    public bool isFirstMove;
    public GameObject clockwise;
    public float percentPower;
    private bool isLockStage;
    public GameObject tutorial;
    public bool isIntro = true;
    public bool isTutorial = true;
    public GameObject interfaceGame;
    public TransitionLevel_TaxiMinigame1 transition;
    


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(instance);
    }

    private void Start()
    {
        isLockStage = false;
        isCannotMove = false;
        isOnMove = false;
        isHoldMouse = false;
        tutorial.SetActive(false);
        customer.SetActive(false);
        checkPoint.SetActive(false);

        txtLevel.gameObject.SetActive(false);
        txtLife.gameObject.SetActive(false);
        txtPower.gameObject.SetActive(false);
        interfaceGame.SetActive(false);
        SetSizeCamera();
        life = 5;
        SetUpMap(1);
        checkPoint.SetActive(false);
        customer.SetActive(false);
        canvas.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().DOAnchorPosX(-374, 4).OnComplete(() =>
        {
            txtLevel.gameObject.SetActive(true);
            txtLife.gameObject.SetActive(true);
            txtPower.gameObject.SetActive(true);
            interfaceGame.SetActive(true);
            checkPoint.SetActive(true);
            customer.SetActive(true);
            for (int i = 0; i < canvas.transform.GetChild(0).GetChild(0).childCount; i++)
            {
                if (i != 6 && i != 8 && i != 16 && i != 18)
                {
                    canvas.transform.GetChild(0).GetChild(0).GetChild(i).GetComponent<Image>().enabled = true;
                }
            }
            isIntro = false;
            Invoke(nameof(DelayShowTutorial), 1);
        });
    }



    void SetSizeCamera()
    {
        float f1 = 16.0f / 9;
        float f2 = Screen.width * 1.0f / Screen.height;

        mainCamera.orthographicSize *= f1 / f2;
    }

    public void SetUpMap(int levelIndex)
    {
        if (life == 0)
        {
            Debug.Log("Thua");
        }
        if (life != 0)
        {
            isLockStage = false;
        }
        if (levelIndex == 1 && life != 0)
        {
            level = 1;
            power = 4;
            maxPower = 4;
            percentPower = 1;
            checkStep = 0;
            isFirstMove = true;
            myTaxi.isWinLevel = false;
            customer.transform.position = listWayPoint[10].position;
            customer.SetActive(true);
            myTaxi.transform.position = listWayPoint[12].position;
            checkPoint.transform.position = listWayPoint[20].position;
            checkPoint.SetActive(true);
            clockwise.transform.DORotate(new Vector3(0, 0, -4.72f + (116.6f + 4.72f) * (1 - percentPower)), 0.5f).SetEase(Ease.Linear);

            canvas.transform.GetChild(0).GetChild(0).GetChild(6).GetComponent<Image>().color = Color.black;
            canvas.transform.GetChild(0).GetChild(0).GetChild(8).GetComponent<Image>().color = Color.black;
            canvas.transform.GetChild(0).GetChild(0).GetChild(16).GetComponent<Image>().color = Color.black;
            canvas.transform.GetChild(0).GetChild(0).GetChild(18).GetComponent<Image>().color = Color.black;
            canvas.transform.GetChild(0).GetChild(0).GetChild(6).tag = "Trash";
            canvas.transform.GetChild(0).GetChild(0).GetChild(8).tag = "Trash";
            canvas.transform.GetChild(0).GetChild(0).GetChild(16).tag = "Trash";
            canvas.transform.GetChild(0).GetChild(0).GetChild(18).tag = "Trash";
        }
        if (levelIndex == 2 && life != 0)
        {
            level = 2;
            power = 6;
            maxPower = 6;
            percentPower = 1;
            checkStep = 0;
            isFirstMove = true;
            myTaxi.isWinLevel = false;
            clockwise.transform.DORotate(new Vector3(0, 0, -4.72f + (116.6f + 4.72f) * (1 - percentPower)), 0.5f).SetEase(Ease.Linear);
            myTaxi.transform.position = listWayPoint[23].position;
            customer.transform.position = listWayPoint[12].position;
            checkPoint.transform.position = listWayPoint[0].position;
            customer.SetActive(true);
            listItem1.Add(Instantiate(item1_Prefab, listWayPoint[1].position, Quaternion.identity));

        }
        if (levelIndex == 3 && life != 0)
        {
            level = 3;
            power = 9;
            maxPower = 9;
            percentPower = 1;
            checkStep = 0;
            isFirstMove = true;
            myTaxi.isWinLevel = false;
            clockwise.transform.DORotate(new Vector3(0, 0, -4.72f + (116.6f + 4.72f) * (1 - percentPower)), 0.5f).SetEase(Ease.Linear);
            myTaxi.transform.position = listWayPoint[24].position;
            customer.transform.position = listWayPoint[0].position;
            checkPoint.transform.position = listWayPoint[12].position;
            customer.SetActive(true);
            listItem1.Add(Instantiate(item1_Prefab, listWayPoint[21].position, Quaternion.identity));
            listItem1.Add(Instantiate(item1_Prefab, listWayPoint[2].position, Quaternion.identity));
            listItem1.Add(Instantiate(item1_Prefab, listWayPoint[7].position, Quaternion.identity));
        }
        if (levelIndex == 4 && life != 0)
        {
            level = 4;
            power = 8;
            maxPower = 8;
            percentPower = 1;
            checkStep = 0;
            isFirstMove = true;
            myTaxi.isWinLevel = false;
            clockwise.transform.DORotate(new Vector3(0, 0, -4.72f + (116.6f + 4.72f) * (1 - percentPower)), 0.5f).SetEase(Ease.Linear);
            myTaxi.transform.position = listWayPoint[12].position;
            customer.transform.position = listWayPoint[1].position;
            checkPoint.transform.position = listWayPoint[15].position;
            customer.SetActive(true);
            listItem2.Add(Instantiate(item2_Prefab, listWayPoint[2].position, Quaternion.identity));
        }
        if (levelIndex == 5 && life != 0)
        {
            level = 5;
            power = 8;
            maxPower = 8;
            percentPower = 1;
            checkStep = 0;
            isFirstMove = true;
            myTaxi.isWinLevel = false;
            canvas.transform.GetChild(0).GetChild(0).GetChild(17).GetComponent<Image>().color = Color.black;
            canvas.transform.GetChild(0).GetChild(0).GetChild(17).tag = "Trash";
            clockwise.transform.DORotate(new Vector3(0, 0, -4.72f + (116.6f + 4.72f) * (1 - percentPower)), 0.5f).SetEase(Ease.Linear);
            myTaxi.transform.position = listWayPoint[24].position;
            customer.transform.position = listWayPoint[12].position;
            checkPoint.transform.position = listWayPoint[1].position;
            customer.SetActive(true);
            listItem1.Add(Instantiate(item1_Prefab, listWayPoint[11].position, Quaternion.identity));
            listItem2.Add(Instantiate(item2_Prefab, listWayPoint[19].position, Quaternion.identity));
        }
        if (levelIndex == 6 && life != 0)
        {
            level = 6;
            power = 9;
            maxPower = 9;
            percentPower = 1;
            checkStep = 0;
            isFirstMove = true;
            myTaxi.isWinLevel = false;
            canvas.transform.GetChild(0).GetChild(0).GetChild(11).GetComponent<Image>().color = Color.black;
            canvas.transform.GetChild(0).GetChild(0).GetChild(11).tag = "Trash";
            canvas.transform.GetChild(0).GetChild(0).GetChild(17).GetComponent<Image>().color = new Color(0.9150943f, 0.9150943f, 0.9150943f, 1);
            canvas.transform.GetChild(0).GetChild(0).GetChild(17).tag = "Path";
            clockwise.transform.DORotate(new Vector3(0, 0, -4.72f + (116.6f + 4.72f) * (1 - percentPower)), 0.5f).SetEase(Ease.Linear);
            myTaxi.transform.position = listWayPoint[15].position;
            customer.transform.position = listWayPoint[12].position;
            checkPoint.transform.position = listWayPoint[4].position;
            customer.SetActive(true);
            listItem1.Add(Instantiate(item1_Prefab, listWayPoint[13].position, Quaternion.identity));
            listItem2.Add(Instantiate(item2_Prefab, listWayPoint[21].position, Quaternion.identity));
        }
        if (levelIndex == 7 && life != 0)
        {
            level = 7;
            power = 8;
            maxPower = 8;
            percentPower = 1;
            checkStep = 0;
            isFirstMove = true;
            myTaxi.isWinLevel = false;
            canvas.transform.GetChild(0).GetChild(0).GetChild(11).GetComponent<Image>().color = new Color(0.9150943f, 0.9150943f, 0.9150943f, 1);
            canvas.transform.GetChild(0).GetChild(0).GetChild(11).tag = "Path";
            clockwise.transform.DORotate(new Vector3(0, 0, -4.72f + (116.6f + 4.72f) * (1 - percentPower)), 0.5f).SetEase(Ease.Linear);
            myTaxi.transform.position = listWayPoint[21].position;
            customer.transform.position = listWayPoint[9].position;
            checkPoint.transform.position = listWayPoint[1].position;
            customer.SetActive(true);
            listItem1.Add(Instantiate(item1_Prefab, listWayPoint[3].position, Quaternion.identity));
            listItem1.Add(Instantiate(item1_Prefab, listWayPoint[4].position, Quaternion.identity));
            listItem2.Add(Instantiate(item2_Prefab, listWayPoint[13].position, Quaternion.identity));
            listItem2.Add(Instantiate(item2_Prefab, listWayPoint[14].position, Quaternion.identity));
        }
    }

    public void Move()
    {
        if (isFirstMove)
        {
            isFirstMove = false;
        }
        isOnMove = true;
        Vector3[] road = new Vector3[listPathMove.Count];
        for (int i = 0; i < road.Length; i++)
        {
            road[i] = listPathMove[i].wayPoint.position;
        }
        float distance = 0;
        for (int i = 1; i < road.Length; i++)
        {
            distance += (road[i - 1] - road[i]).magnitude;
        }

        taxiMovement = myTaxi.transform.DOPath(road, distance / speed, PathType.Linear).SetEase(Ease.Linear);
        taxiMovement.OnComplete(() =>
        {
            isOnMove = false;
        });
    }

    public void TransitionLevelStart()
    {
        transition.LoadTransitionStart();

    }

    public void TransitionLevelEnd()
    {
        transition.LoadTransitionEnd();
    }

    void DelayResetLevel()
    {
        life--;
        if (listItem1.Count > 0)
        {
            for (int v = 0; v < listItem1.Count; v++)
            {
                Destroy(listItem1[v].gameObject);
            }
            listItem1.Clear();
        }
        if (listItem2.Count > 0)
        {
            for (int k = 0; k < listItem2.Count; k++)
            {
                Destroy(listItem2[k].gameObject);
            }
            listItem2.Clear();
        }
        SetUpMap(level);
    }

    void DelayKillMove()
    {
        taxiMovement.Kill();
    }

    void DelayShowTutorial()
    {
        if (tutorial != null)
        {
            tutorial.SetActive(true);
            tutorial.transform.DOMoveX(-7.16f, 2).SetLoops(-1);
        }
    }


    private void Update()
    {
        txtLevel.text = level.ToString();
        txtPower.text = power.ToString();
        txtLife.text = life.ToString();
        if (Input.GetMouseButtonDown(0) && life != 0 && !isIntro)
        {
            isHoldMouse = true;
        }

        if (Input.GetMouseButtonUp(0) && life != 0 && !isIntro)
        {
            isHoldMouse = false;
            int stepActive = 0;
            if (listPathMove.Count > 1)
            {
                for (int j = 0; j < listPathMove.Count - 1; j++)
                {
                    if (System.Math.Round(listPathMove[j].wayPoint.position.x, 3) == System.Math.Round(listPathMove[j + 1].wayPoint.position.x, 3) || System.Math.Round(listPathMove[j].wayPoint.position.y, 3) == System.Math.Round(listPathMove[j + 1].wayPoint.position.y, 3))
                    {
                        stepActive++;
                    }
                }
                if ((stepActive == listPathMove.Count - 1) && listPathMove[0].isTaxiStay && !isCannotMove)
                {
                    if (checkStep == 0)
                    {
                        if (listPathMove[listPathMove.Count - 1].isCustomerStay)
                        {
                            if (isTutorial)
                            {
                                isTutorial = false;
                                if(tutorial != null)
                                {
                                    tutorial.transform.DOKill();
                                    Destroy(tutorial);
                                }
                            }
                            //if(listPathMove[0].wayPoint.position.x > listPathMove[1].wayPoint.position.x)
                            //{
                            //    transform.localScale = new Vector2(0.5f, 0.5f);
                            //}
                            //if (listPathMove[0].wayPoint.position.x < listPathMove[1].wayPoint.position.x)
                            //{
                            //    transform.localScale = new Vector2(-0.5f, 0.5f);
                            //}
                            Move();
                            checkStep++;

                        }
                    }

                    if (checkStep == 1)
                    {
                        if (listPathMove[listPathMove.Count - 1].isCheckPoint)
                        {
                            //if (listPathMove[0].wayPoint.position.x > listPathMove[1].wayPoint.position.x)
                            //{
                            //    transform.localScale = new Vector2(0.5f, 0.5f);
                            //}
                            //if (listPathMove[0].wayPoint.position.x < listPathMove[1].wayPoint.position.x)
                            //{
                            //    transform.localScale = new Vector2(-0.5f, 0.5f);
                            //}
                            Move();
                            checkStep++;

                        }
                    }

                }
            }


            for (int j = 0; j < canvas.transform.GetChild(0).GetChild(0).childCount; j++)
            {
                if (canvas.transform.GetChild(0).GetChild(0).GetChild(j).GetComponent<SpaceLane_TaxiMinigame1>().isCheck)
                {
                    canvas.transform.GetChild(0).GetChild(0).GetChild(j).GetComponent<SpaceLane_TaxiMinigame1>().isCheck = false;
                }
            }
            isCannotMove = false;
            listPathMove.Clear();
        }

        if (isHoldMouse)
        {
            pos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            hit = Physics2D.RaycastAll(pos, Vector2.zero);
            if (hit.Length != 0)
            {
                for (int i = 0; i < hit.Length; i++)
                {
                    if (hit[i] && hit[i].collider != null)
                    {
                        if (hit[i].collider.gameObject.CompareTag("Path"))
                        {
                            if (!hit[i].collider.gameObject.GetComponent<SpaceLane_TaxiMinigame1>().isCheck)
                            {
                                listPathMove.Add((hit[i].collider.gameObject.GetComponent<SpaceLane_TaxiMinigame1>()));
                                hit[i].collider.gameObject.GetComponent<SpaceLane_TaxiMinigame1>().isCheck = true;

                                for (int j = 0; j < canvas.transform.GetChild(0).GetChild(0).childCount; j++)
                                {
                                    if (canvas.transform.GetChild(0).GetChild(0).GetChild(j).GetInstanceID() != hit[i].collider.gameObject.GetInstanceID() + 2)
                                    {
                                        if (canvas.transform.GetChild(0).GetChild(0).GetChild(j).GetComponent<SpaceLane_TaxiMinigame1>().isCheck)
                                        {
                                            canvas.transform.GetChild(0).GetChild(0).GetChild(j).GetComponent<SpaceLane_TaxiMinigame1>().isCheck = false;
                                        }
                                    }

                                }
                            }
                        }
                        if (hit[i].collider.gameObject.CompareTag("Trash"))
                        {
                            isCannotMove = true;
                        }
                    }
                }
            }
        }

        if (isOnMove)
        {
            percentPower = power * 1f / maxPower;
            Mathf.Clamp(clockwise.transform.eulerAngles.z, -4.72f, 116.6f);
            clockwise.transform.DORotate(new Vector3(0, 0, -4.72f + (116.6f + 4.72f) * (1 - percentPower)), 0.5f).SetEase(Ease.Linear);
        }

        if (power == 0)
        {
            if (!myTaxi.isWinLevel && !isLockStage)
            {
                isLockStage = true;
                Invoke(nameof(DelayKillMove), 0.5f);
                Invoke(nameof(DelayResetLevel), 1.6f);
                clockwise.GetComponent<SpriteRenderer>().DOFade(0, 0.4f).OnComplete(() =>
                {
                    clockwise.GetComponent<SpriteRenderer>().DOFade(1, 0.4f).OnComplete(() =>
                    {
                        clockwise.GetComponent<SpriteRenderer>().DOFade(0, 0.4f).OnComplete(() =>
                        {
                            clockwise.GetComponent<SpriteRenderer>().DOFade(1, 0.4f);
                        });
                    });
                });
            }
        }

    }
}







