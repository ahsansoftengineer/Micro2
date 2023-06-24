### Docker with MicroService
1. Running Docker MongoDB Container
```c# 
docker run -d --rm --name mongo -p 27017:27017 mongodbdata:/data/db mongo
```
2. Install Mongo DB Extension then Connect that with the following local port
- mongodb://127.0.0.1:27017