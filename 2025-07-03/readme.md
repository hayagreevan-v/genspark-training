# 2025-07-03    Day - 44    Docker Compose, Docker Swarm, Docker Networks

## Topics
- Docker Compose
    - Frontend - Angular (Production)
- Docker Swarm
- Docker Networks

## Notes

**Docker swarm commands**
``` sh
docker swarm init

docker build -t api:latest ./api


docker build -t web:latest ./web

docker stack deploy -c docker-compose.yml mystack


docker stack services mystack


docker service scale mystack_web=5


docker stack rm mystack


docker swarm leave --force
```

## Links
- https://dev.to/usmslm102/containerizing-angular-application-for-production-using-docker-3mhi
- https://docs.docker.com/engine/swarm/
- https://chatgpt.com/share/68666daa-0718-800a-9e9d-2a0f545c38de
- https://github.com/gayat19/PresidioMay25/tree/d81a092488d75ef4777f6ff5042c06893028900d/Docker