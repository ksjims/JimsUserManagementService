using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.ECS;
using Amazon.ECS.Model;
using Amazon.Lambda.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace LambdaInvoker
{
    public class Function
    {
        public async Task<string> FunctionHandler(ILambdaContext context)
        {
            var result = await sendRequestForEcs();

            return result;
        }

        private async Task<string> sendRequestForEcs()
        {
            //StopTaskRequest
            RunTaskRequest request = new()
            {
                Count = 1,
                Cluster = "ttm-15-cluster",
                LaunchType = LaunchType.FARGATE,
                TaskDefinition = "DemoLambdaInDockerAppTask:5",
                NetworkConfiguration = new NetworkConfiguration
                {
                    AwsvpcConfiguration = new AwsVpcConfiguration
                    {
                        SecurityGroups = new List<string> { "sg-03e4dbf71265a243f" },
                        Subnets = new List<string> { "subnet-0f41d07318c114648", "subnet-07ca342bf69f1c20c" }
                    }
                },
                Overrides = new TaskOverride { ContainerOverrides = new List<ContainerOverride> { new ContainerOverride { Name = "DemoLambdaInDockerContainer" } } }
            };

            try
            {
                var response = await new AmazonECSClient().RunTaskAsync(request);
                var status = response.HttpStatusCode.ToString();
                return response.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
