using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField]
    private Rect[] cameraBounds;

    [SerializeField]
    private Rect[] cameraSecretBounds;

    [SerializeField]
    private Transform playerTransform;

    [SerializeField]
    private Camera myCamera;

    private Vector3 oldPos;

    private bool followingPlayer;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
        TransformToPlayerPos();

        if (cameraBounds.Length != 0 || cameraSecretBounds.Length != 0)
        {
            Bounds camBounds = GetCameraBounds(myCamera);
            foreach (var item in cameraBounds)
            {
                if (item.xMin < camBounds.min.x &&
                    item.xMax > camBounds.max.x &&
                    item.yMin < camBounds.min.y &&
                    item.yMax > camBounds.max.y)
                {
                    return;
                }
            }
            transform.position = oldPos;
            followingPlayer = false;
        }

    }

    /// <summary>
    ///     Asigna la transformación de la camara, a la del jugador
    /// </summary>
    void TransformToPlayerPos()
    {
        Vector3 pos;

        if (followingPlayer)
        {
            pos = playerTransform.position;
        }
        else
        {
            float t = Vector3.Distance(transform.position, playerTransform.position);
            Debug.Log("t in Lerp = " + t);
            pos = Vector3.Lerp(transform.position, playerTransform.position, 0.2f);
            if (t < 10.005f)
            {
                followingPlayer = true;
            }
        }
        pos.z = -10f;
        oldPos = transform.position;
        transform.position = pos;
    }

    /// <summary>
    ///     Obtiene los límites de la camara
    /// </summary>
    /// <param name="camera"></param>
    /// <returns>Obtiene los límites de la camara</returns>
    Bounds GetCameraBounds(Camera camera)
    {
        float aspect = (float)Screen.width / Screen.height;
        float height = camera.orthographicSize * 2;

        return new Bounds(transform.position, new Vector3(height * aspect, height, -10));
    }
}
