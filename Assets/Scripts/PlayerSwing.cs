using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSwing : MonoBehaviour {

    public Animator animator;

    public Collider2D[] anchorColliders = new Collider2D[4];
    //public Collider2D leftAnchorCollider, topAnchorCollider, rightAnchorCollider;
    public Collider2D detectorCollider;

    public Image UISwingIndicator, UISwingControlIndicator;
    public Text UISwingDetectorText;


    public Vector3 originalCenterOfMass;
    public Vector3 swingCenterOfMass;

    public Vector2 SwingLeftForce, swingRightForce;

    public ContactFilter2D contactFilter;
    private int numOverlappedColliders;





    private bool isSwinging = false, isSwingButtonDown = false;



    private Collider2D[] overlappedColliders;


    Rigidbody2D rbody;
    void Awake()
    {
        UISwingIndicator.enabled = false;
        UISwingControlIndicator.enabled = false;

        rbody = GetComponent<Rigidbody2D>();

        DeactivateAnchorColliders();

        overlappedColliders = new Collider2D[1];
    }

    // Use this for initialization
    void Start() {
		
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        //put the controls for swinging back and forth in the Move script
        //if player move left
        //  rbody.AddForceAtPosition
        //should we put the controls for detaching in here?


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            isSwingButtonDown = true;
            UISwingControlIndicator.enabled = true;
        }

        if (Input.GetKeyUp(KeyCode.B)) 
        {
        
            isSwingButtonDown = false;
            UISwingControlIndicator.enabled = false;

            isSwinging = false;
            animator.SetTrigger("SwingEnd");

            ActivateDetectorColliders();
            DeactivateAnchorColliders();

            Detach();

        }



    }

    void OnTriggerEnter2D(Collider2D col)
    {

        //determine if the button is held for swingin'
        //and
        //determine if the collision is with a swingin' knob
        //if not exit out of the script



        if (!isSwingButtonDown) {
            UISwingDetectorText.text = "Trigger: Bnot";
            return;
        }

        numOverlappedColliders = detectorCollider.OverlapCollider(contactFilter, overlappedColliders);

        UISwingDetectorText.text = "Trigger: " + numOverlappedColliders;

        if (numOverlappedColliders > 0) {

            DeactivateDetectorColliders();

            ActivateAnchorColliders();

            UISwingIndicator.enabled = true;

            isSwinging = true;
            animator.SetTrigger("SwingBegin");
        }
        else
        {
            UISwingIndicator.enabled = false;

            return;

        }


        //allow rotation
        rbody.freezeRotation = false;


        //capture original center of mass
        originalCenterOfMass = rbody.centerOfMass;

        //set the new center of mass to the point of contact with the knob
        //swingCenterOfMass = 
        rbody.centerOfMass = swingCenterOfMass;

        //adjust position to swing from the knob, and cancel velocity

        //apply torque based on 


        //set swinging flag to true
        isSwinging = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        UISwingDetectorText.text = "Trigger: Exit";
    }

    private void Detach() {


        //set the rigidbody back upright
        rbody.angularVelocity = 0.0f;
        rbody.rotation = 0.0f;

        //disallow rotation
        rbody.freezeRotation = true;

        isSwinging = false;

    }

    /*
    private void DeactivateAnchorColliders() {
        leftAnchorCollider.enabled = false;
        topAnchorCollider.enabled = false;
        rightAnchorCollider.enabled = false;
    }

    private void ActivateAnchorColliders() {
        leftAnchorCollider.enabled = true;
        topAnchorCollider.enabled = true;
        rightAnchorCollider.enabled = true;
    }
    */

    private void DeactivateAnchorColliders()
    {
        foreach (Collider2D anchorCollider in anchorColliders) {
            anchorCollider.enabled = false;
        }

    }

    private void ActivateAnchorColliders()
    {
        foreach (Collider2D anchorCollider in anchorColliders)
        {
            anchorCollider.enabled = true;
        }
    }

    private void DeactivateDetectorColliders()
    {
        detectorCollider.enabled = false;
    }

    private void ActivateDetectorColliders()
    {
        detectorCollider.enabled = true;
    }


    private void SwingLeft() {
        rbody.AddForceAtPosition(SwingLeftForce,originalCenterOfMass);
    }

    public bool IsSwinging() {
        return isSwinging;
    }
}
