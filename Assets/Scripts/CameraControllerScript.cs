using UnityEngine;
using System.Collections;

public class CameraControllerScript : MonoBehaviour
{
	public float spinSpeed = 0.5f;
	float distance = 10f;

	Vector3 pos = Vector3.zero;
	public Vector2 mouse = Vector2.zero;

	void Start()
	{
		mouse.x = -0.5f;
		mouse.y = 0.5f;
	}

	void Update()
	{
		if (PlayerLevel.isGameOver)	return;

		// マウスの移動の取得
		if (Input.GetMouseButton (0)) {
			mouse += new Vector2 (Input.GetAxis ("Mouse X"), Input.GetAxis ("Mouse Y")) * Time.deltaTime * spinSpeed;
		}

		mouse.y = Mathf.Clamp(mouse.y, -0.3f + 0.5f, 0.3f + 0.5f);

		// 球体位座標変換
		pos.x = distance * Mathf.Sin(mouse.y * Mathf.PI) * Mathf.Cos(mouse.x * Mathf.PI);
		pos.y = -distance * Mathf.Cos(mouse.y * Mathf.PI);
		pos.z = -distance * Mathf.Sin(mouse.y * Mathf.PI) * Mathf.Sin(mouse.x * Mathf.PI);

		// 座標の更新
		transform.LookAt(pos + this.transform.position);
	}

}
