using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class toggleColliders : UdonSharpBehaviour
{
    public GameObject[] collidables;

    public override void Interact()
    {
        foreach(GameObject item in collidables)
        {
            if (item.GetComponent<MeshCollider>().enabled == true)
            {
                item.GetComponent<MeshCollider>().enabled = false;
            }
            else
            {
                item.GetComponent<MeshCollider>().enabled = true;
            }
        }
    }
}
