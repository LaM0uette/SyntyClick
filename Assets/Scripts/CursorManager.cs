using UnityEngine;

public class CursorManager : MonoBehaviour
{
    #region Statements

    public static CursorManager instance;
    
    [SerializeField] private Vector2 HotSpot = new (0, 0);
    public Texture2D CursorMouse;
    public Texture2D CursorClic;
    public Texture2D CursorHand;
    public Texture2D CursorLvlUp;
    
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

    #endregion
    
    #region Functions

    public static void SetHandCursor(string name)
    {
        var texture2D = name.ToLower() switch
        {
            "mouse" => instance.CursorMouse,
            "clic" => instance.CursorClic,
            "hand" => instance.CursorHand,
            "lvlup" => instance.CursorLvlUp,
            _ => instance.CursorMouse
        };
        
        Cursor.SetCursor(texture2D, instance.HotSpot, CursorMode.Auto);
    }

    public static void ResetCursor()
    {
        Cursor.SetCursor(instance.CursorMouse, instance.HotSpot, CursorMode.Auto);
    }

    #endregion
}
