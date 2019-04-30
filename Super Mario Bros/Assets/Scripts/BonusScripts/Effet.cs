using UnityEngine;

public class Effet : MonoBehaviour
{
    public bool enable;
    public float timer;
    public float timerLeft;
    public string nameEffet;
    public Coroutine myCoroutine;

    public delegate void DoEffet();
    DoEffet setter;
    DoEffet reset;

    public Effet(string name,float t, DoEffet setter, DoEffet reset)
    {
        enable = true;
        nameEffet = name;
        timer = t;
        timerLeft = t;

        this.setter = setter;
        this.reset = reset;
    }

    public void StartEffet()
    {
        setter();
    }

    public void UpdateEffet()
    {
        if(timer > 0)
            timerLeft -= Time.deltaTime;
    }

    public void StopEffet()
    {
        reset();
    }
}
