# Etapa de build
FROM node:20-alpine AS builder
WORKDIR /app

# Copiamos package.json y package-lock.json
COPY package*.json ./
RUN npm ci

# Copiamos el resto del código
COPY . .

# Ejecutamos los tests
RUN npm test

# Si los tests pasan, construimos la app
RUN npm run build

# Etapa de producción
FROM node:20-alpine AS runner
WORKDIR /app

ENV NODE_ENV=production

# Copiamos solo la carpeta build desde la etapa builder
COPY --from=builder /app ./

EXPOSE 3000
CMD ["npm", "start"]