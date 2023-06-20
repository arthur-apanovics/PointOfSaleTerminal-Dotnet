# Hi
This is the main solution for the technical exercise.  
I hope everything is clear and makes sense, however, please don't hesitate to 
get in touch in case of any questions or comments.
# Tests
Unit tests should cover most of the "business logic" however I did not go into 
great detail when coming up with scenarios (not a real project, after all). 
# How to
Build the solution and run tests with the following command:
```bash
docker build . --progress=plain
```

We add `--progress=plain` so that the test runner details are not omitted 
from output.

# Requirements of exercise
Consider a grocery market where items have prices per unit but also volume prices.
For example, Apples may be $1.25 each, or 3 for $3.

Implement a class library for a point-of-sale scanning system that accepts an arbitrary ordering of
products, similar to what would happen at an actual checkout line, then returns the correct total
price for an entire shopping cart based on per-unit or volume prices as applicable.

Here are the products listed by code and the prices to use. There is no sales tax.
|Product Code| Price|
|------------|------|
|A| $1.25 each, or 3 for $3.00|
|B| $4.25|
|C| $1.00 or $5 for a six-pack|
|D| $0.75|

The interface at the top-level PointOfSaleTerminal service object should look something like this, (written in pseudo-code for illustration purposes)
```
var terminal = new PointOfSaleTerminal()
terminal.SetPricing(...);
terminal.ScanProduct("A");
terminal.ScanProduct("C");
// etc

decimal result = terminal.CalculateTotal();
```

You are free to define the pricing data model as you see fit.
Here are the minimal inputs you should use for your test cases. These test cases must be shown to work in your program:
1. Scan these items in this order: `ABCDABA` Verify that the total price is $13.25
2. Scan these items in this order: `CCCCCCC` Verify that the total price is $6.00
3. Scan these items in this order: `ABCD` Verify that the total price is $7.25
