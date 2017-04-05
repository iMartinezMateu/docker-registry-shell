using System;
using Microsoft.Extensions.CommandLineUtils;

namespace DockerRegistryShell
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var app = new CommandLineApplication();
			app.Name = "Docker Registry Shell";
			app.HelpOption("-?|-h|--help");

			app.OnExecute(() =>
				{
					Console.WriteLine("Docker Registry Shell 1.0.0");
					Console.WriteLine("Available commands:");
					Console.WriteLine("\tcheck\t\tProvide Docker Registry version support information based on its response status");
					Console.WriteLine("\tcatalog\t\tList all images stored in the repositories of the Docker Registry");
					Console.WriteLine("\treferences\tList all references (tags) under given repository");
					Console.WriteLine("\tmanifest\tGet technical details about a repository stored in Docker Registry");
					Console.WriteLine("\tdelete\t\tDelete a repository stored in Docker Registry");
					Console.WriteLine("Common options:");
					Console.WriteLine("\t-?|-h|--help\tShow help of the typed command");
					return 1;
				});

			app.Command("check", (command) =>
				{

					command.Description = "Provide Docker Registry version support information based on its response status";
					command.HelpOption("-?|-h|--help");

					CommandOption endpointOption = command.Option("-e|--endpoint <endpoint>",
											  "Endpoint where the Docker Registry is deployed.",
											  CommandOptionType.SingleValue);

					CommandOption usernameOption = command.Option("-u|--username <username>",
																  "Username of the account that will be used to connect to the Docker Registry.",
																  CommandOptionType.SingleValue);

					CommandOption passwordOption = command.Option("-p|--password <password>",
																  "Password of the account that will be used to connect to the Docker Registry.",
																  CommandOptionType.SingleValue);

					command.OnExecute(() =>
						{
							if (endpointOption.HasValue() == false || usernameOption.HasValue() == false || passwordOption.HasValue() == false)
							{
								Console.WriteLine(command.GetHelpText());
								return 0;
							}
							else
							{
								string endpoint = endpointOption.Value();
								if (endpoint.Contains("https://") == false)
								{
									endpoint = "https://" + endpointOption.Value();
								}
								DockerRegistryServices dockerRegistryServices = new DockerRegistryServices(endpoint, usernameOption.Value(), passwordOption.Value());
								Console.WriteLine(dockerRegistryServices.CheckAPIVersion().Result);
								return 1;
							}
						});

				});

			app.Command("catalog", (command) =>
			{

				command.Description = "List all images stored in the repositories of the Docker Registry";
				command.HelpOption("-?|-h|--help");

				CommandOption endpointOption = command.Option("-e|--endpoint <endpoint>",
						  "Endpoint where the Docker Registry is deployed.",
						  CommandOptionType.SingleValue);

				CommandOption usernameOption = command.Option("-u|--username <username>",
															  "Username of the account that will be used to connect to the Docker Registry.",
															  CommandOptionType.SingleValue);

				CommandOption passwordOption = command.Option("-p|--password <password>",
															  "Password of the account that will be used to connect to the Docker Registry.",
															  CommandOptionType.SingleValue);

				command.OnExecute(() =>
					{
						if (endpointOption.HasValue() == false || usernameOption.HasValue() == false || passwordOption.HasValue() == false)
						{
							Console.WriteLine(command.GetHelpText());
							return 0;
						}
						else
						{
							string endpoint = endpointOption.Value();
							if (endpoint.Contains("https://") == false)
							{
								endpoint = "https://" + endpointOption.Value();
							}
							DockerRegistryServices dockerRegistryServices = new DockerRegistryServices(endpoint, usernameOption.Value(), passwordOption.Value());
							Console.WriteLine(dockerRegistryServices.ListRepositories().Result);
							return 1;
						}
					});

			});

			app.Command("references", (command) =>
			{

				command.Description = "List all references (tags) under given repository";
				command.HelpOption("-?|-h|--help");

				CommandOption endpointOption = command.Option("-e|--endpoint <endpoint>",
					  "Endpoint where the Docker Registry is deployed.",
					  CommandOptionType.SingleValue);

				CommandOption usernameOption = command.Option("-u|--username <username>",
															  "Username of the account that will be used to connect to the Docker Registry.",
															  CommandOptionType.SingleValue);

				CommandOption passwordOption = command.Option("-p|--password <password>",
															  "Password of the account that will be used to connect to the Docker Registry.",
															  CommandOptionType.SingleValue);

				CommandOption repositoryOption = command.Option("-r|--repository <repository>",
																"Repository (or image) to fetch tags.",
																CommandOptionType.SingleValue);

				command.OnExecute(() =>
					{
						if (endpointOption.HasValue() == false || usernameOption.HasValue() == false || passwordOption.HasValue() == false || repositoryOption.HasValue() == false)
						{
							Console.WriteLine(command.GetHelpText());
							return 0;
						}
						else
						{
							string endpoint = endpointOption.Value();
							if (endpoint.Contains("https://") == false)
							{
								endpoint = "https://" + endpointOption.Value();
							}
							DockerRegistryServices dockerRegistryServices = new DockerRegistryServices(endpoint, usernameOption.Value(), passwordOption.Value());
							Console.WriteLine(dockerRegistryServices.ListImageTags(repositoryOption.Value()).Result);
							return 1;
						}
					});

			});

			app.Command("manifest", (command) =>
				{
					command.Description = "Get technical details about a repository stored in Docker Registry";
					command.HelpOption("-?|-h|--help");

					CommandOption endpointOption = command.Option("-e|--endpoint <endpoint>",
						  "Endpoint where the Docker Registry is deployed.",
						  CommandOptionType.SingleValue);

					CommandOption usernameOption = command.Option("-u|--username <username>",
																  "Username of the account that will be used to connect to the Docker Registry.",
																  CommandOptionType.SingleValue);

					CommandOption passwordOption = command.Option("-p|--password <password>",
																  "Password of the account that will be used to connect to the Docker Registry.",
																  CommandOptionType.SingleValue);

					CommandOption repositoryOption = command.Option("-r|--repository <repository>",
																	"Repository (or image) name.",
																	CommandOptionType.SingleValue);

					CommandOption tagOption = command.Option("-t|--tag <tag>",
																		"Tag of the image to fetch the manifest.",
																		CommandOptionType.SingleValue);

					command.OnExecute(() =>
						{
							if (endpointOption.HasValue() == false || usernameOption.HasValue() == false || passwordOption.HasValue() == false || repositoryOption.HasValue() == false || tagOption.HasValue() == false)
							{
								Console.WriteLine(command.GetHelpText());
								return 0;
							}
							else
							{
								string endpoint = endpointOption.Value();
								if (endpoint.Contains("https://") == false)
								{
									endpoint = "https://" + endpointOption.Value();
								}
								DockerRegistryServices dockerRegistryServices = new DockerRegistryServices(endpoint, usernameOption.Value(), passwordOption.Value());
								Console.WriteLine(dockerRegistryServices.GetManifest(repositoryOption.Value(), tagOption.Value()).Result);
								return 1;
							}
						});
				});

			app.Command("delete", (command) =>
				{
					command.Description = "Delete a repository stored in Docker Registry";
					command.HelpOption("-?|-h|--help");

					CommandOption endpointOption = command.Option("-e|--endpoint <endpoint>",
						  "Endpoint where the Docker Registry is deployed.",
						  CommandOptionType.SingleValue);

					CommandOption usernameOption = command.Option("-u|--username <username>",
																  "Username of the account that will be used to connect to the Docker Registry.",
																  CommandOptionType.SingleValue);

					CommandOption passwordOption = command.Option("-p|--password <password>",
																  "Password of the account that will be used to connect to the Docker Registry.",
																  CommandOptionType.SingleValue);

					CommandOption repositoryOption = command.Option("-r|--repository <repository>",
																	"Repository (or image) name.",
																	CommandOptionType.SingleValue);

					CommandOption digestOption = command.Option("-d|--digest <digest>",
				                                             "Digest of the image. To get the digest of the image you want to delete, type 'docker images --digests' in your local machine.",
																		CommandOptionType.SingleValue);

					command.OnExecute(() =>
						{
							if (endpointOption.HasValue() == false || usernameOption.HasValue() == false || passwordOption.HasValue() == false || repositoryOption.HasValue() == false || digestOption.HasValue() == false)
							{
								Console.WriteLine(command.GetHelpText());
								return 0;
							}
							else
							{
								string endpoint = endpointOption.Value();
								string digest = digestOption.Value();
								if (endpoint.Contains("https://") == false)
								{
									endpoint = "https://" + endpointOption.Value();
								}
								if (digest.Contains("sha256:") == false)
								{
									digest = "sha256:" + digestOption.Value();
								}
								DockerRegistryServices dockerRegistryServices = new DockerRegistryServices(endpoint, usernameOption.Value(), passwordOption.Value());
								Console.WriteLine(dockerRegistryServices.DeleteImage(repositoryOption.Value(), digest).Result);
								return 1;
							}
						});
				});


			app.Execute(args);
		}
	}
}



