
namespace System.IO.Abstraction {

  public class AfsRepositoryCapabilities {
    public bool CanListFilesAndAttributes { get; set; } = false;
    public bool CanSearchFilesByContent { get; set; } = false;
    public bool CanLoadThumnails { get; set; } = false;
    public bool CanCreateNewFile { get; set; } = false;
    public bool CanCreateOverwriteFile { get; set; } = false;
    public bool CanDeleteFile { get; set; } = false;
    public bool CanDownloadFileContent { get; set; } = false; 
  }

}
