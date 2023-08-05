using UnityEngine;

public class MaterialLevelManager : MonoBehaviour
{
    public static MaterialLevelManager instance;
    
    public Material Level1;
    public Material Level2;
    public Material Level3;
    public Material Level4;
    public Material Level5;
    public Material Level6;
    
    private void Awake()
    {
        if (instance is null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
}
