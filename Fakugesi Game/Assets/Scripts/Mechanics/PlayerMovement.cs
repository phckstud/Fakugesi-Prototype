using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Unity Handles")]
    public Transform groundCheck;
    public Transform mask;
    public Vector2 initialPos;
    public Vector3 maxMaskSize, defaultMaskLocalScale;
    public Rigidbody2D rb;
    public Animator anim;
    [SerializeField] LayerMask ground;
    [SerializeField] LayerMask dryGrass, normal, space;
    private SpriteRenderer sr;
    [SerializeField]
    SpriteMaskScript[] scripts;

    [Header("Integers")]
    public int sceneBuildindex;

    [Header("Floats")]
    public float speed;
    public float jumpForce, realForce, doubleJumpMultiplier;
    public float gravity = 9.81f;
    [SerializeField] float jumpingDisabler = 0.3f, defaultJumpingDisabler;

    [Header("Better Jumping FLoats")]
    [SerializeField] float fallMultiplier = 2.5f;
    [SerializeField] float lowJumpMultipler = 2f;

    [Header("Mask Ability FLoats")]
    [SerializeField] float growMultiplier = 0.75f;
    [SerializeField] float decreaseGrowthMultiplier = 0.65f;
    [SerializeField] float rangeForMouseMove = 7f, maxDistance = 4f;

    [Header("Booleans")]
    public bool canMove;
    public bool isOldGrounded;
    public bool isJumping, isDoubleJumping, canDoubleJump;
    public bool isRespawning, facingRight, inConversation;
    public bool hasMask;

    [Header("Strings")]
    [SerializeField] string sceneName;

    [Header("Animation Strings")]
    [SerializeField] string playerIdle = "Rogue_idle_01";
    [SerializeField] string playerRun = "Rogue_run_01";
    [SerializeField] string playerJump = "Rogue_attack_02";

	private void Awake()
	{
        sceneBuildindex = SceneManager.sceneCountInBuildSettings;
        anim = GetComponent<Animator>();
    }
	void Start()
    {
        // subscribe to events
        

        initialPos = transform.position;
        defaultJumpingDisabler = jumpingDisabler;
        realForce = jumpForce;

        rb = GetComponent<Rigidbody2D>();
        canMove = true;

        if (mask != null)
            defaultMaskLocalScale = mask.localScale;

        scripts = FindObjectsOfType<SpriteMaskScript>();
    }

    private void OnDestroy()
    {
        // unsubscribe from events
       
    }

    void Update()
    {
        ConversationProbe();

        Move();

        Jump();
        GravityJump();
        LetOutmask();


        if (isGrounded())
        {
            isOldGrounded = true;
            //isJumping = false;
            isDoubleJumping = false;

            jumpForce = realForce;
        }
        else
            isOldGrounded = false;

        /*if (Input.GetKey(KeyCode.Tab))
             SceneManager.LoadScene(0);

                    canDoubleJump = false;
             isDoubleJumping = false;
             isJumping = false;
         */
    }

	private void LateUpdate()
	{
        /*/ record replay data for this frame
        ReplayData data = new PlayerReplayData(this.transform.position, isGrounded(),
            rb.velocity, sr.color.a, facingRight);
        recorder.RecordReplayFrame(data);*/
    }
	#region Movement
	void Move()
    {
        if (isRespawning)
            return;

        Vector2 move = new Vector2(InputManager.Instance.move.x * speed * Time.deltaTime, rb.velocity.y);
        

        // transform.Translate(move * speed * Time.deltaTime);
        if (canMove && GameManager.Instance.animFinished == true)
        {
            rb.velocity = move;
            UpdateFaceDirection(move);

            //Make sure player is moving
            if (InputManager.Instance.move.x == 1f || InputManager.Instance.move.x == -1f)
                anim.Play(playerRun);
            if (InputManager.Instance.move.x == 0)
                anim.Play(playerIdle);
        }

    }
    public void WalkAudio()
	{

            if (Physics2D.OverlapCircle(groundCheck.position, 0.25f, dryGrass))
                MusicManager.Instance.PlayDryGrassClip();
        if (Physics2D.OverlapCircle(groundCheck.position, 0.25f, normal))
            MusicManager.Instance.PlayNormalClip();
        if (Physics2D.OverlapCircle(groundCheck.position, 0.25f, space))
            MusicManager.Instance.PlaySpaceClip();
    }
    void UpdateFaceDirection(Vector2 dir)
	{
        if (dir.x > 0.1f)
            facingRight = true;
        else if (dir.x < -0.1f)
            facingRight = false;

        //Flip Character
        if (facingRight)
        {
            this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, 0, this.transform.eulerAngles.z);
            //this.transform.localScale = new Vector2(this.transform.localScale.x, this.transform.localScale.y);
        }
        else
        {
            this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, 180, this.transform.eulerAngles.z);
           // this.transform.localScale = new Vector2(-this.transform.localScale.x, this.transform.localScale.y);
        }
    }
	#endregion
	#region Jumping
	void Jump()
    {
        if (isGrounded() && InputManager.Instance.jump)
        {
            isJumping = true;
            canDoubleJump = true;
            InputManager.Instance.jump = false;

            MusicManager.Instance.PlayJumpClip();
            anim.Play(playerJump);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);


            jumpingDisabler = defaultJumpingDisabler;

        }

        DoubleJump();
    }

    void GravityJump()
    {
        if (rb.velocity.y < 0)
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        else if (rb.velocity.y > 0 && !isGrounded())
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultipler - 1) * Time.deltaTime;
    }

    void DoubleJump()
    {
        if (!isGrounded() && canDoubleJump)
        {
            jumpForce = realForce * doubleJumpMultiplier;

            if (InputManager.Instance.jump && isJumping)
            {


                //MusicManager.Instance.PlaySFX(MusicManager.Instance.Jump);
                MusicManager.Instance.PlayJumpClip();
                anim.Play(playerJump);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

                canDoubleJump = false;
                isDoubleJumping = true;
                isJumping = false;
                InputManager.Instance.jump = false;

                Debug.Log("Double Jump!");

                /*if (isGrounded())
                    jumpForce = 0;
                else
                    jumpForce -= gravity * Time.deltaTime;
                //*/

                //transform.position += Vector3.up * jumpForce * Time.deltaTime;

            }
        }
    }

    bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.25f, ground);
    }

    #endregion

    #region Win/Lose/Death
    private void OnGoalReached()
    {
        /*/ freeze movement
        rb.gravityScale = 0;
        rb.velocity = Vector3.zero;
        canMove = true;*/
        // hide player visual
        Debug.Log("Maybe Instantiate here!");
    }

    private void OnRestartLevel()
    {
        // ensure we don't jump right away on reset, since
        // the submit button is the same as the jump button.
        // this is specific to the InputManager in this project
        InputManager.Instance.onInteract = true;
        SceneManager.LoadScene(GameManager.Instance.sceneFailureName);
        // respawn
       
    }
    
     void Death()
    {
            transform.position = new Vector2(initialPos.x, initialPos.y + 1);
            jumpForce = 0;
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Finish")
        {
            OnRestartLevel();
            canMove = false;
            Transform done = col.gameObject.GetComponent<Transform>();
            //this.transform.position = new Vector2(done.position.x, done.position.y + postoAdd);
            rb.bodyType = RigidbodyType2D.Static;
        }

        
    }
	#endregion

	#region Dialogue

    void ConversationProbe()
    {
       inConversation = InputManager.Instance.onConversationEnter;
       
        if (inConversation)
            return;
    }

	#endregion

	#region Mask Ability
    public void LetOutmask()
	{
        if (hasMask)
        {
            if (InputManager.Instance.onMaskAbility)
            {
                Debug.Log("Mask IN USE");
                mask.gameObject.SetActive(true);

                if (mask.localScale.y <= maxMaskSize.y && mask.localScale.x <= maxMaskSize.x)
                    mask.localScale += new Vector3(mask.localScale.x, mask.localScale.y) * growMultiplier * Time.deltaTime;

				/*foreach (var item in scripts)
				{
                    if(item.distance <= maxDistance)
                         item.distance += item.distance * growMultiplier;
				}*/
            }
            else
            {
                if (mask.localScale.y > defaultMaskLocalScale.y || mask.localScale.x > defaultMaskLocalScale.x)
                    mask.localScale -= new Vector3(mask.localScale.x, mask.localScale.y) * decreaseGrowthMultiplier * Time.deltaTime;
                else
                {
                    mask.localScale = defaultMaskLocalScale;
                    mask.gameObject.SetActive(false);
                }

                /*foreach (var item in scripts)
                {
                    if (item.distance > maxDistance)
                        item.distance -= item.distance * growMultiplier;
                    else
                        item.distance = item.defaultDis;
                }*/
            }

           /* if (InputManager.Instance.click)
            {
                //looks for player input/mouse position
                Vector2 mousePos = InputManager.Instance.mouseMove;
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);
                //Debug.Log(mousePos);

                
                float dis = Vector3.Distance(transform.position, mousePos);
                //Debug.Log("Distance: " + dis);

                //Limit the distance
                if (dis < rangeForMouseMove)
                {
                    mask.position = mousePos;
                    InputManager.Instance.click = false;

                }
                InputManager.Instance.click = false;*/
            
        }
	}
	#endregion
	IEnumerator DeathRoutine()
    {
        isRespawning = true;
        rb.bodyType = RigidbodyType2D.Static;
        Death();
        yield return new WaitForSeconds(0.5f);
        rb.bodyType = RigidbodyType2D.Dynamic;
        isRespawning = false;
    }
}
