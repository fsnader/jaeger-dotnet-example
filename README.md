# Jaeger .NET example
Example of .NET applications distributed tracing

![picture alt](https://github.com/fsnader/jaeger-dotnet-example/blob/master/jaeger-example.png)

## Prerequisites:
1. Docker
2. .NET Core 3

## How to Run
1. Create a All-in-one instance of Jaeger in Docker, running the following command:

```
$ docker run -d --name jaeger \
  -e COLLECTOR_ZIPKIN_HTTP_PORT=9411 \
  -p 5775:5775/udp \
  -p 6831:6831/udp \
  -p 6832:6832/udp \
  -p 5778:5778 \
  -p 16686:16686 \
  -p 14268:14268 \
  -p 9411:9411 \
  jaegertracing/all-in-one:1.16
 ```

2. Jaeger will be running in http://localhost:16686/

3. Start the two API's in the CustomerApi solution
4. The Customers API will be running in http://localhost:5000 and the Addresses API will be running in http://localhost:5002
5. Send HTTP requests to the customers API and see the requests being traced in Jaeger
`GET http://localhost:5000/api/customers/`
`GET http://localhost:5000/api/customers/1`
```
POST http://localhost:5000/api/customers/
{
	"id": 8,
	"name": "Random guy",
	"zipCode": "30313-22"
}
```
Obs.: Requests can also be found in Jaeger .NET Example.postman_collection.json postman collection
