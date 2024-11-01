using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerEngine : MonoBehaviour
{
    [Header("Platformer engine by: cyber.lua!")]
    [Header("IMPORTANT")]
    [Tooltip("The collider used for the player")]
    public Collider2D PlayerCollider;
    [Tooltip("The ground layer")]
    public LayerMask Ground;
    [Header("Stats")]
    [Tooltip("The speed the player walks at")]
    public int walkSpeed = 15;
    [Tooltip("The height the player jumps")]
    public int jumpPower = 15;
    [Header("Keybinds")]
    [Tooltip("The key used to jump")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Feelgood")]
    [Tooltip("The amount of jump-buffer frames")]
    public int jumpBuffer = 20;

    private Rigidbody2D Rigidbody;
    private bool canJump = false;
    private int jumpBufferCounter;

    void Start()
    {
        Rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    private bool IsInLayerMask(GameObject obj, LayerMask layerMask) {
        return ((1 << obj.layer) & layerMask) != 0;
    }

    public void UpdateCharacterMove() {
        Rigidbody.velocity = new Vector2(Input.GetAxis("Horizontal") * walkSpeed, Rigidbody.velocity.y);
    }

    public void Jump() {
        Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, jumpPower);
        canJump = false;
        jumpBufferCounter = 0;
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (IsInLayerMask(collision.gameObject, Ground)) {
            canJump = true;
        }
    }

    void Update()
    {
        UpdateCharacterMove();

        if (Input.GetKeyDown(jumpKey))
        {
            jumpBufferCounter = jumpBuffer;
        }

        if (jumpBufferCounter > 0)
        {
            if (canJump)
            {
                Jump();
            }
            jumpBufferCounter--;
        }
    }
}
