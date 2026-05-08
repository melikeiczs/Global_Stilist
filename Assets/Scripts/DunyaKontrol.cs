using UnityEngine;

/// <summary>
/// Dünya objesini hem kullanıcı sürükleyerek hem de otomatik olarak döndürür.
/// Bu scripti döndürmek istediğiniz objeye (örneğin Earth) ekleyin.
/// </summary>
public class GlobeRotator : MonoBehaviour
{
    [Header("Otomatik Dönüş")]
    [Tooltip("Kullanıcı dokunmadığında dönüş hızı (derece/saniye)")]
    public float autoRotateSpeed = 10f;

    [Tooltip("Otomatik dönüş ekseni")]
    public Vector3 autoRotateAxis = Vector3.up;

    [Header("Kullanıcı Sürükleme")]
    [Tooltip("Sürükleme hassasiyeti")]
    public float dragSensitivity = 0.3f;

    [Tooltip("Bırakma sonrası atalet (0 = yok, 1 = tam atalet)")]
    [Range(0f, 0.99f)]
    public float inertiaDamping = 0.95f;

    [Header("Geçiş")]
    [Tooltip("Otomatik dönüşe geri dönüş süresi (saniye)")]
    public float returnToAutoDelay = 2f;

    [Tooltip("Otomatik dönüşe geçiş yumuşaklığı")]
    public float blendSpeed = 2f;

    // --- Private ---
    private bool isDragging = false;
    private Vector3 lastMousePosition;
    private Vector3 dragVelocity = Vector3.zero;   // Sürükleme ataleti
    private float timeSinceDrag = 0f;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        HandleInput();
        ApplyRotation();
    }

    void HandleInput()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        HandleMouseInput();
#else
        HandleTouchInput();
#endif
    }

    // ── Masaüstü / Editor ──────────────────────────────────────────────────
    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (IsPointerOverObject())
            {
                isDragging = true;
                lastMousePosition = Input.mousePosition;
                dragVelocity = Vector3.zero;
            }
        }

        if (Input.GetMouseButton(0) && isDragging)
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;
            ApplyDrag(delta);
            lastMousePosition = Input.mousePosition;
            timeSinceDrag = 0f;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }

    // ── Mobil / Dokunmatik ─────────────────────────────────────────────────
    void HandleTouchInput()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (IsPointerOverObject())
                {
                    isDragging = true;
                    lastMousePosition = touch.position;
                    dragVelocity = Vector3.zero;
                }
            }
            else if (touch.phase == TouchPhase.Moved && isDragging)
            {
                Vector3 delta = (Vector3)touch.position - lastMousePosition;
                ApplyDrag(delta);
                lastMousePosition = touch.position;
                timeSinceDrag = 0f;
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Cancelled)
            {
                isDragging = false;
            }
        }
    }

    // ── Sürükleme delta'sını rotasyona çevir ──────────────────────────────
    void ApplyDrag(Vector3 delta)
    {
        // Yatay hareket → Y ekseni dönüşü
        float rotY = -delta.x * dragSensitivity;
        // Dikey hareket → X ekseni dönüşü (dünya referansı)
        float rotX = delta.y * dragSensitivity;

        transform.Rotate(Vector3.up, rotY, Space.World);
        transform.Rotate(Vector3.right, rotX, Space.World);

        // Atalet için hız kaydet
        dragVelocity = new Vector3(rotX, rotY, 0f);
    }

    // ── Her frame dönüş uygula ────────────────────────────────────────────
    void ApplyRotation()
    {
        if (isDragging) return;

        timeSinceDrag += Time.deltaTime;

        // Atalet: bıraktıktan hemen sonra
        if (dragVelocity.magnitude > 0.01f)
        {
            transform.Rotate(Vector3.up, dragVelocity.y, Space.World);
            transform.Rotate(Vector3.right, dragVelocity.x, Space.World);
            dragVelocity = Vector3.Lerp(dragVelocity, Vector3.zero, (1f - inertiaDamping) * 60f * Time.deltaTime);
        }

        // Gecikme geçtikten sonra otomatik dönüşe yumuşak geçiş
        if (timeSinceDrag >= returnToAutoDelay)
        {
            float blendFactor = Mathf.Clamp01((timeSinceDrag - returnToAutoDelay) * blendSpeed);
            float currentSpeed = Mathf.Lerp(0f, autoRotateSpeed, blendFactor);
            transform.Rotate(autoRotateAxis.normalized, currentSpeed * Time.deltaTime, Space.World);
        }
    }

    // ── Tıklanan nokta bu obje mi? ────────────────────────────────────────
    bool IsPointerOverObject()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out RaycastHit hit) && hit.transform == transform;
    }
}
