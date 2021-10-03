
using UnityEngine;
using UnityEngine.EventSystems;

public class linerendere : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    GameObject linego;
    bool startdrawing;
    Vector3 mousepos;
    LineRenderer lr;
    [SerializeField]
    GameObject cube;
    GameObject lastcube;
    int currentind;

    void Start()
    {
        linego = new GameObject();
        
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        startdrawing = true;
        mousepos = Input.mousePosition;
        lr = linego.AddComponent<LineRenderer>();
        lr.startWidth =  0.2f;
        lr.enabled = false;

    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        startdrawing = false;
        Rigidbody rb= linego.AddComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX;
        if (lastcube!=null)
        {
            Destroy(lastcube.gameObject);
        }
       
        lr.useWorldSpace = false;
        Start();
        currentind = 0;
    }


   
    void Update()
    {
        if (startdrawing)
        {
            float sqrmagniture = (mousepos - Input.mousePosition).sqrMagnitude;
            if (sqrmagniture>400)
            {
                 
                lr.SetPosition(currentind, Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z + 10)));
                if (lastcube!=null)
                {
                    lastcube.gameObject.SetActive(true);
                    lastcube.transform.LookAt(lr.GetPosition(currentind));
                    lastcube.transform.Rotate (0f, lastcube.transform.rotation.y, lastcube.transform.rotation.z);
                    lastcube.transform.localScale = new Vector3(lastcube.transform.localScale.x, lastcube.transform.localScale.y, Vector3.Distance(lastcube.transform.position,lr.GetPosition(currentind)) * 1f);
                    if (lastcube.transform.rotation.y==0)
                    {
                        lastcube.transform.eulerAngles = new Vector3(lastcube.transform.eulerAngles.x, 90, lastcube.transform.eulerAngles.z);
                    }
                }
                
                lastcube = Instantiate(cube, lr.GetPosition(currentind),Quaternion.identity,linego.transform);
                //lastcube.layer = 3;
                lastcube.gameObject.SetActive(false);
                mousepos = Input.mousePosition;
                currentind++;
                lr.positionCount=currentind+1;
                lr.SetPosition(currentind, Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z + 10)));


            }
        }
    }
}
