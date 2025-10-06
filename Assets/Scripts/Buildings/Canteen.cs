using UnityEngine;

public class Canteen : Building
{
    private void Awake()
    {
         GetComponentInChildren<Renderer>().material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }
}
