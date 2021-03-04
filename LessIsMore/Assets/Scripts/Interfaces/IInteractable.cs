using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    /// <summary>
    /// DOES THE INTERACTION FOR IINTERACTABLE OBJECT
    /// </summary>
    void doInteraction();
    /// <summary>
    /// RETURNS THE INTANCE OF THE INTERACTABLE OBJECT
    /// </summary>
    /// <returns></returns>
    IInteractable Instance();
}
