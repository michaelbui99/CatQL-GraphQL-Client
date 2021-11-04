# CatQL-GraphQL-Client
Simple GraphQL Client for .NET

## Add to project

```dotnetcli
dotnet add package CatQL.GraphQL.Client 
```

## Usage: 

### Request with Variables:

```csharp
  GqlClient client = new GqlClient("https://graphql.anilist.co");
  GqlQuery query = new GqlQuery()
  {
    Query = "query($id: Int){Media(id: $id, type:ANIME){id, bannerImage}}", Variables = "{id: 15125}"
  };
  GqlRequestResponse<Media> response = await client.PostQueryAsync<Media>(query);

```
