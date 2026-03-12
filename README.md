# Proyecto Fullstack: API de Marcas de Autos y Aplicación de Clima

Este repositorio contiene dos proyectos independientes que trabajan en conjunto:  

1. **Backend** – API REST de Marcas de Autos usando C#, Entity Framework, PostgreSQL y Docker.  
2. **Frontend** – Aplicación de clima usando Next.js que consume datos de una API pública.  

Ambos proyectos están preparados para ejecutarse de manera **dockerizada** con Docker Compose.

## Backend – API REST de Marcas de Autos

El backend tiene como objetivo evaluar habilidades en desarrollo de aplicaciones **C#**, **Entity Framework**, pruebas unitarias con **XUnit** y configuración de entornos con **Docker Compose**.

### Funcionalidades

- **Conexión a la Base de Datos:**  
  Configura un `DbContext` para conectarse a PostgreSQL.

- **Migración y Data Seed:**  
  - Genera una tabla `MarcasAutos` mediante migración.  
  - Carga al menos tres marcas de autos de ejemplo automáticamente.

- **API REST – Endpoint:**  
  - Controlador `MarcasAutosController` con un endpoint para obtener todas las marcas de autos.  

- **Pruebas Unitarias:**  
  - Tests con **XUnit** para verificar que el endpoint devuelve los datos esperados.  
  - Configuración de un contexto de base de datos en memoria para las pruebas.  

- **Docker Compose:**  
  - Servicio de PostgreSQL para la base de datos.  
  - Servicio de la API REST que se conecta automáticamente a PostgreSQL.

## Frontend – Aplicación de Clima con Next.js

El frontend permite a los usuarios **buscar el clima actual de cualquier ciudad** y mostrar información relevante como temperatura, humedad y descripción del clima.  

### Funcionalidades

- **Interfaz de Usuario:**  
  - Campo de entrada para el nombre de la ciudad.  
  - Botón para buscar el clima.  
  - Sección que muestra:
    - Temperatura actual.  
    - Humedad.  
    - Descripción del clima (ej: “soleado”, “nublado”).

- **Funcionalidades principales:**  
  - Consulta de datos de clima desde una API pública (OpenWeatherMap).  
  - Manejo de errores: ciudad no encontrada o errores de red.  
  - Pruebas unitarias con **Jest** y **React Testing Library** para validar la funcionalidad principal.

## Requisitos previos

- [Docker](https://www.docker.com/) >= 20  
- [Docker Compose](https://docs.docker.com/compose/) >= 2  
- Conexión a internet para instalar dependencias y consultar la API de clima.

## Ejecución del proyecto completo

El proyecto está **dockerizado**, por lo que todo se ejecuta mediante Docker Compose.

### 1. Clonar el repositorio

```bash
git clone <URL_DEL_REPOSITORIO>
cd <directorio_del_proyecto>
```

---

### 2. Crear archivo `.env` en la raíz

Define las variables de entorno necesarias:

```env
# Backend
DB_NAME=carbrands
DB_USER=postgres
DB_PASSWORD=postgres

# Frontend
WEATHER_API_KEY=<TU_API_KEY_DE_OPENWEATHER>
```

Reemplaza ```<TU_API_KEY_DE_OPENWEATHER>``` con tu clave de OpenWeatherMap.

**Nota sobre la API Key de OpenWeather:**

- La clave tarda aproximadamente 20 a 30 minutos en activarse luego de ser creada.
- Si intentas usarla antes de ese tiempo, recibirás un error de Unauthorized (401).
- Para crear la clave:
  - Regístrate en OpenWeatherMap
  - Ve a API Keys en tu perfil.
  - Crea una nueva key y copia el valor en tu .env.

---

### 3. Construir y levantar los contenedores

```bash
docker-compose up --build
```

Esto hará lo siguiente: 
- Construye la API REST y la aplicación frontend.
- Levanta los servicios de PostgreSQL, backend y frontend.
- Ejecuta la migración y el seed de la base de datos.

---

### 4. Acceder a la aplicación

Frontend (aplicación de clima):
http://localhost:3000

Backend (API REST de Marcas de Autos):
http://localhost:5000/swagger/index.html

---

### 5. Ejecutar pruebas unitarias

Si quieres correr los tests dentro de los contenedores:

```bash
# Frontend
docker-compose run --rm frontend_tests
# Recuerda que la API key de OpenWeatherMap puede tardar 20-30 minutos en activarse.
# Antes de eso, los tests que dependan de la API pueden fallar con error de Unauthorized.

# Backend
docker-compose run --rm backend_tests
```