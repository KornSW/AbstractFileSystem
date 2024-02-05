//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Runtime.InteropServices;
//using Utils;

//namespace System.IO.Abstraction {

//  public class AfsLocalRepository : IAfsRepository {

//    private string _RootDir;

//    public AfsLocalRepository(string rootDir) {
//      _RootDir = rootDir;
//      if (!_RootDir.EndsWith(Path.DirectorySeparatorChar.ToString())) {
//        _RootDir = _RootDir + Path.DirectorySeparatorChar.ToString();
//      }
//    }

//    public AfsRepositoryCapabilities GetCapabilities() {
//      return new AfsRepositoryCapabilities {
//        CanCreateNewFile = true,
//        CanCreateOverwriteFile = true,
//        CanDeleteFile = true,
//        CanDownloadFileContent = true,
//        CanListFilesAndAttributes = true,
//        CanLoadThumnails = false,
//        CanSearchFilesByContent = false
//      };
//    }

//    public AfsAttributeDescriptor[] GetAvailableAttributes() {
//      return new AfsAttributeDescriptor[] {
//        new AfsAttributeDescriptor {
//          AttributeName = AfsWellknownAttributeNames.FileFullName,
//          AttributeType = AfsAttributeType.AreaPath,
//          IsManagedValueRange = true, RequiredOnCreation = true, Updatable = true,
//        },
//        new AfsAttributeDescriptor {
//          AttributeName = AfsWellknownAttributeNames.FileName,
//          AttributeType = AfsAttributeType.String,
//          IsManagedValueRange = true, RequiredOnCreation = false, Updatable = true,
//        },
//        new AfsAttributeDescriptor {
//          AttributeName = AfsWellknownAttributeNames.Directory,
//          AttributeType = AfsAttributeType.AreaPath,
//          IsManagedValueRange = true, RequiredOnCreation = false, Updatable = true,
//        },
//        new AfsAttributeDescriptor {
//          AttributeName = AfsWellknownAttributeNames.FileSizeBytes,
//          AttributeType = AfsAttributeType.Number,
//          IsManagedValueRange = false, RequiredOnCreation = false, Updatable = false,
//        },
//        new AfsAttributeDescriptor {
//          AttributeName = AfsWellknownAttributeNames.TimeOfCreation,
//          AttributeType = AfsAttributeType.ISODateTime,
//          IsManagedValueRange = false, RequiredOnCreation = false, Updatable = false,
//        },
//        new AfsAttributeDescriptor {
//          AttributeName = AfsWellknownAttributeNames.TimeOfLastWrite,
//          AttributeType = AfsAttributeType.ISODateTime,
//          IsManagedValueRange = false, RequiredOnCreation = false, Updatable = false,
//        }
//      };
//    }

//    #region " pre-checks for certain file operations "

//    public bool CanDelete(string fileKeys) {
//      return true;
//    }

//    public bool CanDownloadContent(string fileKeys) {
//      return true;
//    }

//    public bool CanOverwrite(string fileKeys) {
//      return true;
//    }

//    public bool CanUpdateAttributes(string fileKeys) {
//      return true;
//    }

//    #endregion

//    private IEnumerable<string> EnumerateFileFullNames(string rootDir, string directoryMatch, string fileNameMatch) {

//      if (string.IsNullOrWhiteSpace(fileNameMatch)) {
//        fileNameMatch = "*";
//      }

//      IEnumerable<string> allFiles;
//      if (string.IsNullOrWhiteSpace(directoryMatch)) {
//        var di = new DirectoryInfo(rootDir);
//        allFiles = di.GetFiles(fileNameMatch, SearchOption.AllDirectories).Select((f) => f.FullName);
//      } 
//      else if (!directoryMatch.Contains("*")) {
//        var di = new DirectoryInfo(Path.Combine(rootDir, directoryMatch));//TODO: remove leading /
//        allFiles = di.GetFiles(fileNameMatch).Select((f) => f.FullName);
//      }
//      else { //full MINIMATCH/GLOB
//        var di = new DirectoryInfo(rootDir);
//        allFiles = di.GetFiles(fileNameMatch, SearchOption.AllDirectories).Select((f) => f.FullName);
//        allFiles = Minimatcher.FilterMulti(allFiles, directoryMatch + "/" + fileNameMatch);
//      }

//      foreach (var file in allFiles) {
//        yield return file;//.Substring(rootDir.Length - 1).Replace(Path.DirectorySeparatorChar, '/');
//      }

//    }


