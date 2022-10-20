using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Organism : MonoBehaviour
{
    [Header("Settings")]
    public int maxHealth;
    public int intiSize;
    public int intiNutrition;
    public int maxNutrition;
    public float lifeSpan;

    [Header("Current Parameters")]
    public int birthDay;
    public int birthHour;
    public int currSize;
    public int health;
    public int nutrition;
    public int age_hours;

    public bool aging=false;
    // Start is called before the first frame update
    void Start()
    {
        age_hours = 0;
        currSize = intiSize;
        health = maxHealth;
        aging = false;
        birthHour = GameObject.Find("EventSystem").GetComponent<TimeSys>().hr;
        birthDay = GameObject.Find("EventSystem").GetComponent<TimeSys>().day;
    }

    // Update is called once per frame
    void Update()
    {
        nutrition = intiNutrition + (int)(((float)maxNutrition - (float)intiNutrition) * ((float)currSize / 100f));
        //grow
        this.gameObject.transform.localScale=new Vector3(currSize / 50f, currSize / 50f, 1);
        //age to die or 
        int hr = GameObject.Find("EventSystem").GetComponent<TimeSys>().hr;
        int day = GameObject.Find("EventSystem").GetComponent<TimeSys>().day;
        age_hours = (day-birthDay) * 24 + hr;
        if ((day>=(birthDay+lifeSpan)&&hr>=birthHour)||health<=0)
        {
            aging = true;
        }
    }
}
