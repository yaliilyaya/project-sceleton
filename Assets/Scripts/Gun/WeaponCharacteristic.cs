using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCharacteristic : MonoBehaviour
{
    // Объект пули которая летит в цель
    public GameObject BulletPrefab;
    // Количество пуль в обойме
    public int Clip = 15;
    public int RemainingBullets = 15;
    public float SpeedShot = .2f;
    private float DelayShot = 0;
    public bool IsReadyShot { get; private set; }

    public GameObject BulletStartPosition;

    public GameObject FirstHandPlace;
    public GameObject SecondHandPlace;

    void Awake()
    {
        if (BulletPrefab == null)
        {
            BulletPrefab = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Destroy(BulletPrefab.GetComponent<Collider>());
            BulletPrefab.SetActive(false);
            BulletPrefab.AddComponent<BulletShot>();
            BulletPrefab.transform.localScale = Vector3.one * .05f;
        }

        IsReadyShot = true;
    }

    private void Update()
    {
        if (!IsReadyShot)
        {
            DelayShot += Time.deltaTime;
            if (DelayShot > SpeedShot)
            {
                IsReadyShot = true;
                DelayShot = 0;
            }
        }
        
        var startPosition = BulletStartPosition ? BulletStartPosition.transform.position : transform.position;

        Debug.DrawRay(startPosition, transform.forward);
        // Shot();
    }

    bool Shot()
    {
        return ShotRay(transform.forward);
    }

    //Выстрел
    // Не стриляет если нет патрон
    // Не стриляет если не готов для выстрела
    bool ShotRay(Vector3 ray)
    {
        if (!IsReadyShot)
        {
            return false;
        }

        if (RemainingBullets <= 0)  
        {
            return false;
        }
        
        IsReadyShot = false;
        RemainingBullets--;
        
        var startPosition = BulletStartPosition ? BulletStartPosition.transform.position : transform.position;

        var bullet = Instantiate(BulletPrefab, startPosition, transform.rotation);
        bullet.SetActive(true);

        return true;
    }
    
    void Recharge()
    {
        RemainingBullets = Clip;
        IsReadyShot = true;
    }
}
