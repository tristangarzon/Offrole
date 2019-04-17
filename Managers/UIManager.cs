using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance;

    public ChampionStats CurrentPlayer;


    public Text TimeText;
    public Text RedKills;
    public Text BlueKills;

    public Text PlayerScore;
    public Text PlayerMinionScore;


    public Text PlayerHealth;
    public Text PlayerResource;
    public Text PlayerLevel;
    public Text PlayerGold;

    public Text PlayerHealthRegen;
    public Text PlayerResourceRegen;


    public Image PlayerHealthImage;
    public Image PlayerResourceImage;
    public Image PlayerXP;

    public List<GameObject> SpellSlots;



    private void Awake()
    {
        if (Instance != this)
            Instance = this;


    }


    private void Start()
    {
        CurrentPlayer = InGameManager.Instance.Teams[InGameManager.Instance.CurrentPlayerTeamId].Champions[0];
       // CurrentPlayer = UIManager.Instance.CurrentPlayer;
        UpdateSpellSlots();

        CurrentPlayer.Stats[GameConsts.STAT_HEALTH_REGEN].statText = PlayerHealthRegen;
        CurrentPlayer.Stats[GameConsts.STAT_RESOURCE_REGEN].statText = PlayerResourceRegen;

    }

    void Update()
    {
        System.TimeSpan t = System.TimeSpan.FromSeconds(InGameManager.Instance.GameTime);
        TimeText.text = string.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);

        RedKills.text = "" + InGameManager.GetTeamKills(0);
        BlueKills.text = "" + InGameManager.GetTeamKills(1);

        //* Index should always be zero*//

        PlayerScore.text = string.Format("{0}/{1}/{2}",
            CurrentPlayer.Kills,
            CurrentPlayer.Deaths,
            CurrentPlayer.Assist);
        PlayerMinionScore.text = CurrentPlayer.MinionScore.ToString();


        PlayerHealth.text = CurrentPlayer.Health.ToString();
        PlayerResource.text = CurrentPlayer.Resource.ToString();
        PlayerLevel.text = CurrentPlayer.Level.ToString();
        PlayerGold.text = CurrentPlayer.Gold.ToString();

        PlayerHealthRegen.text = CurrentPlayer.Stats[GameConsts.STAT_HEALTH_REGEN].Curr.ToString();
        PlayerResourceRegen.text = CurrentPlayer.Stats[GameConsts.STAT_RESOURCE_REGEN].Curr.ToString();


        PlayerHealthImage.fillAmount = CurrentPlayer.Health.Percent(false);
        PlayerResourceImage.fillAmount = CurrentPlayer.Resource.Percent(false);
        PlayerXP.fillAmount = CurrentPlayer.Level.Percent(true);






    }

    void UpdateSpellSlots()
    {
        for (int i = 0; i < SpellSlots.Count; i++)
        {
            CurrentPlayer.SpellSlots[i].Icon = SpellSlots[i].transform.GetChild(0).GetComponent<Image>();
            CurrentPlayer.SpellSlots[i].CoolDown = SpellSlots[i].transform.GetChild(1).GetComponent<Image>();
            CurrentPlayer.SpellSlots[i].KeyName = SpellSlots[i].transform.GetChild(2).GetComponent<Text>();

        }
    }

}
