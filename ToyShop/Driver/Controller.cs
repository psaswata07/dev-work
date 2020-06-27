using System;
using System.Collections.Generic;
using System.Linq;

public static class Controller
{
    private static List<Toy> toys;

    private static List<Invoice> invoices;

    private static Dictionary<string, bool> toyMap;

    public static void MakeRelationToyToPart()
    {
        toyMap = new Dictionary<string, bool>();
        toys = new List<Toy>();

        while(true)
        {
            var input = Console.ReadLine();
            if(input.Equals("End"))
            return;

            var parse = input.Split(',');

            if(toyMap.ContainsKey(parse[0]))
            {
                var toyList = toys.Where(toy => toy.GetToyId().Equals(parse[0]));
                var toy = toyList.First();
                toy.AddPartsRequired(parse[1]);
            }
            else{
                var newToy = new Toy(parse[0]);
                newToy.AddPartsRequired(parse[1]);
                toyMap[newToy.GetToyId()] = true;
                toys.Add(newToy);
            }
        }        
    }

    public static void ParseInput()
    {
        var invoiceMap = new Dictionary<string, bool>();
        invoices = new List<Invoice>();

        while(true)
        {
            var input = Console.ReadLine();
            if(input.Equals("End"))
            return;

            var parse = input.Split(',');

            if(invoiceMap.ContainsKey(parse[3]))
            {
                var invoiceList = invoices.Where(invoice => invoice.GetId().Equals(parse[3]));
                invoiceList.First().AddParts(parse[1], parse[2], parse[5], Int32.Parse(parse[6]));
                Console.WriteLine(Int32.Parse(parse[6]).ToString());
            }
            else{
                var newInvoice = new Invoice(parse[3], Int32.Parse(parse[4]));
                newInvoice.AddParts(parse[1], parse[2], parse[5], Int32.Parse(parse[6]));

                invoiceMap[newInvoice.GetId()] = true;
                invoices.Add(newInvoice);
            }
        }
    }

    public static bool Possible()
    {
        foreach(var key in toyMap)
        {
            if(key.Value)
            return true;
        }

        return false;
    }

    public static void MakeToys()
    {
        foreach(var toy in toys)
        {
            var partsRequired =toy.GetPartsRequired();
            var possible = false;

            foreach(var invoice in invoices)
            {
                var parts = invoice.GetParts();
                var finalList = new List<Part>();
                if(partsRequired.Count <= parts.Count)
                {

                    foreach(var part in partsRequired)
                    {
                        int maxval = -1;
                        foreach(var p in parts)
                        {
                            if(p.GetPartID().Equals(part) && maxval<p.GetQuantity())
                            {
                                maxval = p.GetQuantity();
                            }
                        }
                        foreach(var p in parts)
                        {
                            if(p.GetPartID().Equals(part) && p.GetQuantity().Equals(maxval))
                            {
                                finalList.Add(p);
                                break;
                            }
                        }
                    }

                    if(finalList.Count == partsRequired.Count)
                    {
                        var minVal = finalList.First().GetQuantity();
                        foreach(var fp in finalList)
                        {
                            if(fp.GetQuantity()< minVal)
                            {
                                minVal = fp.GetQuantity();
                            }
                        }

                        var output = toy.GetToyId().ToString();
                        output = output + " ";
                        foreach(var fp in finalList)
                        {
                            output = output + fp.GetPartID().ToString() + ';';
                            fp.SetQuantity(minVal);
                        }
                        output = output + " ";
                        foreach(var fp in finalList)
                        {
                            output = output + fp.GetWarehouse().ToString() + ';';
                        }
                        output = output + " ";
                        output = output + invoice.GetId().ToString() + " " + invoice.GetCostPrice().ToString() + " " + minVal.ToString();

                        Console.WriteLine(output);
                        possible = true;
                    }
                }
            }

            if(!possible)
            toyMap[toy.GetToyId()] = possible;  
        }
    }

    public static void GetUnusedData()
    {
        foreach (var invoice in invoices)
        {
            var parts = invoice.GetParts();
            var count = 0;
            string pString = "";
            string tString = "";
            string warehouse = "";
            string consignmentID = "";
            foreach (var p in parts)
            {
                if (p.GetQuantity() > 0)
                {
                    count = count + p.GetQuantity() + ';';
                    pString = pString + p.GetPartID().ToString() + ';';
                    warehouse = warehouse + p.GetWarehouse() + ';';
                    consignmentID = consignmentID + p.GetConsignmentID() + ';';
                    foreach (var toy in toys)
                    {
                        if (toy.GetPartsRequired().Contains(p.GetPartID()))
                            tString = tString + toy.GetToyId().ToString() + ';';
                    }
                }
            }
            if(count>0)
            {
                var output = tString + " " + pString + " " + warehouse + " " + invoice.GetId() + " " + invoice.GetCostPrice().ToString() + " " + consignmentID + " " + count.ToString();
                Console.WriteLine(output);
            }

        }
    }
}