using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird_Baby : MonoBehaviour
{
    public int herbCount = 0;
    public int carCount = 0;
    public bool isSleep = false;
    public GameObject herbEvo;
    public GameObject carEvo;
    public float speedRange;
    float X_speed;
    float Y_speed;
    Animator a;
    AudioSource evo;
    AudioSource eat;
    AudioSource die;
    TimeSys timer;
    int changeCount;
    // Start is called before the first frame update
    void Start()
    {
        changeCount = 0;
        X_speed = Random.Range(-speedRange, speedRange);
        Y_speed = Random.Range(-speedRange, speedRange);
        isSleep = false;
        eat = GameObject.Find("SE_Eat").GetComponent<AudioSource>();
        evo = GameObject.Find("SE_Evo").GetComponent<AudioSource>();
        die = GameObject.Find("SE_Die").GetComponent<AudioSource>();
        timer = GameObject.Find("EventSystem").GetComponent<TimeSys>();
        a = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!isSleep)
        {
            changeCount++;
            if (changeCount > 60)
            {
                X_speed = Random.Range(-speedRange, speedRange);
                Y_speed = Random.Range(-speedRange, speedRange);
                changeCount = 0;
            }
            if (this.transform.position.x > 14 || this.transform.position.x < -14)
            {
                X_speed = -X_speed;
            }

            if (this.transform.position.y > 10 || this.transform.position.y < -10)
            {
                Y_speed = -Y_speed;
            }
            this.transform.position += new Vector3(X_speed, Y_speed);
        }


        if (timer.type.Equals("day"))
        {
            isSleep = false;
        }
        else
        {
            isSleep = true;
        }
        a.SetBool("isSleep",isSleep);
        //evolution
        if (this.GetComponent<Organism>().aging)
        {
            if (this.GetComponent<Organism>().health == 0)
            {
                die.Play();
                Destroy(gameObject);
            }

            if(herbEvo && herbCount >= carCount)
            {
                Instantiate(herbEvo, this.transform.position, Quaternion.identity);
            }else if (carEvo)
            {
                Instantiate(carEvo, this.transform.position, Quaternion.identity);
            }
            evo.Play();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D c)
    {
        if (!isSleep) {
            //Eats Fruit
            if (c.gameObject.CompareTag("Fruit"))
            {
                eat.Play();
                herbCount++;
                int n = c.gameObject.GetComponent<Organism>().nutrition;
                int max = c.gameObject.GetComponent<Organism>().maxNutrition;
                if (this.GetComponent<Organism>().currSize < 100)
                {
                    this.GetComponent<Organism>().currSize += n;
                }
                if (this.GetComponent<Organism>().nutrition + n <= this.GetComponent<Organism>().maxNutrition)
                {
                    this.GetComponent<Organism>().health += n;
                }
                else
                {
                    this.GetComponent<Organism>().nutrition = this.GetComponent<Organism>().maxNutrition;

                }
                Destroy(c.gameObject);

            }
            //Eats Bug Eggs or small bugs
            if (c.gameObject.CompareTag("BugEgg")||(c.gameObject.CompareTag("Bug")&&( (c.gameObject.GetComponent<Organism>().currSize<this.GetComponent<Organism>().currSize)||(c.gameObject.GetComponent<Organism>().currSize <65))))
            {
                eat.Play();
                carCount++;
                int n = c.gameObject.GetComponent<Organism>().nutrition;
                int max = c.gameObject.GetComponent<Organism>().maxNutrition;
                if (this.GetComponent<Organism>().currSize < 100)
                {
                    this.GetComponent<Organism>().currSize += n;
                }
                if (this.GetComponent<Organism>().health + n <= this.GetComponent<Organism>().maxHealth)
                {
                    this.GetComponent<Organism>().health += n;
                }
                else
                {
                    this.GetComponent<Organism>().health = this.GetComponent<Organism>().maxHealth;

                }
                Destroy(c.gameObject);

            }
            //Bounce Back
            if (c.gameObject.CompareTag("Bird"))
            {

                X_speed = -X_speed;
                Y_speed = -Y_speed;
            }
        }
    }
}
