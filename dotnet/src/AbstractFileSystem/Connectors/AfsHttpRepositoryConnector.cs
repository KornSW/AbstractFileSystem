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

    public AfsHttpRepositoryConnector(string endpointUrl, string httpAuthHeader = null, long cacheSizeBytes = 0) : base(
      DynamicClientFactory.CreateInstance<IAfsRepository>(endpointUrl, () => httpAuthHeader),
      BuildIdentityRelatedCacheDir(endpointUrl),
      cacheSizeBytes
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
