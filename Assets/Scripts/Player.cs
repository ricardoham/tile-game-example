using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
  [SerializeField] float runSpeed = 5f;
  [SerializeField] float jumpSpeedy = 5f;
  [SerializeField] float climbSpeedy = 5f;
  // State
  bool isAlive = true;
  Rigidbody2D myRigidBody;
  Animator myAnimator;
  CapsuleCollider2D myBodyColider2D;
  BoxCollider2D myFeet;

  float gravityScaleAtStart;


  // Start is called before the first frame update
  void Start()
  {
    myRigidBody = GetComponent<Rigidbody2D>();
    myAnimator = GetComponent<Animator>();
    myBodyColider2D = GetComponent<CapsuleCollider2D>();
    myFeet = GetComponent<BoxCollider2D>();
    gravityScaleAtStart = myRigidBody.gravityScale;
  }

  // Update is called once per frame
  void Update()
  {
    Run();
    Jump();
    ClimbLadder();
    FlipSprite();
  }

  private void Run()
  {
    float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");
    bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;

    Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody.velocity.y);
    myRigidBody.velocity = playerVelocity;

    myAnimator.SetBool("Running", playerHasHorizontalSpeed);
  }

  private void ClimbLadder()
  {
    if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Climbing")))
    {
      myAnimator.SetBool("Climbing", false);
      myRigidBody.gravityScale = gravityScaleAtStart;
      return;
    }

    float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");
    Vector2 climbVelocity = new Vector2(myRigidBody.velocity.x, controlThrow * climbSpeedy);
    myRigidBody.velocity = climbVelocity;
    myRigidBody.gravityScale = 0f;
    bool playerHasVerticalSpeedy = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
    myAnimator.SetBool("Climbing", playerHasVerticalSpeedy);
  }

  private void Jump()
  {
    if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"))) return;

    if (CrossPlatformInputManager.GetButtonDown("Jump"))
    {
      Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeedy);
      myRigidBody.velocity += jumpVelocityToAdd;
    }
  }
  private void FlipSprite()
  {
    bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;

    if (playerHasHorizontalSpeed)
    {
      transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
    }
  }
}
