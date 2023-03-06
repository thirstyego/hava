


## Stop, remove container and image and rebuild
```bash
docker stop my-nginx-container
docker rm my-nginx-container
docker rmi my-nginx
docker build -t my-nginx .
```


```bash
docker build -t my-nginx .
```


```bash
docker run -p 80:80 --name my-nginx-container -d my-nginx
docker ps
```



