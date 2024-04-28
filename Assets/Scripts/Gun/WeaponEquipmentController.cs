using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс WeaponEquipmentController отвечает за управление экипировкой оружия и выполнение действий с ним. Этот класс
/// предоставляет методы для экипировки оружия, выполнения выстрела по цели и проигрывания анимации выстрела. Он также
/// может обрабатывать различные состояния оружия и обеспечивать взаимодействие с другими системами игры
///
/// TODO:: Нужно ли делать контроллер один или же для каждого оружия разные контроллеры ?
/// Контроллер писталета, автомата, одноручного оружия
/// </summary>
public class WeaponEquipmentController : MonoBehaviour
{
    public GameObject RightHand;
    public GameObject LeftHand;

    private CharacterArsenal Arsenal;
    
    
    private void Awake()
    {
        Arsenal = GetComponent<CharacterArsenal>();
        
    }

    private void Start()
    {
        WeaponPreparation();
    }

    /// <summary>
    /// Метод отвечает за подготовку оружия к использованию.
    /// Он проверяет состояние оружия, заряжает его, если необходимо,
    /// и устанавливает его в режим готовности к стрельбе
    /// </summary>
    public void WeaponPreparation()
    {
        var weapon = Arsenal.WeaponPreparationCurrent();

        weapon.transform.parent = RightHand.transform;
        weapon.transform.position = Vector3.zero;
      
        
        // weapon.FirstHandPlace.transform.position = RightHand.transform.position;
        // weapon.FirstHandPlace.transform.eulerAngles = RightHand.transform.eulerAngles;

        //TODO:: нужно проиграть анимацию как достаёт оружие из резерва
    }
    
    /// <summary>
    /// Метод отвечает за убрать оружие в резерв на пояс или за спину. Он изменяет положение оружия,
    /// чтобы оно было скрыто от глаз игрока и не мешало обзору.
    /// </summary>
    public void HideWeapon()
    {
        Arsenal.HideWeaponCurrent();   
    }


}
