# JimsUserManagementService
How to run locally?
- Install docker locally
- Compose Up docker-compose.yml file
  - entry point: http://localhost:8899/swagger/index.html

After creating/updating/deleting user via API, it will send a message into SQS queue `user`.

Queue url: http://localhost:9324/queue/user if query outside docker container.
