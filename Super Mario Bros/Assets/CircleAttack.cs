using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleAttack : MonoBehaviour
{
    public Vector3 ResizeVect = new Vector3(0,0,0); //Creation du vecteur de rapetissage (mot pas francais)
    public float TimeForRescale, StartTimeForRescale, TimeForStay; //Mise en place des qui gere la vitesse de rapettisage (augmenter le start pour ralentir) et durée de vie du cercle apres atteint la taille min
    public int tailleMin; //taille minimal du cercle à la fin
    // Start is called before the first frame update
    void Start()
    {
        ResizeVect = this.GetComponent<Transform>().localScale;   //Vecteur qui va prendre la taille du cercle     
    }

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<Transform>().localScale.x >tailleMin){
            if (TimeForRescale < 0){      
            {
                ResizeVect.x-=1; //diminution de la taille
                ResizeVect.y-=1;
                this.GetComponent<Transform>().localScale=ResizeVect; //affectation de la nouvelle taille
            }
            TimeForRescale = StartTimeForRescale; //debut du timer
            }
            else {
                TimeForRescale -= Time.deltaTime; //decrementation du timer 
            }
        }
        else {
            if (TimeForStay < 0){ //debut durée de vie
                Destroy(this.gameObject);
            }
            else{
                TimeForStay -= Time.deltaTime;    
            }
        }
    }
}
