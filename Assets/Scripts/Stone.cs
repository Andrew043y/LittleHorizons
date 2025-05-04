using UnityEngine;

public class Stone : Gatherable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gatherablesLeft=15;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
}
