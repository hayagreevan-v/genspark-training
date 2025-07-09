FROM node:22-alpine AS build
WORKDIR /app
COPY . /app
RUN npm install -g @angular/cli && npm install && ng build

FROM nginx:alpine
COPY --from=build /app/dist/WeatherApp/browser /usr/share/nginx/html
EXPOSE 80