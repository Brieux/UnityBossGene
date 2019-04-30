using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generateur_Platforme : MonoBehaviour
{
    public GameObject Sol; //GO de sol a generer
    public int nb = 0; //nb actuel de platform générée depuis le debut
    public int nbMax;//nb max de platform tout au long du script
    public float TimeBtwGenerate;
    public float StartTimeBtwGenerate;//delais de spawn des platform
    public float xStart/*Position de départ de la platform*/,yMax/*Hauteur max de génération de la platform*/;
    public float Diff; //Difference de generation pour la zone du milieu
    public List<GameObject> FilePlatform;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
            if (TimeBtwGenerate<=0)
            {
                if (nb<nbMax) //tant que il y a pas le nombre max de plateformes en tout alors on en crée
                {
                    float choix;
                    bool Milieu = GenerationMilieu(); //Test pour augmenter le pourcentage en centre de map
                    if (Milieu == false){
                        choix = Random.value * yMax; //Si test negatif alors toute la zone peut etre occupée
                    }
                    else {
                        choix = Diff + (Random.value * (yMax - Diff)); //Si test positif alors on restraint la zone
                    }
                    int choixCast = (int)choix; //Cast pour pas que les platformes se generent l'une dans l'autre
                    Vector3 PosGenerate = new Vector3(xStart,choixCast,0f);
                    Sol.GetComponent<Transform>().localPosition=PosGenerate; //affectation de la position au prefab qui va etre crée
                    GameObject SolInstant = Instantiate(Sol); //Instantiation : scirpt de deplacement dans le prefab
                    FilePlatform.Add(SolInstant);//ajout de la platforme dans la liste actuelle des platesformes existantes
                    nb+=1;
                    TimeBtwGenerate=StartTimeBtwGenerate; //Delais de generation
                }
            }
            else
            {
                //print("Recharge");
                TimeBtwGenerate-=Time.deltaTime;//Diminution du delais 
            }

        }
    public bool GenerationMilieu(){
        if (Random.value < 0.75f){ //75% de chance que la nouvelle platformes soient dans la zone du milieu
            return true;
        }               
        else {
            return false;
        }
    }
}
