
# Everything
```bash

cd hava/Dockerfiles/postgres

docker stop my-postgres-container
docker rm my-postgres-container
docker rmi my-postgres
docker volume rm pgdata
docker build -t my-postgres .

docker volume create pgdata
docker run -p 5432:5432 --name my-postgres-container -v pgdata:/var/lib/postgresql/data -d my-postgres
docker ps

cd ../..
rm -rf Migrations
dotnet ef migrations add initial
dotnet ef database update

#cd ../Testing
#dotnet test --filter DisplayName=Tester.AuthenticateControllerTest.Register_user_test
#dotnet test --filter DisplayName=Tester.AuthenticateControllerTest.Login_user_test

```



# See Everything
```bash
docker ps
systemctl status nginx
```

# On Database Updates
```bash
cd hava
dotnet ef migrations add initial
dotnet ef database update
```

# For production
```bash
sudo systemctl start postgresql
sudo systemctl start apichipper.service
sudo systemctl start nginx
```

### For creating new service for .net core app in production:
#### https://www.linode.com/docs/guides/start-service-at-boot/
 

# For development

### nginx
```bash
sudo systemctl start nginx
```

### postgresql
#### run docker container

### apichipper
#### run app in terminal