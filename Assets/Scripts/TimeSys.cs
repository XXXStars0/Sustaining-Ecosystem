using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeSys : MonoBehaviour
{
    public int day = 0;
    public TextMeshPro dayNum;
    public TextMeshPro clock;
    public Image screenColor;
    public float minLength = 1f;
    float counter;
    public int hr;
    public int min;
    public string type="night";
    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        if (counter>=minLength)
        {
            counter = 0;
            min ++;
        }
        if (min>=60)
        {
            min = 0;
            hr ++;
        }
        if (hr >= 24)
        {
            hr = 0;
            day++;
        }
        dayNum.text = "Day: "+ day;
        string hourPresent=hr.ToString("D2");
        string minPresent = (min/10*10).ToString("D2");
        if (hr>=5 && hr<=7)
        {
            type = "dawn";
        }
        else if (hr >= 17 && hr<=19)
        {
            type = "dusk";
        }else if(hr> 7 && hr < 17)
        {
            type = "day";
        }
        else
        {
            type ="night";
        }
        clock.text = hourPresent + " : " + minPresent;

        screenColor.color = new Color(getR()/255,getG()/255,getB()/255,getA()/255);
    }
    //255 157 0 67

    float getR()
    {
        if (type.Equals ("dawn") || type.Equals("dusk")) { return 255; }
        else if (type.Equals("night")) { return 64; }
        else return 255;
    }

    float getG()
    {
        if (type.Equals("dawn") || type.Equals("dusk")) { return 157; }
        else if (type.Equals("night")) { return 62; }
        else return 255;
    }

    float getB()
    {
        if (type.Equals("dawn") || type.Equals("dusk")) { return 0; }
        else if (type.Equals("night")) { return 255; }
        else return 255;
    }

    float getA()
    {
        if (type.Equals("dawn") || type.Equals("dusk")) { return 75; }
        else if (type.Equals("night")) { return 60; }
        return 0;
    }
}
