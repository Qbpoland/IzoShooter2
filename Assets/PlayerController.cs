using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    float maxHp = 10;
    float currentHp;
    public GameObject bulletPrefab;
    public float bulletSpeed = 20;
    public float playerSpeed = 2;
    Vector2 movementVector;
    Transform bulletSpawn;
    public GameObject hpBar;
    Scrollbar hpScrollBar;
    public float healingAmount = 2;

    void Start()
    {
        movementVector = Vector2.zero;
        bulletSpawn = transform.Find("BulletSpawn");
        currentHp = maxHp;
        hpScrollBar = hpBar.GetComponent<Scrollbar>();
        hpScrollBar.size = currentHp / maxHp;
    }

    void Update()
    {
        //obrót wokó³ osi Y o iloœæ stopni równ¹ wartoœci osi X kontrolera
        transform.Rotate(Vector3.up * movementVector.x);
        //przesuniêcie do przodu (transform.forward) o wychylenie osi Y kontrolera w czasie jednej klatki
        transform.Translate(Vector3.forward * movementVector.y * Time.deltaTime * playerSpeed);
    }

    void OnMove(InputValue inputValue)
    {
        movementVector = inputValue.Get<Vector2>();
    }

    void OnFire()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn);
        bullet.transform.parent = null;
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * bulletSpeed, ForceMode.VelocityChange);
        Destroy(bullet, 5);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            currentHp--;
            if (currentHp <= 0) Die();
            hpScrollBar.size = currentHp / maxHp;
            Vector3 pushVector = collision.gameObject.transform.position - transform.position;
            collision.gameObject.GetComponent<Rigidbody>().AddForce(pushVector.normalized * 5, ForceMode.Impulse);

        }
        else if (collision.gameObject.CompareTag("Heal"))
        {
            currentHp += healingAmount; 
            hpScrollBar.size = currentHp / maxHp;
            Destroy(collision.gameObject);
        }
    }

    void Die()
    {
        GetComponent<BoxCollider>().enabled = false;
        transform.Translate(Vector3.up);
        transform.Rotate(Vector3.right * -90);
        //Time.timeScale = 0;
    }

    public void Heal(float healingAmount)
    {
        currentHp += healingAmount;
        hpScrollBar.size = currentHp / maxHp;
    }
}
