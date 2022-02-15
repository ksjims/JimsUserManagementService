# JimsUserManagementService
How to run locally?
- Install docker locally
- Compose Up docker-compose.yml file
  - entry point: http://localhost:8899/swagger/index.html

After creating/updating/deleting user via API, it will send a message into SQS queue `user`.

Queue url: http://localhost:9324/queue/user if query outside docker container.

Run this command to get rough number of messages in the queue
- `aws --endpoint-url=http://localhost:9324 sqs get-queue-attributes --queue-url=http://localhost:9324/queue/user --attribute-names ApproximateNumberOfMessages`
- Or `aws --endpoint-url=http://localhost:9324 sqs get-queue-attributes --queue-url=http://localhost:9324/queue/user --attribute-names All` to check all attributes
