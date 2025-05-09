using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

/*
            <3
*/
public class TerrainGenerator : MonoBehaviour 
{
    // x (-268, 268)     z (-268, 268)  coords of the floor
    public GameObject tree;
    public GameObject berryBush;
    public GameObject UIHandler;
    public GameObject[] stones;
    public NavMeshSurface floorNavMesh;

    float treeXVal, treeZVal;
    float berryXVal, berryZVal;
    float stoneXVal, stoneZVal;
    float randXInc;
    float randZInc;
    float randRotation;
    // Quaternion rotationQuat;
    Vector3 randomSpawn;
    int amountTreeSpawns=-1;
    int amountBerrySpawns=100; //100
    int amountStoneSpawns=-1;

    void Awake()
    {
        treeXVal=Random.Range(-268f, 268f);
        treeXVal=Random.Range(-268f, 268f);

        berryXVal=Random.Range(-268f, 268f);
        berryZVal=Random.Range(-268f, 268f);

    }

    void Update()
    {

        if(amountBerrySpawns>0){        //berry spawner
            randXInc=Random.Range(-50f,50f);
            randZInc=Random.Range(-50f,50f);
            while((berryXVal+randXInc) < -268 || (berryXVal+randXInc) > 268){
                randXInc=Random.Range(-10f,10f);
            }
            while((berryZVal+randZInc) < -268 || (berryZVal+randZInc) > 268){
                randZInc=Random.Range(-10f,10f);
            }
            berryXVal+=randXInc;
            berryZVal+=randZInc;

            randomSpawn = new Vector3(berryXVal, 0.5f, berryZVal);

            Collider[] spawnCollider = Physics.OverlapSphere(randomSpawn, 5, LayerMask.GetMask("Spawn"));
            if(spawnCollider.Length==0){
                Collider[] gatherableColliders = Physics.OverlapSphere(randomSpawn, 15, LayerMask.GetMask("Gatherable"));
                if(gatherableColliders.Length==0){
                    randRotation=Random.Range(0,360);
                    Quaternion rotation = Quaternion.Euler(0, randRotation, 0);
                    Instantiate(berryBush, randomSpawn, rotation);
                    // floorNavMesh.BuildNavMesh();
                    amountBerrySpawns--;
                }
            }
            // Debug.Log("xVal: "+xVal+" zVal: "+zVal);
        }
        else if(amountBerrySpawns==0){
            amountTreeSpawns=500; //500
            amountBerrySpawns=-1;
        }

        if(amountTreeSpawns>0){         //tree spawner
            randXInc=Random.Range(-50f,50f);
            randZInc=Random.Range(-50f,50f);
            while((treeXVal+randXInc) < -268 || (treeXVal+randXInc) > 268){
                randXInc=Random.Range(-10f,10f);
            }
            while((treeZVal+randZInc) < -268 || (treeZVal+randZInc) > 268){
                randZInc=Random.Range(-10f,10f);
            }
            treeXVal+=randXInc;
            treeZVal+=randZInc;

            randomSpawn = new Vector3(treeXVal, 1f, treeZVal);

            Collider[] spawnCollider = Physics.OverlapSphere(randomSpawn, 5, LayerMask.GetMask("Spawn"));
            if(spawnCollider.Length==0){
                Collider[] gatherableColliders = Physics.OverlapSphere(randomSpawn, 4, LayerMask.GetMask("Gatherable"));
                if(gatherableColliders.Length==0){
                    randRotation=Random.Range(0,360);
                    Quaternion rotation = Quaternion.Euler(0, randRotation, 0);
                    Instantiate(tree, randomSpawn, rotation);
                    amountTreeSpawns--;
                }
            }
            // Debug.Log("xVal: "+xVal+" zVal: "+zVal);
        }
        else if(amountTreeSpawns==0){
            amountStoneSpawns=50;
            amountTreeSpawns=-1;
        }

        if(amountStoneSpawns>0){         //stone spawner
            randXInc=Random.Range(-50f,50f);
            randZInc=Random.Range(-50f,50f);
            while((stoneXVal+randXInc) < -268 || (stoneXVal+randXInc) > 268){
                randXInc=Random.Range(-10f,10f);
            }
            while((stoneZVal+randZInc) < -268 || (stoneZVal+randZInc) > 268){
                randZInc=Random.Range(-10f,10f);
            }
            stoneXVal+=randXInc;
            stoneZVal+=randZInc;

            randomSpawn = new Vector3(stoneXVal, 0.5f, stoneZVal);

            Collider[] spawnCollider = Physics.OverlapSphere(randomSpawn, 5, LayerMask.GetMask("Spawn"));
            if(spawnCollider.Length==0){
                Collider[] gatherableColliders = Physics.OverlapSphere(randomSpawn, 20, LayerMask.GetMask("Gatherable"));
                if(gatherableColliders.Length==0){
                    randRotation=Random.Range(0,360);
                    Quaternion rotation = Quaternion.Euler(0, randRotation, 0);
                    int randStone=Random.Range(0,4);
                    GameObject stone = stones[randStone];
                    // stone.GetComponent<Transform>().localScale = new Vector3(5,5,5);
                    stone.GetComponent<Transform>().localScale = new Vector3(4f,4f,4f);
                    Instantiate(stone, randomSpawn, rotation);
                    amountStoneSpawns--;
                }
            }
            // Debug.Log("xVal: "+xVal+" zVal: "+zVal);
        }
        else if(amountStoneSpawns==0){
            amountStoneSpawns=-1;
            floorNavMesh.BuildNavMesh();
            UIHandler.GetComponent<UIHandler>().gameLoading=false;
        }

    }
}
