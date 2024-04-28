using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITaskPartner
{
    
}

public class JobPartnerController : MonoBehaviour
{
    public bool isFree = true;

    public ITaskPartner Task;
}
