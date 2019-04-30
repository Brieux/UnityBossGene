using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingAttack : MonoBehaviour
{
    public float minY; //Bas de la map
    public float Speed; //Vitesse de chute
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down*Speed*Time.deltaTime); //Chute du prefab
        if (transform.localPosition.y<minY)
        {
            Destroy(this.gameObject); //Destruction quand prefab arrivé tout en bas de la map
        }
    }
}
