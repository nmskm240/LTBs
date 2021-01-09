using UnityEngine;
using UnityEngine.EventSystems;
public class test : MonoBehaviour, IPointerEnterHandler 
{
    public void OnPointerEnter(PointerEventData e)
    {
        Debug.Log(this.gameObject.name);
    }
}