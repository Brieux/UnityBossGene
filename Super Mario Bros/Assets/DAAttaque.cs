using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DAAttaque : MonoBehaviour
{
    public bool activ; //Boolean d'activation
    public float TimeToSetActiv, TimeToDestroy; //timer d'activation pour pas tuer direct le joueur et durée de vie
    // Start is called before the first frame update
    void Start()
    {
        activ=false; //Par defaut
    }

    // Update is called once per frame
    void Update()
    {
        if (TimeToSetActiv <0) //lorsque le timer d'activation est a 0
        {
            activ=true; //activation de la mort
        }
        else
        {
            TimeToSetActiv-=Time.deltaTime; //diminution du timer d'activation
        }
        if (activ) //si zone active alors début de la durée de vie
        {
            if (TimeToDestroy<0)
            {
                Destroy(this.gameObject);
            }
            else
            {
                TimeToDestroy-=Time.deltaTime;
            }
        }
    }
}
