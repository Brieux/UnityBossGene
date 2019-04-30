using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    private GameObject player;
    public Transform cornerMax;
    public Transform cornerMin;
    public Camera  Camera;
    private float xMin;
    private float yMin;
    private float xMax;
    private float yMax;
    
    // Start is called before the first frame update
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");

        float margin_vertical = Camera.orthographicSize;
        float margin_horizontal = Camera.aspect * margin_vertical;

        yMin = cornerMin.transform.position.y+ margin_vertical;
        xMin = cornerMin.transform.position.x+ margin_horizontal;

        yMax = cornerMax.transform.position.y- margin_vertical;
        xMax = cornerMax.transform.position.x- margin_horizontal;

        GetComponent<AudioSource>().Play();
        
    }

    private void Update()
    {
        if (HeadGame.Pause)
        {
            Debug.Log("Pause");
            gameObject.GetComponent<AudioSource>().Pause();
        }
        else
        {
            if (!gameObject.GetComponent<AudioSource>().isPlaying)
            {
                gameObject.GetComponent<AudioSource>().Play();
            }
                
        }
    }

    void LateUpdate() {
        float x = Mathf.Clamp(player.transform.position.x, xMin, xMax);
        float y = Mathf.Clamp(player.transform.position.y, yMin, yMax);
        gameObject.transform.position = new Vector3(x, y, gameObject.transform.position.z);
    }
}
