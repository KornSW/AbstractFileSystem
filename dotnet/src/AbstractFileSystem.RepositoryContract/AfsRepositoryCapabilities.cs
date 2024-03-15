
namespace System.IO.Abstraction {

  /// <summary>
  /// An property bag which holds information about the implemented/supported
  /// capabilities of an IAfsRepository.
  /// </summary>
  public class AfsRepositoryCapabilities {
    public bool CanListFilesAndAttributes { get; set; } = false;
    public bool CanSearchFilesByContent { get; set; } = false;
    public bool CanUpdateKeys { get; set; } = false;
    public bool CanUpdateAttributes { get; set; } = false;
    public bool CanLoadThumnails { get; set; } = false;
    public bool CanCreateNewFile { get; set; } = false;
    public bool CanCreateOverwriteFile { get; set; } = false;
    public bool CanAppendContent { get; set; } = false;
    public bool CanDeleteFile { get; set; } = false;
    public bool CanDownloadFileContent { get; set; } = false; 
  }

}
