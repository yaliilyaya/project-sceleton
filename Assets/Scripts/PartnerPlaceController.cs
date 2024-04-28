using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerPlaceController : MonoBehaviour
{
    public List<GameObject> Places;

    public void placeOnGlasses(List<GameObject> partners)
    {
        for (var i = 0; i < Places.Count || i < partners.Count; i++)
        {
            if (partners[i] != null)
            {
                partners[i].GetComponent<TargetStaticAgentController>().objectTarget = Places[i];
            }
        }
    }
}
