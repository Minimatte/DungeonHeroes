using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : MonoBehaviour {

    private static Animator anim;

    public Image healthBar, manaBar, staminaBar;

    public Image offensiveSpellImage, defensiveSpellImage;
    public Text healthbarText, manaBarText, coinText, staminaText;

    public Image offensiveIcon, defensiveIcon;
    private static Image offensiveIconStatic, defensiveIconStatic;

    public GameObject heroIcon, heroIconPanel;
    private static GameObject HeroIcon, HeroIconPanel;

    public static Dictionary<Hero, HeroIcon> heroIcons = new Dictionary<Hero, HeroIcon>();


    private static UIManager instance;
    private PlayerMovement2D movement;

    void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(transform.root.gameObject);

        anim = GetComponent<Animator>();
        HeroIcon = heroIcon;
        HeroIconPanel = heroIconPanel;
        offensiveIconStatic = offensiveIcon;
        defensiveIconStatic = defensiveIcon;
    }

    void LateUpdate() {
        UpdateHealthBar();
        UpdateManaBar();
        UpdateSpellIcons();
        UpdateCoinText();
        UpdateStamina();
    }

    void UpdateStamina() {
        if (GameEvents.player != null) {
            movement = GameEvents.player.GetComponent<PlayerMovement2D>();

            if (movement.stamina == 0) {
                staminaBar.transform.parent.gameObject.SetActive(false);
              
            } else {
                staminaBar.transform.parent.gameObject.SetActive(true);

                float targetStamina = movement.currentStamina / movement.stamina;
                staminaBar.fillAmount = Mathf.MoveTowards(staminaBar.fillAmount, targetStamina, Time.deltaTime);
                staminaText.text = Mathf.MoveTowards(int.Parse(staminaText.text.Split('/')[0].Split('.')[0]), (int)movement.currentStamina, 40 * Time.deltaTime).ToString("F0") + "/" + movement.stamina;
            }
        } else {
            staminaBar.fillAmount = Mathf.MoveTowards(staminaBar.fillAmount, 0, Time.deltaTime);
            staminaText.text = "DEAD";
        }
    }

    void UpdateCoinText() {
        coinText.text = Mathf.MoveTowards(int.Parse(coinText.text), PlayerItems.gold, 4).ToString("F0");
    }

    public static void CreateHeroIcon(Sprite icon, Hero hero) {
        GameObject go = Instantiate(HeroIcon) as GameObject;
        go.GetComponent<HeroIcon>().icon = icon;
        go.transform.SetParent(HeroIconPanel.transform, false);
        heroIcons.Add(hero, go.GetComponent<HeroIcon>());
    }

    public static void UpdateHeroIcons() {
        foreach (Hero hero in heroIcons.Keys) {
            heroIcons[hero].icon = hero.headIcon;
        }
    }

    public static void SetActiveHeroIcon(Hero hero) {

        if (heroIcons.ContainsKey(hero)) {
            foreach (HeroIcon icon in heroIcons.Values)
                icon.selected = false;

            heroIcons[hero].selected = true;
        }
    }

    void UpdateHealthBar() {
        if (GameEvents.player != null) {

            Health playerHealth = GameEvents.player.GetComponent<Health>();
            float targetHP = playerHealth.currentHealth / playerHealth.maxHealth;
            healthBar.fillAmount = Mathf.MoveTowards(healthBar.fillAmount, targetHP, Time.deltaTime);
            healthbarText.text = Mathf.MoveTowards(int.Parse(healthbarText.text.Split('/')[0].Split('.')[0]), playerHealth.currentHealth, 40 * Time.deltaTime).ToString("F0") + "/" + playerHealth.maxHealth;

        } else {
            healthBar.fillAmount = Mathf.MoveTowards(healthBar.fillAmount, 0, Time.deltaTime);
            healthbarText.text = "DEAD";

        }
    }
    void UpdateManaBar() {
        if (GameEvents.player != null) {
            Mana playerMana = GameEvents.player.GetComponent<Mana>();
            float targetMana = playerMana.currentMana / playerMana.maxMana;

            manaBar.fillAmount = Mathf.MoveTowards(manaBar.fillAmount, targetMana, Time.deltaTime);
            manaBarText.text = Mathf.MoveTowards(int.Parse(manaBarText.text.Split('/')[0].Split('.')[0]), playerMana.currentMana, 40 * Time.deltaTime).ToString("F0") + "/" + playerMana.maxMana;
        } else {
            manaBar.fillAmount = Mathf.MoveTowards(manaBar.fillAmount, 0, Time.deltaTime);
            manaBarText.text = Mathf.MoveTowards(int.Parse(manaBarText.text.Split('/')[0].Split('.')[0]), 0, 40 * Time.deltaTime).ToString("F0");

        }
    }

    public static void UpdateSpellIcons() {
        if (GameEvents.player == null)
            return;

        SpellHandler spells = GameEvents.player.GetComponent<SpellHandler>();
        if (spells.offensive != null)
            offensiveIconStatic.sprite = spells.offensive.image;
        if (spells.defensive != null)
            defensiveIconStatic.sprite = spells.defensive.image;
    }

    public static void NotEnoughManaAnim() {
        anim.SetTrigger("LowMana");
    }
}
