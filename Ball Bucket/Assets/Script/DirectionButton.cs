using UnityEngine;
using UnityEngine.EventSystems;


public class DirectionButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private GameObject RotatingObject;
    [SerializeField] private string btnDirection;
    [SerializeField] private float directionPower;
    bool buttonPressed;

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonPressed = false;
    }

    void Update()
    {
        if (buttonPressed)
        {
            if (btnDirection == "left")
            {
                RotatingObject.transform.Rotate(0, directionPower*Time.deltaTime, 0, Space.Self);
            }
            else
            {
                RotatingObject.transform.Rotate(0, -directionPower*Time.deltaTime, 0, Space.Self);
            }
        }


    }
}
