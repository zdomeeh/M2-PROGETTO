using UnityEngine;

[System.Serializable] // espone i campi privati nell'Inspector
public class Weapon 
{ 
 public enum DAMAGE_TYPE // Tipo di danno dell'arma: fisico o magico
    { 
     PHYSICAL,
     MAGICAL
    }

   [SerializeField] private string name;
   [SerializeField] private DAMAGE_TYPE dmgType;
   [SerializeField] private ELEMENT elem;
   [SerializeField] private Stats bonusStats;

 public Weapon (string name, DAMAGE_TYPE dmgType, ELEMENT elem, Stats bonusStats) // Costruttore: assegna tutti i valori dell'arma
    {
        this.name = name;
        this.dmgType = dmgType;
        this.elem = elem;
        this.bonusStats = bonusStats;
    }

    // Getter per i campi privati
    public string GetName() => name;
    public DAMAGE_TYPE GetDmgType() => dmgType;
    public ELEMENT GetElement() => elem;
    public Stats GetBonusStats() => bonusStats;

    // Setter per i campi privati
    public void SetName(string value) => name = value;
    public void SetdmgType(DAMAGE_TYPE value) => dmgType = value;
    public void SetElem(ELEMENT value) => elem = value;
    public void SetBonusStats(Stats value) => bonusStats = value;
}
