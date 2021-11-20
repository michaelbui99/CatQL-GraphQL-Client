# CatQL-GraphQL-Client
Simple GraphQL Client for .NET
Only versions 2.0.0+ works fully. 
https://www.nuget.org/packages/CatQL.GraphQL.Client
##### Table of Contents  
[Creating Query - no variables](#query-simple) <br>
[Creating Query - with variables](#query-variables)<br>
[Sending a Query](#sending-query)<br>
[Performing a Mutation](#mutation)<br>
[Error handling example](#error)

## Add to project

```dotnetcli
dotnet add package CatQL.GraphQL.Client 
```

## Usage: 

<a name="query-simple"/>
Creating Query with no Variables: 

```csharp
 GqlQuery query = new GqlQuery()
  {
    Query = "query{Media(id: 15125, type:ANIME){id, bannerImage}}"
  };

```

<a name="query-variables"/>
Creating Query with Variables:

```csharp
 
  GqlQuery query = new GqlQuery()
  {
    Query = "query($id: Int){Media(id: $id, type:ANIME){id, bannerImage}}", Variables = new{id = 15125}
  };

```
<a name="sending-query"/>
Sending a Query: 

```csharp
public class ResponseType
{
  // Must create a ResponseType class
  // Name of the class does not matter
  // Class must contain properties with the Return Type of the Query.
  // The property name must match name of they query for deserialization to work. 
  // JsonProperty must be set to PropertyName (PascalCase)
  [Newtonsoft.Json.JsonProperty("Media")]
  public Media Media { get; set; }
 }
            
public class Media
{
  public int Id { get; set; }
  public string BannerImage { get; set; }
}


  GqlClient client = new GqlClient("https://graphql.anilist.co"){EnableLogging = true}; // Logging is optional 
  GqlQuery query = new GqlQuery()
  {
    Query = "query($id: Int){Media(id: $id, type:ANIME){id, bannerImage}}", Variables = new{id = 15125}
  };
  GqlRequestResponse<ResponseType> response = await client.PostQueryAsync<ResponseType>(query);
  return response.Data.Media;
```
<a name="mutation"/>
Performing a Mutation: 

```csharp

   public class CreateResidenceMutationResponseType
    {
        [JsonProperty("createResidence")]
        public Residence CreateResidence { get; set; }
        
        public override string ToString(){
            return JsonConvert.SerializeObject(this); 
        }
        
    }

  GqlClient client = new GqlClient(Url){EnableLogging=true};
  GqlQuery residenceMutation = new GqlQuery()
  {
   Query = @"mutation($residenceInput: ResidenceInput){createResidence(residence: $residenceInput){id,address{id, zipCode, streetName, houseNumber, cityName, streetNumber,      zipCode},description,type,averageRating,isAvailable,pricePerNight,rules{id, description},facilities{id, name},imageUrl,}}",
   Variables= new {residenceInput = residence}
  };
  var mutationResponse = await client.PostQueryAsync<CreateResidenceMutationResponseType>(residenceMutation);
  System.Console.WriteLine($"{this} received: {mutationResponse.Data.CreateResidence}");

  return mutationResponse.Data.CreateResidence;

```
<a name="error"/>
Error handling: 

```csharp
  /* SAMPLE ERROR RESPONSE
  {"data":{"createResidence":null},
  "errors":[{"message":"Unexpected Execution Error",
  "locations":[{"line":1,"column":43}],"path":["createResidence"],
  "extensions":{"message":"Invalid residence!!",
  "stackTrace":"at SEP3T2GraphQL.Services.ResidenceServiceImpl.CreateResidenceAsync(Residence residence) in C:\\Users\\Shark\\Documents\\Coding\\SEP3\\VIABnB-   SEP3\\t2\\SEP3T2API\\SEP3T2API\\SEP3T2GraphQL\\Services\\ResidenceServiceImpl.cs:line 53\r\n   at SEP3T2GraphQL.Graphql.Mutation.CreateResidence(Residence residence) in C:\\Users\\Shark\\Documents\\Coding\\SEP3\\VIABnB-SEP3\\t2\\SEP3T2API\\SEP3T2API\\SEP3T2GraphQL\\Graphql\\Mutation.cs:line 17\r\n   at HotChocolate.Resolvers.Expressions.ExpressionHelper.AwaitTaskHelper[T](Task`1 task)\r\n   at HotChocolate.Types.Helpers.FieldMiddlewareCompiler.<>c__DisplayClass9_0.<<CreateResolverMiddleware>b__0>d.MoveNext()\r\n--- End of stack trace from previous location ---\r\n  
 at HotChocolate.Execution.Processing.Tasks.ResolverTask.ExecuteResolverPipelineAsync(CancellationToken cancellationToken)\r\n   at HotChocolate.Execution.Processing.Tasks.ResolverTask.TryExecuteAsync(CancellationToken cancellationToken)"}}]}
  */
  
if (mutationResponse.Errors != null)
{ 
  // String manipulation to seperate the Error message from the sample error response. 
  throw new ArgumentException(JsonConvert.SerializeObject(mutationResponse.Errors).Split(",")[4].Split(":")[2]); 
}

```
