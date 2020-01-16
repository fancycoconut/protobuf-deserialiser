# Protobuf Deserialiser
Inspired by Json.NET from Newtonsoft

// TODO
- We can add an extra class to make our deserialiser generate a class/type object which can then be used to deserialise your data. (POC done)

- Setup library solution architecture
- Change deserialise implementation to use dictionary only and stack instead of queues (measure performance against existing implementation!)
- Implement IField implementations other types and well known types

- Generate services in separate DLL since NET Standard does not support runtime generation of concrete types
	- Generate service classes that can call either through COAP or HTTP etc


