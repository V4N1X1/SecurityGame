using UnityEngine;

public class GameTimeManager : MonoBehaviour
{
    public static GameTimeManager instance;

    [Range(0, 24)] public float currentHour = 6f;
    public float dayDuration = 120f; // 1 gün kaç saniye sürecek (örnek: 2 dakika)

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Update()
    {
        float timeProgress = Time.deltaTime / dayDuration;
        currentHour += timeProgress * 24f;

        if (currentHour >= 24f) currentHour -= 24f;
    }

    public int GetHour() => Mathf.FloorToInt(currentHour);
    public float GetTime() => currentHour;
}
