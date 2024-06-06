using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    [Header("--- BALL SETTINGS----")]
    [SerializeField] private GameObject[] balls;
    [SerializeField] private GameObject firePoint;
    [SerializeField] private float ballPower;
    [SerializeField] private Animator _thorowBall;
    [SerializeField] private ParticleSystem _thorowBallEffect;
    [SerializeField] private ParticleSystem[] ballExplosionEffect;

    int activeBallEffectIndex;
    int activeBallIndex;
    public int Xvalue;
    public int Yvalue;

    [Header("--- BALL SETTINGS----")]
    [SerializeField] private int targetBallCount;
    [SerializeField] private int currentBallCount;
    [SerializeField] private Slider LevelSlider;
    [SerializeField] private TextMeshProUGUI remaningBallCount;
    int thrownBallCount;

    [Header("--- UI SETTINGS----")]
    [SerializeField] private GameObject[] Paneller;
    [SerializeField] private TextMeshProUGUI starTxt;
    [SerializeField] private TextMeshProUGUI winLevelTxt;
    [SerializeField] private TextMeshProUGUI loseLevelTxt;

    [Header("--- OTHER SETTINGS----")]
    [SerializeField] Renderer Bucket;
    float bucketStartedValue;
    float bucketStepValue;
    string sceneIndex;

    [Header("--- SOUND SETTINGS----")]
    [SerializeField] AudioSource[] sounds;

    void Start()
    {
        sceneIndex = (SceneManager.GetActiveScene().buildIndex).ToString();

        activeBallEffectIndex = 0;

        bucketStartedValue = 0.5f;
        bucketStepValue = 0.25f / targetBallCount;

        LevelSlider.maxValue = targetBallCount;
        remaningBallCount.text = currentBallCount.ToString();
    }
    public void BallEntered()
    {
        sounds[2].Play();
        thrownBallCount++;
        LevelSlider.value = thrownBallCount;

        bucketStartedValue -= bucketStepValue;
        Bucket.material.SetTextureScale("_MainTex", new Vector2(1f, bucketStartedValue));


        if (thrownBallCount == targetBallCount)
        {
            //Win
            PlayerPrefs.SetInt("Star", PlayerPrefs.GetInt("Star") + 20);
            starTxt.text = (PlayerPrefs.GetInt("Star")).ToString();

            PlayerPrefs.SetInt("Level", SceneManager.GetActiveScene().buildIndex + 1);
            winLevelTxt.text = "LEVEL : " + sceneIndex;

            Time.timeScale = 0;
            sounds[1].Play();
            Paneller[1].SetActive(true);
        }

        int num = 0;
        foreach (var item in balls)
        {
            if (item.activeInHierarchy)
            {
                num++;
            }
        }

        if (num == 0)
        {
            if (currentBallCount == 0 && targetBallCount != thrownBallCount)
            {
                Lose();
            }

            if ((thrownBallCount + currentBallCount) < targetBallCount)
            {
                Lose();
            }
        }

    }
    public void BallNotEntered()
    {

        int num = 0;
        foreach (var item in balls)
        {
            if (item.activeInHierarchy)
            {
                num++;
            }
        }
        if (num == 0)
        {
            if (currentBallCount == 0)
            {
                Lose();
            }

            if ((thrownBallCount + currentBallCount) < targetBallCount)
            {
                Lose();
            }
        }
    }
    public void TopPanel(string value)
    {
        switch (value)
        {
            case "quit":
                {
                    Application.Quit();
                }
                break;
            case "paused":
                {
                    Paneller[0].SetActive(true);
                    Time.timeScale = 0;
                }
                break;
        }
    }
    public void PanelButtons(string value)
    {
        switch (value)
        {
            case "quit":
                {
                    Application.Quit();
                }
                break;

            case "resume":
                {
                    Paneller[0].SetActive(false);
                    Time.timeScale = 1;
                }
                break;

            case "replay":
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    Time.timeScale = 1;
                }
                break;

            case "next":
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                    Time.timeScale = 1;
                }
                break;
        }
    }
    public void PartcEffect(Vector3 poz, Color paint)
    {

        ballExplosionEffect[activeBallEffectIndex].transform.position = poz;

        var main = ballExplosionEffect[activeBallEffectIndex].main;
        main.startColor = paint;

        ballExplosionEffect[activeBallEffectIndex].gameObject.SetActive(true);

        activeBallEffectIndex++;

        if (activeBallEffectIndex == ballExplosionEffect.Length - 1)
            activeBallEffectIndex = 0;

    }

   public void ThrowBall()
    {
        if (Time.timeScale != 0)
        {
            
                sounds[3].Play();

                currentBallCount--;
                remaningBallCount.text = currentBallCount.ToString();

                _thorowBall.Play("TopATAR");
                _thorowBallEffect.Play();

                balls[activeBallIndex].transform.SetPositionAndRotation(firePoint.transform.position, firePoint.transform.rotation);
                balls[activeBallIndex].SetActive(true);
                balls[activeBallIndex].GetComponent<Rigidbody>().AddForce(balls[activeBallIndex].transform.TransformDirection(Xvalue, Yvalue, 0) * ballPower, ForceMode.Force);


                if (balls.Length - 1 == activeBallIndex)
                    activeBallIndex = 0;
                else
                    activeBallIndex++;
            
        }
    }
    void Lose()
    {
        Time.timeScale = 0;
        Paneller[2].SetActive(true);
        loseLevelTxt.text = "LEVEL : " + sceneIndex;
        sounds[0].Play();
    }
}

