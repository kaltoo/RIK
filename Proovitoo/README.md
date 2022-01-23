# RIK nooremarendaja proovitöö
## Külaliste registreerimissüsteem
## Paigaldamine
### Rakenduse toimimiseks on vajalik
* Dotnet 6.0 (https://dotnet.microsoft.com/en-us/)
* Dotnet EF CLI tööriistad  

Dotnet EF toolkiti paigaldamine:
```bash
dotnet tool install --global dotnet-ef
dotnet tool update --global dotnet-ef
```
* Visual Studio või Rider (soovitatavalt uuem versioon)
* Postgres SQL server 14.1

Testimiseks on mugav kasutada Dockerit:
```bash
docker pull postgres:14.1
docker run --name postgres-0 -e POSTGRES_PASSWORD=password -d -p 5432:5432 postgres:14.1
```

Kui Te olete Postgres SQL serveri toimima saanud, siis redigeerige WebApp kaustas olevat
***appsettings.json*** faili, muutes vajadusel "AppConnection" väärtuse sobivaks (ilma sulgudeta). 
Kui Te kasutasite eeltoodud Dockeri käske, siis peaks see juba korrektne olema.

```
"AppConnection" : "Host=(ip);Database=(andmebaasi_nimi);Username=(kasutajanimi);Password=(parool)"
```
Käivitage terminalis järgnevad käsud (Solutioni asukohast)
```bash
dotnet ef migrations add InitialCreate --project WebApp --startup-project WebApp 
dotnet ef database update --project WebApp --startup-project WebApp   
```

Seejärel käivitage Visual Studio või Rideri kaudu WebApp

## Rakendusest üldiselt

Tegemist on ASP.NET Core Razor Pages veebirakendusega, mis võimaldab HTML koodi sees vaadete renderdamiseks kasutada C# keelt. 
Razori vaadete sees lehekülje sisu loomiseks on kasutatud igal võimalusel tüübitud muutujaid, et koodi loetavust ja vigade avastamist parandada.  
Andmebaasi loomise ja kasutamise jaoks on kasutatud (ORM) olemiraamistikku Entity Framework.  Antud rakenduse puhul on kasutatud Postgresi SQL Data Providerit, kuid EF puhul on lahendused olemas kõigile enimkasutatavatele DBMS süsteemidele.  
DAL (Data-Access-Layer) kiht vastutab andmebaasi ja rakenduse vahelise suhtluse eest.  
Pages kaustas asuvad dünaamilised Razori vaated ja nende PageModelid.  
Kaustas "wwwroot" asub veebirakenduse staatiline sisu.  
Modelid asuvad kaustas Domain. Kõik lihtsamad valideerimised on modeli enda sees ära kirjeldatud, kasutades "validation attribute" või modeli klassi puhul
IValidatableObject liidest. Andmebaasi kasutust vajavad valideerimised asuvad Page Modelis.  
Andmete vastuvõtt ja valideerimine toimub Model Bindingu kaudu.
Responsiivse kasutajaliidese loomiseks on kasutatud Bootstrapi raamistikku ja natuke Javascripti.  
Testide koostamiseks on kasutatud NUnit raamistikku ja Seleniumit.

## Automaattestid

Testid asuvad WebAppTests projektis.

Enne testide käivitamist palun:
* Paigaldada Google Chrome (Või soovi korral paigaldage mõne teise browseri draiver)
* Seadistage rakendus kasutama testbaasi.  
* ***ConnectionInfo.cs*** failis muuta URL rakendusele vastavaks. 


