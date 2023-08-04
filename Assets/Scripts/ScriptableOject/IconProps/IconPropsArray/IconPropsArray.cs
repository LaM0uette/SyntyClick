using UnityEngine;
using UnityEngine.Serialization;

namespace ScriptableOject.IconProps.IconPropsArray
{
    [CreateAssetMenu(fileName = "New IconPropsArray", menuName = "IconPropsArray")]
    public class IconPropsArray : ScriptableObject
    {
        public string Name;
        [FormerlySerializedAs("Props")] [FormerlySerializedAs("IconProps")] [FormerlySerializedAs("ArrayIconProps")] public IconProps[] Icons;
    }
}