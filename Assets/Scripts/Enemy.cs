using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  [SerializeField] float moveSpeedy = 1f;
  Rigidbody2D myRigidBody;
  // Start is called before the first frame update
  void Start()
  {
    myRigidBody = GetComponent<Rigidbody2D>();
  }

  // Update is called once per frame
  void Update()
  {
    if (IsFacingRight())
    {
      myRigidBody.velocity = new Vector2(moveSpeedy, 0f);
    }
    else
    {
      myRigidBody.velocity = new Vector2(-moveSpeedy, 0f);
    }
  }

  bool IsFacingRight()
  {
    return transform.localScale.x > 0;
  }

  private void OnTriggerExit2D(Collider2D other)
  {
    transform.localScale = new Vector2(-(Mathf.Sign(myRigidBody.velocity.x)), 1f);
  }
}
