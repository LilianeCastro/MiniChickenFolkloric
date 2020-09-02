using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum characterSpecialAttack{
    Boto, Boi, Curupira, Iara, Saci, Vitoria
}

public class GameController : MonoSingleton<GameController>
{
    [Header("HUD")]
    public Image		    imgAttack;
    public Image		    imgSpecialAttack;
    public Slider		    progressAttack;
    public Slider		    progressSpecialAttack;
    public Text			    txtScore;
    public Animator         progressAttackAnim;
    public Animator         progressSpecialAttackAnim;

    [Header("Game Config")]
    public float		    speedGame;
    public float            increaseSpeedGame;
    public float            speedShot;
    public float            increaseSpeedShot;
    public int              scoreToChangeSpeedGame;
    private float           currentSpeed;
    private float           currentSpeedShot;
    private int             score;

    [Header("Ground Config")]
    public GameObject[]		groundPrefab;
    public float		    sizeGround;

    [Header("Prefabs")]
    public GameObject[]		platformPrefab;
    public GameObject[]		collectablePrefab;
    public GameObject[]		collectablePlusPrefab;

    [Header("Prefab that depends on the skin")]
    public Image[]          imgHUDAttackPrefab;
    public Image[]          imgHUDSpecialAttackPrefab;
    public GameObject[]		specialAttackPrefab;
    public GameObject[]		vFxExplosionPrefab;

    private void Start() {
        currentSpeed = speedGame;
        currentSpeedShot = speedShot;

        imgAttack.sprite = imgHUDAttackPrefab[Player.Instance.getLayerSkin()].sprite;
        imgSpecialAttack.sprite = imgHUDSpecialAttackPrefab[Player.Instance.getLayerSkin()].sprite;

        progressAttack.value = progressAttack.maxValue;
        progressSpecialAttack.value = progressSpecialAttack.maxValue;

        Invoke("StartSound", 0.01f);
    }

    private void StartSound()
    {
        SoundManager.Instance.changeSong("InGame");
    }

    public void startGame()
    {
	    Ground.ready = true;
    }

    private void FixedUpdate() {
        if(progressAttack.value < progressAttack.maxValue)
        {
            progressAttack.value += Time.deltaTime;
        }
        if(progressSpecialAttack.value < progressSpecialAttack.maxValue)
        {
            progressSpecialAttack.value += Time.deltaTime;
        }
    }

    public int getIdSkinPlayer()
    {
        return Player.Instance.getLayerSkin();
    }

    public void updateScore(int value)
    {
        score += value;
        txtScore.text = score.ToString();
        setSpeed();
    }

    public int getScore()
    {
        return score;
    }

    public void updateProgressAttack(int value)
    {
        if(progressAttack.value != progressAttack.maxValue)
        {
            progressAttackAnim.SetTrigger("isPlus");
        }
        progressAttack.value += value;
    }

    public float getProgressAttackValue()
    {
        return progressAttack.value;
    }

    public void updateProgressSpecialAttack(int value)
    {
        if(progressSpecialAttack.value != progressSpecialAttack.maxValue)
        {
            progressSpecialAttackAnim.SetTrigger("isPlus");
        }
        progressSpecialAttack.value += value;
    }

    public float getProgressSpecialAttackValue()
    {
        return progressSpecialAttack.value;
    }

    public void instantiateObjects(Transform posSpawn, GameObject[] prefab, int size, int order)
    {
        int idChosen = Random.Range(0, size);

        GameObject platTemp = Instantiate(prefab[idChosen]);
        platTemp.transform.parent = posSpawn.transform;
        platTemp.TryGetComponent(out Renderer rend);
        rend.sortingOrder = order;
        platTemp.transform.localPosition = new Vector2(0, platTemp.transform.position.y);
    }

    public bool canSpawnAbovePercent(int percent)
    {
        return Random.Range(0, 100) < percent;
    }

    public float getSpeed()
    {
        return currentSpeed;
    }

    public float getSpeedShot()
    {
        return currentSpeedShot;
    }

    private void setSpeed()
    {
        currentSpeed = speedGame + (increaseSpeedGame * (Mathf.Floor(getScore() / scoreToChangeSpeedGame)));
        currentSpeedShot = speedShot + (increaseSpeedShot * (Mathf.Floor(getScore() / scoreToChangeSpeedGame)));
    }

    public void playFx(int idFx)
    {
        SoundManager.Instance.playFx(idFx);
    }

    public void GameOver()
    {
        GameManager.Instance.UpdateCurrentScore(score);

        MenuManager.Instance.SceneToLoad("GameOver");
        SoundManager.Instance.changeSong("GameOver");
    }

}
