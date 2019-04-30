using System.Collections;
using UnityEngine;

public class BonusBox : MonoBehaviour
{
    private bool isUsed = false;
    private Collider cld;
    public GameObject[] itemPrefabs;
    public Material disabledMaterial;

    private void Awake()
    {
        cld = GetComponent<Collider>();
    }
    private void Update()
    {

        if (isUsed && gameObject.tag != "multicoin")
        {
            gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material = disabledMaterial;
        }
    }

    private void OnCollisionEnter2D(Collision2D c)
    {
        if (c.collider.GetComponent<PlayerHead>() && c.contacts[0].normal.y > 0.5f && isUsed == false)
        {
            isUsed = true;
            GetComponent<AudioSource>().Play();

            GameObject g = itemPrefabs[Random.Range(0, itemPrefabs.Length)];

            Vector3 v = new Vector3();
            v.x = transform.position.x;

            if (g.name.Contains("Sphere"))
            {
                v.y = transform.position.y + 1;
                GameObject go = Instantiate(g, v, transform.rotation);
            }
            else if(g.name == "CoinBox")
            {
                v.y = transform.position.y;
                GameObject go = Instantiate(g, v, transform.rotation);
                StartCoroutine(BackstageDestroy(go));
            }
        }
    }

    IEnumerator BackstageDestroy(GameObject g)
    {
        yield return new WaitForSeconds(1);
        Destroy(g);
    }

}
