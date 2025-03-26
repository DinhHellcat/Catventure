using System.Collections.Generic;
using UnityEngine;
using System.Collections; // Thêm để dùng Coroutine

public class Trampoline : MonoBehaviour
{
    private float bounce = 20f;
    private bool isEnabled = true; // Biến kiểm soát chức năng bật nhảy

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && isEnabled)
        {
            // Thực hiện bật nhảy
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounce, ForceMode2D.Impulse);
            AudioManager.Instance.PlayTrampolineSound();
            // Bắt đầu vô hiệu hóa tạm thời
            StartCoroutine(DisableBounceTemporarily());
        }
    }

    private IEnumerator DisableBounceTemporarily()
    {
        isEnabled = false; // Tắt chức năng bật nhảy
        yield return new WaitForSeconds(0.2f); // Chờ 1 giây
        isEnabled = true; // Bật lại chức năng
    }
}