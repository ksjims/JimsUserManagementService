FROM public.ecr.aws/lambda/dotnet:6 as base

WORKDIR /var/task
EXPOSE 8080

# # This COPY command copies the .NET Lambda project's build artifacts from the host machine into the image. 
# # The source of the COPY should match where the .NET Lambda project publishes its build artifacts. If the Lambda function is being built 
# # with the AWS .NET Lambda Tooling, the `--docker-host-build-output-dir` switch controls where the .NET Lambda project
# # will be built. The .NET Lambda project templates default to having `--docker-host-build-output-dir`
# # set in the aws-lambda-tools-defaults.json file to "bin/Release/lambda-publish".
# #
# # Alternatively Docker multi-stage build could be used to build the .NET Lambda project inside the image.
# # For more information on this approach checkout the project's README.md file.
# #COPY "bin/Release/lambda-publish"  .


FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
WORKDIR /src
COPY ["LambdaInDocker.csproj", "LambdaInDocker/"]
RUN dotnet restore "LambdaInDocker/LambdaInDocker.csproj"

WORKDIR "/src/LambdaInDocker"
COPY . .
RUN dotnet build "LambdaInDocker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LambdaInDocker.csproj" -c Release -o /app/publish 

FROM base AS final
WORKDIR /var/task
COPY --from=publish /app/publish .

CMD ["LambdaInDocker::LambdaInDocker.Function::FunctionHandler"]
