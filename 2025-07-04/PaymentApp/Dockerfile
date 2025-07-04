FROM node:22-alpine AS build
WORKDIR /app
COPY . /app
RUN npm install -g @angular/cli && npm install
RUN ng build

FROM nginx:alpine
COPY --from=build /app/dist/PaymentApp/browser /usr/share/nginx/html
EXPOSE 80