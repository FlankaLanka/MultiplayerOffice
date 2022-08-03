using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drawer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private GameObject newLine;
    private bool canDraw = false;
    public Coroutine drawing;
    public Camera cam;
    public GameObject player;

    private void Start()
    {
        StartCoroutine(LateStart());
    }

    private IEnumerator LateStart()
    {
        yield return new WaitForSeconds(5f);
        cam = FindObjectOfType<Camera>();
        player = FindObjectOfType<PlayerMove>().gameObject;
        player.GetComponent<PlayerMove>().enabled = false;
        cam.orthographic = false;
    }



    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("mousefown");

            StartLine();
        }
        if(Input.GetMouseButtonUp(0))
        {
            FinishLine();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(eventData.pointerCurrentRaycast.gameObject == gameObject)
        {
            canDraw = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        canDraw = false;
    }

    private void StartLine()
    {
        if(drawing != null)
        {
            StopCoroutine(drawing);
        }
        drawing = StartCoroutine(DrawLine());
    }

    private void FinishLine()
    {
        StopCoroutine(drawing);
    }

    private IEnumerator DrawLine()
    {
        Debug.Log("IN drawlione");

        GameObject newGameObject = Instantiate(newLine, new Vector3(10, 30, 6), Quaternion.identity);
        LineRenderer line = newGameObject.GetComponent<LineRenderer>();
        line.positionCount = 0;

        while(true)
        {
            Debug.Log("INWHILE");
            
            RaycastHit hit = new RaycastHit();
            if(Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit))
            {
                Vector3 position = hit.point;
                position.z = 0;
                line.positionCount++;
                line.SetPosition(line.positionCount - 1, position);
            }


            yield return null;
        }
    }

}
