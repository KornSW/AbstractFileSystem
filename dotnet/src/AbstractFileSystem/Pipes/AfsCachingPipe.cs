using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace System.IO.Abstraction {

  /// <summary>
  /// Applies a Caching-Feature when accessig slow source repositories over internet connections
  /// </summary>
  [DebuggerDisplay("{InfoString} (AfsCachingPipe)")]
  public class AfsCachingPipe : IAfsRepository {

    protected static string BuildIdentityRelatedCacheDir(string storeIdentity, string localCacheDirectory = null) {
      if (string.IsNullOrWhiteSpace(localCacheDirectory)) {
        localCacheDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "AFS-Cache");
      }
      using (var md5 = new MD5CryptoServiceProvider()) {
        byte[] urlHash = md5.ComputeHash(
          Encoding.ASCII.GetBytes(storeIdentity)
        );

        return Path.Combine(localCacheDirectory, Convert.ToBase64String(urlHash));
      }
    }

    /// <summary> </summary>
    /// <param name="innerRepository">An AFS Repsitory that contains the files to be accessed.</param>
    /// <param name="localCacheDirectory">A directory on the local disk, where the cache should be located (or null to use the default)</param>
    /// <param name="maxSizeBytes">Limit the size of the cache (0 will disable the caching)</param>
    public AfsCachingPipe(IAfsRepository innerRepository, string localCacheDirectory = null, long maxSizeBytes = 50 * 1024 * 1024) {
      //ATTENTION: if maxSizeBytes==0 the pipe should act as it would not be present - so in this case it MUST NOT auto-create or access the localCacheDirectory dir!!
      _InnerRepository = innerRepository;
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private IAfsRepository _InnerRepository;

    public IAfsRepository InnerRepository { 
      get {
        return _InnerRepository;
      } 
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    internal virtual string InfoString {
      get {
        return  this.GetOriginIdentity();
      }
    }

    public string GetOriginIdentity() {
      return _InnerRepository.GetOriginIdentity();
    }

    public AfsRepositoryCapabilities GetCapabilities() {
      return _InnerRepository.GetCapabilities();
    }

    public AfsAttributeDescriptor[] GetAvailableAttributes() {
      return _InnerRepository.GetAvailableAttributes();
      //? evtl Cache-Date zusätzlich
    }

    #region " pre-checks for certain file operations "

    public bool CanDelete(string[] fileKeys) {
      return _InnerRepository.CanDelete(fileKeys);
    }

    public bool CanDownloadContent(string[] fileKeys) {
      return _InnerRepository.CanDownloadContent(fileKeys);
    }

    public bool CanOverwrite(string[] fileKeys) {
      return _InnerRepository.CanOverwrite(fileKeys);
    }

    public bool CanUpdateAttributes(string[] fileKeys) {
      return _InnerRepository.CanUpdateAttributes(fileKeys);
    }

    #endregion

    public string[] ListAllFiles(string sortingAttributeName, int limit, int skip) {
      //TODO: one of the first places to integrate the cache
      return _InnerRepository.ListAllFiles(sortingAttributeName, limit, skip);
    }

    public string[] SearchFilesByAttribute(Dictionary<string, string> attributesToFilter, string sortingAttributeName, int limit, int skip) {
      return _InnerRepository.SearchFilesByAttribute(attributesToFilter, sortingAttributeName, limit, skip);
    }

    public string[] SearchFilesByContent(string textWithinContent, Dictionary<string, string> attributesToFilter, string sortingAttributeName, int limit, int skip) {
      return _InnerRepository.SearchFilesByContent(textWithinContent, attributesToFilter, sortingAttributeName, limit, skip);
    }

    public Dictionary<string, string>[] LoadFileAttributes(string[] fileKeys, string[] includedAttributeNames) {
      return _InnerRepository.LoadFileAttributes(fileKeys, includedAttributeNames);
    }

    public bool CheckFileExists(string fileKey) {
      return _InnerRepository.CheckFileExists(fileKey);
    }

    public bool UpdateKey(string oldFileKey, string newFileKey) {
      return _InnerRepository.UpdateKey(oldFileKey, newFileKey);
    }

    public Dictionary<string, string> UpdateAttributes(Dictionary<string, string> attributes) {
      return _InnerRepository.UpdateAttributes(attributes);
    }

    public bool TryBeginCreateNewFile(Dictionary<string, string> attributeValues, string contentType, out string contentPushOtp, out string intendedFileKeyOnSuccess) {
      return _InnerRepository.TryBeginCreateNewFile(attributeValues, contentType, out contentPushOtp , out intendedFileKeyOnSuccess);
    }

    public bool TryBeginOverwriteFile(string fileKey, string contentType, out string contentPushOtp) {
      return _InnerRepository.TryBeginOverwriteFile(fileKey, contentType, out contentPushOtp);
    }

    public bool TryBeginAppendFileContent(string fileKey, out string contentPushOtp) {
      return _InnerRepository.TryBeginAppendFileContent(fileKey, out contentPushOtp);
    }

    public bool TryBeginPullFileContent(string fileKey, out string contentType, out long contentSizeBytes, out string contentPullOtp) {
      return _InnerRepository.TryBeginPullFileContent(fileKey, out contentType,out  contentSizeBytes, out contentPullOtp);
    }

    public void ContentOperationComplete(string contentPushOrPullOtp) {
      _InnerRepository.ContentOperationComplete(contentPushOrPullOtp);
    }

    public bool TryPushFileContent(ref string contentPushOtp, byte[] content, bool willContinuePush) {
      return _InnerRepository.TryPushFileContent(ref contentPushOtp, content, willContinuePush);
    }

    public bool TryPullFileContent(ref string contentPullOtp, out byte[] content, bool willContinuePull, long byteOffset, long byteCount) {
      return _InnerRepository.TryPullFileContent(ref contentPullOtp, out content, willContinuePull, byteOffset, byteCount);
    }

    public bool TryDeleteFile(string fileKey) {
      return _InnerRepository.TryDeleteFile(fileKey);
    }

    public byte[] LoadThumnail(string fileKey, int sizePx, bool square) {
      //TODO: one of the first places to integrate the cache
      return _InnerRepository.LoadThumnail(fileKey, sizePx, square);
    }

    #region " ManagedValueRange "

    public string[] GetValueRange(string attributeName, string filter, out bool isReadOnly) {
      return _InnerRepository.GetValueRange(attributeName, filter, out isReadOnly);
    }

    public bool TryAddToValueRange(string attributeName, string valueToAdd) {
      return _InnerRepository.TryAddToValueRange(attributeName, valueToAdd);
    }

    public bool TryRemoveFromValueRange(string attributeName, string valueToRemove) {
      return _InnerRepository.TryRemoveFromValueRange(attributeName, valueToRemove);
    }

    #endregion

  }

}
