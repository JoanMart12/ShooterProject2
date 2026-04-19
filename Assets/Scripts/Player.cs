using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float playerSpeed;
    private float horizontalInput;
    private float verticalInput;

    private float horizontalScreenLimit = 9.5f;
    private float verticalScreenLimit = 3.5f;

    public GameObject bulletPrefab;

    public float normalFireRate = 0.5f;
    public float rapidFireRate = 0.1f;

    private float currentFireRate;
    private float nextFireTime;

    public AudioClip powerUpSound;
    public AudioClip powerDownSound;

    private AudioSource audioSource;

    void Start()
    {
        playerSpeed = 6f;
        transform.position = new Vector3(0, -2f, 0);

        currentFireRate = normalFireRate;
        nextFireTime = 0f;

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Movement();
        Shooting();
    }

    void Shooting()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time > nextFireTime)
        {
            Instantiate(bulletPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            nextFireTime = Time.time + currentFireRate;
        }
    }

    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * Time.deltaTime * playerSpeed);

        if (transform.position.x > horizontalScreenLimit)
        {
            transform.position = new Vector3(-horizontalScreenLimit, transform.position.y, 0);
        }
        else if (transform.position.x < -horizontalScreenLimit)
        {
            transform.position = new Vector3(horizontalScreenLimit, transform.position.y, 0);
        }

        if (transform.position.y > 0f)
        {
            transform.position = new Vector3(transform.position.x, 0f, 0);
        }

        if (transform.position.y < -verticalScreenLimit)
        {
            transform.position = new Vector3(transform.position.x, -verticalScreenLimit, 0);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RapidFire"))
        {
            currentFireRate = rapidFireRate;

            audioSource.PlayOneShot(powerUpSound);

            Destroy(other.gameObject);

            CancelInvoke("ResetFireRate");
            Invoke("ResetFireRate", 5f);

            Debug.Log("Rapid fire collected!");
        }
    }

    void ResetFireRate()
    {
        currentFireRate = normalFireRate;

        audioSource.PlayOneShot(powerDownSound);

        Debug.Log("Rapid fire ended!");
    }
}