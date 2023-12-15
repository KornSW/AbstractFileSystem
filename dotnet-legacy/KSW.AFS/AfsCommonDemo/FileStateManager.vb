Imports System
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Diagnostics

Public Class FileStateManager

#Region " Declarations & Constructor "

  <DebuggerBrowsable(DebuggerBrowsableState.Never)>
  Private _PendingChanges As Boolean = False
  <DebuggerBrowsable(DebuggerBrowsableState.Never)>
  Private _CurrentFileName As String = Nothing

  Public Sub New()
  End Sub

#End Region

#Region " Handler Delegates / Properties "

  <EditorBrowsable(EditorBrowsableState.Advanced)>
  Public Delegate Function AskSaveHandlerMethod(ByRef save As Boolean) As Boolean
  <EditorBrowsable(EditorBrowsableState.Advanced)>
  Public Delegate Function SaveFileHandlerMethod(targetFileName As String) As Boolean
  <EditorBrowsable(EditorBrowsableState.Advanced)>
  Public Delegate Function OpenFileHandlerMethod(sourceFileName As String) As Boolean

  <EditorBrowsable(EditorBrowsableState.Advanced)>
  Public Property AskSaveHandler As AskSaveHandlerMethod = AddressOf Me.DefaultAskSaveHandler

  Public Property ResetToNewContentHandler As Action = Nothing
  Public Property SaveFileHandler As SaveFileHandlerMethod = Function(a) False
  Public Property OpenFileHandler As OpenFileHandlerMethod = Function(a) False


  <EditorBrowsable(EditorBrowsableState.Advanced)>
  Public Property SelectTargetFileNameHandler As Func(Of String) = AddressOf Me.DefaultSelectTargetFileNameHandler
  <EditorBrowsable(EditorBrowsableState.Advanced)>
  Public Property SelectSourceFileNameHandler As Func(Of String) = AddressOf Me.DefaultSelectSourceFileNameHandler

  <EditorBrowsable(EditorBrowsableState.Advanced)>
  Public Property CurrentFileNameDisplayHandler As Action(Of String) = Sub(s)
                                                                       End Sub
  <EditorBrowsable(EditorBrowsableState.Advanced)>
  Public Property PendingChangesDisplayHandler As Action(Of Boolean) = Sub(s)
                                                                       End Sub


#End Region

