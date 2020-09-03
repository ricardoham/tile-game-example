using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
  [SerializeField] float runSpeed = 5f;
  [SerializeField] float jumpSpeedy = 5f;
  // State
  bool isAlive = true;
  Rigidbody2D myRigidBody;
  Animator myAnimator;
  Collider2D myColider2D;


  // Start is called before the first frame update
  void Start()
  {
    myRigidBody = GetComponent<Rigidbody2D>();
    myAnimator = GetComponent<Animator>();
    myColider2D = GetComponent<Collider2D>();
  }

  // Update is called once per frame
  void Update()
  {
    Run();
    Jump();
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

  private void Jump()
  {
    if (!myColider2D.IsTouchingLayers(LayerMask.GetMask("Ground"))) return;

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
