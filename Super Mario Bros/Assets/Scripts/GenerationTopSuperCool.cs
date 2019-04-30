using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationTopSuperCool : MonoBehaviour
{
    public List<GameObject> ListePrefab;
    public List<Vector3> TailleListePrefab;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 Cursor;
        Cursor.x = 0f;
        Cursor.y = 0f;
        Cursor.z = 0f;
        //RANDOM

        for(int i = 0; i < 10; i++)
        {
            float choix = Random.value * ListePrefab.Count;
            int newChoix = (int)choix;
            if(newChoix == ListePrefab.Count)
            {
                newChoix = 2;
            }


            Debug.Log(newChoix);
            ListePrefab[newChoix].GetComponent<Transform>().localPosition = Cursor;
            Instantiate(ListePrefab[newChoix]);
            Cursor.x += TailleListePrefab[newChoix].x;

            //ListePrefab[1].GetComponent<Transform>().localPosition = Cursor;
            //Instantiate(ListePrefab[1]);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
