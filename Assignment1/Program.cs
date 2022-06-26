using Assignment1;


TestChildObject tObject2 = new();
tObject2.name = "property1";
tObject2.age = 10;
tObject2.toy = new Toy() { name = "plane" };

TestObject tObject = new TestObject();
tObject.property1 = "Shafayet";
tObject.property2 = "Maruf";
tObject.property3 = 10;
tObject.property4 = 20.10;
tObject.property5 = 1111110;
tObject.t = tObject2;

string? json = JsonFormatter.Convert(tObject);
Console.WriteLine(json);
