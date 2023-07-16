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
            CalculateScreenBounds(); // Расчет границ экрана для учета как горизонтального, так и портретного режимов
            SpawnCookies();
        }

        private void CalculateScreenBounds()
        {
            float screenAspect = (float)Screen.width / Screen.height;

            if (screenAspect >= 1f) // Если широкий экран или квадратный экран (горизонтальный режим)
            {
                screenHalfWidth = mainCamera.orthographicSize * screenAspect;
                screenHalfHeight = mainCamera.orthographicSize;
            }
            else // Если узкий экран (портретный режим)
            {
                screenHalfWidth = mainCamera.orthographicSize;
                screenHalfHeight = mainCamera.orthographicSize / screenAspect;
            }
        }

        private void SpawnCookies()
        {
            float cookieSize = cookiePrefab.GetComponent<Renderer>().bounds.size.magnitude;
            float maxCookiesPerScreen =
                Mathf.Floor(Mathf.Min(screenHalfWidth, screenHalfHeight) * 2f / (cookieSize + minDistance));

            numCookies = Mathf.RoundToInt(Mathf.Min(numCookies, maxCookiesPerScreen));

            for (int i = 0; i < numCookies; i++)
            {
                Vector2 spawnPosition = GetRandomSpawnPosition();

                GameObject cookie = Instantiate(cookiePrefab, spawnPosition, Quaternion.identity);
                cookie.transform.localScale = GetRandomScale(cookieSize);
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
            Vector3 screenPoint = mainCamera.WorldToScreenPoint(position);

            if (screenPoint.x < 0 || screenPoint.x > Screen.width || screenPoint.y < 0 || screenPoint.y > Screen.height)
                return false;

            Collider2D[] nearbyColliders = Physics2D.OverlapCircleAll(position, minDistance);

            foreach (Collider2D collider in nearbyColliders)
            {
                if (collider.CompareTag("Cookie"))
                    return false;
            }

            return true;
        }

        private Vector3 GetRandomScale(float cookieSize)
        {
            float randomScale = Random.Range(minScale, maxScale);

            float maxScreenWidth = screenHalfWidth * 2f - minDistance;
            float maxScreenHeight = screenHalfHeight * 2f - minDistance;

            float maxCookieSize = Mathf.Min(maxScreenWidth, maxScreenHeight) / numCookies;

            if (randomScale * cookieSize > maxCookieSize)
                randomScale = maxCookieSize / cookieSize;

            return new Vector3(randomScale, randomScale, 1f);
        }

        private Quaternion GetRandomRotation()
        {
            float randomAngle = Random.Range(0f, 360f);
            return Quaternion.Euler(0f, 0f, randomAngle);
        }
    }
}