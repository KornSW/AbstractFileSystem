using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using Utils;

namespace System.IO.Abstraction {

  [DebuggerDisplay("{InfoString} (AfsLocalRepository)")]
  public class AfsLocalRepository : IAfsRepository {

    private string _RootDir;

    public AfsLocalRepository(string rootDir) {
      _RootDir = rootDir;
      if (!_RootDir.EndsWith(Path.DirectorySeparatorChar.ToString())) {
        _RootDir = _RootDir + Path.DirectorySeparatorChar.ToString();
      }
    }

    #region " internal Helpers "

    private string fileKeyToFileFullName(string fileKey) {
      if (fileKey.StartsWith("/")) {
        fileKey = fileKey.Substring(1);
      }
      string fileFullName = Path.Combine(_RootDir, fileKey.Replace('/', Path.DirectorySeparatorChar));
      return fileFullName;
    }

    private string fileFullNameToFileKey(string fileFullName) {
      string fileKey = "/" + fileFullName.Substring(_RootDir.Length).Replace(Path.DirectorySeparatorChar, '/');
      return fileKey;
    }

    #endregion

    public AfsRepositoryCapabilities GetCapabilities() {
      return new AfsRepositoryCapabilities {
        CanCreateNewFile = true,
        CanCreateOverwriteFile = true,
        CanDeleteFile = true,
        CanDownloadFileContent = true,
        CanListFilesAndAttributes = true,
        CanLoadThumnails = false,
        CanSearchFilesByContent = false
      };
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    internal virtual string InfoString {
      get {
        return this.GetOriginIdentity();
      }
    }

    public string GetOriginIdentity() {
      return $"fs://{Environment.MachineName}/{_RootDir.Replace(Path.DirectorySeparatorChar,'/')}";
    }

    public AfsAttributeDescriptor[] GetAvailableAttributes() {
      return new AfsAttributeDescriptor[] {
        new AfsAttributeDescriptor {
          AttributeName = AfsWellknownAttributeNames.FileFullName,
          AttributeType = AfsAttributeType.String,
          IsManagedValueRange = false, RequiredOnCreation = false, Updatable = false,
        },
        new AfsAttributeDescriptor {
          AttributeName = AfsWellknownAttributeNames.FileName,
          AttributeType = AfsAttributeType.String,
          IsManagedValueRange = true, RequiredOnCreation = true, Updatable = true,
        },
        new AfsAttributeDescriptor {
          AttributeName = AfsWellknownAttributeNames.Directory,
          AttributeType = AfsAttributeType.AreaPath,
          IsManagedValueRange = true, RequiredOnCreation = true, Updatable = true,
        },
        new AfsAttributeDescriptor {
          AttributeName = AfsWellknownAttributeNames.FileSizeBytes,
          AttributeType = AfsAttributeType.Number,
          IsManagedValueRange = false, RequiredOnCreation = false, Updatable = false,
        },
        new AfsAttributeDescriptor {
          AttributeName = AfsWellknownAttributeNames.TimeOfCreation,
          AttributeType = AfsAttributeType.ISODateTime,
          IsManagedValueRange = false, RequiredOnCreation = false, Updatable = false,
        },
        new AfsAttributeDescriptor {
          AttributeName = AfsWellknownAttributeNames.TimeOfLastWrite,
          AttributeType = AfsAttributeType.ISODateTime,
          IsManagedValueRange = false, RequiredOnCreation = false, Updatable = false,
        }
      };
    }

    #region " pre-checks for certain file operations "

    public bool CanDelete(string[] fileKeys) {
      return true;
    }

    public bool CanDownloadContent(string[] fileKeys) {
      return true;
    }

    public bool CanOverwrite(string[] fileKeys) {
      return true;
    }

    public bool CanUpdateAttributes(string[] fileKeys) {
      return true;
    }

    #endregion

    private IEnumerable<string> EnumerateFileFullNames(string rootDir, string directoryMatch, string fileNameMatch) {

      if (string.IsNullOrWhiteSpace(fileNameMatch)) {
        fileNameMatch = "*";
      }

      IEnumerable<string> allFiles;
      if (string.IsNullOrWhiteSpace(directoryMatch)) {
        var di = new DirectoryInfo(rootDir);
        allFiles = this.GetFileInfosRecursive(di, fileNameMatch);
      } 
      else if (!directoryMatch.Contains("*")) {
        var di = new DirectoryInfo(Path.Combine(rootDir, directoryMatch));//TODO: remove leading /
        allFiles = di.GetFiles(fileNameMatch).Select((f) => f.FullName);
      }
      else { //full MINIMATCH/GLOB
        var di = new DirectoryInfo(rootDir);
        allFiles = this.GetFileInfosRecursive(di, fileNameMatch);
        allFiles = Minimatcher.FilterMulti(allFiles, directoryMatch + "/" + fileNameMatch);
      }

      foreach (var file in allFiles) {
        yield return file;
      }

    }

    private IEnumerable<string> GetFileInfosRecursive(DirectoryInfo dirInfo, string fileNameMatch) {

      IEnumerable<FileInfo> files = null;
      try {
        files = dirInfo.GetFiles(fileNameMatch);
      }
      catch {
      }
      if(files != null) {
        foreach (var file in files.Select((f) => f.FullName)) {
          yield return file;
      
        }
      }

      IEnumerable<DirectoryInfo> subDirInfos = null;
      try {
        subDirInfos = dirInfo.GetDirectories();
      }
      catch {
      }
      if (files != null) { 
        foreach (var subDirInfo in subDirInfos) {
          foreach (var file in this.GetFileInfosRecursive(subDirInfo, fileNameMatch)) {
            yield return file;
          }
        }
      }

    }

    private IEnumerable<IDictionary<string,string>> BuildAttributes(IEnumerable<string> fileFullNames, params string[] attribNames) {  
      foreach (var fileFullName in fileFullNames) {

        var fi = new FileInfo(fileFullName);
        string fileKey = this.fileFullNameToFileKey(fileFullName);

        yield return this.LoadFileAttributes(fileKey, attribNames);

        //attribs[AfsWellknownAttributeNames.FileFullName] = fileFullName.Substring(_RootDir.Length - 1).Replace(Path.DirectorySeparatorChar, '/');
        //if (attribNames.Contains(AfsWellknownAttributeNames.Directory)) {
        //  attribs[AfsWellknownAttributeNames.Directory] = Path.GetDirectoryName(fileFullName).Substring(_RootDir.Length - 1).Replace(Path.DirectorySeparatorChar, '/');
        //}
        //if (attribNames.Contains(AfsWellknownAttributeNames.FileName)) {
        //  attribs[AfsWellknownAttributeNames.FileName] = Path.GetFileName(fileFullName);
        //}
        //if (attribNames.Contains(AfsWellknownAttributeNames.FileSizeBytes)) {
        //  attribs[AfsWellknownAttributeNames.FileSizeBytes] = fi.Length.ToString();
        //}   
        //if (attribNames.Contains(AfsWellknownAttributeNames.TimeOfCreation)) {
        //  attribs[AfsWellknownAttributeNames.TimeOfCreation] = fi.CreationTime.ToString();
        //}
        //if (attribNames.Contains(AfsWellknownAttributeNames.TimeOfLastWrite)) {
        //  attribs[AfsWellknownAttributeNames.TimeOfLastWrite] = fi.LastWriteTime.ToString();
        //}      
        
      }
    }

    public string[] ListAllFiles(string sortingAttributeName, int limit, int skip) {
      IEnumerable<string> fileFullNames = this.EnumerateFileFullNames(_RootDir, string.Empty, "*");

      //TEMP:
      //string[] dbg = fileFullNames.ToArray(); 


      if (sortingAttributeName.StartsWith("^")) {

        var fileAttribs = this.BuildAttributes(
          fileFullNames, sortingAttributeName.Substring(1), AfsWellknownAttributeNames.FileFullName
        );

        return (
          fileAttribs.OrderByDescending((a) => a[sortingAttributeName.Substring(1)]).
          Skip(skip).
          Take(limit).
          Select((a)=> a[AfsWellknownAttributeNames.FileFullName]).
          ToArray()
        );

      }
      else {

        var fileAttribs = this.BuildAttributes(
          fileFullNames, sortingAttributeName, AfsWellknownAttributeNames.FileFullName
        );

        return (
          fileAttribs.OrderBy((a) => a[sortingAttributeName]).
          Skip(skip).
          Take(limit).
          Select((a)=> a[AfsWellknownAttributeNames.FileFullName]).
          ToArray()
        );
      }
    }

    private bool EvaluateAttribFilter(IDictionary<string,string> attribs, IDictionary<string, string> filter) {
      foreach(var attr in filter) {
        if (attribs[attr.Key] != attr.Value) {
          return false;
        }
      }
      return true;
    }

    public string[] SearchFilesByAttribute(Dictionary<string, string> attributesToFilter, string sortingAttributeName, int limit, int skip) {

      //TODO: aus den attributesToFilter die filename fitlerrausnehmen!!!!

      IEnumerable<string> fileFullNames = this.EnumerateFileFullNames(_RootDir, string.Empty, "*");
      var includedAttribs = attributesToFilter.Keys.ToList();
      includedAttribs.Add(sortingAttributeName);
      var fileAttribs = this.BuildAttributes(fileFullNames, includedAttribs.ToArray());
      return (
        fileAttribs.Where((a)=>this.EvaluateAttribFilter(a, attributesToFilter)).
        OrderBy((a) => a[sortingAttributeName]).
        Skip(skip).
        Take(limit).
        Select((a) => a[AfsWellknownAttributeNames.FileFullName]).
        ToArray()
      );
    }

    public string[] SearchFilesByContent(string textWithinContent, Dictionary<string, string> attributesToFilter, string sortingAttributeName, int limit, int skip) {
      return null;
    }

    public Dictionary<string, string>[] LoadFileAttributes(string[] fileKeys, string[] includedAttributeNames) {
      var result  = new List<Dictionary<string, string>>();
      foreach (string fileKey in fileKeys) {
        Dictionary<string, string> attribs = this.LoadFileAttributes(fileKey, includedAttributeNames);
        result.Add(attribs);
      }
      return result.ToArray();
    }

    private Dictionary<string, string> LoadFileAttributes(string fileKey, string[] includedAttributeNames) {
      var attribs = new Dictionary<string, string>();
      string fileFullNameInFsStyle = this.fileKeyToFileFullName(fileKey);
      var fi = new FileInfo(fileFullNameInFsStyle);
   
      if(includedAttributeNames.Contains(AfsWellknownAttributeNames.FileFullName))
        attribs[AfsWellknownAttributeNames.FileFullName] = fileKey;

      if (includedAttributeNames.Contains(AfsWellknownAttributeNames.Directory))
        attribs[AfsWellknownAttributeNames.Directory] = Path.GetDirectoryName(fileKey.Replace('/', Path.DirectorySeparatorChar)).Replace(Path.DirectorySeparatorChar, '/');

      if (includedAttributeNames.Contains(AfsWellknownAttributeNames.FileName))
        attribs[AfsWellknownAttributeNames.FileName] = Path.GetFileName(fileKey.Replace('/', Path.DirectorySeparatorChar));

      if (includedAttributeNames.Contains(AfsWellknownAttributeNames.FileSizeBytes))
        attribs[AfsWellknownAttributeNames.FileSizeBytes] = fi.Length.ToString();

      if (includedAttributeNames.Contains(AfsWellknownAttributeNames.TimeOfCreation))
        attribs[AfsWellknownAttributeNames.TimeOfCreation] = fi.CreationTime.ToString("O");

      if (includedAttributeNames.Contains(AfsWellknownAttributeNames.TimeOfLastWrite))
        attribs[AfsWellknownAttributeNames.TimeOfLastWrite] = fi.LastWriteTime.ToString("O");

      return attribs;
    }

    public bool CheckFileExists(string fileKey) {
      string fileFullNameInFsStyle = this.fileKeyToFileFullName(fileKey);
      return File.Exists(fileFullNameInFsStyle);
    }

    public bool UpdateKey(string oldFileKey, string newFileKey) {
      return false; //not supported, fileName and/or directory can be updated seperately...
    }

    public Dictionary<string, string> UpdateAttributes(Dictionary<string, string> attributes) {
      return null;
    }

    public string RequestOtpForDownloadContent(string fileKey) {
      return null;
    }

    public string RequestOtpForFileOverwrite(string fileKey) {
      return null;
    }

    public string RequestOtpForNewFileCreation(Dictionary<string, string> attributeValues) {
      return null;
    }

    public Stream DownloadFileContent(string otp, out string fileName, out string fileContentType) {
      fileName = "dummy";
      fileContentType = "dummy";
      return null;
    }

    public string CreateNewFile(string otp, Stream file, string fileContentType) {
      return null;
    }

    public bool TryOverwriteFile(string otp, Stream file, string fileContentType){
      return false;
    }

    public bool TryDeleteFile(string fileKey) {
      string fileFullNameInFsStyle = this.fileKeyToFileFullName(fileKey);
      try {
        File.Delete(fileFullNameInFsStyle);
        return true;
      }
      catch (Exception ex){ 
      }
      return false;
    }

    public byte[] LoadThumnail(string fileKey, int sizePx, bool square) {
      //TODO: unter windows mit passender API-LIB!!!!
      return null;
    }

    #region " ManagedValueRange "

    public string[] GetValueRange(string attributeName, string filter, out bool isReadOnly) {
      isReadOnly = true;//TODO: verzeichniss-analge ermöglichen!
      if(attributeName == AfsWellknownAttributeNames.Directory) {
        var di = new DirectoryInfo(_RootDir);
        var list = di.GetDirectories("*", SearchOption.AllDirectories).Select((sd)=>sd.FullName);
        return Minimatcher.FilterMulti(list, filter).ToArray();
      }
      return null;
    }

    public bool TryAddToValueRange(string attributeName, string valueToAdd) {
      return false;
    }

    public bool TryRemoveFromValueRange(string attributeName, string valueToRemove) {
      return false;
    }

    #endregion

  }

}