#Region " Default Handler Methods (WinForms Based) "

  Protected Function DefaultAskSaveHandler(ByRef save As Boolean) As Boolean

    Select Case MessageBox.Show("Möchten sie die Änderungen speichern?", "Änderungen Speichern", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
      Case DialogResult.Yes, DialogResult.OK : save = True
      Case DialogResult.No : save = False
      Case DialogResult.Cancel
        Return False
    End Select

    Return True
  End Function

  Protected Function DefaultSelectTargetFileNameHandler() As String
    Using dlg As New SaveFileDialog

      If (Me.CurrentFileName IsNot Nothing) Then
        dlg.FileName = IO.Path.GetFileName(Me.CurrentFileName)
        dlg.InitialDirectory = IO.Path.GetDirectoryName(Me.CurrentFileName)
      End If
      dlg.Title = "Datei Speichern"
      dlg.Filter = Me.DefaultFileNamePattern

      Select Case dlg.ShowDialog
        Case DialogResult.Cancel, DialogResult.Abort
          Return Nothing
        Case Else
          Return dlg.FileName
      End Select

    End Using
  End Function

  Protected Function DefaultSelectSourceFileNameHandler() As String
    Using dlg As New OpenFileDialog

      dlg.Title = "Datei Öffnen"
      dlg.Filter = Me.DefaultFileNamePattern

      Select Case dlg.ShowDialog
        Case DialogResult.Cancel, DialogResult.Abort
          Return Nothing
        Case Else
          Return dlg.FileName
      End Select

    End Using
  End Function

#End Region

#Region " Properties "

  Public Property PendingChanges As Boolean
    Get
      Return _PendingChanges
    End Get
    Set(value As Boolean)
      If (Not _PendingChanges = value) Then
        _PendingChanges = value
        If (Me.PendingChangesDisplayHandler IsNot Nothing) Then
          Me.PendingChangesDisplayHandler.Invoke(value)
        End If
      End If
    End Set
  End Property

  Public Property CurrentFileName As String
    Get
      Return _CurrentFileName
    End Get
    Set(value As String)
      _CurrentFileName = value
      If (Me.CurrentFileNameDisplayHandler IsNot Nothing) Then
        Me.CurrentFileNameDisplayHandler.Invoke(value)
      End If
    End Set
  End Property

  <EditorBrowsable(EditorBrowsableState.Advanced)>
  Public Property DefaultFileNamePattern As String = "Alle Dateien (*.*)|*.*"

#End Region

#Region " Public Methods "

  Public Function NewFile() As Boolean

    If (Not Me.SaveChangesIfRequired()) Then
      Return False
    End If

    If (Me.ResetToNewContentHandler IsNot Nothing) Then
      Me.ResetToNewContentHandler.Invoke()
    End If

    Me.PendingChanges = False
    Me.CurrentFileName = Nothing

    Return True
  End Function

  Public Function OpenFile() As Boolean

    If (Not Me.SaveChangesIfRequired()) Then
      Return False
    End If

    Dim sourceFileName As String = Nothing
    If (Me.SelectSourceFileNameHandler IsNot Nothing) Then
      sourceFileName = Me.SelectSourceFileNameHandler.Invoke()
    End If
    If (sourceFileName Is Nothing) Then
      Return False 'cancel
    End If

    Dim fileOpenedSuccessfully As Boolean = False
    If (Me.OpenFileHandler IsNot Nothing) Then
      fileOpenedSuccessfully = Me.OpenFileHandler.Invoke(sourceFileName)
    End If
    If (fileOpenedSuccessfully) Then
      Me.PendingChanges = False
      Me.CurrentFileName = sourceFileName
    End If

    Return fileOpenedSuccessfully
  End Function

  <EditorBrowsable(EditorBrowsableState.Advanced)>
  Public Function SaveChangesIfRequired() As Boolean

    If (_PendingChanges) Then

      Dim persistChanges As Boolean = False
      If (Me.AskSaveHandler IsNot Nothing) Then
        persistChanges = True

        If (Me.AskSaveHandler.Invoke(persistChanges) = False) Then
          'return cancel
          Return False 'cancel
        End If

      End If

      If (persistChanges) Then
        'return result of save
        Return Me.SaveFile
      End If

    End If

    'return success (save not neccesarry OR skipped by user)
    Return True
  End Function

  Public Function SaveFile() As Boolean

    If (Me.CurrentFileName Is Nothing) Then
      Return Me.SaveFileAs()
    End If

    Dim fileSavedSuccessfully As Boolean = False
    If (Me.SaveFileHandler IsNot Nothing) Then
      fileSavedSuccessfully = Me.SaveFileHandler.Invoke(Me.CurrentFileName)
    End If

    If (fileSavedSuccessfully) Then
      Me.PendingChanges = False
    End If

    Return fileSavedSuccessfully
  End Function

  Public Function SaveFileAs() As Boolean

    Dim targetFileName As String = Nothing
    If (Me.SelectTargetFileNameHandler IsNot Nothing) Then
      targetFileName = Me.SelectTargetFileNameHandler.Invoke()
    End If
    If (targetFileName Is Nothing) Then
      Return False 'cancel
    End If

    Dim fileSavedSuccessfully As Boolean = False
    If (Me.SaveFileHandler IsNot Nothing) Then
      fileSavedSuccessfully = Me.SaveFileHandler.Invoke(targetFileName)
    End If
    If (fileSavedSuccessfully) Then
      Me.PendingChanges = False
      Me.CurrentFileName = targetFileName
    End If

    Return fileSavedSuccessfully
  End Function

#End Region

End Class
