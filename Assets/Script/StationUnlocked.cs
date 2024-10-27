using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Unity.VisualScripting;

public class StationUnlocked : MonoBehaviour
{   
    [SerializeField] private GameObject AnnoucementCanvas;

    [SerializeField] private TextMeshProUGUI AnnoucementText;

    [SerializeField] private string stationName;
    private bool unlocked = false;



    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !unlocked){
            StartCoroutine(Annoucement());
        }

    }

    private IEnumerator Annoucement()
    {   
        AnnoucementCanvas.SetActive(true);
        AnnoucementText.text = stationName + " Station Unlocked!";
        yield return new WaitForSeconds(3f);
        AnnoucementCanvas.SetActive(false);

    }

}
