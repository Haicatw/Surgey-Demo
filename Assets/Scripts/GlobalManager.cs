using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Parabox.CSG;

public class GlobalManager : MonoBehaviour
{
    public GameObject scannedSample;
    public GameObject tool;
    private Vector3 failureRaycastPoint;
    // Start is called before the first frame update
    void Start()
    {
        failureRaycastPoint = new Vector3(0.0f, 0.0f, 0.0f);
        tool.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 toolPos = getHitPoint();
        if (toolPos != failureRaycastPoint)
        {
            tool.transform.position = toolPos;
        }
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            scannedSample = ReturnClickedObject(out hitInfo);
            if (scannedSample != null)
            {
                CSG_Model result = subtraction(scannedSample, tool);
                scannedSample.GetComponent<MeshFilter>().sharedMesh = result.mesh;
                scannedSample.GetComponent<MeshRenderer>().sharedMaterials = result.materials.ToArray();
                // composite = new GameObject();
                // composite.AddComponent<MeshFilter>().sharedMesh = result.mesh;
                // composite.AddComponent<MeshRenderer>().sharedMaterials = result.materials.ToArray();
            }
        }
    }

    GameObject ReturnClickedObject(out RaycastHit hit)
    {
        GameObject targetObject = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction * 10, out hit))
        {
            targetObject = hit.collider.gameObject;
        }
        return targetObject;
    }

    Vector3 getHitPoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        Debug.Log("Hit" + hit.point);
        return hit.point;
    }

    CSG_Model subtraction(GameObject scannedSample, GameObject tool)
    {
        return Boolean.Subtract(scannedSample, tool);
    }

    void setToolObject(GameObject toolObject)
    {
        tool = toolObject;
    }
}