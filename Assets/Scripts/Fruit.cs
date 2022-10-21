using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    AudioSource die;
    public float maxGrowTime;
    int inti;
    // Fruits Don't move. They grow
    void Start()
    {
        inti = this.GetComponent<Organism>().intiSize;
        die = GameObject.Find("SE_Die").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        int size = this.GetComponent<Organism>().currSize;
        int age = this.GetComponent<Organism>().age_hours;
        if (size < 100)
        {
            this.GetComponent<Organism>().currSize =  inti + (int)((100 - inti) * ((float)age / (float)maxGrowTime));
        }
        if (this.GetComponent<Organism>().aging)
        {
            die.Play();
            Destroy(gameObject);
        }

    }
}
