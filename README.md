# API1 (NHibernate + FluentNHibernate) - proyecto listo para Visual Studio

## Abrir
1. Descomprime el ZIP.
2. Abre `API1.sln` (o directamente `API1/API1.csproj`) en Visual Studio.

## Configuración
Edita `API1/appsettings.json` y cambia:
- `ConnectionStrings:DefaultConnection` (host, usuario, password y nombre de BD).

## Ejecutar
- En Visual Studio: F5
- Swagger: `/swagger`

## Qué incluye
- Modelos + Mapas (FluentNHibernate) para todas las tablas del dump:
  erabiltzaileak, erregistroak, erreserbak, eskaerak, faktura_historikoak,
  fakturak, langileak, mahaiak, platera_motak, platerak,
  produktuak, produktuen_eskaerak, produktuen_motak, zerbitzua
  + tablas puente:
  platerak_has_eskaerak, platerak_has_kontsumizioa, produktuak_has_platerak
- Repositorios NHibernate para todas las tablas
- Controllers CRUD (y para las tablas puente: GET/POST/DELETE)

Nota: si tu BD tiene columnas diferentes (por ejemplo `erreserbak.izena`),
ajusta el modelo/map/DTO correspondiente.
