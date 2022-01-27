using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    // moving limits right and left
    private float min_X = -2.2f, max_X = 2.2f;

    private bool canMove;

    private float moveSpeed = 2f;

    private Rigidbody2D myBody;
    public DistanceJoint2D mDistancejoint;

    private bool gameOver;

    private bool ignoreCollision;
    private bool ignoreTrigger;

    private void Awake()
    {
        // get the rigidbody 2d component
        myBody = GetComponent<Rigidbody2D>();
        mDistancejoint = GetComponent<DistanceJoint2D>();
        // don't fall initially
        myBody.gravityScale = 1f;
    }
    // Start is called before the first frame update
    void Start()
    {
        //canMove = true;
        //50-50 chance
        if(Random.Range(0, 2) > 0)
        {
            //randomize the move speed 
            moveSpeed *= -1.0f;
        }
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = new Vector3(0, 6, 0);
        mDistancejoint.connectedAnchor = new Vector2(0, 6);
        GameplayController.instance.currentBox = this;
    }
    // Update is called once per frame
    void Update()
    {
        MoveBox();   
    }
    void MoveBox()
    {
        if (canMove)
        {
            //Get the current position of the box
            Vector3 temp = transform.position;

            //Move the box with move speed in the current direction
            temp.x += moveSpeed * Time.deltaTime;

            if(temp.x > max_X)
            {
                //change direction if too right
                moveSpeed *= -1.0f;
            }
            else if (temp.x < min_X)
            {
                //change direction if too left
                moveSpeed *= -1.0f;
            }
            transform.position = temp;
        }
    }
    public void DropBox()
    {
        //stop the movement
        canMove = false;
        //add pull down
        myBody.gravityScale = Random.Range(2, 4);
        mDistancejoint.enabled = false;
    }
    IEnumerator Landed(float stillwait)
    {
        yield return new WaitForSeconds(stillwait);
        if (gameOver)
        {
            //return;
            yield break;
        }
        ignoreCollision = true;
        ignoreTrigger = true;
        GameplayController.instance.SpawnNewBox();
        GameplayController.instance.moveCamera();
        myBody.gravityScale = 5;
        myBody.mass = 500;
    }
    private void OnCollisionEnter2D(Collision2D target)
    {
        if (ignoreCollision)
        {
            return;
        }   
        if(target.gameObject.CompareTag("Platform"))
        {
            //Invoke("Landed", 2.0f);
            StartCoroutine(Landed(.2f));
            ignoreCollision = true;
        }
        if(target.gameObject.CompareTag("Box"))
        {
            //Invoke("Landed", 2.0f);
            StartCoroutine(Landed(.2f));
            Debug.Log("Box Landed");
            ignoreCollision = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D target)
    {
        if (ignoreTrigger)
        {
            return;
        }
        if(target.gameObject.CompareTag("GameOver"))
        {
            CancelInvoke("Landed");
            StopCoroutine(Landed(0f));
            gameOver = true;
            ignoreTrigger = true;
            RestartGame();
            //Invoke("RestartGame", 2.0f);
        }
        void RestartGame()
        {
            Debug.Log("Restart game");
            GameplayController.instance.Restart();
        }
    }
}
