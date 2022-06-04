using UnityEngine;

namespace WhiteRoom.Utils
{
    public class UtilsClass : MonoBehaviour
    {
        //object creat if isn't have else find object
        public static GameObject FindOrCreateObject(string objectName,string tag = "Untagged")
        {
            GameObject find_object = GameObject.Find(objectName);
            return find_object != null ? find_object : new GameObject(objectName){tag = tag};
        }
        
        //Get Mouse Position in world without Z;
        public static Vector3 GetMouseWorldPositionWithoutZ()
        {
            Vector3 vec = GetMouseWorldPosition(Input.mousePosition, Camera.main);
            vec.z = 0f;
            return vec;
        }
        
        //Get Mouse Position in the World
        public static Vector3 GetMouseWorldPosition()
        {
            return GetMouseWorldPosition(Input.mousePosition, Camera.main);
        }
        
        //Get Mouse Position in the World only with camera
        public static Vector3 GetMouseWorldPosition(Camera worldCamera)
        {
            return GetMouseWorldPosition(Input.mousePosition, worldCamera);
        }
        
        //Get Mouse Position in the world with position and camera 
        public static Vector3 GetMouseWorldPosition(Vector3 screenPosition, Camera worldCamera)
        {
            Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
            return worldPosition;
        }
        
        //Create text in world with null checker
        public static TextMesh CreateWorldText(string text, Transform parent = null, Vector3 localPosition = default(Vector3), int fontSize = 40, Color? color = null, TextAnchor textAnchor = TextAnchor.UpperLeft, TextAlignment textAlignment = TextAlignment.Left, int shortingOrder = 5000)
        {
            color ??= Color.white;
            return CreateWorldText(parent, text, localPosition, fontSize, (Color) color, textAnchor, textAlignment, shortingOrder);
        }
        
        //Create text in world
        public static TextMesh CreateWorldText(Transform parent, string text, Vector3 localPosition, int fontSize, Color color, TextAnchor textAnchor, TextAlignment textAlignment, int shortingOrder)
        {
            GameObject gameObject= new GameObject("World_Text",typeof(TextMesh));
            Transform transform = gameObject.transform;
            transform.SetParent(parent,false);
            transform.localPosition = localPosition;
            TextMesh textMesh = gameObject.GetComponent<TextMesh>();
            textMesh.anchor = textAnchor;
            textMesh.alignment= textAlignment;
            textMesh.text = text;
            textMesh.fontSize = fontSize;
            textMesh.color = color;
            textMesh.GetComponent<MeshRenderer>().sortingOrder = shortingOrder;
            return textMesh;
        }
    }
}
