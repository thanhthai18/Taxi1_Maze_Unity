using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceLane_TaxiMinigame1 : MonoBehaviour
{
    public Transform wayPoint;
    public bool isTaxiStay = false;
    public bool isCheck = false;
    public bool isCustomerStay = false;
    public bool isCheckPoint = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isTaxiStay = true;           
        }
        if (collision.gameObject.CompareTag("People"))
        {
            isCustomerStay = true;
        }
        if (collision.gameObject.CompareTag("Finish"))
        {
            isCheckPoint = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isTaxiStay = false;
        }
        if (collision.gameObject.CompareTag("People"))
        {
            isCustomerStay = false;
        }
        if (collision.gameObject.CompareTag("Finish"))
        {
            isCheckPoint = false;
        }
    }



}