//    private IEnumerable<IDictionary<string,string>> BuildAttributes(IEnumerable<string> fileFullNames, params string[] attribNames) {  
//      foreach (var fileFullName in fileFullNames) {
//        var fi = new FileInfo(fileFullName);
//        var attribs = new Dictionary  <string, string>();
//        attribs[AfsWellknownAttributeNames.FileFullName] = fileFullName.Substring(_RootDir.Length - 1).Replace(Path.DirectorySeparatorChar, '/');
//        if (attribNames.Contains(AfsWellknownAttributeNames.Directory)) {
//          attribs[AfsWellknownAttributeNames.Directory] = Path.GetDirectoryName(fileFullName).Substring(_RootDir.Length - 1).Replace(Path.DirectorySeparatorChar, '/');
//        }
//        if (attribNames.Contains(AfsWellknownAttributeNames.FileName)) {
//          attribs[AfsWellknownAttributeNames.FileName] = Path.GetFileName(fileFullName);
//        }
//        if (attribNames.Contains(AfsWellknownAttributeNames.FileSizeBytes)) {
//          attribs[AfsWellknownAttributeNames.FileSizeBytes] = fi.Length.ToString();
//        }   
//        if (attribNames.Contains(AfsWellknownAttributeNames.TimeOfCreation)) {
//          attribs[AfsWellknownAttributeNames.TimeOfCreation] = fi.CreationTime.ToString();
//        }
//        if (attribNames.Contains(AfsWellknownAttributeNames.TimeOfLastWrite)) {
//          attribs[AfsWellknownAttributeNames.TimeOfLastWrite] = fi.LastWriteTime.ToString();
//        }       
//        yield return attribs;
//      }
//    }

//    public string[] ListAllFiles(string sortingAttributeName, int limit, int skip) {
//      IEnumerable<string> fileFullNames = this.EnumerateFileFullNames(_RootDir, string.Empty, "*");
//      var fileAttribs = this.BuildAttributes(fileFullNames, sortingAttributeName);
//      return (
//        fileAttribs.OrderBy((a) => a[sortingAttributeName]).
//        Skip(skip).
//        Take(limit).
//        Select((a)=> a[AfsWellknownAttributeNames.FileFullName]).
//        ToArray()
//      );
//    }

//    private bool EvaluateAttribFilter(IDictionary<string,string> attribs, IDictionary<string, string> filter) {
//      foreach(var attr in filter) {
//        if (attribs[attr.Key] != attr.Value) {
//          return false;
//        }
//      }
//      return true;
//    }

//    public string[] SearchFilesByAttribute(Dictionary<string, string> attributesToFilter, string sortingAttributeName, int limit, int skip) {


//      //TODO: aus den attributesToFilter die filename fitlerrausnehmen!!!!

//      IEnumerable<string> fileFullNames = this.EnumerateFileFullNames(_RootDir, string.Empty, "*");
//      var includedAttribs = attributesToFilter.Keys.ToList();
//      includedAttribs.Add(sortingAttributeName);
//      var fileAttribs = this.BuildAttributes(fileFullNames, includedAttribs.ToArray());
//      return (
//        fileAttribs.Where((a)=>this.EvaluateAttribFilter(a, attributesToFilter)).
//        OrderBy((a) => a[sortingAttributeName]).
//        Skip(skip).
//        Take(limit).
//        Select((a) => a[AfsWellknownAttributeNames.FileFullName]).
//        ToArray()
//      );
//    }

//    public string[] SearchFilesByContent(string textWithinContent, Dictionary<string, string> attributesToFilter, string sortingAttributeName, int limit, int skip) {
//      return null;
//    }

//    public Dictionary<string, string>[] LoadFileAttributes(string[] fileKeys, string[] includedAttributeNames) {
//      return null;









//    }

//    public bool CheckFileExists(string fileKey) {
//      return false;




//    }

//    public bool UpdateKey(string oldFileKey, string newFileKey) {
//      return false;




//    }

//    public bool UpdateAttributes(Dictionary<string, string> attributes) {
//      return false;
//    }

//    public string RequestOtpForDownloadContent(string fileKey) {
//      return null;
//    }

//    public string RequestOtpForFileOverwrite(string fileKey) {
//      return null;
//    }

//    public string RequestOtpForNewFileCreation(Dictionary<string, string> attributeValues) {
//      return null;
//    }

//    public string CreateNewFile(string otp, byte[] content, string newMimeType) {
//      return null;
//    }

//    public byte[] DownloadFileContent(string otp) {
//      return null;
//    }

//    public bool TryOverwriteFile(string otp, byte[] content, string newMimeType) {
//      return false;
//    }

//    public bool TryDeleteFile(string key) {
//      return false;
//    }

//    public byte[] LoadThumnail(string fileKey, int sizePx, bool square) {
//      return null;
//    }

//    #region " ManagedValueRange "

//    public string[] GetValueRange(string attributeName, string filter, out bool isReadOnly) {
//      isReadOnly = true;
//      return null;
//    }

//    public bool TryAddToValueRange(string attributeName, string valueToAdd) {
//      return false;
//    }

//    public bool TryRemoveFromValueRange(string attributeName, string valueToRemove) {
//      return false;
//    }

//    #endregion

//  }

//}
