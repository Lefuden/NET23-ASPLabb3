# ASPLabb3
# Om uppgiften

I den här labben ska du testa att bygga ditt första enkla Webb-API. Det API du kommer konstruera använder en REST-arkitektur och kommer möjliggöra för externa tjänster och applikationer att hämta och ändra data i din egen applikation.

# Vad du ska göra

⚙️ **Applikationen/databasen**

Det första du ska skapa är en väldigt grundläggande API med en databas som klarar följande.

- [ ]  Det ska gå att lagra personer med grundläggande information om dem som namn och telefonnummer.
- [ ]  Systemet ska kunna lagra ett obegränsat antal intressen som de har. Varje intresse ska ha en titel och en kort beskrivning.
- [ ]  Varje person ska kunna vara kopplad till ett valfritt antal intressen
- [ ]  Det ska gå att lagra ett obegränsat antal länkar (till webbplatser) till varje intresse för varje person. Om en person lägger in en länk så är den alltså kopplad både till den personen och till det intresset.

🗣 **Skapa ett REST-API**

Det andra steget du ska göra är att skapa ett REST-API som tillåter externa tjänster att utföra följande anrop till ditt API samt genomför dessa förändringar i din applikation.

- [ ]  Hämta alla personer i systemet
- [ ]  Hämta alla intressen som är kopplade till en specifik person
- [ ]  Hämta alla länkar som är kopplade till en specifik person
- [ ]  Koppla en person till ett nytt intresse
- [ ]  Lägga in nya länkar för en specifik person och ett specifikt intresse

🕹️ **Testa ditt API**

Det sista steget är att göra anrop mot ditt API genom tjänsten [Postman](https://www.postman.com/) eller swagger. 

- [ ]  Gör ett anrop för varje krav ovan för API:et
- [ ]  I din readme-fil i Git lägger du in alla anrop du gjort för varje krav ovan kring APIet så vi kan se hur du tänker dig att anropen ska se ut.

# Anrop

## Hämta alla personer i systemet

Request URL
```
https://localhost:7159/people
```

Server response
```json
[
  {
    "id": 1,
    "name": "Karl",
    "phone": "070123456",
    "interests": [
      {
        "id": 1,
        "title": "Football",
        "description": "Kick balls",
        "links": [
          {
            "id": 1,
            "address": "www.siteaboutfootball.com"
          },
          {
            "id": 3,
            "address": "www.siteaboutballsofallkinds.com"
          }
        ]
      },
      {
        "id": 3,
        "title": "bowling",
        "description": "very heavy balls",
        "links": [
          {
            "id": 4,
            "address": "www.bowlinghallen.se"
          },
          {
            "id": 5,
            "address": "www.bowlinghallen.se"
          },
          {
            "id": 6,
            "address": "www.heavyballs.se"
          }
        ]
      }
    ]
  },
  {
    "id": 2,
    "name": "Olle",
    "phone": "070654321",
    "interests": [
      {
        "id": 1,
        "title": "Football",
        "description": "Kick balls",
        "links": [
          {
            "id": 1,
            "address": "www.siteaboutfootball.com"
          },
          {
            "id": 3,
            "address": "www.siteaboutballsofallkinds.com"
          }
        ]
      },
      {
        "id": 2,
        "title": "Handball",
        "description": "Throw balls",
        "links": [
          {
            "id": 2,
            "address": "www.siteabouthandball.com"
          }
        ]
      }
    ]
  }
]
```

## Hämta alla intressen som är kopplade till en specifik person

Request URL
```
https://localhost:7159/people/{id}/interests
```

Server response
```json
[
  {
    "id": 1,
    "title": "Football",
    "description": "Kick balls",
    "links": null
  },
  {
    "id": 2,
    "title": "Handball",
    "description": "Throw balls",
    "links": null
  }
]
```

## Hämta alla länkar som är kopplade till en specifik person

Request URL
```
https://localhost:7159/people/{id}/links
```

Server response
```json
[
  [
    {
      "id": 1,
      "address": "www.siteaboutfootball.com"
    },
    {
      "id": 3,
      "address": "www.siteaboutballsofallkinds.com"
    }
  ],
  [
    {
      "id": 2,
      "address": "www.siteabouthandball.com"
    }
  ]
]
```

## Koppla en person till ett nytt intresse

Request URL
```
https://localhost:7159/people/{id}/interest
```

POST request body
```json
{
  "title": "baseball",
  "description": "hit balls with bats"
}
```


## Lägga in nya länkar för en specifik person och ett specifikt intresse

Request URL
```
https://localhost:7159/people/{pId}/interest/{iId}/links?address=www.addresshere.com
```
