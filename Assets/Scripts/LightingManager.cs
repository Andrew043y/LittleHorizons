using UnityEngine;

public class LightingManager : MonoBehaviour
{
    public Light DirectionalLight;
    public LightingPreset preset;
    public float TimeOfDay;

    void Awake()
    {
        TimeOfDay=55;
    }

    private void Update(){
        if(preset == null){
            return;
        }
        if(Application.isPlaying){
            TimeOfDay += Time.deltaTime;
            TimeOfDay %= 200;    //Clamp between 0-200
            UpdateLighting(TimeOfDay/200);
        }
        else{
            UpdateLighting(TimeOfDay/200);
        }
    }

    private void OnValidate()
    {
        if(DirectionalLight !=null){
            return;
        }
        if(RenderSettings.sun !=null){
            DirectionalLight = RenderSettings.sun;
        }
        else{
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (Light light in lights){
                if(light.type == LightType.Directional){
                    DirectionalLight = light;
                    return;
                }
            }
        }
    }

    private void UpdateLighting(float timePercent){
        RenderSettings.ambientLight = preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = preset.FogColor.Evaluate(timePercent);

        if(DirectionalLight!=null){
            DirectionalLight.color = preset.DirectionalColor.Evaluate(timePercent);
            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent*360f)-90f, 170,0));
        }
    }
}
