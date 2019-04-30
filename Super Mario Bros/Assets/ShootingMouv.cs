using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingMouv : MonoBehaviour
{
    public GameObject Shooter,Target; //GO entre origine et cible du projectile
    public Vector3 Road; //Vecteur servant a la trajectoire du projectile
    public float SpeedF;//vitesse du projectile

    // Start is called before the first frame update
    void Start()
    {
        Shooter=GameObject.Find("Shooting(Clone)");
        Target=GameObject.Find("Target(Clone)");
        Road.x=Target.GetComponent<Transform>().localPosition.x-Shooter.GetComponent<Transform>().localPosition.x; //Trajectoire en largeur
        Road.y=Target.GetComponent<Transform>().localPosition.y-Shooter.GetComponent<Transform>().localPosition.y; //Trajectoire en hauteur
        
    } 

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Transform>().Translate(Road*(SpeedF*Time.deltaTime)); //Depalcement en fonction de la trajectoire calculé et vitesse 
        if (this.GetComponent<Transform>().localPosition.x<=Target.GetComponent<Transform>().localPosition.x) //test d'arrivé et de collision avec la cible
        {
            /*Destruction des objets de l'attaque*/
            Destroy(Shooter);
            Destroy(Target);
            Destroy(this.gameObject);
        }
    }
}
