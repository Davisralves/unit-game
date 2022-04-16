using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
  private BoxCollider2D boxCollider;
  private Vector3 moveDelta;

  private RaycastHit2D verticalHit;
  private RaycastHit2D horizontalHit;
  private void Start()
  {
    boxCollider = GetComponent<BoxCollider2D>();
  }

  private void FixedUpdate()
  {

    float x = Input.GetAxisRaw("Horizontal");
    float y = Input.GetAxisRaw("Vertical");

    //reseta movelDelta
    moveDelta = new Vector3(x, y, 0);

    // muda a direção do sprite, enquato você se move para esquerda ou direita
    if (moveDelta.x > 0)
      transform.localScale = new Vector3(1, 1, 1);
    else if (moveDelta.x < 0)
      transform.localScale = new Vector3(-1, 1, 1);

    // movimento e colisão
    verticalHit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y),
      Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Creature", "Blocking"));
    horizontalHit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0),
      Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Creature", "Blocking"));


    if (verticalHit.collider == null)
    {
      transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
    }
    if (horizontalHit.collider == null)
    {
      transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
    }
  }
}
