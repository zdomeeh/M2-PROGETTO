using UnityEngine;

[System.Serializable]
public class Weapon 
{ 
 public enum DAMAGE_TYPE 
    { 
     PHYSICAL,
     MAGICAL
    }

   [SerializeField] private string name;
   [SerializeField] private DAMAGE_TYPE dmgType;
   [SerializeField] private ELEMENT elem;
   [SerializeField] private Stats bonusStats;

 public Weapon (string name, DAMAGE_TYPE dmgType, ELEMENT elem, Stats bonusStats)
    {
        this.name = name;
        this.dmgType = dmgType;
        this.elem = elem;
        this.bonusStats = bonusStats;
    }

    public string GetName() => name;
    public DAMAGE_TYPE GetDmgType() => dmgType;
    public ELEMENT GetElement() => elem;
    public Stats GetBonusStats() => bonusStats;

    public void SetName(string value) => name = value;
    public void SetdmgType(DAMAGE_TYPE value) => dmgType = value;
    public void SetElem(ELEMENT value) => elem = value;
    public void SetBonusStats(Stats value) => bonusStats = value;
}
