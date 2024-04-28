using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Рюкзак со всем оружием
/// Отвечает за выбранное ожужие которое нужно надеть или снять
/// Также есть 
/// </summary>
public class CharacterArsenal : MonoBehaviour
{
    /// <summary>
    /// Текущее оружие. можно взять или убрать
    /// TODO:: для разного типа вооружения должно быть место хранения, например писталет в кабуре
    /// </summary>
    public GameObject currentGun;

    public List<GameObject> WeaponList = new(); 
    private void Awake()
    {
        if (currentGun != null)
        {
            currentGun = currentGun.scene.name == null ? Instantiate(currentGun) : currentGun;
            currentGun.SetActive(false);
        }
    }
    /// <summary>
    /// Метод отвечает за подготовку текущего оружия к использованию.
    /// Он проверяет состояние текущего оружия, заряжает его, если необходимо,
    /// и устанавливает его в режим готовности к стрельбе
    /// </summary>
    public WeaponCharacteristic WeaponPreparationCurrent()
    {
        currentGun.SetActive(true);
        //TODO:: нужно установить на метста скелета, а именно в руки. 
        // но этот контроллер не отвечает за установку, только за активацию
        
        return currentGun.GetComponent<WeaponCharacteristic>();

    }
    
    /// <summary>
    /// Метод HideWeapon отвечает за убрать оружие в резерв на пояс или за спину. Он изменяет положение оружия,
    /// чтобы оно было скрыто от глаз игрока и не мешало обзору.
    /// </summary>
    public void HideWeaponCurrent()
    {
        currentGun.SetActive(false);
    }
}
