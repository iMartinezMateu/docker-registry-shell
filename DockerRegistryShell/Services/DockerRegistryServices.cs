using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Newtonsoft.Json;

namespace DockerRegistryShell
{
	public class DockerRegistryServices
	{
		private String registryEndpoint;
		private String registryUsername;
		private String registryPassword;

		public string RegistryEndpoint
		{
			get
			{
				return registryEndpoint;
			}

			set
			{
				registryEndpoint = value;
			}
		}

		public string RegistryUsername
		{
			get
			{
				return registryUsername;
			}

			set
			{
				registryUsername = value;
			}
		}

		public string RegistryPassword
		{
			get
			{
				return registryPassword;
			}

			set
			{
				registryPassword = value;
			}
		}

		/// <summary>
		///  The constructor of the class and provides access and credential information of the Docker Registry.
		/// </summary>
		/// <param name="registryEndpoint">Registry endpoint.</param>
		/// <param name="registryUsername">Registry username.</param>
		/// <param name="registryPassword">Registry password.</param>
		public DockerRegistryServices(String registryEndpoint, String registryUsername, String registryPassword)
		{
			RegistryEndpoint = registryEndpoint;
			RegistryUsername = registryUsername;
			RegistryPassword = registryPassword;
		}

		/// <summary> 
		/// A minimal endpoint, mounted at /v2/ will provide version support information based on its response status.
		/// </summary>
		/// <returns>
		/// If a 200 OK response is returned, the registry implements the V2(.1) registry API and the client may proceed
		/// safely with other V2 operations. 
		/// If a 401 Unauthorized response is returned, the client should take action based on the contents of the WWW-Authenticate
		/// header and try the endpoint again.
		/// If 404 Not Found response status, or other unexpected status, is returned, thte client should proceed with the assumption that 
		/// the registry does not implement V2 of the API.
		/// </returns>
		public async Task<HttpStatusCode> CheckAPIVersion()
		{
			HttpResponseMessage response = await (registryEndpoint + "/v2").WithBasicAuth(registryUsername,registryPassword).GetAsync();
			return response.StatusCode;
		}

		/// <summary>
		/// Images are stored in collections, known as a repository, which is keyed by a name, as seen troughout the API specification. 
		/// A registry instance may contain several repositories. The list of available repositories is made available through the catalog.
		/// </summary>
		/// <returns>The repositories.</returns>
		public async Task<RepositoriesEntity> ListRepositories()
		{
			HttpResponseMessage response = await (registryEndpoint + "/v2/_catalog").WithBasicAuth(registryUsername, registryPassword).GetAsync();
			RepositoriesEntity repositoriesEntity = JsonConvert.DeserializeObject<RepositoriesEntity>(response.Content.ReadAsStringAsync().Result);
			return repositoriesEntity;
		}

		/// <summary>
		/// Images are stored in collections, known as a repository, which is keyed by a name, as seen troughout the API specification. 
		/// A registry instance may contain several repositories. The list of available repositories is made available through the catalog.
		/// </summary>
		/// <returns>The repositories.</returns>
		/// <param name="beginSet">Get first n entries from the result set</param>
		/// <param name="lastSet">Ignore first n entries from the result set</param>
		public async Task<RepositoriesEntity> ListRepositories(int beginSet, int lastSet)
		{
			HttpResponseMessage response = await (registryEndpoint + "/v2/_catalog").SetQueryParams(new { n = beginSet, last = lastSet}).WithBasicAuth(registryUsername, registryPassword).GetAsync();
			RepositoriesEntity repositoriesEntity = JsonConvert.DeserializeObject<RepositoriesEntity>(response.Content.ReadAsStringAsync().Result);
			return repositoriesEntity;
		}

		/// <summary>
		/// Lists the image tags under a given repository.
		/// </summary>
		/// <returns>The image tags.</returns>
		/// <param name="repositoryName">Repository name.</param>
		public async Task<TagsEntity> ListImageTags(string repositoryName)
		{
			HttpResponseMessage response = await (registryEndpoint + "/v2/" + repositoryName + "/tags/list").WithBasicAuth(registryUsername, registryPassword).GetAsync();
			TagsEntity tagsEntity = JsonConvert.DeserializeObject<TagsEntity>(response.Content.ReadAsStringAsync().Result);
			return tagsEntity;
		}

		/// <summary>
		/// Fetch the manifest identified by name and reference.
		/// </summary>
		/// <returns>The image manifest.</returns>
		/// <param name="repositoryName">Repository name.</param>
		/// <param name="reference">Digest (without SHA256: preffix) or tag of the image.</param>
		public async Task<ManifestEntity> GetManifest(string repositoryName, string reference)
		{
			HttpResponseMessage response = await (registryEndpoint + "/v2/" + repositoryName + "/manifests/" + reference).WithBasicAuth(registryUsername, registryPassword).GetAsync();
			ManifestEntity manifestEntity = JsonConvert.DeserializeObject<ManifestEntity>(response.Content.ReadAsStringAsync().Result);
			return manifestEntity;
		}

		/// <summary>
		/// An image may be deleted from the registry via its name and reference. Reference must be a digest or the delete will fail (not include sha256: preffix!)
		/// </summary>
		/// <returns>202 Accepted response is returned if the image has been succesfully deleted. Otherwise, if the image had already been deleted or did not exist, a 404 Not Found response will be issued instead</returns>
		/// <param name="imageName">Image name.</param>
		/// <param name="reference">Digest (without SHA256: preffix) of the image to be deleted.</param>
		public async Task<HttpStatusCode> DeleteImage(string imageName, string reference)
		{
			HttpResponseMessage response = await (registryEndpoint + "/v2/" + imageName + "/manifests/" + reference).WithHeader("Accept", "application/vnd.docker.distribution.manifest.v2+json").WithBasicAuth(registryUsername, registryPassword).DeleteAsync();
			return response.StatusCode;
		}
	}
}
