using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbyssMoth
{
    using UnityEngine;

    public class CookieSpawner : MonoBehaviour
    {
        public GameObject cookiePrefab;
        public int numCookies = 7;
        public float minScale = 0.5f;
        public float maxScale = 1f;
        public float minDistance = 1f;

        private Camera mainCamera;
        private float minDistanceSquared;
        private float screenHalfWidth;
        private float screenHalfHeight;

        private void Start()
        {
            mainCamera = Camera.main;
            minDistanceSquared = minDistance * minDistance;
            CalculateScreenBounds();
            SpawnCookies();
        }

        private void CalculateScreenBounds()
        {
            float screenAspect = (float)Screen.width / Screen.height;

            if (screenAspect >= 16 / 9f) // широкий экран
            {
                screenHalfWidth = mainCamera.orthographicSize * screenAspect;
                screenHalfHeight = mainCamera.orthographicSize;
            }
            else // узкий экран
            {
                screenHalfWidth = mainCamera.orthographicSize;
                screenHalfHeight = mainCamera.orthographicSize / screenAspect;
            }
        }

        private void SpawnCookies()
        {
            for (int i = 0; i < numCookies; i++)
            {
                Vector2 spawnPosition = GetRandomSpawnPosition();

                GameObject cookie = Instantiate(cookiePrefab, spawnPosition, Quaternion.identity);
                cookie.transform.localScale = GetRandomScale(cookie.GetComponent<Renderer>().bounds.size);
                cookie.transform.rotation = GetRandomRotation();
            }
        }

        private Vector2 GetRandomSpawnPosition()
        {
            Vector2 randomPosition = new Vector2();
            bool isValidPosition = false;

            while (!isValidPosition)
            {
                randomPosition.x = Random.Range(-screenHalfWidth + minDistance, screenHalfWidth - minDistance);
                randomPosition.y = Random.Range(-screenHalfHeight + minDistance, screenHalfHeight - minDistance);

                if (IsValidPosition(randomPosition))
                    isValidPosition = true;
                else
                    AdjustCookiePosition(ref randomPosition);
            }

            return randomPosition;
        }

        private void AdjustCookiePosition(ref Vector2 position)
        {
            while (!IsValidPosition(position))
            {
                position.x += Random.Range(-minDistance, minDistance);
                position.y += Random.Range(-minDistance, minDistance);
            }
        }

        private bool IsValidPosition(Vector2 position)
        {
            Collider2D[] nearbyColliders = Physics2D.OverlapCircleAll(position, minDistance);

            foreach (Collider2D collider in nearbyColliders)
            {
                if (collider.CompareTag("Cookie"))
                    return false;
            }

            return true;
        }

        private Vector3 GetRandomScale(Vector3 cookieSize)
        {
            float randomScale = Random.Range(minScale, maxScale);

            float maxScreenWidth = screenHalfWidth * 2 - cookieSize.x;
            float maxScreenHeight = screenHalfHeight * 2 - cookieSize.y;

            float maxCookieSize = Mathf.Min(maxScreenWidth, maxScreenHeight) / numCookies;

            if (randomScale * maxCookieSize < minScale)
                randomScale = minScale / maxCookieSize;

            return new Vector3(randomScale, randomScale, 1f);
        }

        private Quaternion GetRandomRotation()
        {
            float randomAngle = Random.Range(0f, 360f);
            return Quaternion.Euler(0f, 0f, randomAngle);
        }
    }
}