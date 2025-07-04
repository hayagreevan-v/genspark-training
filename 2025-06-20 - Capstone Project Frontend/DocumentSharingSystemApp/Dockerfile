FROM node:22.16.0-bullseye AS builder

WORKDIR /app

COPY . .

RUN npm install -g @angular/cli && npm install

RUN ng build


FROM nginx

COPY --from=builder /app/dist/DocumentSharingSystemApp/browser /usr/share/nginx/html

EXPOSE 80
