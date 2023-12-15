'Imports System.Security

'Public NotInheritable Class EncryptingFsProxy
'  Inherits FsProxy

'#Region " Declarations & Constructor "

'  Private _Source As IFileSystemProvider

'  Public Sub New(source As IFileSystemProvider, encryptionKey As SecureString)
'    MyBase.New(source)
'  End Sub

'#End Region

'#Region " Cryptopgraphy "

'  Private Function EncryptPath(planePath As AbsolutePath) As AbsolutePath
'    'ACHTUNG, JEDER ORDNERNAME EINZELN!! SONST GIBT ES DISCREPANZEN
'  End Function

'  Private Function DecryptPath(planePath As AbsolutePath) As AbsolutePath

'  End Function

'#End Region

'  Public Overrides Function CreateDirectory(directoryPath As AbsolutePath) As Boolean
'    Return MyBase.CreateDirectory(Me.EncryptPath(directoryPath))
'  End Function

'End Class
