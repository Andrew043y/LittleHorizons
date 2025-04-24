using UnityEngine;

public class Food : Gatherable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gatherablesLeft=10;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
}
