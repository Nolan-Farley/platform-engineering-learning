using NUnit.Framework;

/*
  Your task: Write a full NUnit test suite for this class. Think about what each method should do, what the edge cases are, and what could go wrong.
 
  Things to consider while writing your tests:
    What happens when you add a product with a zero price? What about adding the same product twice (does it update or throw)? What if CalculateTotal gets a list with items that were never added as products? 
    What if someone tries to process the same order ID twice? What does a 0% discount do versus a 100% discount? What about the formatting of the return string from ProcessOrder?

  Take a crack at it and paste your test class back here. I'll review it and give you feedback on what you covered well and what you might have missed.

*/

public class OrderProcessor
{
    private readonly Dictionary<string, decimal> _prices = new(); // Priavte readonly Dictt with a key:int,value:decimal, undercase naming convenion for private and 
    private readonly List<string> _processedOrders = new(); // New() Keyword creates compile time object based on left side context - var declaration, return type, method ards

    public void AddProduct(string name, decimal price) // public method with no return type takes:string,decimal
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Product name cannot be empty"); // Throw raises exception(creates ArgumentException class object,base class exception), must be caught or program terminates
        if (price < 0)
            throw new ArgumentException("Price cannot be negative");
        _prices[name] = price;
    }

    public decimal CalculateTotal(List<string> items) //pubic method of type decimal, returns a list of strings
    {
        if (items == null || items.Count == 0)
            return 0;

        decimal total = 0;
        foreach (var item in items)// for in loop, var is used to infer the type, so we dont have to do List<string> instead
        {
            if (_prices.ContainsKey(item))
                total += _prices[item];
        }
        return total;
    }

    public decimal ApplyDiscount(decimal total, int percentOff)
    {
        if (percentOff < 0 || percentOff > 100)
            throw new ArgumentOutOfRangeException("Discount must be 0 to 100");//argument = paramter - the arg is outside acceptible params

        return total - (total * percentOff / 100);
    }

    public string ProcessOrder(string orderId, List<string> items, int discountPercent)
    {
        if (string.IsNullOrEmpty(orderId))
            throw new ArgumentException("Order ID required");

        if (_processedOrders.Contains(orderId))
            throw new InvalidOperationException("Order already processed");

        var total = CalculateTotal(items);
        var finalTotal = ApplyDiscount(total, discountPercent);

        _processedOrders.Add(orderId);
        return $"Order {orderId}: ${finalTotal:F2}";
    }

    public bool IsOrderProcessed(string orderId)
    {
        return _processedOrders.Contains(orderId);
    }
}

//Unit Testing Framework Standard Approach is to create one test class per class and methods
public class TestOrderProcessorValidatorTests
{
    private OrderProcessor _testProcessor();

    //AddProduct 
    // When starting with unit tests start with base case(happy path), edge cases, error cases

    public void TestAddProduct_ValidInput_AddsProduct()// MethodName_Scenario_ExpectedResult
    {
        //**  Arrannge **
        string name = "orange"; //local var do not get Cap'd
        decimal price = 1.0;

        list<string> products = ["orange"];
        decimal expectedTotal = 1.0;

        //**  Act **
        _testProcessor.AddProduct(name,price);
        //Since ProcessOrders prices list is private we need to find a way to test it without direct invocation, 
        //Calling Calcuate total verifes this as it proves the key is in the list since CalcualteTotal wouldnt return the total without it, then it returning the correct price shows the value is right as well
        decimal total = _testProcessor.CalculateTotal(products);

        //**  Assert **
        Assert.AreEqual(expectedTotal,total);
    }

    


    //CalcuateTotal
    //ApplyDiscout
    //ProcessOrder
    //IsOrderProcessed


}



// Arrange
// Act
// Assert