# Poengrenn

## Local development

```bash
docker compose up --build
```

API runs on `http://localhost:8080`, frontend on `http://localhost:4200`.

## Railway deployment

1) Create a new Railway project.
2) Add a Postgres plugin.
3) Create a backend service from `backend/`.
   - Dockerfile: `backend/Dockerfile.prod` (picked up by `backend/railway.toml`).
   - Set `ConnectionStrings__PoengrennContext` to:
     `Host=${PGHOST};Port=${PGPORT};Database=${PGDATABASE};Username=${PGUSER};Password=${PGPASSWORD};Ssl Mode=Require;Trust Server Certificate=true`
4) Create a frontend service from `frontend/`.
   - Dockerfile: `frontend/Dockerfile` (picked up by `frontend/railway.toml`).
   - Update `frontend/src/environments/environment.prod.ts` with your backend URL before deploying.

The API applies migrations automatically on startup. If you want seed data, run the SQL scripts in `scripts/` against the Railway Postgres instance.
