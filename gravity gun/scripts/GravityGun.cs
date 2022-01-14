using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityGun : MonoBehaviour
{
    [SerializeField] Transform cam, positionofpickedobj, gravityguntip;
    [SerializeField] float maxraycastdis, lerpspeed, max_disfromcam, min_disfromcam, scrollspeed, Shootspeed;
    [SerializeField] LayerMask pickablelayer;
    [SerializeField] LineRenderer lr;
    public bool pickedup = false;
    public KeyCode Buttontolift, shootkey;
    public RaycastHit pickuphit;


    private void Update()
    {
        pickup();
    }

    private void LateUpdate()
    {
    }

    void pickup()
    {
        //the object picking must have the pickable layer and a rigidbody

        if(Input.GetKeyUp(Buttontolift) && !pickedup)
        {
            if(Physics.Raycast(cam.position, cam.forward,out pickuphit, maxraycastdis, pickablelayer))
            {
                pickuphit.transform.gameObject.GetComponent<Rigidbody>().useGravity = false;
                pickedup = true;
            }
        }
        else if (Input.GetKeyUp(Buttontolift) && pickedup)
        {
            pickuphit.transform.gameObject.GetComponent<Rigidbody>().useGravity = true;
            pickedup = false;
        }

        if(Input.GetKey(shootkey) && pickedup)
        {
            pickuphit.transform.gameObject.GetComponent<Rigidbody>().useGravity = true;
            pickuphit.transform.gameObject.GetComponent<Rigidbody>().AddForce(cam.forward * Shootspeed, ForceMode.Impulse);
            pickedup = false;
        }

        if(pickedup)
        {
            if(Input.GetAxis("Mouse ScrollWheel") > 0 && positionofpickedobj.transform.position.z <= max_disfromcam)
            {
                positionofpickedobj.Translate(0, 0, scrollspeed);
            }
            else if(Input.GetAxis("Mouse ScrollWheel") < 0 && positionofpickedobj.transform.position.z >= min_disfromcam)
            {
                positionofpickedobj.Translate(0, 0, -scrollspeed);
            }
            lr.gameObject.SetActive(true);
            pickuphit.transform.position = Vector3.Lerp(pickuphit.transform.position, positionofpickedobj.position, Time.deltaTime * lerpspeed);
        }
        else
        {
            lr.gameObject.SetActive(false);
        }
        
    }
}
