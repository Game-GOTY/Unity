using UnityEngine;

public class GroundCheck : Main
{
    protected bool isGrounded = false;
    public bool IsGrounded => isGrounded;
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Tilemap2D")
        {
            isGrounded = true; // Nhân vật đang chạm đất
        }
    }
    protected virtual void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Tilemap2D")
        {
            isGrounded = true; // Nhân vật đang chạm đất
        }
    }

    protected virtual void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Tilemap2D")
        {
            isGrounded = false; // Nhân vật không còn chạm đất
        }
    }

}
