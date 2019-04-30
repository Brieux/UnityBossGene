 using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class background : MonoBehaviour
{
    public Camera mainCamera;
    public float scroll_speed = 0.1f;
    private MeshRenderer mesh_renderer;

    private void Start()
    {
        mesh_renderer = GetComponent<MeshRenderer>();

    }

    private float camerax;
    private float cameray;


    // Update is called once per frame
    void LateUpdate()
    {
        //float x = scroll_speed * PlayerHead.HMove / 10;
        camerax = mainCamera.transform.position.x;
        cameray = mainCamera.transform.position.y;
        transform.position = new Vector3(camerax, transform.position.y, transform.position.z);
        Vector2 offset = new Vector2(camerax/50, 0);
        mesh_renderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }
}
