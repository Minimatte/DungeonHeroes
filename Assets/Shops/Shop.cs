using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;

public class Shop : WindowPopup {

    [Multiline]
    public string signText;

    public Buyable buyableObject;

    public int cost;
    private bool playerInRange = false;
    private bool hasBought = false;
    public bool buyable {
        get { return cost <= PlayerItems.gold && playerInRange && !hasBought; }
    }

    void Start() {
        if (buyableObject.GetType() == typeof(BuyableHero) || buyableObject.GetType() == typeof(Heart))
            cost = buyableObject.cost;

    }

    void Update() {
        if (Input.GetButtonDown("Interact")) {
            if (buyable) {
                buyableObject.Buy();
                PlayerItems.gold -= cost;
                CloseShop();
                hasBought = true;
                GameSaver.SaveGame();
            }
        }
    }

    protected override void OnEnter(Collider2D collision) {

        if (hasBought)
            return;

        if (buyableObject.GetType() == typeof(BuyableHero))
            signText = GetHeroText();

        if (buyableObject.GetType() == typeof(Heart))
            signText = GetHeartText();

        base.OnEnter(collision);

        if (collision.CompareTag("Player") && !playerInRange) {
            playerInRange = true;
            textbox.GetComponentInChildren<Text>().text = signText;

        }
    }

    private void CloseShop() {
        Destroy(textbox);
        textbox = null;
    }

    protected override void OnExit(Collider2D collision) {

        base.OnExit(collision);

        if (collision.CompareTag("Player") && playerInRange) {
            playerInRange = false;

            CloseShop();

        }
    }

    private string GetHeartText() {
        string text = "";
        StringBuilder sb = new StringBuilder(text);

        sb.AppendLine("Heart");
        sb.AppendLine("Heals for " + ((Heart)buyableObject).HealthAmount);
        sb.AppendLine(" ");
        sb.AppendLine("Cost " + cost + " Coins");
        return sb.ToString();
    }

    private string GetHeroText() {
        string text = "";
        StringBuilder sb = new StringBuilder(text);
        BuyableHero hero = (BuyableHero)buyableObject;
        if (PlayerHeroes.FullHeroes) {
            sb.AppendLine("Name:" + hero.hero.heroName);
            sb.AppendLine("Upgrades:" + hero.hero.upgrade);
            sb.AppendLine("Amount:" + hero.hero.upgradeAmount);

            sb.AppendLine(" ");
            sb.AppendLine("Cost:" + cost + " Coins");
        } else {
            sb.AppendLine("Name:" + hero.hero.heroName);
            sb.AppendLine("Health:" + hero.hero.health);
            sb.AppendLine("Mana:" + hero.hero.mana);
            sb.AppendLine("Power:" + hero.hero.power);

            sb.AppendLine(" ");
            sb.AppendLine("Cost:" + cost + " Coins");
        }

        return sb.ToString();

    }

}
