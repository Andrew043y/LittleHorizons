using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public int numWood, numFood, numStone;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        numWood=0;
        numFood=0;
        numStone=0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addFood(int num){
        numFood+=num;
    }
    public void addWood(int num){
        numWood+=num;
    }
    public void addStone(int num){
        numStone+=num;
    }
    public void removeFood(int num){
        numFood-=num;
    }
    public void removeWood(int num){
        numWood-=num;
    }
    public void removeStone(int num){
        numStone-=num;
    }
    public int getNumFood(){
        return numFood;
    }
    public int getNumWood(){
        return numWood;
    }
    public int getNumStone(){
        return numStone;
    }
}
