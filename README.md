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

It is recommended that you fork the code and build it.

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
#### Charges #####
Create a charge
```cs
BbvaAPI api = new BbvaAPI("sk_xxxxxxxxxxxxxxxxxxxxxx", "mptdggroasfcmqs8plpy");

ParameterContainer customer = new ParameterContainer("customer");
    customer.AddValue("name", "Juan");
    customer.AddValue("last_name", "Vazquez Juarez");
    customer.AddValue("email", "juan.vazquez@empresa.com.mx");
    customer.AddValue("phone_number", "554-170-3567");

ParameterContainer request = new ParameterContainer("charge");
    request.AddValue("affiliation_bbva", "781500");
    request.AddValue("amount", "100.00");
    request.AddValue("description", "Cargo inicial a mi merchant");
    request.AddValue("currency", "MXN");
    request.AddValue("order_id", "oid-00051");
    request.AddValue("redirect_url", "https://sand-portal.ecommercebbva.com");
    request.AddMultiValue(customer):

Dictionary<String, Object> chargeDictionary = bbvaAPI.ChargeService.Create(request.ParameterValues);
ParameterContainer charge = new ParameterContainer("charge", chargeDictionary);
```


