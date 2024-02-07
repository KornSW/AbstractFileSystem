using System;
using System.Diagnostics;
using System.Net.Http;
using System.Web.UJMW;

namespace System.IO.Abstraction {

  /// <summary>
  /// Connects to an AFS Repository-Webservice via HTTP (UJMW)
  /// </summary>
  [DebuggerDisplay("{InfoString} (AfsHttpRepositoryConnector)")]
  public class AfsHttpRepositoryConnector : AfsCachingPipe {

    private static IAfsRepository BuildUjmwClient(string endpointUrl, string httpAuthHeader = null) {
      var hc = new HttpClient();
      if (!string.IsNullOrWhiteSpace(httpAuthHeader)) {
        hc.DefaultRequestHeaders.Add("Authorization", httpAuthHeader);
      }
      return BuildUjmwClient(endpointUrl, hc);
    }

    private static IAfsRepository BuildUjmwClient(string endpointUrl, HttpClient hc) {
      return DynamicClientFactory.CreateInstance<IAfsRepository>(hc, () => endpointUrl);
    }
    public AfsHttpRepositoryConnector(string endpointUrl, string httpAuthHeader = null, long cacheSizeBytes = 0) : base(
      BuildUjmwClient(endpointUrl, httpAuthHeader), BuildIdentityRelatedCacheDir(endpointUrl), cacheSizeBytes
    ) {
      _EndpointUrl = endpointUrl;
    }

    public AfsHttpRepositoryConnector(string endpointUrl, HttpClient httpClient, long cacheSizeBytes = 0) : base(
      BuildUjmwClient(endpointUrl, httpClient), BuildIdentityRelatedCacheDir(endpointUrl), cacheSizeBytes
    ) {
      _EndpointUrl = endpointUrl;
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string _EndpointUrl;

    public string EndpointUrl {
      get {
        return _EndpointUrl;
      } 
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    internal override string InfoString {
      get {
        return _EndpointUrl;
      }
    }

  }

}
