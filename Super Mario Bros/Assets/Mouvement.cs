using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouvement : MonoBehaviour
{
    public bool Tampon; //Boolean pour savoir si oui ou non la platforme va avoir un tampon pour taper le boss
    public float minBoss; //Minimum requis pour etre suceptible a avoir un tampon
    public int randomMax; //Valeur pour le pourcentage de chance d'avoir un tampon
    public float TampValide; //Valeur minimal pour laquelle un tampon apparait
    public float valeurRand; //Valeur du test de tampon
    public float minX;//Position en X minimal pour le deplacement et la destruction
    public float Speed;//beh vitesse de deplacement
    public List<GameObject> ListGen;//Liste des platesformes existantes

    // Start is called before the first frame update
    void Start()
    {
        if (transform.localPosition.y > minBoss) //suceptible d'avoir un tampon ? 
        {
            float random = Random.value * randomMax;
            valeurRand=random;//affichage dans unity
            if (random >TampValide) //test d'affectation
            {
                Tampon=true;
            }
        }
        GameObject Generateur = GameObject.Find("Generateur");
        ListGen = Generateur.GetComponent<Generateur_Platforme>().FilePlatform;//reprise de la liste existante
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left*Speed*Time.deltaTime);//deplacement
        if (transform.localPosition.x<minX) //test de positionnement
        {
            Destroy(this.gameObject);
            ListGen.RemoveAt(0);//Quand la plateformes disparait c'est qu'elle est premiere dans la liste c'est donc pour la mettre a jour en tant que queue
        }
    }
}
