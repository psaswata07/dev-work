using System.Collections.Generic;

public class Invoice
{
    string id;
    int costPrice;

    List<Part> parts;

    public Invoice(string id, int costPrice)
    {
        this.id = id;
        this.costPrice = costPrice;
        this.parts = new List<Part>();
    }

    public void AddParts(string partid, string warehouse, string consignmentid, int quantity)
    {
        this.parts.Add(new Part(partid, warehouse, consignmentid, quantity));
    }

    public string GetId()
    {
        return this.id;
    }

    public int GetCostPrice()
    {
        return this.costPrice;
    }

    public List<Part> GetParts()
    {
        return this.parts;
    }
}