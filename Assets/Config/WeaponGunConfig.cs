using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;

[CreateAssetMenu(fileName = "Config", menuName = "ScriptableObjects/WeaponConfig", order = 1)]
public class WeaponGunConfig : ScriptableObject
    {
        public Bullet bulletPrefab;
        public float impulse;
        public float shootDelay;
    }
