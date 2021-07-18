using UnityEngine;

public class Controller : MonoBehaviour
{
    private Rigidbody rb;
    private float initialForce;
    [SerializeField]
    private float force = 0f;
    [SerializeField]
    private float speed = 300f;
    [SerializeField]
    private float maxForce;
    private float lastLeapForce;
    private bool onGround = true;
    private Quaternion startingRotation;
    private bool gameStarted = false;
    private PowerBar powerBar;
    private PlayerSounds playerSounds;
    ParentPlayerRotation parentPlayerRotation;
    CheckpointReset checkPointReset;
    private LilyPad stuckTo;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        powerBar = GetComponent<PowerBar>();
        initialForce = force;
        parentPlayerRotation = transform.parent.GetComponent<ParentPlayerRotation>();
        checkPointReset = GameObject.Find("CheckpointReset").GetComponent<CheckpointReset>();
        playerSounds = GetComponent<PlayerSounds>();
        startingRotation = transform.rotation;
    }
    private void Update()
    {
        if (!gameStarted)
        {
            return;
        }
        if (onGround)
        {
            JumpingMovement();
            GroundedMovement();
        }
        else
        {
            if (transform.position.y <= -1.5 && !fadingOut)
            {
                    //Respawn function is controlled by fadeout script. (Respawn after faded out) 
                FadeOut(1f);
                playerSounds.PlayDeathSound();
                
            }
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            FadeOut(1f);
        }

        if (Input.GetKeyUp(KeyCode.Escape)){
            Application.Quit();
        }
    }

        //bool ensures the fade out script can only run once 
    bool fadingOut = false;
    private void FadeOut(float time)
    {
        FadePanel.Instance.FadeOut(time);
        fadingOut = true;
    }

    public void Respawn()
    {
        fadingOut = false;
        if (checkPointReset)
        {
           FadePanel.Instance.FadeIn(0.3f);
            checkPointReset.ResetScene();
            
        }
    }

    private void GroundedMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector3 moveDirection = transform.right * x + transform.forward * y;
        Vector3 moveForce = moveDirection * speed;
        

        rb.AddForce(moveForce * Time.deltaTime, ForceMode.Acceleration) ;
    }
    private void JumpingMovement()
    {
        if ((Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) && onGround && force < maxForce)
        {
            force += Time.deltaTime * 175;
            playerSounds.PlayPowerBarSound();
            powerBar.UpdateScreenHazeAlpha(force / maxForce);
            return;
        }
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
        {

            Leap();
            powerBar.ResetPowerBar();
        }
    }

    private void Leap()
    {
        onGround = false;
        rb.angularVelocity = new Vector3(0f, 0f, 0f);
        rb.velocity = new Vector3(0f, 0f, 0f);
        LeapForce(force);
        lastLeapForce = force;
        force = initialForce;
        playerSounds.StopPowerBarSound();
    }
    public void LeapForce(float force)
    {
        rb.AddForce(((Camera.main.transform.forward) * (force / 3)),ForceMode.Impulse);
        rb.AddForce(Vector3.up * force, ForceMode.Impulse);
    }
    public void UnSitck(LilyPad lilyPad)
    {
        powerBar.ResetPowerBar();
        if (stuckTo == lilyPad)
        {
            onGround = false;
            parentPlayerRotation.SetShouldRotate(false);
            rb.useGravity = true;
        }
    }
    public void Stick(LilyPad stuckObj, float rotationRate)
    {
        stuckTo = stuckObj;
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        onGround = true;
        UpdateParentRotation(true, rotationRate);
    }
    private void UpdateParentRotation(bool shouldRotate, float rotationRate)
    {
        parentPlayerRotation.SetRotateRate(rotationRate);
        parentPlayerRotation.SetShouldRotate(shouldRotate);
    }
    public void UpdateRotation(Quaternion _rotation)
    {
        this.transform.localRotation = _rotation;
    }

    public Rigidbody GetRigidbody()
    {
        return this.rb;
    }
    public float GetLastLeapForce()
    {
        return this.lastLeapForce;
    }
    public void SetGameStarted()
    {
        this.gameStarted = true;
    }
    public bool GetOnGround()
    {
        return this.onGround;
    }
}
