// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System.Collections.Generic;
using UnityEngine;

namespace AbyssMoth.Internal.Codebase
{
    public class CookieSpawner : MonoBehaviour
    {
        public GameObject cookiePrefab; // Префаб печенья
        public int cookieCount = 7; // Количество печенек на старте
        public float minimumDistance = 1f; // Минимальное расстояние между печеньками
        public float scaleRatio = 0.8f; // Коэффициент масштабирования печенек

        private List<GameObject> cookies = new List<GameObject>(); // Список созданных печенек

        private void Start()
        {
            SpawnCookies(cookieCount);
        }

        private void SpawnCookies(int count)
        {
            for (int i = 0; i < count; i++)
            {
                // Создаем экземпляр печенья из префаба
                GameObject cookie = Instantiate(cookiePrefab, GetRandomPosition(),
                    Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));

                // Масштабируем печенье
                ScaleCookie(cookie);

                // Добавляем печенье в список
                cookies.Add(cookie);
            }
        }

        private void ScaleCookie(GameObject cookie)
        {
            Camera mainCamera = Camera.main;
            float distance = Mathf.Min(mainCamera.orthographicSize, mainCamera.orthographicSize * mainCamera.aspect) *
                             2;
            float scaleFactor = distance / Screen.height * scaleRatio;
            cookie.transform.localScale = new Vector3(scaleFactor, scaleFactor, 1f);
        }

        private Vector3 GetRandomPosition()
        {
            Vector3 randomPosition = GetRandomScreenPoint();
            Vector3 clampedPosition =
                ClampToScreen(randomPosition, cookiePrefab.GetComponent<SpriteRenderer>().bounds.size);
            Vector3 collisionFreePosition = GetCollisionFreePosition(clampedPosition,
                cookiePrefab.GetComponent<Collider2D>(), minimumDistance);
            return collisionFreePosition;
        }

        private Vector3 GetRandomScreenPoint()
        {
            Vector3 position = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), 0f);
            Camera mainCamera = Camera.main;
            return mainCamera.ViewportToWorldPoint(position);
        }

        private Vector3 ClampToScreen(Vector3 position, Vector2 size)
        {
            Camera mainCamera = Camera.main;
            Vector3 clampedPosition = position;
            clampedPosition.x = Mathf.Clamp(clampedPosition.x,
                mainCamera.ScreenToWorldPoint(Vector3.zero).x + size.x / 2,
                mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x - size.x / 2);
            clampedPosition.y = Mathf.Clamp(clampedPosition.y,
                mainCamera.ScreenToWorldPoint(Vector3.zero).y + size.y / 2,
                mainCamera.ScreenToWorldPoint(new Vector3(0f, Screen.height, 0f)).y - size.y / 2);
            clampedPosition.z = 0f; // Установка позиции Z на 0
            return clampedPosition;
        }

        private Vector3 GetCollisionFreePosition(Vector3 originalPosition, Collider2D collider, float distance)
        {
            Vector3 position = originalPosition;
            float clearance = distance;
            float size = collider.bounds.size.x;

            while (IsOverlap(position, collider))
            {
                position += new Vector3(clearance, clearance, 0f);
                Vector3 clampedPosition = ClampToScreen(position, new Vector2(size, size));
                if (clampedPosition == position)
                {
                    break;
                }

                position = clampedPosition;
            }

            return position;
        }

        private bool IsOverlap(Vector3 position, Collider2D collider)
        {
            foreach (GameObject existingCookie in cookies)
            {
                // Проверяем перекрытие печеньек
                Collider2D existingCollider = existingCookie.GetComponent<Collider2D>();
                if (existingCollider.bounds.Intersects(collider.bounds))
                {
                    return true;
                }
            }

            return false;
        }

        public void RemoveCookie(GameObject cookie)
        {
            cookies.Remove(cookie);
            Destroy(cookie);

            if (cookies.Count == 0)
            {
                SpawnCookies(cookieCount);
            }
        }
    }
}