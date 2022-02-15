using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    [SerializeField] private Jumper body;

    [SerializeField] private float movementSpeed = 0;

    [SerializeField] private float jumpForce = 0;
    [SerializeField] private float directionForce = 0;

    [SerializeField] private Vector3 direction = Vector3.zero;
    [SerializeField] private GameObject target;

    private bool isMove = false;
    private float distance = 0;

    private Rigidbody rigidbody;

    private IEnumerator MovementCoroutine = null;
    //private IEnumerator JumpCoroutine = null;

    public bool IsMove { get => isMove; }
    public Jumper Body { get => body; }
    public float DirectionForce { get => directionForce; }

    #region UnityBehaviour
    void Start()
    {
        rigidbody = body.GetComponent<Rigidbody>();
        body.OnGounded += Grounded;
    }

    void Update()
    {
        //if (!isMove && body.IsGrounded)
        //{
        //    bool horizontal = Input.GetButtonDown("Horizontal");
        //    bool vertical = Input.GetButtonDown("Vertical");

        //    if (horizontal)
        //    {
        //        direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
        //        Move();
        //    }

        //    if (vertical)
        //    {
        //        direction = new Vector3(0, 0, Input.GetAxisRaw("Vertical"));
        //        Move();
        //    }
        //}

        if (rigidbody.velocity.y < 0)
        {
            rigidbody.velocity += Vector3.up * Physics.gravity.y * (jumpForce * 2 - 1) * Time.deltaTime;
        }
    }
    #endregion

    private void Grounded()
    {
        isMove = false;
        transform.SetParent(body.ObjectToCollision.transform);

        if (MovementCoroutine != null)
            StopCoroutine(MovementCoroutine);

        transform.position = target.transform.position;
        body.transform.localRotation = Quaternion.LookRotation(direction, Vector3.up);
    }    

    public void Move(GameObject target, Vector3 direction)
    {

        if (MovementCoroutine != null)
            StopCoroutine(MovementCoroutine);

        //if (JumpCoroutine != null)
        //    StopCoroutine(JumpCoroutine);

        rigidbody.velocity = Vector3.up * jumpForce;
        body.IsGrounded = false;
        //this.target = transform.position + (direction * directionForce);
        this.target = target;
        this.direction = direction;

        transform.SetParent(null);

        MovementCoroutine = MoveFrog();
        //JumpCoroutine = JumpFrog();

        StartCoroutine(MovementCoroutine);
        //StartCoroutine(JumpCoroutine);
    }  
    
    public void Restart()
    {

    }

    #region Coroutines
    private IEnumerator MoveFrog()
    {
        float elapsedTime = 0;
        isMove = true;

        distance = Vector3.Distance(transform.position, target.transform.position);

        //while (elapsedTime < movementSpeed || distance > 0.1f)
        while (distance > 0.05f)
        {
            distance = Vector3.Distance(transform.position, target.transform.position);

            transform.position = Vector3.Lerp(transform.position, target.transform.position, (elapsedTime / movementSpeed));
            body.transform.localRotation = Quaternion.Lerp(body.transform.localRotation, Quaternion.LookRotation(direction, Vector3.up), (elapsedTime / movementSpeed));

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = target.transform.position;
        body.transform.localRotation = Quaternion.LookRotation(direction, Vector3.up);

        isMove = false;
        yield return null;
    }

    Vector3 pos = Vector3.zero;

  

    //private IEnumerator JumpFrog()
    //{
    //    float elapsedTime = 0;
    //    isMove = true;

    //    pos = Vector3.zero;

    //    while (elapsedTime < movementSpeed)
    //    {
    //        pos.y = curve.Evaluate(elapsedTime);
    //        body.transform.position = new Vector3(body.transform.position.x, + pos.y, body.transform.position.z);
    //        elapsedTime += Time.deltaTime;
    //        yield return null;
    //    }

    //    yield return null;
    //}
    #endregion
}