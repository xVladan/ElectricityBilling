# Book Rating Engine API

ASP.NET Core Web API (.NET 8) ElectricityBilling. Projekat koristi Docker i PostgreSQL bazu podataka.

##  Pokretanje aplikacije

*Preduslov

 - Instaliran [Docker Desktop](https://www.docker.com/products/docker-desktop)

## Pokretanje preko Dockera

1. Otvorite terminal u root folderu projekta (gdje se nalazi `docker-compose.yml`)
2. Pokrenite:

   ```bash
   
   docker-compose up --build
   
   ```
3. Kada se kontejneri pokrenu, otvorite browser:

      http://localhost:6001

  *Bićete automatski preusmjereni na Swagger UI za testiranje API-ja.


4. Svi endpointi su dostupni putem Swaggera:
     http://localhost:6001/swagger/index.htm
	 
5. Migracije ce biti automatski izvršene.


## Dodatne informacije

* Prvi kreirani korisnik dobija rolu Admin.
* Logger je implementiran samo za potrebe debagovanja i ne zapisuje se u fajl.
* XUnit testovi su napisani samo za dva primjera kao demonstracija testiranja.