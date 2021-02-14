using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataMapper : MonoBehaviour
{
    public GameObject Scrapper;
    private DataBase[] data;
    public GameObject IndiaMap;
    public GameObject infoFlag;
    public GameObject IndiaInfo;
    
    private Camera cam;
    private bool execute = false;
    private TextMesh indiaInfoText;

    private void Start()
    {
        Scrapper = GameObject.Find("Scrapper");
        indiaInfoText = IndiaInfo.GetComponent<TextMesh>();
        cam = Camera.main;
        Invoke("LoadedData", 3.0f);
    }

    private void LoadedData()
    {
        int totalCasesIndia = 0;
        int activeCasesIndia = 0;
        int curedIndia = 0;
        int diseasedIndia = 0;
        data = Scrapper.GetComponent<DataScraper>().stateList.ToArray();
        indiaInfoText.text = "Last Updated On : " + Scrapper.GetComponent<DataScraper>().lastUpdated;
        execute = true;
        

        for(int i = 0; i< data.Length; i++)
        {
            totalCasesIndia += data[i].totalCases;
            activeCasesIndia += data[i].totalCases - data[i].cured - data[i].death;
            curedIndia += data[i].cured;
            diseasedIndia += data[i].death;
        }
        indiaInfoText.text += "\nTotal Cases : " + totalCasesIndia.ToString();
        indiaInfoText.text += "\nActive Cases : " + activeCasesIndia.ToString();
        indiaInfoText.text += "\nCured/Discharged : " + curedIndia.ToString();
        indiaInfoText.text += "\nDeaths : " + diseasedIndia.ToString();


    }
    GameObject flag;
    GameObject state;
    GameObject prevState;
    GameObject[] planes;
    
    void Update()
    {
        if(flag == null)
        {
            flag = GameObject.FindGameObjectWithTag("flag");

        }
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        planes = GameObject.FindGameObjectsWithTag("plane");
        foreach(var plane in planes)
        {
            Destroy(plane);
        }


        string stateName;
        string prevStateName = "";

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && execute)
        {
            
            if (hit.transform.name.Contains("Cap"))
            {
                 stateName = hit.transform.parent.transform.parent.name;


            }
            else
            {
                stateName = hit.transform.name;
            }
            if(flag != null && prevState != null && prevState.name != stateName )
            {
                Debug.Log("Reaching here, " + prevState.name +", " + stateName);
                Destroy(flag);
                prevState.transform.position = prevState.transform.position - new Vector3(0, 0.2f * gameObject.transform.localScale.y, 0);
                foreach (var part in prevState.GetComponentsInChildren<Transform>())
                {
                    if (part.gameObject.GetComponent<Renderer>() != null)
                        part.gameObject.GetComponent<Renderer>().material.color = new Color(1, 1, 1);
                }

            }

            for (int i = 0; i < data.Length; i++)
            {
                if (data[i].name == stateName)
                {
                    Debug.Log("Here");
                    if(flag == null || flag.transform.parent.name != stateName)
                    {
                        state = GameObject.Find(stateName);
                        foreach (var part in state.GetComponentsInChildren<Transform>())
                        {
                            if(part.gameObject.GetComponent<Renderer>() != null)
                                part.gameObject.GetComponent<Renderer>().material.color = new Color(0.3f, 0.3f, 0.3f);
                        }
                        Debug.Log(state);
                        if(state.transform.parent.name == state.name)
                        {
                            state = state.transform.parent.gameObject;
                        }
                        GameObject info = Instantiate(infoFlag, state.transform);
                        info.transform.SetParent(state.transform);
                        state.transform.position = state.transform.position + new Vector3(0, 0.2f * gameObject.transform.localScale.y, 0);
                        Transform[] items = info.GetComponentsInChildren<Transform>();
                        items[2].gameObject.GetComponent<TextMesh>().text = data[i].name;
                        items[3].gameObject.GetComponent<TextMesh>().text = "Confirmed Cases - " + (data[i].totalCases + data[i].cured + data[i].death).ToString();
                        items[4].gameObject.GetComponent<TextMesh>().text = "Active Cases - " + data[i].totalCases.ToString();
                        items[5].gameObject.GetComponent<TextMesh>().text = "Recovered - " + data[i].cured.ToString();
                        items[6].gameObject.GetComponent<TextMesh>().text = "Deceased - " + data[i].death.ToString();
                        prevState = state;
                    }
                    prevStateName = stateName;


                }

            }
        }
            //print("I'm looking at nothing!");
    }
}
