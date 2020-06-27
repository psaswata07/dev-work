public class Part
{
    string part_id;
    string warehouse;
    string consignment_id;
    int quantity;

    public Part(string part_id, string warehouse, string consignment_id, int quantity)
    {
        this.part_id = part_id;
        this.warehouse = warehouse;
        this.consignment_id = consignment_id;
        this.quantity = quantity;
    }

    public string GetPartID()
    {
        return this.part_id;
    }

    public string GetWarehouse()
    {
        return this.warehouse;
    }

    public string GetConsignmentID()
    {
        return this.consignment_id;
    }

    public int GetQuantity()
    {
        return this.quantity;
    }

    public void SetQuantity(int val)
    {
        this.quantity = this.quantity - val;
    }
}