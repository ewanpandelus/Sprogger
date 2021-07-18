using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class CheckpointReset : MonoBehaviour
{
    private Controller playerController;
    private GameObject[] checkPoints;
    private GameObject[] sortedCheckPoints = new GameObject[6];
    private int currentCheckPoint = 0;
    private List<GameObject> possibleGameObjectsToReset = new List<GameObject>();
    private List<GameObject> gameObjectsToReset =  new List<GameObject>();
    private SmoothMouseLook smoothMouseLook;
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<Controller>();
        checkPoints = GameObject.FindGameObjectsWithTag("Checkpoint");
        smoothMouseLook = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SmoothMouseLook>();
        SortCheckPoints();
    }
    public void CalculateCheckPointGameObjects()
    {
        for (int i = 0; i < sortedCheckPoints[currentCheckPoint].transform.childCount; i++)
        {
             possibleGameObjectsToReset.Add(sortedCheckPoints[currentCheckPoint].transform.GetChild(i).gameObject);
        }
    }
  
    private void SortCheckPoints()
    {

        int[] checkPointNumbers = new int[checkPoints.Length];
        int counter = 0;
        foreach (GameObject checkPoint in checkPoints)
        {
            int checkPointNumber = int.Parse(Regex.Match(checkPoint.name, @"\d+").Value);
            checkPointNumbers[counter] = checkPointNumber;
            counter++;
        }
        Array.Sort(checkPointNumbers);
        counter = 0;
        foreach (int number in checkPointNumbers)
        {
            sortedCheckPoints[counter] = (GameObject.Find("Checkpoint"+number.ToString()));
            counter++;
        }
    }
    public void AddGameObjectsToReset(GameObject gameObj)
    {
        gameObjectsToReset.Add(gameObj);
    }

    public void ResetScene()
    {
        possibleGameObjectsToReset.Clear();
        CalculateCheckPointGameObjects();
        ActivateLilyPads();
        ResetPlayer();
        Ending.Instance.ResetEndLilyPads();
    }
    private void ResetPlayer()
    {
        playerController.transform.position = sortedCheckPoints[currentCheckPoint].transform.position;
        playerController.GetRigidbody().velocity = new Vector3(0f, 0f, 0f);
        playerController.GetRigidbody().useGravity = true;
    }
    public void ClearGameObjectsToReset()
    {
        gameObjectsToReset.Clear();
    }
    private void ActivateLilyPads()
    {
        foreach (GameObject obj in gameObjectsToReset)
        {
            if (possibleGameObjectsToReset.Contains(obj))
            {
                obj.SetActive(true);
            }
        }
    }
   
    public int GetCurrentCheckPoint()
    {
        return this.currentCheckPoint;
    }
    public void SetCurrentCheckPoint(int _currentCheckPoint)
    {
        this.currentCheckPoint = _currentCheckPoint;
    }
}
