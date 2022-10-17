using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] float bulletSpeed;
	[SerializeField] GameObject explosion;
	[SerializeField] Rigidbody2D rb;
	public bool bulletToRight = true;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();

		if (bulletToRight)
		{
			this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, 0, this.transform.eulerAngles.z);
			rb.velocity = Vector2.right * bulletSpeed * Time.deltaTime;
		}
		else
		{
			this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, 180, this.transform.eulerAngles.z);
			rb.velocity = (-1 * Vector2.right) * bulletSpeed * Time.deltaTime;
		}
	}


	private void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.CompareTag("Enemy"))
		{
			Instantiate(explosion, col.gameObject.transform.position, Quaternion.identity);
			Destroy(col.gameObject);
			Destroy(gameObject);
		}
	}
}

/*

Copyright (C) Nhlanhla 'Stud' Langa

*/