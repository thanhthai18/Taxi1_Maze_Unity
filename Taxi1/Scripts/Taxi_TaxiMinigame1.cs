using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taxi_TaxiMinigame1 : MonoBehaviour
{
    public Vector2 prePos, currentPos;
    public Coroutine posCoroutine;
    public bool isPlayingCoroutine = false;
    public int levelIndex;
    public bool isWinLevel;


    private void Start()
    {
        levelIndex = 1;
        currentPos = transform.position;
        prePos = currentPos;
        posCoroutine = StartCoroutine(UpdatePos());

    }

    IEnumerator UpdatePos()
    {
        while (!isPlayingCoroutine)
        {
            if (GameController_TaxiMinigame1.instance.isOnMove)
            {
                currentPos = transform.position;
                yield return new WaitForSeconds(0.1f);
                prePos = currentPos;
                yield return new WaitForSeconds(0.01f);
                currentPos = transform.position;
            }
            else
                yield return new WaitForSeconds(0.1f);
        }
    }

    private void Update()
    {
        if (GameController_TaxiMinigame1.instance.isOnMove)
        {
            if (prePos.x != 0)
            {
                if (currentPos.x > prePos.x)
                {
                    transform.localScale = new Vector2(-0.5f, 0.5f);
                }
                if (currentPos.x < prePos.x)
                {
                    transform.localScale = new Vector2(0.5f, 0.5f);
                }
                if (currentPos.x == prePos.x)
                {
                    return;
                }
            }

        }
    }

    void DelayTransitionEnd()
    {
        GameController_TaxiMinigame1.instance.TransitionLevelEnd();
    }

    void DelayNextLevel()
    {
        if (levelIndex == 7)
        {
            Debug.Log("Win");
        }
        if (levelIndex <= 6)
        {
            levelIndex++;
            GameController_TaxiMinigame1.instance.taxiMovement.Kill();

            GameController_TaxiMinigame1.instance.txtLevel.transform.DOPunchScale(new Vector3(1.3f, 1.3f, 1.3f), 1);
            if (GameController_TaxiMinigame1.instance.listItem1.Count > 0)
            {
                for (int v = 0; v < GameController_TaxiMinigame1.instance.listItem1.Count; v++)
                {
                    Destroy(GameController_TaxiMinigame1.instance.listItem1[v].gameObject);
                }
                GameController_TaxiMinigame1.instance.listItem1.Clear();
            }
            if (GameController_TaxiMinigame1.instance.listItem2.Count > 0)
            {
                for (int k = 0; k < GameController_TaxiMinigame1.instance.listItem2.Count; k++)
                {
                    Destroy(GameController_TaxiMinigame1.instance.listItem2[k].gameObject);
                }
                GameController_TaxiMinigame1.instance.listItem2.Clear();
            }

            GameController_TaxiMinigame1.instance.SetUpMap(levelIndex);
            currentPos = transform.position;
            prePos = currentPos;

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!GameController_TaxiMinigame1.instance.isFirstMove)
        {
            if (collision.gameObject.CompareTag("Finish") && GameController_TaxiMinigame1.instance.checkStep == 2)
            {
                isWinLevel = true;
                if(levelIndex <= 6)
                {
                    GameController_TaxiMinigame1.instance.TransitionLevelStart();
                    Invoke(nameof(DelayTransitionEnd), 0.5f);
                }
                
                Invoke(nameof(DelayNextLevel), 0.5f);
            }
            if (collision.gameObject.CompareTag("Balloon"))
            {
                GameController_TaxiMinigame1.instance.power++;
                collision.gameObject.GetComponent<SpriteRenderer>().DOFade(0, 0.3f).OnComplete(() =>
                {
                    collision.gameObject.GetComponent<SpriteRenderer>().DOFade(1, 0.3f);
                });
            }
            if (collision.gameObject.CompareTag("Thief"))
            {
                GameController_TaxiMinigame1.instance.power--;
                collision.gameObject.GetComponent<SpriteRenderer>().DOFade(0, 0.3f).OnComplete(() =>
                {
                    collision.gameObject.GetComponent<SpriteRenderer>().DOFade(1, 0.3f);
                });
            }
            if (collision.gameObject.CompareTag("Path"))
            {
                if (GameController_TaxiMinigame1.instance.power > 0)
                {
                    GameController_TaxiMinigame1.instance.power--;
                }
            }
            if (collision.gameObject.CompareTag("People") && GameController_TaxiMinigame1.instance.checkStep == 1)
            {
                Debug.Log("Don khach");
                GameController_TaxiMinigame1.instance.customer.SetActive(false);
                for (int i = 0; i < GameController_TaxiMinigame1.instance.canvas.transform.GetChild(0).GetChild(0).childCount; i++)
                {
                    if (GameController_TaxiMinigame1.instance.canvas.transform.GetChild(0).GetChild(0).GetChild(i).GetComponent<SpaceLane_TaxiMinigame1>().isCustomerStay)
                    {
                        GameController_TaxiMinigame1.instance.canvas.transform.GetChild(0).GetChild(0).GetChild(i).GetComponent<SpaceLane_TaxiMinigame1>().isCustomerStay = false;

                    }
                }
            }
        }
    }
}
