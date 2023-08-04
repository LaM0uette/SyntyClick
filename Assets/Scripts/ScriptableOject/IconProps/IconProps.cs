using UnityEngine;

namespace ScriptableOject.IconProps
{
    [CreateAssetMenu(fileName = "New IconProps", menuName = "IconProps")]
    public class IconProps : ScriptableObject
    {
        public Sprite[] Icons;
    }
}
