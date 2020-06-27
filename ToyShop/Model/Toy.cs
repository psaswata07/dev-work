using System.Collections.Generic;
public class Toy
{
    string toy_id;

    List<string> partsRequired;

    public Toy(string toy_id)
    {
        this.toy_id = toy_id;
        this.partsRequired = new List<string>();
    }

    public void AddPartsRequired(string part_id)
    {
        this.partsRequired.Add(part_id);
    }

    public string GetToyId()
    {
        return this.toy_id;
    }

    public List<string> GetPartsRequired()
    {
        return this.partsRequired;
    }
}