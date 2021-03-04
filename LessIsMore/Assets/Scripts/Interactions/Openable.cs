using System.Collections;
using UnityEngine;


public class Openable : MonoBehaviour,IInteractable
{
    Animator animator;
    private bool Opened;
    public BoxCollider2D closedCollider;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    /// <summary>
    /// OPENS THE OPENABLE OBJECT
    /// </summary>
    public void doInteraction()
    {
        closedCollider.enabled = false;
        Opened = !Opened;
        animator.SetBool("InteractedWith", Opened);
    }
    public IInteractable Instance()
    {
        return this;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
