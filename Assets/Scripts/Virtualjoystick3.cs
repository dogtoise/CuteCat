using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Virtualjoystick3 : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private float joystickVisualDistance = 50;
    public Image container;
    public Image joystick;

    [SerializeField]
    private Vector3 direction;
    public Vector3 Direction { get { return direction; } }

    private void Start()
    {
        container.gameObject.SetActive(false);
        joystick.gameObject.SetActive(false);
        //var imgs = GetComponentsInChildren<Image>();
        //container = imgs[0];
        //joystick = imgs[1];
        // Debug.Log("Container : " + container);
        // Debug.Log("Joystick : " + joystick);
    }
    private void Update()
    {
        //    if (!isDrag && Input.GetMouseButtonDown(0))
        //{
        //   // container.rectTransform.position = Camera.main.WorldToScreenPoint(Input.mousePosition);
        //   // joystick.rectTransform.position = Camera.main.WorldToScreenPoint(Input.mousePosition);

        //    container.rectTransform.position = Input.mousePosition;
        //    joystick.rectTransform.position =Input.mousePosition;
        //}
    }
    bool isDrag = false;
    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos = Vector2.zero;
        Debug.Log("in?");
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(container.rectTransform, ped.position, ped.pressEventCamera, out pos))
        {
            pos.x = (pos.x / container.rectTransform.sizeDelta.x);
            pos.y = (pos.y / container.rectTransform.sizeDelta.y);
            Debug.Log("pos : " + pos);
            Vector2 refPivot = new Vector2(0.5f, 0.5f);
            Vector2 p = container.rectTransform.pivot;
            pos.x += p.x - 0.5f;
            pos.y += p.y - 0.5f;

            float x = Mathf.Clamp(pos.x, -1, 1);
            float y = Mathf.Clamp(pos.y, -1, 1);

            direction = new Vector3(x, 0, y).normalized;
            Debug.Log(direction);

            joystick.rectTransform.anchoredPosition = new Vector3(x * joystickVisualDistance, y * joystickVisualDistance);
        }
    }
    public virtual void OnPointerDown(PointerEventData ped)
    {
        isDrag = true;
        container.gameObject.SetActive(true);
        joystick.gameObject.SetActive(true);
        container.rectTransform.position = ped.position;
        joystick.rectTransform.position = ped.position;
        OnDrag(ped);
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        container.gameObject.SetActive(false);
        joystick.gameObject.SetActive(false);
        isDrag = false;
        direction = default(Vector3);
        joystick.rectTransform.anchoredPosition = default(Vector3);
    }


}
