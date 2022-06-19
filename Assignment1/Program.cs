using Assignment1;

TestObject tObject = new TestObject();
tObject.property1 = "property1";
tObject.property2 = "property2";
tObject.property3 = 10;
tObject.property4 = 20.10;
tObject.property5 = 1111110;

IObjectToJsonConverter objectToJsonConverter = new ObjectToJsonConverter();
objectToJsonConverter.ConvertToJson(tObject);
objectToJsonConverter.Print();