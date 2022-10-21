using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : MonoBehaviour
{
    public float speed = 0.1f;
    public GameObject egg;
    AudioSource die;
    AudioSource eat;
    int moveTimer;
    int maxTimer;
    float X_move;
    float Y_move;
    // Start is called before the first frame update
    void Start()
    {
        X_move = Random.Range(-2, 3);
        Y_move = Random.Range(-2, 3);
        moveTimer = 0;
        die = GameObject.Find("SE_Die").GetComponent<AudioSource>();
        eat = GameObject.Find("SE_Eat").GetComponent<AudioSource>();
        maxTimer= Random.Range(120,360);
    }

    // Update is called once per frame
    void Update()
    {
        //gittering process
        moveTimer++;
        if (moveTimer == maxTimer)
        {
            maxTimer = Random.Range(60, 300);
            X_move = Random.Range(-2, 3);
            Y_move = Random.Range(-2, 3);
            //lay egg
            int Ran = Random.Range(0,3);
            if (this.GetComponent<Organism>().currSize>=80 && Ran<1)
            {
                Instantiate(egg, this.transform.position, Quaternion.identity);
            }
            moveTimer = 0;
        }
        float x = this.transform.position.x;
        float y = this.transform.position.y;
        if (x > 14)
        {
            X_move = -1;
        }
        else if (x < -14)
        {
            X_move = 1;
        }
        if (y > 10)
        {
            Y_move = -1;
        }
        else if (y < -10)
        {
            Y_move = 1;
        }
        this.transform.position += new Vector3(X_move * speed, Y_move * speed, 0);

        //die process
        if (this.GetComponent<Organism>().aging)
        {
            die.Play();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D c)
    {
        //Bugs Eats Fruit
        if (c.gameObject.CompareTag("Fruit"))
        {
            eat.Play();
            int n = c.gameObject.GetComponent<Organism>().nutrition;
            int max = c.gameObject.GetComponent<Organism>().maxNutrition;
            if (this.GetComponent<Organism>().currSize < 100)
            {
                this.GetComponent<Organism>().currSize += n;
             }
            if (this.GetComponent<Organism>().health+n<= this.GetComponent<Organism>().maxHealth)
            {
                this.GetComponent<Organism>().health += n;
            }
            else
            {
                this.GetComponent<Organism>().health = this.GetComponent<Organism>().maxHealth;
               
            }
            Destroy(c.gameObject);

        }
    }
}
