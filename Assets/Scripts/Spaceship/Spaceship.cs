using GameManagers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Spaceship
{
    public class Spaceship : MonoBehaviour
    {
        [SerializeField] int health = 10;
        [SerializeField] float velocity = 9;
        [SerializeField] [AssetsOnly] GameObject bullet;
        [SerializeField] Transform bulletAnker;
        [SerializeField] float shootingInterval = 0.3f;
        [SerializeField, AssetsOnly, Required] GameObject destroyEffects;

        Vector3 _bulletStartPos;
        float _currentInterval;
        Rigidbody2D _rb;
        int _colObjID;
        GameManager _gameManager;

        void Start()
        {
            _gameManager = FindObjectOfType<GameManager>();
            _rb = GetComponent<Rigidbody2D>();
            _bulletStartPos = bulletAnker.transform.position;
        }

        void Update()
        {
            Movement();

            Shoot();
        }

        void OnDestroy()
        {
            if (GameManager.IsSceneUnloading) return;

           var go = Instantiate(destroyEffects);
           Destroy(go, 3);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            var collidingObject = other.gameObject;

            if (collidingObject.GetInstanceID()==_colObjID) return; // Same Object collided twice

            _colObjID = collidingObject.GetInstanceID();
            
            if (collidingObject.layer==11) return;
            
            
            //Debug.Log("Collision with SpaceShip");

                health -= 1;
                if (health <= 0)
                {
                    //Debug.Log("Dead, Spaceship has no health anymore");
                    FindObjectOfType<GameManager>()?.DecreaseLives();
                    //Destroy(gameObject);
                    //TODO: Flacker and be immutable for few seconds
                    if (GameManager.CurrentLives <=0)
                    {
                        Destroy(gameObject);
                    }
                }
        }

        void Shoot()
        {
            //Single Shot
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _bulletStartPos = bulletAnker.transform.position;
                GenerateBullet();
            }

            //Static Shooting
            if (Input.GetKey(KeyCode.Space))
            {
                if (_currentInterval > 0)
                {
                    _currentInterval -= Time.deltaTime;
                }
                else
                {
                    _bulletStartPos = bulletAnker.transform.position;
                    GenerateBullet();
                    _currentInterval = shootingInterval;
                }
            }
        }

        void GenerateBullet()
        {
            Instantiate(bullet, _bulletStartPos, Quaternion.identity);
        }


        void Movement()
        {
            // Left and Up
            if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W))
            {
                _rb.MovePosition(_rb.position + new Vector2(-1, 1) * (velocity * Time.deltaTime));
            }

            // Left and Down
            else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S))
            {
                _rb.MovePosition(_rb.position + new Vector2(-1, -1) * (velocity * Time.deltaTime));
            }

            // Right and Up
            else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W))
            {
                _rb.MovePosition(_rb.position + new Vector2(1, 1) * (velocity * Time.deltaTime));
            }

            // Right and Down
            else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S))
            {
                _rb.MovePosition(_rb.position + new Vector2(1, -1) * (velocity * Time.deltaTime));
            }

            //Left
            else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
                _rb.MovePosition(_rb.position + new Vector2(-1, 0) * (velocity * Time.deltaTime));

            // Right
            else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                _rb.MovePosition(_rb.position + new Vector2(1, 0) * (velocity * Time.deltaTime));

            // Up
            else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
                _rb.MovePosition(_rb.position + new Vector2(0, 1) * (velocity * Time.deltaTime));

            // Down
            else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
                _rb.MovePosition(_rb.position + new Vector2(0, -1) * (velocity * Time.deltaTime));
        }
    }
}