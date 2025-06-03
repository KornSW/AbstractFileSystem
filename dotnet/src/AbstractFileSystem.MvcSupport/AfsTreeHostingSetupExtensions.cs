using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using System.IO;
using System.IO.Abstraction;

namespace Microsoft.AspNetCore.AfsSupport {

  public static class AfsTreeHostingSetupExtensions {

    //TODO: wie siehts mit security aus???
    public static void HostFilesFromAfsRepository(
      this IApplicationBuilder app,
      IAfsRepository afsRepo,
      string baseUrl = "/"
    ) {

      var sfo = new StaticFileOptions {
        ServeUnknownFileTypes = true,
        OnPrepareResponse = ctx => {
          var resp = ctx.Context.Response;
          resp.Headers[HeaderNames.CacheControl] = "no-cache, no-store, must-revalidate";
          resp.Headers[HeaderNames.Expires] = "0";
          resp.Headers[HeaderNames.Pragma] = "no-cache";
        },
        FileProvider = new MsFileProviderForAfsRepository(afsRepo, baseUrl),
        ContentTypeProvider = new FileExtensionContentTypeProvider(),
        RequestPath = baseUrl.TrimEnd('/') //die sub-route unter der die dateien gehostet werden
      };

      app.UseSpaStaticFiles(sfo);

    }

  }

}
