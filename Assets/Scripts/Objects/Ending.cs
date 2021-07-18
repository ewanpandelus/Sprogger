using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    private Dictionary<GameObject, bool> endLilys = new Dictionary<GameObject, bool>();
    private GameObject[] endPads = new GameObject[9];
    private Planet planet;
    private CheckpointReset checkPointReset;
    private static Ending instance;
    public static Ending Instance { get { return instance; } }
    private Controller playerController;

   
    private void Start()
    {
        checkPointReset = GameObject.Find("CheckpointReset").GetComponent<CheckpointReset>();
        if (instance!=null && instance!=this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        planet = GameObject.Find("planetFrog").GetComponent<Planet>();
        endPads = GameObject.FindGameObjectsWithTag("EndPad");
        if (endPads!=null)
        {
            ResetEndLilyPads();
        }
    }
    private void ResetEndLilyPadsBool()
    {
        foreach(GameObject endPad in endPads)
        {
            endLilys[endPad.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject] = false;
        }
    }
    public void ResetEndLilyPads()
    {
        foreach(KeyValuePair<GameObject,bool> lilyPad in endLilys)
        {
            lilyPad.Key.transform.parent.transform.parent.gameObject.SetActive(true);
        }
        ResetEndLilyPadsBool();
    }
    public void UpdateEndPad(GameObject endPad)
    {
        endLilys[endPad] = true;
        CheckIfComplete();
    }
    private void CheckIfComplete()
    {
        bool check = true;
        foreach(KeyValuePair<GameObject,bool> lily in endLilys)
        {
            check = check && lily.Value;
        }
        if(check == true)
        {
            FinishGame();
        }
    }
    private void FinishGame()
    {
        if (planet)
        {
            if (playerController)
            {
                playerController.GetRigidbody().useGravity = false;
            }
            planet.SetShouldPull(true);
        }
  
    }
}