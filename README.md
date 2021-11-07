# CatQL-GraphQL-Client
Simple GraphQL Client for .NET
Only versions 1.2.0+ works. 

## Add to project

```dotnetcli
dotnet add package CatQL.GraphQL.Client 
```

## Usage: 


### Creating Query with no Variables: 
```csharp
 GqlQuery query = new GqlQuery()
  {
    Query = "query{Media(id: 15125, type:ANIME){id, bannerImage}}"
  };

```


### Creating Query with Variables: 

```csharp
 
  GqlQuery query = new GqlQuery()
  {
    Query = "query($id: Int){Media(id: $id, type:ANIME){id, bannerImage}}", Variables = new{id = 15125}
  };


```

### Sending a Query:

```csharp
public class ResponseType
{
  // Must create a ResponseType class
  // Name of the class does not matter
  // Class must contain properties with the Return Type of the Query.
  // The property name must match the property type for deserialization to work. 
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
    Query = "query($id: Int){Media(id: $id, type:ANIME){id, bannerImage}}", Variables = new{id = 15125
  };
  GqlRequestResponse<ResponseType> response = await client.PostQueryAsync<ResponseType>(query);
  return response.Data.Media;
```
