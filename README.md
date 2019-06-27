![Bbva NET](http://www.bbva.mx/img/github/net.jpg)
[![Build status](https://ci.appveyor.com/api/projects/status/o8ivc5myofhx2kxm)](https://ci.appveyor.com/project/mecoronado/bbva-dotnet)

This is a client implementing the payment services for Bbva at bbva.mx


Compatibility
-------------

* .Net Framework 4.5 or later 

Dependencies
------------
* [Newtonsoft.Json](http://james.newtonking.com/json)

Quick Start
----------------
#### Installation #####

It is recommended that you use [NuGet](http://docs.nuget.org) for install this library. Or you can fork the code and build it.

#### Configuration #####

Before use the library will be necessary to set up your Merchant ID and
Private key. Use:

```net
BbvaAPI bbvaAPI = new BbvaAPI(API_KEY, MERCHANT_ID);
```

#### Sandbox/Production Mode #####

By convenience and security, the sandbox mode is activated by default in the client library. This allows you to test your own code when implementing Bbva, before charging any credit card in production environment. Once you have finished your integration, create BbvaAPI object like this:

```cs
Boolean production = true;
BbvaAPI bbvaAPI = new BbvaAPI(API_KEY, MERCHANT_ID, production);
```
or use Production property:
```cs
BbvaAPI bbvaAPI = new BbvaAPI(API_KEY, MERCHANT_ID);
bbvaAPI.Production = true;
```

#### API Services #####

Once configured the library, you can use it to interact with Bbva API services. All the API services are properties of the BbvaAPI class.

```cs
bbvaAPI.ChargeTokenService
bbvaAPI.TokenService
```

Each service has methods to **create**, **get**, **update**, **delete** or **list** resources according to the documetation described on http://docs.bbva.mx

Examples
---------
#### Customers #####

**Create a customer**
```cs
Customer customer = new Customer();
customer.Name = "Net Client";
customer.LastName = "C#";
customer.Email = "net@c.com";
customer.Address = new Address();
customer.Address.Line1 = "line 1";
customer.Address.PostalCode = "12355";
customer.Address.City = "Queretaro";
customer.Address.CountryCode = "MX";
customer.Address.State = "Queretaro";

Customer customerCreated = bbvaAPI.CustomerService.Create(customer);
```

**Get a customer**
```cs
string customer_id = "adyytoegxm6boiusecxm";
Customer customer = bbvaAPI.CustomerService.Get(customer_id);
```   
**Delete a customer**
```cs
string customer_id = "adyytoegxm6boiusecxm";
bbvaAPI.CustomerService.Delete(customer.Id);
``` 
**Update a customer**
```cs
string customer_id = "adyytoegxm6boiusecxm";
Customer customer = bbvaAPI.CustomerService.Get(customer_id);
customer.Name = "My new name";

customer = bbvaAPI.CustomerService.Update(customer);
```

**List customers**
```cs  
SearchParams search = new SearchParams();
search.Limit = 50;

List<Customer> customers = bbvaAPI.CustomerService.List(search);
```

#### Charges #####
Create a charge
```cs

String token = 'kqgykn96i7bcs1wwhvgw';

List<IParameter> request = new List<IParameter> {
	new SingleParameter("affiliation_bbva", "720931"),
	new SingleParameter("amount", "200.00"),
	new SingleParameter("description", "Test Charge"),
	new SingleParameter("customer_language", "SP"),
	new SingleParameter("capture", "true"),
	new SingleParameter("use_3d_secure", "false"),
	new SingleParameter("use_card_points", "NONE"),
	new SingleParameter("token", tokenCreated),
	new SingleParameter("currency", "MXN"),
	new SingleParameter("order_id", "oid-00051")
};

Charge charge = bbvaAPI.ChargeTokenService.Create(request);
```
Capture a charge
```cs

String token = 'kqgykn96i7bcs1wwhvgw';

List<IParameter> request = new List<IParameter> {
	new SingleParameter("affiliation_bbva", "720931"),
	new SingleParameter("amount", "200.00"),
	new SingleParameter("description", "Test Charge"),
	new SingleParameter("customer_language", "SP"),
	new SingleParameter("capture", "false"), //false indicate that only we want capture the amount
	new SingleParameter("use_3d_secure", "false"),
	new SingleParameter("use_card_points", "NONE"),
	new SingleParameter("token", tokenCreated),
	new SingleParameter("currency", "MXN"),
	new SingleParameter("order_id", "oid-00051")
};

Charge charge = bbvaAPI.ChargeTokenService.Create(request);
```
Refund a charge
```cs
string charge_id = "ttcg5roe2py2bur38cx2";

Charge chargeRefunded = bbvaAPI.ChargeTokenService.Refund(charge.Id, "refund desc");
```
#### Tokens #####
Create a token
```cs
List<IParameter> request = new List<IParameter>{
	new SingleParameter("holder_nane", "Juan Perez Ramirez"),
	new SingleParameter("card_number", "4111111111111111"),
	new SingleParameter("cvv2", "022"),
	new SingleParameter("expiration_month", "12"),
	new SingleParameter("expiration_year", "20"),
};

Token tokenCreated = bbvaAPI.TokenService.Create(request);
```
Get a token
```cs
String token = 'kqgykn96i7bcs1wwhvgw';

Token tokenGet = bbvaAPI.TokenService.Get(token);
```
