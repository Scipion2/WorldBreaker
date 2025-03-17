using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    [Header("Components")]
    [Space(10)]

        [SerializeField] private Material[] BallMaterials;
        [SerializeField] private SpriteRenderer ballRenderer;
        [SerializeField] private AudioSource hitAudio;
        [SerializeField] private Rigidbody2D body; 
        [SerializeField] private Ball ballPrefab;
        [SerializeField] public Colormodifier ColorBall;
        [SerializeField] private float AnglePower=10f;

    [Header("Physics Data")]
    [Space(10)]

        public float launchSpeed = 1.0f;
        public float maxSpeed = 255f;
        public float BallSize=0.2f;

    [Header("Audio Data")]
    [Space(10)]

        public AudioClip[] hitSounds;
        public AudioClip kickSound;

    [Header("Game Data")]
    [Space(10)]

        private bool needKickOff = true;
        private bool isActiveBall=false;
        private int numberOfBalls=2;
        private float splitAngle=15f;


    //SETTERS
        public void SetActiveBall(){isActiveBall=true;}//Setter For isActiveBall

    //ESSENTIALS

        public void Start()
        {

            body.simulated=false;
            //numberOfBalls=Random.Range(2,6);

        }
    
        void FixedUpdate()
        {
            // Check if game is on
            if (isActiveBall == false)
            {
                // Equivalent to Rigidbody.isKinematic=true, but rigidbody2D works a bit differently!
                body.simulated = false;
            }
            // needKickOff is on by default and after reset 
            else if (needKickOff == true && Input.GetButton("Jump"))
            {
                KickBall();
            }

            // limit the velocity
            if (body.linearVelocity.magnitude > maxSpeed)
            {
                body.linearVelocity = body.linearVelocity.normalized * maxSpeed;
            }

        }

        // Called if this collided with another collider
        void OnCollisionEnter2D(Collision2D other)
        {
            // Check if the Audiosource is not null and avoid a call on start up as the ball is on the paddle 
            if (hitAudio && GameManager.instance.GetIsAbleToPlay())
            {
                // Plays a random clip to make it less repetitive
                int randomNumber = Random.Range(0, hitSounds.Length);
                hitAudio.clip = hitSounds[randomNumber];
                hitAudio.Play();
            }

            if(other!=null)
            {

                float angle=Vector2.SignedAngle(this.transform.position,other.gameObject.transform.position);
                body.AddForce(Vector2.right*angle*AnglePower);

            }

            /*if(other!=null && other.gameObject.tag=="MultiBall")
            {

                for (int i = 0; i < numberOfBalls; i++)
                {
                    // Calculer l'angle de direction pour chaque balle
                    float angle = i  * splitAngle - (splitAngle * (numberOfBalls - 1) / 2);
                    Vector2 direction = new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle));
                    
                    Ball newball = Instantiate(ballPrefab, transform.position, Quaternion.identity);
                    newball.GetComponent<Rigidbody2D>().linearVelocity = direction * 5f; // Vitesse de la balle (ajuste selon ton besoin)
                }

                // Détruire la balle actuelle après qu'elle ait cassé la brique
                //GameManager.instance.PlayerBalls.Remove(this);
                Destroy(gameObject);

            }*/

        }

    //GAME'S UTIL

        // Called on start up and after Reset
        public void KickBall()
        {
            if (kickSound)
            {
                hitAudio.clip = kickSound;
                hitAudio.Play();
            }
            // remove the ball from the paddle parent
            body.transform.parent = null;
            // restore physics
            body.simulated = true;
            // give a new velocity to the ball
            body.linearVelocity = Vector2.up*launchSpeed;
            // Make sure we can kick only once
            needKickOff = false;
        }

        // Called by DeadZone.cs
        public void ResetBall()
        {

            if(GameManager.instance.LoseLive())
            {

                needKickOff = true;
                body.simulated=false;
                ballRenderer.material=GetBallMaterial();
                Transform padle=GameManager.instance.GetPadleTransform();
                this.transform.position=new Vector3(padle.position.x,padle.position.y+BallSize,padle.position.z);
                this.transform.SetParent(padle);

            }
                
        }

        public Material GetBallMaterial()
        {

            return BallMaterials[Random.Range(0,BallMaterials.Length)];

        }

}