using System;
using System.Collections.Generic;

namespace System.IO.Abstraction {

  //public class AfsCachingPipe : IAfsRepository {

  //  public AfsAttributeDescriptor[] GetAvailableAttributes() {
  //    return new AfsAttributeDescriptor[] {
  //      new AfsAttributeDescriptor {
  //        AttributeName = "Id",
  //        AttributeType = AfsAttributeType.String
  //      }
  //    };
  //  }

  //  public AfsRepositoryCapabilities GetCapabilities() {
  //    return new AfsRepositoryCapabilities { };
  //  }

  //  #region " pre-checks for certain file operations "

  //  public bool CanDelete(string fileKeys) {
  //    return false;
  //  }

  //  public bool CanDownloadContent(string fileKeys) {
  //    return false;
  //  }

  //  public bool CanOverwrite(string fileKeys) {
  //    return false;
  //  }

  //  public bool CanUpdateAttributes(string fileKeys) {
  //    return false;
  //  }

  //  #endregion

  //  public string[] ListAllFiles(string sortingAttributeName, int limit, int skip) {
  //    return null;
  //  }

  //  public string[] SearchFilesByAttribute(Dictionary<string, string> attributesToFilter, string sortingAttributeName, int limit, int skip) {
  //    return null;
  //  }

  //  public string[] SearchFilesByContent(string textWithinContent, Dictionary<string, string> attributesToFilter, string sortingAttributeName, int limit, int skip) {
  //    return null;
  //  }

  //  public Dictionary<string, string>[] LoadFileAttributes(string[] fileKeys, string[] includedAttributeNames) {
  //    return null;
  //  }

  //  public bool CheckFileExists(string fileKey) {
  //    return false;
  //  }

  //  public bool UpdateKey(string oldFileKey, string newFileKey) {
  //    return false;
  //  }

  //  public bool UpdateAttributes(Dictionary<string, string> attributes) {
  //    return false;
  //  }

  //  public string RequestOtpForDownloadContent(string fileKey) {
  //    return null;
  //  }

  //  public string RequestOtpForFileOverwrite(string fileKey) {
  //    return null;
  //  }

  //  public string RequestOtpForNewFileCreation(Dictionary<string, string> attributeValues) {
  //    return null;
  //  }

  //  public string CreateNewFile(string otp, byte[] content, string newMimeType) {
  //    return null;
  //  }

  //  public byte[] DownloadFileContent(string otp) {
  //    return null;
  //  }

  //  public bool TryOverwriteFile(string otp, byte[] content, string newMimeType) {
  //    return false;
  //  }

  //  public bool TryDeleteFile(string key) {
  //    return false;
  //  }

  //  public byte[] LoadThumnail(string fileKey, int sizePx, bool square) {
  //    return null;
  //  }

  //  #region " ManagedValueRange "

  //  public string[] GetValueRange(string attributeName, string filter, out bool isReadOnly) {
  //    isReadOnly = true;
  //    return null;
  //  }

  //  public bool TryAddToValueRange(string attributeName, string valueToAdd) {
  //    return false;
  //  }

  //  public bool TryRemoveFromValueRange(string attributeName, string valueToRemove) {
  //    return false;
  //  }

  //  #endregion

  //}

}
