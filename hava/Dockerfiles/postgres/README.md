


## Stop, remove container and image and rebuild
```bash
docker stop my-postgres-container
docker rm my-postgres-container
docker rmi my-postgres
docker volume rm pgdata
docker build -t my-postgres .
```


# Run container
```bash
docker volume create pgdata
docker run -p 5432:5432 --name my-postgres-container -v pgdata:/var/lib/postgresql/data -d my-postgres
docker ps
````


# Connect to container
```bash
docker exec -ti my-postgres-container psql -U postgres

\conninfo
\l
\d
select * from blog.posts;
```



# Remove everything
```bash

docker stop $(docker ps -a -q)
docker rm $(docker ps -a -q)

docker rmi $(docker images ls -a -q)

#docker volume rm $(docker volume ls -q)
docker volume prune

```

# Backup
```bash
cd ~/Desktop
mkdir postgres-backup && cd $_
mkdir `date +%d-%m-%Y"_"%H_%M_%S` && cd $_
docker exec -t my-postgres-container pg_dumpall -c -U postgres > dump.sql
```

# Restore
```bash
cat dump.sql | docker exec -i my-postgres-container psql -U postgres
```


