
# See Everything
```bash
docker ps
systemctl status nginx
```

# On Database Updates
```bash
dotnet ef migrations add
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