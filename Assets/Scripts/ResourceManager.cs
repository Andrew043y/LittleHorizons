using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
    public int numWood, numFood, numStone;
    public TMP_Text foodText, woodText, stoneText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        numWood=0;
        numFood=0;
        numStone=0;
        foodText.text = "Food: 0";
        woodText.text = "Wood: 0";
        stoneText.text = "Stone: 0";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addFood(int num){
        numFood+=num;
        foodText.text = "Food: " + numFood;
    }
    public void addWood(int num){
        numWood+=num;
        woodText.text = "Wood: " + numWood;
    }
    public void addStone(int num){
        numStone+=num;
        stoneText.text = "Stone: " + numStone;
    }
    public void removeFood(int num){
        numFood-=num;
        foodText.text = "Food: " + numFood;
    }
    public void removeWood(int num){
        numWood-=num;
        woodText.text = "Wood: " + numWood;
    }
    public void removeStone(int num){
        numStone-=num;
        stoneText.text = "Stone: " + numStone;
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
