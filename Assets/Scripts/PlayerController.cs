using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float jumpForce = 3f;
    public float fireRange = 5f;
    public float rotationYSensitivity;
    public float rotationXSensitivity;

    /* New */
    public GameObject explosion;
    public ParticleSystem muzzleEffect;
    public Text pointsText;
    public int points;

    private PlayerInputAction mInputAction;
    private InputAction mMovementAction;
    private InputAction mViewAction;
    private Rigidbody mRigidbody;
    private Transform mFirePoint;
    private Transform mCameraTransform;
    private float mRotationX = 0f;
    private bool jumpPressed = false;
    private bool onGround = true;

    private GameVariables gameVariables;

    private void Awake()
    {
        mInputAction = new PlayerInputAction();
        mRigidbody = GetComponent<Rigidbody>();
        mFirePoint = transform.Find("FirePoint");
        mCameraTransform = transform.Find("Main Camera");
        pointsText.text = "Puntos: " + points;

        Cursor.lockState = CursorLockMode.Locked;
        gameVariables = GameVariables.instance;
        rotationYSensitivity = gameVariables.rotationYSensitivity;
        rotationXSensitivity = gameVariables.rotationXSensitivity;
    }

    private void OnEnable()
    {
        // Codigo que se ejecutara al habilitar un GO
        mInputAction.Player.Jump.performed += DoJump;
        mInputAction.Player.Jump.Enable();

        mInputAction.Player.Fire.performed += DoFire;
        mInputAction.Player.Fire.Enable();

        mViewAction = mInputAction.Player.View;
        mInputAction.Player.View.Enable();

        mMovementAction = mInputAction.Player.Movement;
        mMovementAction.Enable();

    }

    private void DoFire(InputAction.CallbackContext obj)
    {
        // Efecto de disparo
        muzzleEffect.Play();

        // Lanzar un raycast
        RaycastHit hit;

        if (Physics.Raycast(
            mFirePoint.position,
            mCameraTransform.forward,
            out hit,
            fireRange
        ))
        {
            // Hubo una colision //Debug.Log(hit.collider.name);
            GameObject nuevaExplosion = Instantiate(explosion, hit.point, transform.rotation);
            Destroy(nuevaExplosion, 1f);
            EnemyController enemyShooted = hit.collider.gameObject.GetComponent<EnemyController>();
            if (enemyShooted)
            {
                switch (enemyShooted.data.enemyName) 
                {
                    case "EnemyBig":
                        {
                            bool isDead = enemyShooted.DamageEnemy(5);
                            if (isDead)
                            {
                                // sumar 10 puntos y actualizar texto
                                points += 10;
                                pointsText.text = "Puntos: " + points; 
                            }
                            break;
                        }
                    case "EnemySmall":
                        {
                            bool isDead = enemyShooted.DamageEnemy(10);
                            if (isDead)
                            {
                                // sumar 5 puntos y actualizar texto
                                points += 5;
                                pointsText.text = "Puntos: " + points;
                            }
                            break;
                        }
                }
            }
        }

        Debug.DrawRay(mFirePoint.position,
            transform.forward * fireRange,
            Color.red,
            .25f
        );
    }

    private void OnDisable()
    {
        // Codigo que se ejecutara al deshabilitar un GO
        mInputAction.Player.Jump.Disable();
        mMovementAction.Disable();
        mInputAction.Disable();
        //mInputAction.Player.View.Disable();
    }

    private void Update()
    {
        #region Rotacion
        Vector2 deltaPos = mViewAction.ReadValue<Vector2>();
        transform.Rotate(
            deltaPos.x * rotationXSensitivity * Time.deltaTime * Vector3.up
        );

        mRotationX -= deltaPos.y * rotationYSensitivity;
        mCameraTransform.localRotation = Quaternion.Euler(
            Mathf.Clamp(mRotationX, -90f, 90f),
            0f,
            0f
        );
        #endregion

        #region Movimiento
        Vector2 movement = Vector2.ClampMagnitude(
            mMovementAction.ReadValue<Vector2>(),
            1f
        );

        mRigidbody.velocity = movement.x * transform.right * moveSpeed +
            movement.y * transform.forward * moveSpeed + 
            transform.up * mRigidbody.velocity.y;
        /*mRigidbody.velocity = new Vector3(
            movement.x * moveSpeed,
            mRigidbody.velocity.y,
            movement.y * moveSpeed
        );*/
        #endregion

        #region Salto
        if (jumpPressed && onGround)
        {
            mRigidbody.velocity += Vector3.up * jumpForce;
            jumpPressed = false;
            onGround = false;
        }
        #endregion
    }

    private void DoJump(InputAction.CallbackContext obj)
    {
        jumpPressed = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("collision");
        onGround = true;
        jumpPressed = false;
    }

}
