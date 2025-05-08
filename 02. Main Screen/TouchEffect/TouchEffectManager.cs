using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchEffectManager : MonoBehaviour
{
    [SerializeField] GameObject effectPrefab;
    [SerializeField] Transform effectParent;
    [SerializeField] Camera playerCam;
    [SerializeField] Canvas canvas;

    float spawnTimer = 0f;
    [SerializeField] float spawnCooldown = 0.1f; // 이펙트 생성 간격

    void Start()
    {
        ObjectPoolManager.instance.Init(PoolKey.TouchEffect, effectParent, effectPrefab);
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            spawnTimer += Time.deltaTime;

            if (spawnTimer >= spawnCooldown)
            {
                Vector2 mousePos = Input.mousePosition;
                GameObject effect = ObjectPoolManager.instance.ShowObjectPool(PoolKey.TouchEffect, Vector3.zero, Quaternion.identity);
                effect.transform.position = mousePos;

                spawnTimer = 0f;
            }
        }
    }
}
