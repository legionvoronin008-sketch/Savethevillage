using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Timer Harvest_Timer;
    public Timer Eating_Timer;
    public Timer Raid_Timer;
    public Image Villager_Timer;
    public Image Warriors_Timer;
    public Image Raid_TTimer;
    public Image Raid_TTTimer;


    public Button villagerButton;
    public Button warriorButton;

    public Text village_resourcesText;
    public Text warriors_resourcesText;
    public Text wheat_resourcesText;
    public Text Raid_NumberText;
    public Text Raid_Survived_Text;
    public Text WRaid_Survived_Text;
    public Text Overall_WheatText;
    public Text Overall_VillagersText;
    public Text Overall_WarriorsText;
    public Text Overall_EnemiesText;
    public Text WOverall_Wheat;
    public Text WOverall_Villagers;
    public Text WOverall_Warriors;
    public Text WOverall_Enemies;

    public int villagerCount;
    public int warriorsCount;
    public int wheatCount;

    public int wheatPerVillager;
    public int wheatToWarrior;
    public int Overall_Wheat;
    public int Overall_Villagers;
    public int Overall_Warriors;
    public int Overall_Enemies;

    public int VillagerCost;
    public int WarriorCost;

    public float VillagerCycleTime;
    public float WarriorCycleTime;
    public float RaidMaxTime;

    public int raidIncrease;
    public int nextRaid;
    public int Winning_condition;
    public GameObject Loose_Screen;
    public GameObject Win_Screen;

    private float VillagerCycle = -2;
    private float WarriorCycle = -2;
    private float RaidTimer;
    private int Raid_Survived_count = 0;
    private int WarriorsEat;

    public AudioSource click;
    public AudioSource WarriorsS;
    public AudioSource VillagersS;
    public AudioSource RaidS;
    public AudioSource WinS;
    public AudioSource LooseS;
    public AudioSource BackgroundS;

    public Button Pause;
    public Button SaundOFF;


    void Start()
    {
        UpdateText();
        RaidTimer = RaidMaxTime;
    }

    void Update()
    {
        RaidTimer -= Time.deltaTime;
        Raid_TTimer.fillAmount = RaidTimer / RaidMaxTime;
        Raid_TTTimer.fillAmount = RaidTimer / RaidMaxTime;
        if (RaidTimer <= 0)
        {
            RaidTimer = RaidMaxTime;
            warriorsCount -= nextRaid;
            nextRaid += raidIncrease;
            Raid_Survived_count += 1;
            RaidS.Play();

        }
        if(Harvest_Timer.tick)
        {
            wheatCount += villagerCount * wheatPerVillager;
            Overall_Wheat += villagerCount * wheatPerVillager;
        }
        if (Eating_Timer.tick)
        {
            WarriorsEat = warriorsCount * wheatToWarrior;
            if (wheatCount > WarriorsEat)
            {

                wheatCount -= WarriorsEat;
            }
            else
            {
                wheatCount = 0;
            }
        }
        if (VillagerCycle > 0)
        {
            VillagerCycle -= Time.deltaTime;
            Villager_Timer.fillAmount = VillagerCycle / VillagerCycleTime;
        }
        else if(VillagerCycle > -1)
        {
            Villager_Timer.fillAmount = 1;
            villagerButton.interactable = true;
            villagerCount += 1;
            Overall_Villagers += 1;
            VillagerCycle = -2;
            VillagersS.Play();

        }
        else if (wheatCount < VillagerCost)
        {
            villagerButton.interactable = false;
        }
        else if (wheatCount >= VillagerCost)
        {
            villagerButton.interactable = true;
        }
        if (WarriorCycle > 0)
        {
            WarriorCycle -= Time.deltaTime;
            Warriors_Timer.fillAmount = WarriorCycle / WarriorCycleTime;
        }
        else if(WarriorCycle > -1)
        {
            Warriors_Timer.fillAmount = 1;
            warriorButton.interactable = true;
            warriorsCount += 1;
            Overall_Warriors += 1;
            WarriorCycle = -2;
            WarriorsS.Play();

        }
        else if (wheatCount < WarriorCost)
        {
            warriorButton.interactable = false;
        }
        else if (wheatCount >= WarriorCost)
        {
            warriorButton.interactable = true;
        }
        UpdateText();
        if (warriorsCount < 0)
        {
            Time.timeScale = 0;
            Loose_Screen.SetActive(true);
            LooseS.Play();
            BackgroundS.mute = true;
            Raid_Survived_count -= 1;
            warriorsCount = 0;
            Pause.interactable = false;
            SaundOFF.interactable = false;
            Overall_Enemies = Raid_Survived_count * raidIncrease;
            Raid_Survived_Text.text = Raid_Survived_count.ToString();
            Overall_WheatText.text =  Overall_Wheat.ToString();
            Overall_VillagersText.text = Overall_Villagers.ToString();
            Overall_WarriorsText.text = Overall_Warriors.ToString();
            Overall_EnemiesText.text = Overall_Enemies.ToString();
        }
        if (Raid_Survived_count >= Winning_condition)
        {
            Time.timeScale = 0;
            Win_Screen.SetActive(true);
            WinS.Play();
            BackgroundS.mute = true;
            RaidS.mute = true;
            Pause.interactable = false;
            SaundOFF.interactable = false;
            Overall_Enemies = Raid_Survived_count * raidIncrease;
            WRaid_Survived_Text.text = Winning_condition.ToString();
            WOverall_Wheat.text =  Overall_Wheat.ToString();
            WOverall_Villagers.text = Overall_Villagers.ToString();
            WOverall_Warriors.text = Overall_Warriors.ToString();
            WOverall_Enemies.text = Overall_Enemies.ToString();
        }
    }
    private void UpdateText()
    {
        village_resourcesText.text = villagerCount.ToString();
        warriors_resourcesText.text = warriorsCount.ToString();
        wheat_resourcesText.text = wheatCount.ToString();
        Raid_NumberText.text = nextRaid.ToString();
    }
    public void CreateVillager()
    {
        wheatCount -= VillagerCost;
        VillagerCycle = VillagerCycleTime;
        villagerButton.interactable = false;
        click.Play();
    }
    public void CreateWarrior()
    {
        wheatCount -= WarriorCost;
        WarriorCycle = WarriorCycleTime;
        warriorButton.interactable = false;
        click.Play();
    }
    public void StopSounds()
    {
        if (BackgroundS.mute == false)
        {
            BackgroundS.mute = true;
            click.mute = true;
            WarriorsS.mute = true;
            VillagersS.mute = true;
            RaidS.mute = true;
            WinS.mute = true;
            LooseS.mute = true;
        }
        else if (BackgroundS.mute == true)
        {
            BackgroundS.mute = false;
            click.mute = false;
            WarriorsS.mute = false;
            VillagersS.mute = false;
            RaidS.mute = false;
            WinS.mute = false;
            LooseS.mute = false;
        }
    }
    public void RetryButton()
    {
        Time.timeScale = 1;

        villagerCount = 1;
        warriorsCount = 0;
        wheatCount = 15;
        Overall_Wheat = 0;
        Overall_Villagers = 0;
        Overall_Warriors = 0;
        Overall_Enemies = 0;
        Raid_Survived_count = 0;
        nextRaid = 2;
        RaidTimer = RaidMaxTime;


        BackgroundS.mute = false;
        click.mute = false;
        WarriorsS.mute = false;
        VillagersS.mute = false;
        RaidS.mute = false;
        WinS.mute = false;
        LooseS.mute = false;
        UpdateText();


        if (!BackgroundS.isPlaying)
        {
            BackgroundS.Play();
        }

        Loose_Screen.SetActive(false);
        Win_Screen.SetActive(false);


        Pause.interactable = true;
        SaundOFF.interactable = true;
        
    }
}
