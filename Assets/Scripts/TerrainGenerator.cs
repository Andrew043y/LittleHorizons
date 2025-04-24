using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public int width=256;
    public int height=256;
    void Update()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = generateTexture();
    }

    Texture2D generateTexture(){
        Texture2D texture = new Texture2D(width, height);

        for(int x=0; x<width; x++){
            for(int y=0; y<height; y++){
                Color color=calculateColor(x,y);
                texture.SetPixel(x,y, color);
            }
        }

        //Generate Perlin noise
        texture.Apply();
        return texture;
    }

    Color calculateColor(int x, int y){
        float xCoord = (float)x/width;
        float yCoord = (float)y/height;

        float sample=Mathf.PerlinNoise(xCoord,yCoord);
        return new Color(sample,sample,sample);
    }
}
