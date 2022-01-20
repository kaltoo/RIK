# RIK nooremarendaja proovitöö

## Draft

Andmebaasiga toimetamiseks
```bash
docker pull postgres:14.1
docker run --name postgres-0 -e POSTGRES_PASSWORD=password -d -p 5432:5432 postgres:14.1
```

Esmane migration ja update
```bash
dotnet ef migrations add InitialCreate --project WebApp --startup-project WebApp 
dotnet ef database update --project WebApp --startup-project WebApp   
```

