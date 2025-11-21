using UnityEngine;


[System.Serializable] // permette di assegnare gli Hero nell'Inspector

// Classe che rappresenta un eroe, con tutte le sue proprieta'
public class Hero
{
    [SerializeField] private string name;
    [SerializeField] private int hp;
    [SerializeField] private Stats baseStats;
    [SerializeField] private ELEMENT resistance;
    [SerializeField] private ELEMENT weakness;
    [SerializeField] private Weapon weapon;

    public Hero (string name, int hp, Stats baseStats, ELEMENT resistance, ELEMENT weakness, Weapon weapon) // Costruttore: inizializza tutti i campi dell'eroe
    {
        this.name = name;
        this.hp = hp;
        this.baseStats = baseStats;
        this.resistance = resistance;
        this.weakness = weakness;
        this.weapon = weapon;
    }

    // Getter e setter per i campi privati della classe Hero
    public string GetName() => name;
    public int GetHp() => hp;
    public Stats GetBaseStats() => baseStats;
    public ELEMENT GetResistance() => resistance;
    public ELEMENT GetWeakness() => weakness;
    public Weapon GetWeapon() => weapon;

    public void SetName(string value) => name = value;
    public void SetHp(int value) => hp = Mathf.Max(0, value); // Setter uguale a tutti gli altri con un'aggiunta per evitare che gli hp scendano sotto lo 0
    public void SetBaseStats(Stats value) => baseStats = value;
    public void SetResistance(ELEMENT value) => resistance = value;
    public void SetWeakness(ELEMENT value) => weakness = value;
    public void SetWeapon(Weapon value) => weapon = value;

    public void AddHp(int amount) // Aumenta gli HP di una certa quantita'
    {
        SetHp(hp + amount);
    }

    public void TakeDamage(int damage) // Riduce gli HP di una certa quantita'
    {
        AddHp(-damage);
    }

    public bool IsAlive() // Restituisce true se l'eroe e' vivo
    {
        return hp > 0;
    }

    public Stats GetTotalStats()                                      // Faccio questa funzione che restituisce tutte le statistiche complete dell'eroe (base + arma)
    {                                                                 // in modo tale da non ripetere, duplicare codice ed avere un codice piu' pulito e meno lungo/confuso
        return Stats.Sum(baseStats, weapon.GetBonusStats());
    }

}

