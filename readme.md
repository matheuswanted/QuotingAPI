### Prerequisites
RabbitMQ server running, MSSQL localdb

Installing RabbitMQ:
https://www.rabbitmq.com/download.html

but basically you have to execute Erlang installer as administrator, then execute rabbitmq installer as administrator.

After installing RabbitMQ, you need to start rabbitmq service. If you have changed rabbitmq configs, like user/pass, hostname or port,
it's necessary to update appsettings.json RawRabbit section.

### Executing the API:
Just run the script Run.ps1, it will publish the main project as release into App folder and execute.

### API tests:
The API tests were executed with Postman. The environment and test collection is available inside Postman folder.

### Routes

```
 /api/Quotes/Quote
	{
		"Customer" : {
			"SSN" : "001-259-963",
			"Address":"15th street",
			"Email":"email@email.com",
			"Phone":"88696635",
			"Gender":"F",
			"BirthDate": "1999-04-30"
		},
		"Vehicle":{
			"Type":"Car",
			"ManufacturingYear":1850,
			"Model":"Palio",
			"Make":"Fiat"
		}
	}
```

```
 /api/Quotes/Status?Id={{QuoteId}} 
```
 
```
 /api/Quotes/Information?Id={{QuoteId}}
```
